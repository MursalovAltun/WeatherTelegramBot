using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.DataAccess.EFCore.Migrations
{
    public partial class Add_SubscriberSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubscriberSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifyDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsReceiveDailyWeather = table.Column<bool>(nullable: false),
                    SubscriberId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriberSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriberSettings_Subscribers_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "Subscribers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriberSettings_SubscriberId",
                table: "SubscriberSettings",
                column: "SubscriberId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriberSettings");
        }
    }
}

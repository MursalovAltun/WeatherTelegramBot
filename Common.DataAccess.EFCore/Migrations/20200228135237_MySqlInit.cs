using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.DataAccess.EFCore.Migrations
{
    public partial class MySqlInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifyDate = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    TelegramUserId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    Username = table.Column<string>(nullable: true),
                    WaitingFor = table.Column<string>(maxLength: 50, nullable: true),
                    ChatId = table.Column<long>(nullable: false),
                    City = table.Column<string>(maxLength: 50, nullable: true),
                    Language = table.Column<string>(maxLength: 2, nullable: false, defaultValue: "en"),
                    UtcOffset = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriberSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifyDate = table.Column<DateTime>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    IsReceiveDailyWeather = table.Column<bool>(nullable: false),
                    MeasureSystem = table.Column<string>(maxLength: 10, nullable: false, defaultValue: "metric"),
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

            migrationBuilder.DropTable(
                name: "Subscribers");
        }
    }
}

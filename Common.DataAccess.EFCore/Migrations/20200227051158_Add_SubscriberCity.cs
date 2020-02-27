using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.DataAccess.EFCore.Migrations
{
    public partial class Add_SubscriberCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Subscribers",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Subscribers");
        }
    }
}

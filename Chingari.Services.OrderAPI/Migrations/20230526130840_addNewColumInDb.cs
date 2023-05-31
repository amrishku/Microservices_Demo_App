using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chingari.Services.OrderAPI.Migrations
{
    public partial class addNewColumInDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "MessageCreated",
                table: "OrderDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageCreated",
                table: "OrderDetails");
        }
    }
}

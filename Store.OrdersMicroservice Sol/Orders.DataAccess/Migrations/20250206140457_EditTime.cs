using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orders.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EditTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DeliveryTime",
                table: "DeliveryMethods",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "DeliveryTime",
                table: "DeliveryMethods",
                type: "time",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}

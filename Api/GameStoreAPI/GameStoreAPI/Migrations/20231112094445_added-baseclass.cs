using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStoreAPi.Migrations
{
    /// <inheritdoc />
    public partial class addedbaseclass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "SKUs",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "SKUs",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SKUs");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "SKUs");
        }
    }
}

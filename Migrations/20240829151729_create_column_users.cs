﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectobasenet.Migrations
{
    /// <inheritdoc />
    public partial class create_column_users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "salt",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "salt",
                table: "users");
        }
    }
}

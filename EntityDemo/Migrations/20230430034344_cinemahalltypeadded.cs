﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityDemo.Migrations
{
    /// <inheritdoc />
    public partial class cinemahalltypeadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CinemaHallType",
                table: "CinemaHalls",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CinemaHallType",
                table: "CinemaHalls");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Database.Migrations
{
    public partial class ChangeDataColumType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Data",
                table: "EventStore",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "BLOB",
                oldMaxLength: 16);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Data",
                table: "EventStore",
                type: "BLOB",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}

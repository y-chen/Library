using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Api.Database.Migrations
{
    public partial class ChangeAuthorColumnType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EventStore_StreamId_Revision",
                table: "EventStore");

            migrationBuilder.DropColumn(
                name: "Authors",
                table: "Book");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Book",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Book");

            migrationBuilder.AddColumn<string>(
                name: "Authors",
                table: "Book",
                type: "JSON",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_EventStore_StreamId_Revision",
                table: "EventStore",
                columns: new[] { "StreamId", "Revision" },
                unique: true);
        }
    }
}

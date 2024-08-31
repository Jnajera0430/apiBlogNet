using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectobasenet.Migrations
{
    /// <inheritdoc />
    public partial class DeleteColumnSaltUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "salt",
                table: "users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "salt",
                table: "users",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}

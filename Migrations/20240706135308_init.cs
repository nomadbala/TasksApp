using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasksApp.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Todos_Title_ActiveAt",
                table: "Todos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Todos_Title_ActiveAt",
                table: "Todos",
                columns: new[] { "Title", "ActiveAt" },
                unique: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToDoProjectServer.Migrations
{
    /// <inheritdoc />
    public partial class mg1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Work = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Todos",
                columns: new[] { "Id", "IsCompleted", "Work" },
                values: new object[,]
                {
                    { 1, false, "Get to work" },
                    { 2, false, "Pick up groceries" },
                    { 3, false, "Go home" },
                    { 4, false, "Fall asleep" },
                    { 5, true, "Get up" },
                    { 6, true, "Brush teeth" },
                    { 7, true, "Take a shower" },
                    { 8, true, "Check e-mail" },
                    { 9, true, "Walk dog" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Todos_Work",
                table: "Todos",
                column: "Work",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todos");
        }
    }
}

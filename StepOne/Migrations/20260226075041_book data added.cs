using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StepOne.Migrations
{
    /// <inheritdoc />
    public partial class bookdataadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Purchase", "Title" },
                values: new object[,]
                {
                    { 1, "Alain Henri", 2, "Le Grand Meaulnus" },
                    { 2, "Trevanian", 3, "The Summer of Katya" },
                    { 3, "George R.R. Martin", 1, "A Song of Ice and Fire: A Game of Thrones" },
                    { 4, "Terry Deary", 3, "Terrible Tudors" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}

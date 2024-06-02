using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace To_Do_App.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryToWorkTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "WorkTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "WorkTasks");
        }
    }
}

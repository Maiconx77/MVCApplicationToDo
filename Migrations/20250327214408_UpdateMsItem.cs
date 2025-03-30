using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCApplicationToDo.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMsItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "MilestoneItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSelected",
                table: "MilestoneItems");
        }
    }
}

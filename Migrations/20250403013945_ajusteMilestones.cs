using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCApplicationToDo.Migrations
{
    /// <inheritdoc />
    public partial class ajusteMilestones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MilestoneItemId",
                table: "Milestones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Milestones_MilestoneItemId",
                table: "Milestones",
                column: "MilestoneItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Milestones_MilestoneItems_MilestoneItemId",
                table: "Milestones",
                column: "MilestoneItemId",
                principalTable: "MilestoneItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Milestones_MilestoneItems_MilestoneItemId",
                table: "Milestones");

            migrationBuilder.DropIndex(
                name: "IX_Milestones_MilestoneItemId",
                table: "Milestones");

            migrationBuilder.DropColumn(
                name: "MilestoneItemId",
                table: "Milestones");
        }
    }
}

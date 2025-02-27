using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCApplicationToDo.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTaskClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_MilestoneChains_MilestoneChainId",
                table: "TaskItems");

            migrationBuilder.AlterColumn<int>(
                name: "MilestoneChainId",
                table: "TaskItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_MilestoneChains_MilestoneChainId",
                table: "TaskItems",
                column: "MilestoneChainId",
                principalTable: "MilestoneChains",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_MilestoneChains_MilestoneChainId",
                table: "TaskItems");

            migrationBuilder.AlterColumn<int>(
                name: "MilestoneChainId",
                table: "TaskItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_MilestoneChains_MilestoneChainId",
                table: "TaskItems",
                column: "MilestoneChainId",
                principalTable: "MilestoneChains",
                principalColumn: "Id");
        }
    }
}

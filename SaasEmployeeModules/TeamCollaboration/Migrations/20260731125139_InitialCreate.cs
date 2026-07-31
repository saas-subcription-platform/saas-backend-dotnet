using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamCollaboration.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "conversation_id",
                table: "teams",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_teams_conversation_id",
                table: "teams",
                column: "conversation_id");

            migrationBuilder.AddForeignKey(
                name: "FK_teams_conversations_conversation_id",
                table: "teams",
                column: "conversation_id",
                principalTable: "conversations",
                principalColumn: "conversation_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_teams_conversations_conversation_id",
                table: "teams");

            migrationBuilder.DropIndex(
                name: "IX_teams_conversation_id",
                table: "teams");

            migrationBuilder.DropColumn(
                name: "conversation_id",
                table: "teams");
        }
    }
}

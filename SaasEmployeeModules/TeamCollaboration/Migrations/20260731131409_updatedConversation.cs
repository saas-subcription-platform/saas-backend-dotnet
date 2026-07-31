using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamCollaboration.Migrations
{
    /// <inheritdoc />
    public partial class updatedConversation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "team_id",
                table: "conversations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "team_id",
                table: "conversations",
                type: "bigint",
                nullable: true);
        }
    }
}

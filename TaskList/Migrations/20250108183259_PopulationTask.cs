using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskList.Migrations
{
    /// <inheritdoc />
    public partial class PopulationTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Tasks(Title, Description, Date, Status, UserId) VALUES('CORRIDA DOS DEVS', 'Se trata de um evento de corrida que tem o intuito de reunir todos os programadores da cidade do recife', '2025-04-20T08:00:00', 'PENDENTE', 1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Users");
        }
    }
}

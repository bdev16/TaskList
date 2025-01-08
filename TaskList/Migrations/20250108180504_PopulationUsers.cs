using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

#nullable disable

namespace TaskList.Migrations
{
    /// <inheritdoc />
    public partial class PopulationUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Users(Name, Email, Password) VALUES('Bruno', 'brunoantonio0210@gmail.com', 'Bruno123')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Users");
        }
    }
}

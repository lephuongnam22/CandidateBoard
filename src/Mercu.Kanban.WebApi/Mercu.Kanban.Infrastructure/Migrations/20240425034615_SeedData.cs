using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mercu.Kanban.Infrastructure.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Jobs (JobTitle, JobDescription) VALUES('Backend Developer','Backend Developer')" +
                ",('Frontend Developer','Frontend Developer')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Jobs");
        }
    }
}

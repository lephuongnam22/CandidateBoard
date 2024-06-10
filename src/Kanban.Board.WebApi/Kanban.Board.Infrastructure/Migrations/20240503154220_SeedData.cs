using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kanban.Board.Infrastructure.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Jobs (JobTitle, JobDescription) VALUES" +
                                 "('Backend Developer','Backend Developer')," +
                                 "('Frontend Developer','Frontend Developer'), " +
                                 "('Devop Engineer', 'Devop Engineer')");

            migrationBuilder.Sql("INSERT INTO Interviewers (Name) VALUES" +
                                 "('Nam Le')," +
                                 "('Ket Tran')," +
                                 "('Khoa Dang')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Jobs");
            migrationBuilder.Sql("DELETE FROM Interviewers");
        }
    }
}

using Kanban.Board.Domain.Entities;
using Kanban.Board.Domain.Interfaces;
using Kanban.Board.Infrastructure.Persistence;

namespace Kanban.Board.Infrastructure.Repositories
{
    public class InterviewerRepository : GenericRepository<Interviewer>, IInterviewerRepository
    {
        public InterviewerRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}

using Mercu.Kanban.Domain.Entities;
using Mercu.Kanban.Domain.Interfaces;
using Mercu.Kanban.Infrastructure.Persistence;

namespace Mercu.Kanban.Infrastructure.Repositories
{
    public class InterviewerRepository : GenericRepository<Interviewer>, IInterviewerRepository
    {
        public InterviewerRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}

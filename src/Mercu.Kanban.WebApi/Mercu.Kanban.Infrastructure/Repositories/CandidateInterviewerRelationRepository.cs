using Mercu.Kanban.Domain.Entities;
using Mercu.Kanban.Domain.Interfaces;
using Mercu.Kanban.Infrastructure.Persistence;

namespace Mercu.Kanban.Infrastructure.Repositories
{
    public class CandidateInterviewerRelationRepository : GenericRepository<CandidateInterviewerRelation>, ICandidateInterviewerRelationRepository
    {
        public CandidateInterviewerRelationRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}

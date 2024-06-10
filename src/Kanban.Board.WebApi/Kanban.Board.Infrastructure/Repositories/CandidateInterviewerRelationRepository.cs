using Kanban.Board.Domain.Entities;
using Kanban.Board.Domain.Interfaces;
using Kanban.Board.Infrastructure.Persistence;

namespace Kanban.Board.Infrastructure.Repositories
{
    public class CandidateInterviewerRelationRepository : GenericRepository<CandidateInterviewerRelation>, ICandidateInterviewerRelationRepository
    {
        public CandidateInterviewerRelationRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}

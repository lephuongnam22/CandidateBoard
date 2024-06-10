using Kanban.Board.Domain.Entities;
using Kanban.Board.Domain.Interfaces;
using Kanban.Board.Infrastructure.Persistence;

namespace Kanban.Board.Infrastructure.Repositories
{
#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
    public class CandidateJobRelationRepository : GenericRepository<CandidateJobRelation>, ICandidateJobRelationRepository
    {
        public CandidateJobRelationRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}

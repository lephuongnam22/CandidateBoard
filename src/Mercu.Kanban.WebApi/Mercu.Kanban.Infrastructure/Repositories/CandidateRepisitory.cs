using Mercu.Kanban.Domain.Entities;
using Mercu.Kanban.Domain.Interfaces;
using Mercu.Kanban.Infrastructure.Persistence;

namespace Mercu.Kanban.Infrastructure.Repositories
{
#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
    public class CandidateRepisitory : GenericRepository<Candidate>, ICandidateRepository
    {
        public CandidateRepisitory(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}

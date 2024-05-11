using Mercu.Kanban.Domain.Interfaces;
using Mercu.Kanban.Infrastructure.Persistence;

namespace Mercu.Kanban.Infrastructure.UnitOfWorks
{
    public interface IUnitOfWork
    {
        ICandidateRepository CandidateRepository { get; }
        IJobRepository JobRepository { get; }
        ICandidateJobRelationRepository CandidateJobRelationRepository { get; }
        IInterviewerRepository InterviewerRepository { get; }
        ICandidateInterviewerRelationRepository CandidateInterviewerRelationRepository { get; }
        DatabaseContext DatabaseContext { get; }
        Task CreateTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}

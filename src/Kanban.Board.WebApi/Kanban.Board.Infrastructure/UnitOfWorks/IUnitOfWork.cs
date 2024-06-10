using Kanban.Board.Domain.Interfaces;
using Kanban.Board.Infrastructure.Persistence;

namespace Kanban.Board.Infrastructure.UnitOfWorks
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

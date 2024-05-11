using Mercu.Kanban.Domain.Interfaces;
using Mercu.Kanban.Infrastructure.Persistence;
using Mercu.Kanban.Infrastructure.Repositories;

namespace Mercu.Kanban.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _databaseContext;
        private ICandidateRepository _candidateRepository;
        private IJobRepository _jobRepository;
        private ICandidateJobRelationRepository _candidateJobRelationRepository;
        private IInterviewerRepository _interviewerRepository;
        private ICandidateInterviewerRelationRepository _candidateInterviewerRelationRepository;
        public DatabaseContext DatabaseContext => _databaseContext;

        public UnitOfWork(DatabaseContext databaseContext)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _databaseContext = databaseContext;
        }

        public ICandidateRepository CandidateRepository 
        {
            get { return _candidateRepository = _candidateRepository ?? new CandidateRepisitory(_databaseContext); }
        }

        public IJobRepository JobRepository
        {
            get { return _jobRepository = _jobRepository ?? new JobRepository(_databaseContext); }
        }

        public IInterviewerRepository InterviewerRepository
        {
            get { return _interviewerRepository = _interviewerRepository ?? new InterviewerRepository(_databaseContext); }
        }

        public ICandidateJobRelationRepository CandidateJobRelationRepository
        {
            get { return _candidateJobRelationRepository = _candidateJobRelationRepository ?? new CandidateJobRelationRepository(_databaseContext); }
        }

        public ICandidateInterviewerRelationRepository CandidateInterviewerRelationRepository
        {
            get { return _candidateInterviewerRelationRepository = _candidateInterviewerRelationRepository ?? new CandidateInterviewerRelationRepository(_databaseContext); }
        }

        public async Task CommitAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }

        public async Task CreateTransactionAsync()
        {
            await _databaseContext.Database.BeginTransactionAsync();
        }

        public async Task RollbackAsync()
        {
            await _databaseContext.Database.RollbackTransactionAsync();
        }
    }
}

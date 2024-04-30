using Mercu.Kanban.Domain.Entities;

namespace Mercu.Kanban.Application.DTOS
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class AddCandidateRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public CandidateStatus CandidateStatus { get; set; }
        public int JobId { get; set; }
    }
}

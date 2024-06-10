using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kanban.Board.Domain.Entities
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class CandidateInterviewerRelation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public virtual Candidate Candidate { get; set; }
        public int InterviewerId { get; set; }
        public virtual Interviewer Interviewer { get; set; }
    }
}

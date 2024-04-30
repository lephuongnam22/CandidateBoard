using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mercu.Kanban.Domain.Entities
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class Candidate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
        [MaxLength(100)]
        public CandidateStatus CandidateStatus { get; set; }
        [MaxLength(255)]
        public string PhoneNumber { get; set; }
        public virtual ICollection<CandidateJobRelation> CandidateJobRelations { get; set; }
    }
}
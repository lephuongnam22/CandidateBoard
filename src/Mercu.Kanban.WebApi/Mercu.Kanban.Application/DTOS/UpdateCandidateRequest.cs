using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercu.Kanban.Application.DTOS
{
    public record UpdateCandidateRequest : AddCandidateRequest
    {
        public string OldEmail { get; set; }
        public string OldPhoneNumber { get; set; }
        public int OldJobId { get; set; }

    }
}

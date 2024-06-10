using AutoMapper;
using Kanban.Board.Application.DTOS;
using Kanban.Board.Domain.Entities;

namespace Kanban.Board.Application.Mapper
{
    public class CandidateProfile : Profile
    {
        public CandidateProfile()
        {
            CreateMap<CandidateModel, Candidate>().ReverseMap();
            CreateMap<AddCandidateRequest, Candidate>().ReverseMap();
        }
    }
}

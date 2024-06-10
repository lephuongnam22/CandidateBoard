using AutoMapper;
using Kanban.Board.Application.DTOS;
using Kanban.Board.Domain.Entities;

namespace Kanban.Board.Application.Mapper
{
    public class InterviewerProfile : Profile
    {
        public InterviewerProfile()
        {
            CreateMap<Interviewer, InterviewerModel>().ReverseMap();
        }
    }
}

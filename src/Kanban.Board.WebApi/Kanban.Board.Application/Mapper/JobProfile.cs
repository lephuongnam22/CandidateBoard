using AutoMapper;
using Kanban.Board.Application.DTOS;
using Kanban.Board.Domain.Entities;

namespace Kanban.Board.Application.Mapper
{
    public class JobProfile : Profile
    {
        public JobProfile()
        {
            CreateMap<Job, JobModel>().ReverseMap();
        }
    }
}

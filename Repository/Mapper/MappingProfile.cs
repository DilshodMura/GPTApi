using AutoMapper;
using Database.Entities;
using Domain.Entities;

namespace Repository.Mapper
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Mapper for history table
            CreateMap<IHistory, HistoryDb>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User)).ReverseMap();

            CreateMap<HistoryDb, BusinessModels.History>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User)).ReverseMap();

            //Mapper for message table
            CreateMap<IMessage, MessageDb>()
                .ForMember(dest => dest.TopicId, opt => opt.MapFrom(src => src.Topic.Id))
                .ForMember(dest => dest.Topic, opt => opt.MapFrom(src => src.Topic));

            CreateMap<MessageDb, BusinessModels.Message>()
                .ForMember(dest => dest.TopicId, opt => opt.MapFrom(src => src.Topic.Id))
                .ForMember(dest => dest.Topic, opt => opt.MapFrom(src => src.Topic));

            //Mapper for topic table
            CreateMap<ITopic, TopicDb>()
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages));

            CreateMap<TopicDb, BusinessModels.Topic>()
                .ForMember(dest => dest.HistoryId, opt => opt.MapFrom(src => src.History.Id))
                .ForMember(dest => dest.History, opt => opt.MapFrom(src => src.History))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages));


            //Mapper for user table
            CreateMap<IUser, UserDb>().ReverseMap();
            CreateMap<UserDb, BusinessModels.User>().ReverseMap();
        }
    }
}

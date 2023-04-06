using AutoMapper;
using Database.Entities;
using Domain.Entities;
using Repository.BusinessModels;

namespace Repository.Mapper
{
    public sealed class Mapper : Profile
    {
        public Mapper()
        {
            //Mapper for history table
            CreateMap<IHistory, HistoryDb>();
            CreateMap<HistoryDb,History>();

            //Mapper for message table
            CreateMap<IMessage, MessageDb>();
            CreateMap<MessageDb, Message>();

            //Mapper for topic table
            CreateMap<ITopic, TopicDb>();
            CreateMap<TopicDb, Topic>();

            //Mapper for user table
            CreateMap<IUser, UserDb>();
            CreateMap<UserDb, User>();
        }
    }
}

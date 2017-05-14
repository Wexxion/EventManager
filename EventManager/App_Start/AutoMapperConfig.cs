using System;
using AutoMapper;
using TaskManager.DomainLayer;
using TaskManager.RepoLayer;
using TaskManager.RepoLayer.DataBase.DbModel;

namespace TaskManager.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Person, PersonInDb>();
                    cfg.CreateMap<PersonInDb, Person>();

                    cfg.CreateMap<Location, LocationInDb>();
                    cfg.CreateMap<LocationInDb, Location>();

                    cfg.CreateMap<VEventInDb, VEvent>()
                        .ForMember(d => d.Start,
                            opt => opt.MapFrom(c => DateTime.Parse(c.Start)))
                        .ForMember(d => d.End,
                            opt => opt.MapFrom(c => DateTime.Parse(c.End)))
                        .ForMember(d => d.FirstReminder,
                            opt => opt.MapFrom(c => TimeSpan.Parse(c.FirstReminder)))
                        .ForMember(d => d.SecondReminder,
                            opt => opt.MapFrom(c => TimeSpan.Parse(c.SecondReminder)));
                    ;

                    cfg.CreateMap<VEvent, VEventInDb>()
                        .ConvertUsing(e => new VEventInDb
                        {
                            Name = e.Name,
                            Description = e.Description,
                            Start = e.Start.ToString(),
                            End = e.End.ToString(),
                            FirstReminder = e.FirstReminder.ToString(),
                            SecondReminder = e.SecondReminder.ToString(),
                            Location = Mapper.Map<LocationInDb>(e.Location) ?? new LocationInDb()
                        });
                }
            );
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EntityDemo.DTO;
using EntityDemo.Entities;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace EntityDemo.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Actor, ActorDto>();

            CreateMap<Cinema, CinemaDto>().ForMember(dto => dto.Latitute, ety => ety.MapFrom(p => p.Location.Y))
            .ForMember(dto => dto.Longitute, ety => ety.MapFrom(p => p.Location.X));

            CreateMap<Genre, GenreDto>();
            CreateMap<GenreAddDto, Genre>();

            CreateMap<Movie, MovieDto>()
            .ForMember(dto => dto.Genres, ent => ent.MapFrom(p => p.Genres.OrderByDescending(g => g.Name)))
            .ForMember(dto => dto.Cinemas, ety => ety.MapFrom(m => m.CinemaHall.OrderByDescending(c => c.Cinema.Name).Select(c => c.Cinema)))
            .ForMember(dto => dto.Actors, ety => ety.MapFrom(m => m.MovieActors.Select(ma => ma.Actor)));

            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid:4326);

            CreateMap<CinemaCreationDto, Cinema>()
            .ForMember(ent => ent.Location, dto => dto.MapFrom(prop => geometryFactory.CreatePoint(new Coordinate(prop.Longitude, prop.Latitude))));

            CreateMap<CinemaHallCreationDto,CinemaHall>();
            CreateMap<CinemaOfferCreationDto,CinemaOffer>();

            CreateMap<CinemaUpdationDto, Cinema>()
            .ForMember(ent => ent.Location, dto => dto.MapFrom(prop => geometryFactory.CreatePoint(new Coordinate(prop.Longitude, prop.Latitude))));

            CreateMap<CinemaHallUpdationDto,CinemaHall>();
            CreateMap<CinemaOfferUpdationDto,CinemaOffer>();

            CreateMap<MovieCreationDto, Movie>()
            .ForMember(ent => ent.Genres, dto => dto.MapFrom(prop => prop.GenreIds.Select(id => new Genre{Id = id})))
            .ForMember(ent => ent.CinemaHall, dto => dto.MapFrom(prop => prop.CinemaHallIds.Select(id => new CinemaHall{Id = id})));

            CreateMap<MovieActorCreationDto, MovieActor>();

            CreateMap<ActorCreationDto, Actor>();
        }
    }
}
using AutoMapper;
using lbdbackend.Core.Entities;
using lbdbackend.Service.DTOs.AccountDTOs;
using lbdbackend.Service.DTOs.CommentDTOs;
using lbdbackend.Service.DTOs.GenreDTOs;
using lbdbackend.Service.DTOs.ListDTOs;
using lbdbackend.Service.DTOs.MovieDTOs;
using lbdbackend.Service.DTOs.NewsDTOs;
using lbdbackend.Service.DTOs.PersonDTOs;
using lbdbackend.Service.DTOs.ProfessionDTOs;
using lbdbackend.Service.DTOs.ReviewDTOs;
using lbdbackend.Service.DTOs.UserDTOs;
using lbdbackend.Service.DTOs.YearDTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Service.Mappings {
    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<RegisterDTO, AppUser>();

            CreateMap<GenreUpdateDTO, Genre>();
            CreateMap<Genre, GenreUpdateDTO>();
            CreateMap<Genre, GenreCreateDTO>();
            CreateMap<GenreCreateDTO, Genre>();
            CreateMap<Genre, GenreGetDTO>();
            CreateMap<GenreGetDTO, Genre>();

            CreateMap<ProfessionUpdateDTO, Profession>();
            CreateMap<Profession, ProfessionUpdateDTO>();
            CreateMap<Profession, ProfessionCreateDTO>();
            CreateMap<ProfessionCreateDTO, Profession>();
            CreateMap<Profession, ProfessionGetDTO>();
            CreateMap<ProfessionGetDTO, Profession>();

            CreateMap<Person, PersonCreateDTO>();
            CreateMap<PersonCreateDTO, Person>();
            CreateMap<Person, PersonGetDTO>();
            CreateMap<PersonGetDTO, Person>();
            CreateMap<Person, PersonGetWithProfessionDTO>();
            CreateMap<PersonGetWithProfessionDTO, Person>();

            CreateMap<Movie, MovieCreateDTO>();
            CreateMap<MovieCreateDTO, Movie>();
            CreateMap<Movie, MovieGetDTO>();
            CreateMap<MovieGetDTO, Movie>();

            CreateMap<YearGetDTO, Year>();
            CreateMap<Year, YearGetDTO>();

            CreateMap<Review, ReviewCreateDTO>();
            CreateMap<ReviewCreateDTO, Review>();
            CreateMap<ReviewCreateDTO, Review>();
            CreateMap<Review, ReviewGetDTO>();

            CreateMap<CommentCreateDTO, Comment>();
            CreateMap<Comment, CommentCreateDTO>();
            CreateMap<CommentGetDTO, Comment>();
            CreateMap<Comment, CommentGetDTO>();

            CreateMap<UserGetDTO, AppUser>();
            CreateMap<AppUser, UserGetDTO>();

            CreateMap<MovieListCreateDTO, MovieList>();
            CreateMap<MovieList, MovieListCreateDTO>();
            CreateMap<MovieListGetDTO, MovieList>();
            CreateMap<MovieList, MovieListGetDTO>();

            CreateMap<NewsCreateDTO, News>();
            CreateMap<News, NewsCreateDTO>();
            CreateMap<NewsGetDTO, News>();
            CreateMap<News, NewsGetDTO>();




        }
    }
}

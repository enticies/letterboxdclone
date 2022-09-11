using lbdbackend.Service.DTOs.ListDTOs;
using lbdbackend.Service.DTOs.MovieDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lbdbackend.Service.Interfaces {
    public interface IMovieListService {
        Task Create(MovieListCreateDTO movieListCreateDTO);
        Task<PaginatedListDTO<MovieListGetDTO>> GetUserLists(string userName, int i);
        Task<List<MovieGetDTO>> GetListMovies(int id);
        Task<List<MovieListGetDTO>> GetRecentsLists();
        Task Delete(int id);

    }
}

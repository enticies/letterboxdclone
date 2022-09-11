using lbdbackend.Core.Entities;
using lbdbackend.Service.DTOs.GenreDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lbdbackend.Service.Interfaces {
    public interface IGenresService {
        Task Create(GenreCreateDTO genrePostDTO);
        Task DeleteOrRestore(int? id);
        Task Update(int? id, GenreUpdateDTO genreUpdateDTO);
        Task<List<GenreGetDTO>> GetGenres(int? id);
        Task<GenreGetDTO> GetByID(int? id);


    }
}
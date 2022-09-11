using lbdbackend.Service.DTOs.ListDTOs;
using lbdbackend.Service.DTOs.MovieDTOs;
using lbdbackend.Service.DTOs.NewsDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lbdbackend.Service.Interfaces {
    public interface INewsService {
        Task Create(NewsCreateDTO newsCreateDTO);
        Task<PaginatedListDTO<NewsGetDTO>> GetUserNews(string userName, int i);
        Task Delete(int id);
        Task<NewsGetDTO> GetById(int id);
        Task<NewsGetDTO> GetLatestNews();
        Task<List<NewsGetDTO>> GetRecentNews();


    }
}
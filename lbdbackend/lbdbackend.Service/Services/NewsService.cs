using AutoMapper;
using lbdbackend.Core.Entities;
using lbdbackend.Core.Repositories;
using lbdbackend.Service.DTOs.CommentDTOs;
using lbdbackend.Service.DTOs.GenreDTOs;
using lbdbackend.Service.DTOs.MovieDTOs;
using lbdbackend.Service.DTOs.NewsDTOs;
using lbdbackend.Service.DTOs.UserDTOs;
using lbdbackend.Service.Exceptions;
using lbdbackend.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using P225Allup.Extensions;
using P225NLayerArchitectura.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lbdbackend.Service.Services {
    public class NewsService : INewsService {
        private readonly IMapper _mapper;
        private readonly INewsRepository _repo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _env;

        public NewsService(INewsRepository repo, IMapper mapper, UserManager<AppUser> userManager, IWebHostEnvironment env) {
            _repo = repo;
            _mapper = mapper;
            _userManager = userManager;
            _env = env;
        }

        public async Task Create(NewsCreateDTO newsCreateDTO) {
            var user = await _userManager.FindByNameAsync(newsCreateDTO.OwnerUserName);

            if (user == null) {
                throw new ItemNotFoundException("User not found.");
            }

            News news = _mapper.Map<News>(newsCreateDTO);

            news.Image = await newsCreateDTO.Image.CreateFileAsync(_env, "images", "news");
            news.OwnerId = user.Id;

            await _repo.AddAsync(news);
            await _repo.CommitAsync();
        }

        public async Task Delete(int id) {
            if (!await _repo.ExistsAsync(n => !n.IsDeleted && n.ID == id)) {
                throw new ItemNotFoundException("News not found.");
            }
            var news = await _repo.GetAsync(n => !n.IsDeleted && n.ID == id);
            news.IsDeleted = true;
            await _repo.CommitAsync();
        }

        public async Task<PaginatedListDTO<NewsGetDTO>> GetUserNews(string userName, int i) {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) {
                throw new ItemNotFoundException("User not found.");
            }

            List<NewsGetDTO> newsGetDTOs = new List<NewsGetDTO>();

            foreach (var item in await _repo.GetAllAsync(n => !n.IsDeleted && n.OwnerId == user.Id, "Owner")) {
                var dto = _mapper.Map<NewsGetDTO>(item);
                dto.OwnerUsername = item.Owner.UserName;
                newsGetDTOs.Add(dto);
            }

            PaginatedListDTO<NewsGetDTO> paginatedListDTO = new PaginatedListDTO<NewsGetDTO>(newsGetDTOs, i, 4);

            return paginatedListDTO;
        }

        public async Task<NewsGetDTO> GetById(int id) {
            var news = await _repo.GetAsync(n => !n.IsDeleted && n.ID == id, "Owner");

            if (news == null) {
                throw new ItemNotFoundException("News not found.");
            }

            var dto = _mapper.Map<NewsGetDTO>(news);
            dto.OwnerUsername = news.Owner.UserName;
            return dto;
        }

        public async Task<NewsGetDTO> GetLatestNews() {
            var news = _mapper.Map<NewsGetDTO>(await _repo.GetLast());
            return news;
        }

        public async Task<List<NewsGetDTO>> GetRecentNews() {
            List<NewsGetDTO> newsGetDTOs = new List<NewsGetDTO>();

            List<News> news = await _repo.GetAllAsync(r => !r.IsDeleted, "Owner");

            for (int i = Math.Max(0, news.Count - 12); i < news.Count; ++i) {
                var dto = _mapper.Map<NewsGetDTO>(news[i]);
                dto.OwnerUsername = news[i].Owner.UserName;
                dto.Image = news[i].Image;
                newsGetDTOs.Add(dto);
            }

            return newsGetDTOs;
        }
    }
}
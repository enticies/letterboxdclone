using AutoMapper;
using lbdbackend.Core.Entities;
using lbdbackend.Core.Repositories;
using lbdbackend.Service.DTOs.YearDTOs;
using lbdbackend.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lbdbackend.Service.Services {
    public class YearsService : IYearsService {
        private readonly IMapper _mapper;
        private readonly IYearRepository _repo;
        public YearsService(IYearRepository repo, IMapper mapper) {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<List<YearGetDTO>> GetYears() {
            List<YearGetDTO> dtos = new List<YearGetDTO>();
            foreach (Year year in await _repo.GetAllAsync(e => e != null)) {
                dtos.Add(_mapper.Map<YearGetDTO>(year));
            }

            return dtos;
        }
    }
}
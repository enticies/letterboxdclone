using AutoMapper;
using lbdbackend.Core.Entities;
using lbdbackend.Core.Repositories;
using lbdbackend.Service.DTOs.ProfessionDTOs;
using lbdbackend.Service.Exceptions;
using lbdbackend.Service.Interfaces;
using P225NLayerArchitectura.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lbdbackend.Service.Services {
    public class ProfessionsService : IProfessionsService {
        private readonly IMapper _mapper;
        private readonly IProfessionRepository _repo;
        public ProfessionsService(IProfessionRepository repo, IMapper mapper) {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task Create(ProfessionCreateDTO professionCreateDTO) {
            if (await _repo.ExistsAsync(e => e.Name.ToLower() == professionCreateDTO.Name.ToLower())) {
                throw new AlreadyExistException($"Profession name \"{professionCreateDTO.Name}\" already exists.");
            }

            Profession profession = _mapper.Map<Profession>(professionCreateDTO);
            profession.CreatedAt = DateTime.UtcNow;

            await _repo.AddAsync(profession);
            await _repo.CommitAsync();
        }

        public async Task DeleteOrRestore(int? id) {
            if (id == null) {
                throw new BadRequestException("ID can't be null.");
            }

            if (!await _repo.ExistsAsync(e => e.ID == id)) {
                throw new ItemNotFoundException("ID not found.");
            }
            await _repo.RemoveOrRestore(id);
            await _repo.CommitAsync();
        }

        public async Task Update(int? id, ProfessionUpdateDTO professionUpdateDTO) {
            if (id == null) {
                throw new BadRequestException("Id can't be null.");
            }
            if (id != professionUpdateDTO.ID) {
                throw new BadRequestException("IDs do not match.");
            }

            if (!await _repo.ExistsAsync(e => e.ID == professionUpdateDTO.ID)) {
                throw new ItemNotFoundException("ID doesn't exist.");
            }

            Profession profession = await _repo.GetAsync(e => e.ID == professionUpdateDTO.ID);
            profession.Name = professionUpdateDTO.Name;
            profession.UpdatedAt = DateTime.UtcNow;

            if (profession == null) {
                throw new NullReferenceException();
            }

            await _repo.CommitAsync();
        }

        public async Task<List<ProfessionGetDTO>> GetProfessions() {
            List<ProfessionGetDTO> dtos = new List<ProfessionGetDTO>();
            foreach (Profession profession in await _repo.GetAllAsync(e => e != null)) {
                dtos.Add(_mapper.Map<ProfessionGetDTO>(profession));
            }

            return dtos;
        }
        public async Task<ProfessionGetDTO> GetByID(int? id) {
            if (id == null) {
                throw new ArgumentNullException("id");
            }

            return _mapper.Map<ProfessionGetDTO>(await _repo.GetAsync(e => e.ID == id));
        }

    }
}
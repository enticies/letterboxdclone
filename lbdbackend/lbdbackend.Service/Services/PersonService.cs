using AutoMapper;
using lbdbackend.Core.Entities;
using lbdbackend.Core.Repositories;
using lbdbackend.Data.Repositories;
using lbdbackend.Service.DTOs.GenreDTOs;
using lbdbackend.Service.DTOs.PersonDTOs;
using lbdbackend.Service.Exceptions;
using lbdbackend.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using P225Allup.Extensions;
using P225NLayerArchitectura.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lbdbackend.Service.Services {
    public class PersonService : IPersonService {
        private readonly IMapper _mapper;
        private readonly IPersonRepository _repo;
        private readonly IProfessionRepository _professionRepo;
        private readonly IJoinMoviesPeopleRepository _joinMoviesPeopleRepository;

        private readonly IWebHostEnvironment _env;

        public PersonService(IPersonRepository repo, IMapper mapper, IWebHostEnvironment env, IProfessionRepository professionRepo, IJoinMoviesPeopleRepository joinMoviesPeopleRepository) {
            _repo = repo;
            _mapper = mapper;
            _env = env;
            _professionRepo = professionRepo;
            _joinMoviesPeopleRepository = joinMoviesPeopleRepository;   
        }
        public async Task Create(PersonCreateDTO personCreateDTO) {
            if (await _repo.ExistsAsync(e => e.Name == personCreateDTO.Name)) {
                throw new AlreadyExistException($"Person name \"{personCreateDTO.Name}\" already exists.");
            }

            if (!await _professionRepo.ExistsAsync(e => e.ID == personCreateDTO.ProfessionID)) {
                throw new ItemNotFoundException($"{personCreateDTO.ProfessionID} is not a valid ID.");
            }


            if (personCreateDTO.File.CheckFileContentType("image/jpeg")) {
                throw new BadRequestException("Wrong file type.");
            }

            if (personCreateDTO.File.CheckFileSize(300)) {
                throw new BadRequestException("File too big.");
            }


            Person person = _mapper.Map<Person>(personCreateDTO);

            person.Image = await personCreateDTO.File.CreateFileAsync(_env, "images", "people");

            person.CreatedAt = DateTime.UtcNow;

            await _repo.AddAsync(person);
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
        public async Task Update(int? id, PersonUpdateDTO personUpdateDTO) {
            if (id == null) {
                throw new BadRequestException("ID can't be null.");
            }
            if (id != personUpdateDTO.ID) {
                throw new BadRequestException("IDs do not match.");
            }

            if (!await _repo.ExistsAsync(e => e.ID == personUpdateDTO.ID)) {
                throw new ItemNotFoundException("Person ID doesn't exist.");
            }
            if (!await _professionRepo.ExistsAsync(e => e.ID == personUpdateDTO.ProfessionID)) {
                throw new ItemNotFoundException("Profession ID doesn't exist.");
            }


            Person person = await _repo.GetAsync(e => e.ID == personUpdateDTO.ID);
            person.Name = personUpdateDTO.Name;
            person.Description = personUpdateDTO.Description;
            person.Image = await personUpdateDTO.File.CreateFileAsync(_env, "images", "people");
            person.UpdatedAt = DateTime.UtcNow;


            if (person == null) {
                throw new NullReferenceException();
            }

            await _repo.CommitAsync();
        }

        public async Task<List<PersonGetDTO>> GetPeople() {
            List<PersonGetDTO> dtos = new List<PersonGetDTO>();
            foreach (Person person in await _repo.GetAllAsync(e => e != null)) {
                dtos.Add(_mapper.Map<PersonGetDTO>(person));
            }

            return dtos;
        }
        public async Task<PersonGetDTO> GetByID(int? id) {
            if (id == null) {
                throw new ArgumentNullException("id");
            }
            return _mapper.Map<PersonGetDTO>(await _repo.GetAsync(e => e.ID == id));
        }

        public async Task<List<PersonGetDTO>> GetMoviePeople(int? id) {
            if (id == null) {
                throw new ArgumentNullException();
            }

            List<PersonGetDTO> personGetDTOs = new List<PersonGetDTO>();
            foreach (JoinMoviesPeople row in await _joinMoviesPeopleRepository.GetAllAsync(p => !p.IsDeleted && p.MovieID == id, "Person", "Movie", "Person.Profession")) {
                var dto = _mapper.Map<PersonGetDTO>(row.Person);
                dto.ProfessionName = row.Person.Profession.Name;
                personGetDTOs.Add(dto);
            }
            var _ = 1;
            return personGetDTOs;
        }
    }
}
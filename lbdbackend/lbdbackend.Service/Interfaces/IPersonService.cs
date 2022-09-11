using lbdbackend.Service.DTOs.PersonDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lbdbackend.Service.Interfaces {
    public interface IPersonService {
        Task Create(PersonCreateDTO professionCreateDTO);
        Task DeleteOrRestore(int? id);
        Task Update(int? ID, PersonUpdateDTO personUpdateDTO);
        Task<List<PersonGetDTO>> GetPeople();
        Task<PersonGetDTO> GetByID(int? id);
        Task<List<PersonGetDTO>> GetMoviePeople(int? id);


    }
}
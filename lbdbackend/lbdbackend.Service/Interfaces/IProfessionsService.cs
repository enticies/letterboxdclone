using lbdbackend.Service.DTOs.GenreDTOs;
using lbdbackend.Service.DTOs.ProfessionDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lbdbackend.Service.Interfaces {
    public interface IProfessionsService {
        Task Create(ProfessionCreateDTO professionCreateDTO);
        Task DeleteOrRestore(int? id);
        Task Update(int? id, ProfessionUpdateDTO professionUpdateDTO);
        Task<List<ProfessionGetDTO>> GetProfessions();
        Task<ProfessionGetDTO> GetByID(int? id);



    }
}
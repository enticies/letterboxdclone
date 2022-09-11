using lbdbackend.Service.DTOs.YearDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lbdbackend.Service.Interfaces {
    public interface IYearsService {
        Task<List<YearGetDTO>> GetYears();


    }
}
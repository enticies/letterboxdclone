using lbdbackend.Service.DTOs.AccountDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace lbdbackend.Service.Interfaces {
    public interface IEmailService {
        void Register(RegisterDTO registerDTO, string link);
    }
}

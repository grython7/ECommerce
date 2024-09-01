using BusinessDomain.DTOs;
using Infrastructure.Entities;
using Infrastructure.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Interfaces
{
    public interface IUserService
    {
        public Task<UserDTO> RegisterUserAsync(UserDTO userIn);
        public Task<UserDTO> UserLoginAsync(String name, String password);
    }
}

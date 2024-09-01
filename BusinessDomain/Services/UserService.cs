using BusinessDomain.DTOs;
using BusinessDomain.Exceptions;
using BusinessDomain.Helpers;
using BusinessDomain.Interfaces;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.UnitofWork;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDTO> RegisterUserAsync(UserDTO userIn)
        {
            //// Handled by the controller
            //if (!EmailValidator.IsValid(userIn.Email))
            //    throw new InvalidEmailFormatException();
            //if (!PasswordValidator.IsValid(userIn.Password))
            //    throw new WeakPasswordException();

            User user = userIn.Adapt<User>();

            User existingUser = await _userRepository.GetByEmailAsync(user.Email);
            if (existingUser != null)
                throw new EmailAlreadyExistsException();

            user.PasswordSalt = PasswordManager.GenerateSalt();
            user.PasswordHash = PasswordManager.HashPassword(userIn.Password, user.PasswordSalt);

            await _userRepository.AddAsync(user);
            if (await _unitOfWork.SaveAsync() == 0)
                throw new NoSavedChangesException();
            

            return user.Adapt<UserDTO>();
        }

        public async Task<UserDTO> UserLoginAsync(String email, String password)
        {
            User existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser is null)
            {
                throw new WrongEmailException();
            }
            if (PasswordManager.HashPassword(password, existingUser.PasswordSalt) != existingUser.PasswordHash)
            {
                throw new WrongPasswordException();
            }
            return existingUser.Adapt<UserDTO>();
        }
    }
}

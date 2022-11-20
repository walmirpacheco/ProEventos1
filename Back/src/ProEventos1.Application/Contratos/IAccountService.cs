using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProEventos1.Application.Dtos;

namespace ProEventos1.Application.Contratos
{
    public interface IAccountService
    {
        Task<bool> UserExists(String userName);
        Task<UserUpdateDto> GetUserByUserNameAsync(string userName);
        Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password);
        Task<UserUpdateDto> CreateAccountAsync(UserDto userDto);
        Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto);      
    }
}
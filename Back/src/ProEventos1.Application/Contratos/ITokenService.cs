using System.Threading.Tasks;
using ProEventos1.Application.Dtos;

namespace ProEventos1.Application.Contratos
{
    public interface ITokenService
    {
        Task<string> CreateToken(UserUpdateDto userUpdateDto);
    }
}
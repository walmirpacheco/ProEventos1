using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ProEventos1.API.Helpers
{
    public interface IUtil
    {
        Task<string> SaveImage(IFormFile imageFile, string destino);
        void DeleteImage(string imageName, string destino);        
    }
}
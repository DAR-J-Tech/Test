using PruebaPractica.Models;

namespace PruebaPractica.Services
{
    public interface IApiService
    {
        Task<List<Buscador>> GetBuscador();
        Task<List<Ciudadano>> GetCiudadano();
    }
}

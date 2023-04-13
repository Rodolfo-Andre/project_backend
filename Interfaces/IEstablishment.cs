using project_backend.Models;

namespace project_backend.Interfaces
{
    public interface IEstablishment
    {

        Task<List<Establishment>> GetEstablishments();

        Task<Establishment> GetEstablishment(int id);
        Task<bool> updateEstblisment(Establishment establishment);
    }
}

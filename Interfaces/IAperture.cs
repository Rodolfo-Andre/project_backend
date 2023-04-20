using project_backend.Models;

namespace project_backend.Interfaces
{
    public interface IAperture
    {
        public Task<List<Aperture>> GetAll();
        public Task<Aperture> GetById(int id);
        public Task<bool> CreateAperture(Aperture aperture);
        public Task<bool> UpdateAperture(Aperture aperture);
        public Task<bool> DeleteAperture(Aperture aperture);
    }
}

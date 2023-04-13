using project_backend.Models;
using project_backend.Schemas;

namespace project_backend.Interfaces
{
    public interface IAperture
    {
        Task<List<ApertureGet>> getAll();
        Task<Aperture> getApertureById(int id);
        Task<int> saveAperture(Aperture aperture);

        Task<int> updateAperture(Aperture aperture);

        Task<int> deleteAperture(int id);
    }
}

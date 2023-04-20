using project_backend.Models;

namespace project_backend.Interfaces
{
    public interface IDetailsCommand
    {
        public Task<List<DetailsComand>> GetAll();
        public Task<List<DetailsComand>> GetByCommandId(int idCommand);
        public Task<DetailsComand> GetByCommandIdAndDishId(int idCommand, string idDish);
        public Task<bool> CreateDetailCommand(DetailsComand detailsCommand);
        public Task<bool> DeleteDetailCommand(DetailsComand detailsCommand);
        public Task<bool> UpdateDetailCommand(DetailsComand detailsCommand);
    }
}

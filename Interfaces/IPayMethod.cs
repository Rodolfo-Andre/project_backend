using project_backend.Models;

namespace project_backend.Interfaces
{
    public interface IPayMethod
    {
        public Task<List<PayMethod>> GetAll();
        public Task<PayMethod> GetById(int id);
        public Task<bool> CreatePaymethod(PayMethod payMethod);
        public Task<bool> UpdatePaymethod(PayMethod payMethod);
        public Task<bool> DeletePaymethod(PayMethod payMethod);
    }
}

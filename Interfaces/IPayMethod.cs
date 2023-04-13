using project_backend.Models;
using project_backend.Schemas;

namespace project_backend.Interfaces
{
    public interface IPayMethod
    {

        Task<List<PayMethodGet>> getPayMethods();
        Task<PayMethod> getPayMethod(int  id);
        Task<bool> createPaymethod(PayMethod payMethod);
        Task<bool> updatePaymethod(PayMethod payMethod);
        Task<bool> deletePaymethod(PayMethod payMethod);
        
    }
}

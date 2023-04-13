using Mapster;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using project_backend.Data;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Schemas;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace project_backend.Services
{
    public class PaymethodService : IPayMethod
    {
        private readonly CommandsContext _comandsContext;


        public PaymethodService(CommandsContext paymethodContext)
        {
            _comandsContext = paymethodContext;
        }



        public async Task<bool> createPaymethod(PayMethod payMethod)
        {
           bool result = false;

            try {
                _comandsContext.PayMethods.Add(payMethod);

                await _comandsContext.SaveChangesAsync();

            }
            catch(Exception ex) {

                result = true;

                Console.WriteLine(ex);

            }
            return result;
        }
        public async Task<bool> updatePaymethod(PayMethod payMethod)
        {
            bool result=false;
            
            try
            {
                result = true;
                _comandsContext.Entry(payMethod).State = EntityState.Modified;
                await _comandsContext.SaveChangesAsync();
            }
            catch (Exception ex)

            {
              
               Console.WriteLine(ex);   
            }
            return result;
        }

        public async Task<bool> deletePaymethod(PayMethod payMethod)
        {
            bool result = false;
            try
            {
                _comandsContext.PayMethods.Remove(payMethod);
                await _comandsContext.SaveChangesAsync();
                result = true;
            }
            catch(Exception ex)
            {
                
                
                Console.WriteLine(ex);
            }

            return result;
          
        }

        public async Task<PayMethod> getPayMethod(int id)
        {

            var payMethod = await _comandsContext.PayMethods.FirstOrDefaultAsync(x => x.Id == id);

            return payMethod;

        }

        public async Task<List<PayMethodGet>> getPayMethods()
        {
            List<PayMethodGet> listPayMethod = await _comandsContext.PayMethods.Select(x => x.Adapt<PayMethodGet>()).ToListAsync();

            return listPayMethod;
        }



    }

}


using AspNetCore.Reporting;
using project_backend.Dto;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Utils;

namespace project_backend.Services
{
    public class ReportService : IReport
    {
        public async Task<byte[]> ReportVoucher(Voucher voucher)
        {
            LocalReport report = ReportUtils.GetReport("VoucherReport");

            List<PurchaseInformation> purchaseInformation = new();
            purchaseInformation.Add(new PurchaseInformation { PayMethod = voucher.VoucherDetails.First().PayMethod.Paymethod, IssueDate = voucher.DateIssued });

            List<OrderDetail> orderDetails = new();

            voucher.Commands.DetailsComand.ForEach(dc =>
            {
                orderDetails.Add(new OrderDetail
                {
                    Quantity = dc.CantDish,
                    Amount = dc.CantDish * dc.PrecOrder,
                    Price = dc.PrecOrder,
                    Description = dc.Dish.NameDish
                });
            });

            report.AddDataSource("PurchaseInformation", purchaseInformation);
            report.AddDataSource("OrderDetail", orderDetails);

            Dictionary<string, string> parameters = new();
            parameters.Add("NameEstablishment", voucher.Establishment.Name);
            parameters.Add("AddressEstablishment", voucher.Establishment.Address);
            parameters.Add("PhoneEstablishment", voucher.Establishment.Phone);
            parameters.Add("RucEstablishment", voucher.Establishment.Ruc);
            parameters.Add("IdVoucher", voucher.Id.ToString());
            parameters.Add("NameCustomer", voucher.Customer.FirstName + " " + voucher.Customer.LastName);
            parameters.Add("DniCustomer", voucher.Customer.Dni);
            parameters.Add("NameWaiter", voucher.Commands.Employee.FirstName + " " + voucher.Commands.Employee.LastName);
            parameters.Add("NameCashier", voucher.Employee.FirstName + " " + voucher.Employee.LastName);
            parameters.Add("NumberCash", voucher.CashId.ToString());
            parameters.Add("SubTotal", voucher.Commands.PrecTotOrder.ToString());
            parameters.Add("TaxableAmount", voucher.TaxableAmount.ToString());
            parameters.Add("Igv", voucher.Igv.ToString());
            parameters.Add("TotalDiscount", voucher.Discount.ToString());
            parameters.Add("Total", voucher.TotalPrice.ToString());

            var result = report.Execute(RenderType.Pdf, 1, parameters);

            return await Task.FromResult(result.MainStream.ToArray());
        }
    }
}

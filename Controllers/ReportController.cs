using Microsoft.AspNetCore.Mvc;
using project_backend.Interfaces;
using project_backend.Models;
using project_backend.Services;
using System.Net.Mime;

namespace project_backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IReport _reportService;
        private readonly IVoucher _voucherService;

        public ReportController(IReport reportService, IVoucher voucherService)
        {
            _reportService = reportService;
            _voucherService = voucherService;
        }

        [HttpGet("voucher/{id}")]
        public async Task<IActionResult> GetReportVoucher(int id)
        {
            var voucher = await _voucherService.GetById(id);

            if (voucher == null)
            {
                return NotFound("Comprobante de pago no encontrado");
            }

            var reportName = $"Comprobante_{voucher.Id}";
            var reportFileBytes = await _reportService.ReportVoucher(voucher);

            return File(reportFileBytes, MediaTypeNames.Application.Pdf, reportName + ".pdf");
        }
    }
}

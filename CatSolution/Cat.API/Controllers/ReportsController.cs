using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Cat.Infrastructure.Data;
using Cat.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cat.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Organizer")]
    public class ReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IReportGenerator _reportGenerator;

        public ReportsController(ApplicationDbContext context, IReportGenerator reportGenerator)
        {
            _context = context;
            _reportGenerator = reportGenerator;
        }

        [HttpGet("club-stats")]
        public async Task<IActionResult> GetClubStats()
        {
            var clubs = await _context.Clubs
                .Include(c => c.Cats)
                .Select(c => new {
                    c.Name,
                    c.GoldMedals,
                    c.SilverMedals,
                    c.BronzeMedals,
                    CatCount = c.Cats.Count
                })
                .ToListAsync();

            var report = _reportGenerator.GenerateReport(clubs);
            return File(report, "application/pdf", "club-stats.pdf");
        }
    }
}

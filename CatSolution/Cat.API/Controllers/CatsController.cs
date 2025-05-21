using Cat.Core.Entities;
using Cat.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cat.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CatsController : ControllerBase
    {
        private readonly IRepository<Core.Entities.Cat> _catRepository;
        private readonly IValidator<Core.Entities.Cat> _catValidator;

        public CatsController(IRepository<Core.Entities.Cat> catRepository, IValidator<Core.Entities.Cat> catValidator)
        {
            _catRepository = catRepository;
            _catValidator = catValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cats = await _catRepository.GetAllAsync();
            return Ok(cats);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cat = await _catRepository.GetByIdAsync(id);
            if (cat == null)
            {
                return NotFound();
            }
            return Ok(cat);
        }

        [HttpPost]
        [Authorize(Roles = "Organizer,ClubChairman")]
        public async Task<IActionResult> Create(Core.Entities.Cat cat)
        {
            var validationResult = await _catValidator.ValidateAsync(cat);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _catRepository.AddAsync(cat);
            return CreatedAtAction(nameof(GetById), new { id = cat.Id }, cat);
        }

        [HttpPut("{id}/disqualify")]
        [Authorize(Roles = "Organizer,ClubChairman,Expert")]
        public async Task<IActionResult> Disqualify(int id)
        {
            var cat = await _catRepository.GetByIdAsync(id);
            if (cat == null) return NotFound();

            cat.IsDisqualified = true;
            await _catRepository.UpdateAsync(cat);
            return NoContent();
        }
    }
}
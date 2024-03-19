using Microsoft.AspNetCore.Mvc;
using RegistrationIncomingLetters.Common.Models.Dto;
using RegistrationIncomingLetters.DataAccess.Services;

namespace RegistrationIncomingLetters.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LetterController : ControllerBase
    {
        private readonly LetterService _service;
        public LetterController(LetterService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] LetterCutDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] LetterCutDto dto)
        {
            await _service.Update(dto);
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(ulong id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}

using ask2.Models;
using ask2.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ask2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LetterController : ControllerBase
    {
        #region [fields]
        private readonly ILetterService _letterService;
        private readonly ILogger<LetterController> _logger;
        #endregion

        #region [constructor]
        public LetterController(ILogger<LetterController> logger, ILetterService letterService)
        {
            _logger = logger;
            _letterService = letterService;
        }
        #endregion

        #region [methods]
        // GET: api/<LetterController>
        [HttpGet]
        public async Task<ActionResult<List<Letter>>> Get()
        {
            return Ok(_letterService.GetLetters());
        }
        #endregion
    }
}

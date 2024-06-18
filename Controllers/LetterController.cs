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
        private ILetterService letterService;
        private readonly ILogger<LetterController> _logger;

        public LetterController(ILogger<LetterController> logger, ILetterService service)
        {
            _logger = logger;
            letterService = service;
        }

        // GET: api/<LetterController>
        [HttpGet]
        public IEnumerable<Letter> Get()
        {
            IEnumerable<Letter> list = letterService.ReadLetter();
            return list;
        }
    }
}

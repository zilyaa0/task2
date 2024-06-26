using ask2.Models;
using ask2.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ask2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LetterController : ControllerBase
    {
        #region fields
        private readonly ILetterService _letterService;
        private readonly ILogger<LetterController> _logger;
        #endregion

        #region constructor
        public LetterController(ILogger<LetterController> logger, ILetterService letterService)
        {
            _logger = logger;
            _letterService = letterService;
        }
        #endregion

        #region methods
        [HttpGet]
        public ActionResult<List<Letter>> GetLettersByPage(int page, int limit)
        {
            try
            {
                var lettersByPage = _letterService.GetLettersByPage(page, limit);
                if (lettersByPage.Count != 0)
                    return Ok(lettersByPage);
                else
                    return Ok("No letters in this page");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }

        }
        #endregion
    }
}

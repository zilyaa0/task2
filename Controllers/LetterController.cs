using ask2.Models;
using ask2.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ask2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LetterController : ControllerBase
    {
        #region fields
        private readonly ILetterService _letterService;
        private readonly IFileService _filesService;
        private readonly ILogger<LetterController> _logger;
        #endregion

        #region constructor
        public LetterController(ILogger<LetterController> logger, ILetterService letterService, IFileService fileService)
        {
            _logger = logger;
            _letterService = letterService;
            _filesService = fileService;
        }
        #endregion

        #region methods
        [HttpGet]
        public IActionResult GetLettersByPage(int page, int limit, string searchString)
        {
            if (page <= 0)
                return BadRequest(new { ErrorText = "Page должен быть больше 0" });
            if (limit <= 0)
                return BadRequest(new { ErrorText = "Limit должен быть больше 0" });

            var lettersByPage = _letterService.GetLetters(page, limit, searchString);
            return Ok(lettersByPage);

        }

        [HttpGet]
        public IActionResult GetAllLetters(int page, int limit)
        {
            if (page <= 0)
                return BadRequest(new { ErrorText = "Page должен быть больше 0" });
            if (limit <= 0)
                return BadRequest(new { ErrorText = "Limit должен быть больше 0" });

            var lettersByPage = _letterService.GetAllLetters(page, limit);
            return Ok(lettersByPage);

        }

        [HttpGet]
        public IActionResult GetFilesNames(string uniqueId)
        {
            var fileNames = _filesService.GetAllFiles(uniqueId);
            return Ok(fileNames);
        }
        #endregion
    }
}

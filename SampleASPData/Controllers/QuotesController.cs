using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleASPData.Data;
using SampleASPData.Models;

namespace SampleASPData.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotesController : ControllerBase
    {
        private readonly IQuote _quoteRepository;
        public QuotesController(IQuote quoteRepository)
        {
            _quoteRepository = quoteRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Quote>> Get()
        {
            return Ok(_quoteRepository.GetAll());
        }

    }
}
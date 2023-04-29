using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SampleASPData.Data;
using SampleASPData.DTO;
using SampleASPData.Models;

namespace SampleASPData.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotesController : ControllerBase
    {
        private readonly IQuote _quoteRepository;
        private readonly IMapper _mapper;

        public QuotesController(IQuote quoteRepository, IMapper mapper)
        {
            _quoteRepository = quoteRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<QuoteWithSamuraiDto>> Get()
        {
            var quote = _quoteRepository.GetAll();
            var quoteWithSamuraiDto = _mapper.Map<IEnumerable<QuoteWithSamuraiDto>>(quote);
            return Ok(quoteWithSamuraiDto);
        }

    }
}
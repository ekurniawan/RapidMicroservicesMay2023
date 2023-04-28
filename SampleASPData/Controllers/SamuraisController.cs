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
    public class SamuraisController : ControllerBase
    {
        private readonly ISamurai _samuraiRepository;
        public SamuraisController(ISamurai samuraiRepository)
        {
            _samuraiRepository = samuraiRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Samurai>> Get()
        {
            return Ok(_samuraiRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Samurai> Get(int id)
        {
            var samurai = _samuraiRepository.GetById(id);
            if (samurai == null)
            {
                return NotFound();
            }
            return Ok(samurai);
        }

        [HttpGet("byname")]
        public ActionResult<IEnumerable<Samurai>> GetByName(string name)
        {
            var samurai = _samuraiRepository.GetByName(name);
            if (samurai == null)
            {
                return NotFound();
            }
            return Ok(samurai);
        }
    }
}
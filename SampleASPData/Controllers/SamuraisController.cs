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

        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<Samurai> GetById(int id)
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

        [HttpPost]
        public ActionResult Post(Samurai samurai)
        {
            try
            {
                if (samurai == null)
                {
                    return BadRequest();
                }
                 _samuraiRepository.Add(samurai);
                return CreatedAtAction(nameof(GetById), new { id = samurai.Id }, samurai);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Samurai samurai)
        {
            try
            {
                _samuraiRepository.Update(id,samurai);
                return Ok($"Data with id {id} has been updated!");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
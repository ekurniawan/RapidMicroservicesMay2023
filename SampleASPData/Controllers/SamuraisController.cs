using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleASPData.Data;
using SampleASPData.DTO;
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
        public ActionResult<IEnumerable<SamuraiReadDto>> Get()
        {
            List<SamuraiReadDto> samuraiReadDto = new List<SamuraiReadDto>();
            var samurai = _samuraiRepository.GetAll();
            foreach (var item in samurai)
            {
                samuraiReadDto.Add(new SamuraiReadDto
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return Ok(samuraiReadDto);
        }

        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<SamuraiReadDto> GetById(int id)
        {
            var samurai = _samuraiRepository.GetById(id);
            if (samurai == null)
            {
                return NotFound();
            }
            SamuraiReadDto samuraiReadDto = new SamuraiReadDto
            {
                Id = samurai.Id,
                Name = samurai.Name
            };
            return Ok(samuraiReadDto);
        }

        [HttpGet("byname")]
        public ActionResult<IEnumerable<SamuraiReadDto>> GetByName(string name)
        {
            var samurai = _samuraiRepository.GetByName(name);
            if (samurai == null)
            {
                return NotFound();
            }
            List<SamuraiReadDto> samuraiReadDto = new List<SamuraiReadDto>();
            foreach (var item in samurai)
            {
                samuraiReadDto.Add(new SamuraiReadDto
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return Ok(samuraiReadDto);
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
                _samuraiRepository.Update(id, samurai);
                return Ok($"Data with id {id} has been updated!");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _samuraiRepository.Delete(id);
                return Ok($"Data with id {id} has been deleted!");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
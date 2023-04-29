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
    public class SamuraisController : ControllerBase
    {
        private readonly ISamurai _samuraiRepository;
        private readonly IMapper _mapper;

        public SamuraisController(ISamurai samuraiRepository,
        IMapper mapper)
        {
            _samuraiRepository = samuraiRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SamuraiReadDto>> Get()
        {
            var samurai = _samuraiRepository.GetAll();
            var samuraiReadDto = _mapper.Map<IEnumerable<SamuraiReadDto>>(samurai);
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
            var samuraiReadDto = _mapper.Map<SamuraiReadDto>(samurai);
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
            var samuraiReadDto = _mapper.Map<IEnumerable<SamuraiReadDto>>(samurai);
            return Ok(samuraiReadDto);
        }

        [HttpPost]
        public ActionResult Post(SamuraiInsertDto samuraiInsertDto)
        {
            try
            {
                if (samuraiInsertDto == null)
                {
                    return BadRequest();
                }
                var samurai = _mapper.Map<Samurai>(samuraiInsertDto);
                _samuraiRepository.Add(samurai);
                var samuraiReadDto = _mapper.Map<SamuraiReadDto>(samurai);
                return CreatedAtAction(nameof(GetById), new { id = samurai.Id }, samuraiReadDto);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, SamuraiInsertDto samuraiInsertDto)
        {
            try
            {
                var samurai = _mapper.Map<Samurai>(samuraiInsertDto);
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
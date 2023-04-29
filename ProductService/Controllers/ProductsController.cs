using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductService.Data;
using ProductService.Dtos;
using ProductService.Models;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private IProduct _productRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProduct productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductReadDto>> Get()
        {
            var products = _productRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(products));
        }

        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<ProductReadDto> GetById(int id)
        {
            var product = _productRepository.GetById(id);
            if (product != null)
            {
                return Ok(_mapper.Map<ProductReadDto>(product));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult Post(ProductUpdateDto productUpdateDto)
        {
            try
            {
                if (productUpdateDto == null)
                {
                    return BadRequest();
                }
                var product = _mapper.Map<Product>(productUpdateDto);
                _productRepository.Add(product);
                var productReadDto = _mapper.Map<ProductReadDto>(product);
                return CreatedAtAction(nameof(GetById),
                    new { id = productReadDto.ProductId }, productReadDto);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
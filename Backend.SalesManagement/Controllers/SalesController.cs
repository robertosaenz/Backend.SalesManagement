using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.SalesManagement.Models;
using Backend.SalesManagement.Services.Interfaces;
using Backend.SalesManagement.Validations;

namespace Backend.SalesManagement.Controllers
{
    [Route("api/sales")]
    [ApiController]
    public class SalesController : Controller
    {
        private readonly ISalesService _salesService;

        public SalesController(ISalesService salesService)
        {
            this._salesService = salesService;
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] Sales sales)
        {
            if (sales.IsValid(out IEnumerable<string> errors))
            {
                var result = await _salesService.Create(sales);

                return CreatedAtAction(
                    nameof(GetAllByUserAccountId),
                    new { id = result.UserAccountId }, result);
            }
            else
            {
                return BadRequest(errors);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllByUserAccountId(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                var result = _salesService.GetAllByUserAccountId(id);

                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("update")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] Sales sales)
        {
            if (sales.IsValid(out IEnumerable<string> errors))
            {
                var result = await _salesService.Update(sales);

                return Ok(result);
            }
            else
            {
                return BadRequest(errors);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var result = _salesService.GetAll();

            return Ok(result);
        }

        [HttpPost("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                var result = await _salesService.Delete(id);

                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

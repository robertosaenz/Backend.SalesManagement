using Backend.SalesManagement.Models;
using Backend.SalesManagement.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.SalesManagement.Validations;
using Microsoft.AspNetCore.Authorization;

namespace Backend.SalesManagement.Controllers
{
    [Route("api/sales")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;

        public SalesController(ISalesService salesService)
        {
            this._salesService = salesService;
        }

        [Authorize]
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

        [Authorize]
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

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var result = _salesService.GetAll();

            return Ok(result);
        }

        [Authorize]
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

        [Authorize]
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

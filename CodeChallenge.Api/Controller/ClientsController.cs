using CodeChallenge.Business.Services.Interfaces;
using CodeChallenge.Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Api.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Returns a Client record by DocNumber, city or seller code
        /// </summary>
        /// <param name="Doc"></param>
        /// <param name="City"></param>
        /// <param name="SellerCode"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get(string? Doc, string? City, string? SellerCode)
        {
            IEnumerable<BranchDto>? clients = null;

            if (Doc == null && City == null && SellerCode == null)
                clients = _clientService.GetAll();
            if (Doc != null) 
                clients = _clientService.GetById(Doc);
            if(City != null)
                clients = _clientService.GetByCity(City);
            if(SellerCode != null)
                clients = await _clientService.GetBySeller(SellerCode);
             
            return (clients == null || clients.Count() == 0) ? NotFound() : Ok(clients);
        }


        /// <summary>
        /// Updates a Client record
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="client"></param>
        /// <returns>No content</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update(string doc, BranchDto client)
        {
            if (client == null) return BadRequest("Client is null");

            await _clientService.Update(client, doc);
            return NoContent();
        }

        /// <summary>
        /// Delete a client record
        /// </summary>
        /// <param name="doc">Identification number</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(string doc)
        {
            var deleted = await _clientService.Delete(doc);
            if (deleted) return NoContent(); else return NotFound();
        }

        /// <summary>
        /// Creates a new Client record
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Client record</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create(BranchDto dto)
        {
            await _clientService.Create(dto);
            return CreatedAtAction(nameof(Create), dto);
        }
    }
}

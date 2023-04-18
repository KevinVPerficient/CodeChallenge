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
        /// <param name="Doc">Client doc number</param>
        /// <param name="City">Client city</param>
        /// <param name="SellerCode">Seller code in branch</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get(string? Doc, string? City, string? SellerCode)
        {
            IEnumerable<ClientDto>? clients = null;

            if (Doc is null && City is null && SellerCode is null)
                clients = _clientService.GetAll();
            if (Doc is not null) 
                clients = await _clientService.GetById(Doc);
            if(City is not null)
                clients = await _clientService.GetByCity(City);
            if(SellerCode is not null)
                clients = await _clientService.GetBySeller(SellerCode);
             
            return (clients == null || clients.Count() == 0) ? NotFound() : Ok(clients);
        }


        /// <summary>
        /// Updates a Client record
        /// </summary>
        /// <param name="Doc">Doc number</param>
        /// <param name="Client">Client Dto</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update(string Doc, ClientDto Client)
        {
            if (Client == null) return BadRequest("Client is null");

            await _clientService.Update(Client, Doc);
            return NoContent();
        }

        /// <summary>
        /// Delete a client record
        /// </summary>
        /// <param name="Doc">Identification number</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(string Doc)
        {
            var deleted = await _clientService.Delete(Doc);
            if (deleted) return NoContent(); else return NotFound();
        }

        /// <summary>
        /// Creates a new Client record
        /// </summary>
        /// <param name="Dto">Client Dto</param>
        /// <returns>Client record</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ClientDto),StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create(ClientDto Dto)
        {
            await _clientService.Create(Dto);
            return CreatedAtAction(nameof(Create), Dto);
        }
    }
}

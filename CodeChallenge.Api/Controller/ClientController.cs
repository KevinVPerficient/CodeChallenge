using CodeChallenge.Business.Services.Interfaces;
using CodeChallenge.Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Api.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Returns all Clients Records
        /// </summary>
        /// <returns>List of clientsDto</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<ClientDto> Get()
        {
            return _clientService.GetAll();
        }

        /// <summary>
        /// Returns a Client record by DocNumber
        /// </summary>
        /// <returns>clientDto</returns>
        [HttpGet("{doc}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByDoc(string doc)
        {
            var client = _clientService.GetById(doc);
            return client == null ? NotFound() : Ok(client);
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
        public async Task<IActionResult> Update(string doc, ClientDto client)
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
        public IActionResult Delete(string doc)
        {
            var deleted = _clientService.Delete(doc);
            if (deleted) return NoContent(); else return NotFound();
        }

        /// <summary>
        /// Creates a new Client record
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Client record</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(ClientDto dto)
        {
            await _clientService.Create(dto);
            return CreatedAtAction(nameof(Create), dto);
        }
    }
}

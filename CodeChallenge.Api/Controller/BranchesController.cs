﻿using CodeChallenge.Business.Services.Interfaces;
using CodeChallenge.Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Api.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchService _branchService;
        public BranchesController(IBranchService branchService)
        {
            _branchService = branchService;   
        }

        /// <summary>
        ///  Gets all or one branch record by its code, city or client doc number
        /// </summary>
        /// <param name="Code">Branch code</param>
        /// <param name="City">Branch city</param>
        /// <param name="ClientDoc">Client document number</param>
        /// <returns>List of BranchDto</returns>        
        [HttpGet]
        [ProducesResponseType(typeof(BranchDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get(string? Code, string? City, string? ClientDoc)
        {
            IEnumerable<BranchDto>? branches = null;
            if (Code is null && City is null && ClientDoc is null)
                branches = _branchService.GetAll();
            if (Code is not null)
                branches = await _branchService.GetById(Code);
            if (City is not null)
                branches = await _branchService.GetByCity(City);
            if (ClientDoc is not null)
                branches = await _branchService.GetByClientDoc(ClientDoc);

            return (branches == null || branches.Count() == 0) ? NotFound() : Ok(branches);
        }

        /// <summary>
        /// Updates a branch record
        /// </summary>
        /// <param name="Code">Branch code</param>
        /// <param name="branch">BranchDto</param>
        /// <param name="ClientDoc">Client document</param>
        /// <returns>No content</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update(string ClientDoc, string Code, BranchDto branch)
        {
            if (branch == null) return BadRequest("Branch is null");

            await _branchService.Update(branch, Code, ClientDoc);
            return NoContent();
        }

        /// <summary>
        /// Delete a branch record
        /// </summary>
        /// <param name="Code">Branch code</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(string Code, string ClientDoc)
        {
            var deleted = await _branchService.Delete(Code, ClientDoc);
            if (deleted) return NoContent(); else return NotFound();
        }

        /// <summary>
        /// Creates a new Branch record
        /// </summary>
        /// <param name="Dto">CLient Dto</param>
        /// <param name="Doc">Client document</param>
        /// <returns>Branch record</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ClientDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create(BranchDto Dto, string Doc)
        {
            await _branchService.Create(Dto, Doc);
            return CreatedAtAction(nameof(Create), Dto);
        }
    }
}

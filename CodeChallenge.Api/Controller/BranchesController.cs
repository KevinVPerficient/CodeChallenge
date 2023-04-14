using CodeChallenge.Business.Services.Interfaces;
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
        ///  Gets all or one branch record by its code or city
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="City"></param>
        /// <returns>List of BranchDto</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Get(string? Code, string? City)
        {
            IEnumerable<BranchDto>? branches = null;
            if (Code == null && City == null)
                branches = _branchService.GetAll();
            if (Code != null)
                branches = _branchService.GetById(Code);
            if (City != null)
                branches = _branchService.GetByCity(City);

            return (branches == null || branches.Count() == 0) ? NotFound() : Ok(branches);
        }

        /// <summary>
        /// Updates a branch record
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="branch"></param>
        /// <returns>No content</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update(string doc, BranchDto branch)
        {
            if (branch == null) return BadRequest("Branch is null");

            await _branchService.Update(branch, doc);
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
        public async Task<IActionResult> Delete(string Code)
        {
            var deleted = await _branchService.Delete(Code);
            if (deleted) return NoContent(); else return NotFound();
        }

        /// <summary>
        /// Creates a new Branch record
        /// </summary>
        /// <param name="Dto"></param>
        /// <returns>Branch record</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create(BranchDto Dto, string Doc)
        {
            await _branchService.Create(Dto);
            return CreatedAtAction(nameof(Create), Dto);
        }
    }
}

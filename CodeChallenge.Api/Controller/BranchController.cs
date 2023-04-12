using CodeChallenge.Business.Services;
using CodeChallenge.Business.Services.Interfaces;
using CodeChallenge.Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Api.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;
        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;   
        }

        /// <summary>
        /// Returns all Branches Records
        /// </summary>
        /// <returns>List of branchDto</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<BranchDto> Get()
        {
            return _branchService.GetAll();
        }

        /// <summary>
        /// Returns a Branch record by Code
        /// </summary>
        /// <returns>branchDto</returns>
        [HttpGet("{code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByCode(string code)
        {
            var branch = _branchService.GetById(code);
            return branch == null ? NotFound() : Ok(branch);
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
        public async Task<IActionResult> Update(string doc, BranchDto branch)
        {
            if (branch == null) return BadRequest("Branch is null");

            await _branchService.Update(branch, doc);
            return NoContent();
        }

        /// <summary>
        /// Delete a branch record
        /// </summary>
        /// <param name="doc">Identification number</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(string code)
        {
            var deleted = _branchService.Delete(code);
            if (deleted) return NoContent(); else return NotFound();
        }

        /// <summary>
        /// Creates a new Branch record
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Branch record</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Create(BranchDto dto)
        {
            _branchService.Create(dto);
            return CreatedAtAction(nameof(Create), dto);
        }
    }
}

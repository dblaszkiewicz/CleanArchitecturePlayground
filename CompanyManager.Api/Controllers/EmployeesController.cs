using CompanyManager.Application.Commands.Employees;
using CompanyManager.Application.Requests.Employees;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddEmployee([FromBody] CreateEmployeeRequest request)
        {
            if (request is null)
            {
                return BadRequest("Request body cannot be null.");
            }

            var command = new CreateEmployeeCommand { Gender = request.Gender, Surname = request.Surname };

            var employeeId = await _mediator.Send(command);

            return Ok(employeeId);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditEmployee([FromRoute] Guid id, [FromBody] UpdateEmployeeRequest request)
        {
            if (request is null)
            {
                return BadRequest("Request body cannot be null.");
            }

            var command = new UpdateEmployeeCommand { Id = id, Gender = request.Gender, Surname = request.Surname };

            await _mediator.Send(command);

            return Ok();
        }
    }
}
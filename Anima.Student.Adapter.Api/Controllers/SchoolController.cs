using System.Threading.Tasks;
using Anima.Student.Adapter.Api.Mappers;
using Anima.Student.Adapter.Api.Models;
using Anima.Student.Application.Commands.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Anima.Student.Adapter.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SchoolController : ControllerBase
    {

        private readonly IMediator _mediator;

        public SchoolController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Aluno")]
        public async Task<IActionResult> Aluno([FromForm] CriarAlunoModel model)
        {
            return Ok(_mediator.Send(model.MapToCommand()));
        }
        
        [HttpPost]
        [Route("Professor")]
        public async Task<IActionResult> Professor([FromForm] CriarProfessorModel model)
        {
            return Ok(_mediator.Send(model.MapToCommand()));
        }
        
        [HttpPost]
        [Route("Grade")]
        public async Task<IActionResult> Grade([FromForm] CriarGradeModel model)
        {
            return Ok(_mediator.Send(model.MapToCommand()));
        }
        
        [HttpPost]
        [Route("Matricula")]
        public async Task<IActionResult> Grade([FromForm] CriarMatriculaModel model)
        {
            return Ok(_mediator.Send(model.MapToCommand()));
        }
        
        [HttpDelete]
        [Route("Matricula")]
        public async Task<IActionResult> Grade([FromForm] DeletarMatriculaModel model)
        {
            return Ok(_mediator.Send(model.MapToCommand()));
        }
        
        [HttpGet]
        [Route("Grade/{codGrade}")]
        public async Task<IActionResult> Grade(int codGrade)
        {
            return Ok(_mediator.Send(new GetCurriculumByCodeCommandRequest(codGrade)));
        }
        
        [HttpGet]
        [Route("Professor/{cpf}")]
        public async Task<IActionResult> Professor(string cpf)
        {
            return Ok(_mediator.Send(new GetEmployeeByDocumentCommandRequest(cpf)));
        }
        
    }
}
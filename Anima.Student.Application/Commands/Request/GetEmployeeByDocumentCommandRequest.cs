using Anima.Student.Application.Commands.Response;
using MediatR;

namespace Anima.Student.Application.Commands.Request
{
    public class GetEmployeeByDocumentCommandRequest : IRequest<GetEmployeeByDocumentCommandResponse>, IRequest<GetEmployeeByDocumentCommandRequest>
    {
        public string Document { get; }

        public GetEmployeeByDocumentCommandRequest(string document)
        {
            Document = document;
        }
    }
}
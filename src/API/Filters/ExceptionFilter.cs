using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using API.RickAndMorty.Exceptions;
using API.RickAndMorty.Outputs;

namespace API.RickAndMorty.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var status = 500;
            if (context.Exception is ServiceException
                    || context.Exception.InnerException is ServiceException
                    )
            {
                status = 400;
            }
            else if (context.Exception is NotFoundException
                   || context.Exception.InnerException is NotFoundException
                   )
            {
                status = 404;
            }


            var mensagem = status == 400 || status == 404 ? (
                            context.Exception.InnerException == null ? context.Exception.Message
                            : context.Exception.InnerException.Message)
                                    : "Ocorreu uma falha inesperada no servidor. Entre em contato com o administrador";
            var response = context.HttpContext.Response;

            response.StatusCode = status;
            response.ContentType = "application/json";
            context.Result = new JsonResult(new DefaultOutput(false, mensagem));
        }
    }
}

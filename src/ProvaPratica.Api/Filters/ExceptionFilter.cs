using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProvaPratica.Communication.Responses;
using ProvaPratica.Exception.ExceptionsBase;


namespace ProvaPratica.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
          
            if (context.Exception is ProvaPraticaException)
            {
                HandleProjectException(context);
            }
            else
            {
                ThorwUnkowError(context);
            }
        }

        private void HandleProjectException(ExceptionContext context)
        {
          

            var ProvaPraticaException = (ProvaPraticaException)context.Exception;
            var errorResponse = new ResponseErrorJson(ProvaPraticaException.GetErrors());

            context.HttpContext.Response.StatusCode = ProvaPraticaException.StatusCode;
            context.Result = new ObjectResult(errorResponse);

        }

        private void ThorwUnkowError(ExceptionContext context)
        {
            var errorResponse = new ResponseErrorJson("Erro desconhecido");

            
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(errorResponse);


        }

    }
}

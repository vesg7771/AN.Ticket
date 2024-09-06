using AN.Ticket.Application.Exceptions;
using AN.Ticket.Domain.EntityValidations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace AN.Ticket.WebUI.Filters;

public class CustomExceptionFilter : IExceptionFilter
{
    private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

    public CustomExceptionFilter(
        ITempDataDictionaryFactory tempDataDictionaryFactory
    )
        => _tempDataDictionaryFactory = tempDataDictionaryFactory;

    public void OnException(ExceptionContext context)
    {
        var tempData = _tempDataDictionaryFactory.GetTempData(context.HttpContext);

        if (context.Exception is EntityValidationException ex)
        {
            tempData["ErrorMessage"] = $"{ex.Message}";
            context.ExceptionHandled = true;
        }
        else if (context.Exception is NotFoundException enfx)
        {
            tempData["ErrorMessage"] = $"{enfx.Message}";
            context.ExceptionHandled = true;
        }
        else
        {
            tempData["ErrorMessage"] = "Ocorreu um erro ao processar a solicitação. Verifique os dados e tente novamente";
            context.ExceptionHandled = true;
        }

        var controllerDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
        object model = null;

        if (controllerDescriptor != null && context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
        {
            var controller = context.HttpContext.RequestServices.GetService(controllerActionDescriptor.ControllerTypeInfo.AsType()) as Controller;
            if (controller != null)
            {
                model = controller.ViewData.Model;
            }
        }

        var result = new ViewResult
        {
            ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState)
            {
                Model = model
            }
        };

        context.Result = result;
    }
}

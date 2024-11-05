using AN.Ticket.Application.Exceptions;
using AN.Ticket.Domain.EntityValidations;
using AN.Ticket.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Diagnostics;

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
        var errorMessage = $"Ocorreu um erro ao processar a solicitação. Verifique os dados e tente novamente. {context.Exception.Message}";
        var statusCode = 500;

        if (context.Exception is EntityValidationException ex)
        {
            tempData["ErrorMessage"] = $"{ex.Message}";
            errorMessage = ex.Message;
            context.ExceptionHandled = true;
            statusCode = 502;
        }
        else if (context.Exception is NotFoundException enfx)
        {
            tempData["ErrorMessage"] = $"{enfx.Message}";
            errorMessage = enfx.Message;
            context.ExceptionHandled = true;
            statusCode = 404;
        }
        else
        {
            tempData["ErrorMessage"] = "Ocorreu um erro ao processar a solicitação. Verifique os dados e tente novamente";
            errorMessage = "Ocorreu um erro ao processar a solicitação. Verifique os dados e tente novamente";
            context.ExceptionHandled = true;
        }

        var errorViewModel = new ErrorViewModel
        {
            ErrorMessage = errorMessage,
            RequestId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier,
            StatusCode = statusCode
        };

        var result = new ViewResult
        {
            ViewName = "Error",
            ViewData = new ViewDataDictionary<ErrorViewModel>(
                new EmptyModelMetadataProvider(),
                new ModelStateDictionary())
            {
                Model = errorViewModel
            }
        };

        //var controllerDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
        //object model = null;

        //if (controllerDescriptor != null && context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
        //{
        //    var controller = context.HttpContext.RequestServices.GetService(controllerActionDescriptor.ControllerTypeInfo.AsType()) as Controller;
        //    if (controller != null)
        //    {
        //        model = controller.ViewData.Model;
        //    }
        //}

        //var result = new ViewResult
        //{
        //    ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState)
        //    {
        //        Model = model
        //    }
        //};

        context.Result = result;
    }
}

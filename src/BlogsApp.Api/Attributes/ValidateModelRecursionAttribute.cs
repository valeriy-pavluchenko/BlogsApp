using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;

using BlogsApp.Api.Extensions;

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BlogsApp.Api.Attributes
{
    /// <summary>
    /// Validate model attribute
    /// </summary>
    /// <remarks>
    /// TODO: Wot working for arrays 
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ValidateModelRecursionAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                var controller = (ControllerBase)actionContext.Controller;
                actionContext.Result = controller.BadRequest(GetValidationErrorResponse(actionContext));
            }
        }

        private Dictionary<string, object> GetValidationErrorResponse(ActionExecutingContext context)
        {
            var resultTotal = new Dictionary<string, object>();

            var actionParameters = context.ActionDescriptor.Parameters;
            var modelState = context.ModelState;

            foreach (var actionParameter in actionParameters)
            {
                var parameterType = actionParameter.ParameterType;
                var parameterTypeAttribute = (DataContractAttribute)parameterType.GetTypeInfo().GetCustomAttribute(typeof(DataContractAttribute));
                var parameterJsonName = parameterTypeAttribute?.Name ?? actionParameter.Name;

                var result = GetErrorsObject(context, parameterType, modelState);
                if (result.Count > 0)
                {
                    resultTotal.Add(parameterJsonName, result);
                }
            }

            return resultTotal;
        }

        private Dictionary<string, object> GetErrorsObject(ActionExecutingContext context, Type parameterType, ModelStateDictionary modelState, string namePrefix = "")
        {
            var result = new Dictionary<string, object>();

            foreach (var propertyInfo in parameterType.GetProperties())
            {
                var propertyTypeAttribute = (DataMemberAttribute)propertyInfo.GetCustomAttribute(typeof(DataMemberAttribute));
                var propertyJsonName = propertyTypeAttribute?.Name;
                
                if (propertyInfo.PropertyType.IsSimple())
                { 
                    ModelStateEntry modelStateEntry;
                    if ((modelState.TryGetValue(namePrefix + propertyInfo.Name, out modelStateEntry) || modelState.TryGetValue(propertyJsonName, out modelStateEntry))
                        && modelStateEntry.ValidationState == ModelValidationState.Invalid)
                    {
                        result.Add(propertyJsonName, GetErrorMessages(modelStateEntry));
                    }
                }
                else
                {
                    var errors = GetErrorsObject(context, propertyInfo.PropertyType, modelState, namePrefix + propertyInfo.Name + ".");
                    if (errors.Count > 0)
                    {
                        result.Add(propertyJsonName, errors);
                    }
                }
            }

            return result;
        }

        private string[] GetErrorMessages(ModelStateEntry modelStateEntry)
        {
            var errors = modelStateEntry.Errors
                .Where(x => x.ErrorMessage != string.Empty)
                .Select(x => x.ErrorMessage)
                .ToArray();

            if (errors.Length == 0)
            {
                // ErrorMessage equals to empty string when exception was encountered while parsing value
                errors = modelStateEntry.Errors
                    .Select(x => x.Exception.Message)
                    .ToArray();

                if (errors.Length == 0)
                {
                    // TODO: Log this case, something went wrong
                    throw new ValidationException();
                }
            }

            return errors;
        }
    }
}
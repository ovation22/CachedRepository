using System.Collections.Generic;
using Example.API.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;

namespace Example.API.Tests.Attributes.ValidateModelAttributeTests
{
    public class OnActionExecuting
    {
        [Fact]
        public void ItReturnsBadObjectResult()
        {
            // Arrange
            var validateModelAttribute = new ValidateModelAttribute();
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("name", "invalid");

            var actionContext = new ActionContext(
                new Mock<HttpContext>().Object,
                new Mock<RouteData>().Object,
                new Mock<ActionDescriptor>().Object,
                modelState);

            var actionExcecutingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                new Mock<Controller>());

            // Act
            validateModelAttribute.OnActionExecuting(actionExcecutingContext);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionExcecutingContext.Result);
        }
    }
}

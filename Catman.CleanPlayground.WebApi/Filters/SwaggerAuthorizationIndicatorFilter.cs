namespace Catman.CleanPlayground.WebApi.Filters
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    internal class SwaggerAuthorizationIndicatorFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var actionMetadata = context.ApiDescription.ActionDescriptor.EndpointMetadata;
            var isAuthorized = actionMetadata.Any(metadataItem => metadataItem is AuthorizeAttribute);
            var allowAnonymous = actionMetadata.Any(metadataItem => metadataItem is AllowAnonymousAttribute);

            // authorization is not required
            if (!isAuthorized || allowAnonymous)
            {
                return;
            }

            var securityScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            };

            var scopeNames = Array.Empty<string>();

            var securityRequirement = new OpenApiSecurityRequirement {{securityScheme, scopeNames}};
            operation.Security.Add(securityRequirement);
        }
    }
}

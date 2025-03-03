using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Any;

namespace Employee_Monitoring_System_API
{
    public class SwaggerFileOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var fileParams = context.MethodInfo.GetParameters()
                .Where(p => p.ParameterType == typeof(IFormFile) ||
                            p.ParameterType == typeof(IEnumerable<IFormFile>) ||
                            p.ParameterType == typeof(List<IFormFile>));

            if (fileParams.Any())
            {
                operation.RequestBody = new OpenApiRequestBody
                {
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["multipart/form-data"] = new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = "object",
                                Properties = fileParams.ToDictionary(
                                    param => param.Name ?? string.Empty,
                                    param => new OpenApiSchema
                                    {
                                        Type = "string",
                                        Format = "binary"
                                    }
                                ),
                                Required = new HashSet<string>(fileParams.Select(p => p.Name ?? string.Empty))
                            }
                        }
                    }
                };

                // Ensure the operation consumes multipart/form-data
                if (operation.Extensions == null)
                {
                    operation.Extensions = new Dictionary<string, IOpenApiExtension>();
                }
                operation.Extensions.Add("x-ms-blob-content-type", new OpenApiString("multipart/form-data"));
            }
        }
    }
}

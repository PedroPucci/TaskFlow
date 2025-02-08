using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TaskFlow.Extensions.SwaggerDocumentation
{
    public class CustomOperationDescriptions : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context?.ApiDescription?.HttpMethod is null || context.ApiDescription.RelativePath is null)
                return;

            var routeHandlers = new Dictionary<string, Action>
                {
                    { "user", () => HandleUserOperations(operation, context) }
                };

            foreach (var routeHandler in routeHandlers)
            {
                if (context.ApiDescription.RelativePath.Contains(routeHandler.Key))
                {
                    routeHandler.Value.Invoke();
                    break;
                }
            }
        }

        private void HandleUserOperations(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.HttpMethod == "POST")
            {
                operation.Summary = "Create a new user";
                operation.Description = "This endpoint allows you to create a new user by providing the necessary details.";
                AddResponses(operation, "200", "The user was successfully created.");
            }
            else if (context.ApiDescription.HttpMethod == "PUT")
            {
                operation.Summary = "Update an existing user";
                operation.Description = "This endpoint allows you to update an existing user by providing the necessary details.";
                AddResponses(operation, "200", "The user was successfully updated.");
            }
            else if (context.ApiDescription.HttpMethod == "DELETE")
            {
                operation.Summary = "Delete an existing account user";
                operation.Description = "This endpoint allows you to delete an existing user by providing the account ID.";
                AddResponses(operation, "200", "The user was successfully deleted.");
                AddResponses(operation, "404", "user not found. Please verify the account ID.");
            }
            else if (context.ApiDescription.HttpMethod == "GET" &&
                    context.ApiDescription.RelativePath != null &&
                    context.ApiDescription.RelativePath.Contains("All"))
            {
                operation.Summary = "Retrieve all users";
                operation.Description = "This endpoint allows you to retrieve details of all existing users.";
                AddResponses(operation, "200", "All user details were successfully retrieved.");
            }
            else if (context.ApiDescription.HttpMethod == "GET" &&
                     context.ApiDescription.RelativePath?.Contains("Balance for accountUser") == true)
            {
                operation.Summary = "Retrieve balance for an user";
                operation.Description = "This endpoint allows you to retrieve the balance of an user by providing the account ID.";
                AddResponses(operation, "200", "The user's balance was successfully retrieved.");
            }
        }

        private void AddResponses(OpenApiOperation operation, string statusCode, string description)
        {
            if (!operation.Responses.ContainsKey(statusCode))
            {
                operation.Responses.Add(statusCode, new OpenApiResponse { Description = description });
            }
        }
    }
}
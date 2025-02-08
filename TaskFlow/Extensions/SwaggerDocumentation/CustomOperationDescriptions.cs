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
                    { "user", () => HandleUserOperations(operation, context) },
                    { "task", () => HandleTaskOperations(operation, context) }
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
                operation.Summary = "Delete an existing user";
                operation.Description = "This endpoint allows you to delete an existing user by providing the user ID.";
                AddResponses(operation, "200", "The user was successfully deleted.");
                AddResponses(operation, "404", "user not found. Please verify the user ID.");
            }
            else if (context.ApiDescription.HttpMethod == "GET" &&
                    context.ApiDescription.RelativePath != null &&
                    context.ApiDescription.RelativePath.Contains("All"))
            {
                operation.Summary = "Retrieve all users";
                operation.Description = "This endpoint allows you to retrieve details of all existing users.";
                AddResponses(operation, "200", "All user details were successfully retrieved.");
            }
            else if (context.ApiDescription.HttpMethod == "GET")
            {
                operation.Summary = "Retrieve user";
                operation.Description = "This endpoint allows you to retrieve the user by providing the user ID.";
                AddResponses(operation, "200", "The user's was successfully retrieved.");
            }
        }

        private void HandleTaskOperations(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.HttpMethod == "POST")
            {
                operation.Summary = "Create a new task";
                operation.Description = "This endpoint allows you to create a new task by providing the necessary details.";
                AddResponses(operation, "200", "The task was successfully created.");
            }
            else if (context.ApiDescription.HttpMethod == "PUT")
            {
                operation.Summary = "Update an existing task";
                operation.Description = "This endpoint allows you to update an existing task by providing the necessary details.";
                AddResponses(operation, "200", "The task was successfully updated.");
            }
            else if (context.ApiDescription.HttpMethod == "DELETE")
            {
                operation.Summary = "Delete an existing task";
                operation.Description = "This endpoint allows you to delete an existing task by providing the task ID.";
                AddResponses(operation, "200", "The task was successfully deleted.");
                AddResponses(operation, "404", "Task not found. Please verify the task ID.");
            }
            else if (context.ApiDescription.HttpMethod == "GET" &&
                     context.ApiDescription.RelativePath != null &&
                     context.ApiDescription.RelativePath.Contains("GetTasksWithUserAsync", StringComparison.OrdinalIgnoreCase))
            {
                operation.Summary = "Retrieve all tasks with user information";
                operation.Description = "This endpoint retrieves all tasks along with the associated user details.";
                AddResponses(operation, "200", "All task details with user information were successfully retrieved.");
            }
            else if (context.ApiDescription.HttpMethod == "GET" &&
                     context.ApiDescription.RelativePath != null &&
                     context.ApiDescription.RelativePath.Contains("GetTasksByUserAsync", StringComparison.OrdinalIgnoreCase))
            {
                operation.Summary = "Retrieve tasks for a specific user";
                operation.Description = "This endpoint retrieves all tasks assigned to a specific user, identified by their User ID.";
                AddResponses(operation, "200", "Tasks for the user were successfully retrieved.");
                AddResponses(operation, "404", "No tasks found for the specified user.");
            }
            else if (context.ApiDescription.HttpMethod == "GET" &&
                     context.ApiDescription.RelativePath != null &&
                     context.ApiDescription.RelativePath.Contains("All"))
            {
                operation.Summary = "Retrieve all tasks";
                operation.Description = "This endpoint allows you to retrieve details of all existing tasks.";
                AddResponses(operation, "200", "All task details were successfully retrieved.");
            }
            else if (context.ApiDescription.HttpMethod == "GET")
            {
                operation.Summary = "Retrieve task";
                operation.Description = "This endpoint allows you to retrieve a specific task by its ID.";
                AddResponses(operation, "200", "The task was successfully retrieved.");
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
namespace TaskFlow.Shared.Logging
{
    public static class LogMessages
    {        
        //Users
        public static string InvalidUserInputs() => "Message: Invalid inputs to User.";
        public static string NullOrEmptyUserName() => "Message: The Name field is null, empty, or whitespace.";
        public static string UpdatingErrorUser(Exception ex) => $"Message: Error updating User: {ex.Message}";
        public static string UpdatingSuccessUser() => "Message: Successfully updated User.";
        public static string UserNotFound(string action) => $"Message: User not found for {action} action.";
        public static string AddingUserError(Exception ex) => $"Message: Error adding a new User: {ex.Message}";
        public static string AddingUserSuccess() => "Message: Successfully added a new User.";
        public static string DeleteUserError(Exception ex) => $"Message: Error to delete a User: {ex.Message}";
        public static string DeleteUserSuccess() => "Message: Delete with success User.";
        public static string GetAllUsersError(Exception ex) => $"Message: Error to loading the list User: {ex.Message}";
        public static string GetAllUsersSuccess() => "Message: GetAll with success User.";
        //Tasks
        public static string InvalidTaskInputs() => "Message: Invalid inputs to Task.";
        public static string GetAllTasksError(Exception ex) => $"Message: Error to loading the list Task: {ex.Message}";
        public static string GetAllTasksSuccess() => "Message: GetAll with success Task.";
        public static string UpdatingErrorTask(Exception ex) => $"Message: Error updating Task: {ex.Message}";
        public static string UpdatingSuccessTask() => "Message: Successfully updated Task.";
        public static string DeleteTaskError(Exception ex) => $"Message: Error to delete a Task: {ex.Message}";
        public static string DeleteTaskSuccess() => "Message: Delete with success Task.";
    }
}
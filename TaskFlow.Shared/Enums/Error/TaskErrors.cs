using System.ComponentModel;

namespace TaskFlow.Shared.Enums.Error
{
    public enum TaskErrors
    {
        [Description("'Title' can not be null or empty!")]
        Task_Error_TitleCanNotBeNullOrEmpty,

        [Description("'Title' can not be less 4 letters!")]
        Task_Error_TitleLenghtLessFour,

        [Description("'Description' can not be null or empty!")]
        Task_Error_DescriptionCanNotBeNullOrEmpty,

        [Description("'Description' can not be less 4 letters!")]
        Task_Error_DescriptionLenghtLessFour,
    }
}
using System.ComponentModel;

namespace TaskFlow.Shared.Enums.Error
{
    public enum CategoryErrors
    {
        [Description("'Name' can not be null or empty!")]
        Category_Error_NameCanNotBeNullOrEmpty,

        [Description("'Name' can not be less 4 letters!")]
        Category_Error_NameLenghtLessFour,
    }
}
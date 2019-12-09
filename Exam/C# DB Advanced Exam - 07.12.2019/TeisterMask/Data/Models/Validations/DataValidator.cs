namespace TeisterMask.Data.Models.Validations
{
    public static class DataValidator
    {
        public static class Employee
        {
            public const int MaxUsernameLenght = 40;
            public const int MinUsernameLenght = 3;
            public const string UsernamePattern = @"^[a-zA-Z0-9]+$";

            public const string PhonePattern = @"^[0-9]{3}[-]{1}[0-9]{3}[-]{1}[0-9]{4}$";
        }

        public static class Project
        {
            public const int MaxNameLenght = 40;
            public const int MinNameLenght = 2;
        }

        public static class Task
        {
            public const int MaxNameLenght = 40;
            public const int MinNameLenght = 2;
        }
    }
}

namespace MusicHub.Data.Models.Validations
{
    public static class DataValidator
    {
        public class Song
        {
            public const int NameMaxLength = 20;
            public const int NameMinLength = 3;

            public const double MinPriceValue = 0;
        }

        public class Album
        {
            public const int NameMaxLength = 40;
            public const int NameMinLength = 3;
        }

        public class Performer
        {
            public const int NameMaxLength = 20;
            public const int NameMinLength = 3;

            public const int MaxAgeValue = 70;
            public const int MinAgeValue = 18;

            public const double MinNetWorthValue = 0; 
        }

        public class Producer
        {
            public const int NameMaxLength = 30;
            public const int NameMinLength = 3;

            public const string PseudonymPattern = @"^[A-Z]{1}[a-z]+[ ][A-Z]{1}[a-z]+\b$";

            public const string PhoneNumberPattern = @"^[+]{1}[3]{1}[5]{1}[9]{1}[ ]{1}[0-9]{3}[ ]{1}[0-9]{3}[ ]{1}[0-9]{3}\b";
        }

        public class Writer
        {
            public const int NameMaxLength = 20;
            public const int NameMinLength = 3;

            public const string PseudonymPattern = @"^[A-Z]{1}[a-z]+[ ][A-Z]{1}[a-z]+\b$";
        }
    }
}

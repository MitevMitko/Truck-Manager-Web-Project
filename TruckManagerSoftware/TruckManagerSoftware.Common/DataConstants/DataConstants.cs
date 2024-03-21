namespace TruckManagerSoftware.Common.DataConstants
{
    public static class DataConstants
    {
        public static class Garage
        {
            // Country
            public const int CountryMinLength = 5;
            public const int CountryMaxLength = 50;

            // City
            public const int CityMinLength = 5;
            public const int CityMaxLength = 50;

            // Size
            public const int SizeMinLength = 3;
            public const int SizeMaxLength = 30;
        }

        public static class Truck
        {
            // Brand
            public const int BrandMinLength = 3;
            public const int BrandMaxLength = 30;

            // Series
            public const int SeriesMinLength = 3;
            public const int SeriesMaxLength = 30;

            // Image
            public const int ImageMinLength = 3;
            public const int ImageMaxLength = 50;
        }

        public static class Trailer
        {
            // Title
            public const int TitleMinLength = 3;
            public const int TitleMaxLength = 30;

            // Series
            public const int SeriesMinLength = 3;
            public const int SeriesMaxLength = 30;

            // Trailer Type
            public const int TrailerTypeMinLength = 3;
            public const int TrailerTypeMaxLength = 30;

            // Body Type
            public const int BodyTypeMinLength = 3;
            public const int BodyTypeMaxLength = 30;

            // Cargo Types
            public const int CargoTypesMinLength = 3;
            public const int CargoTypesMaxLength = 70;

            // Image
            public const int ImageMinLength = 3;
            public const int ImageMaxLength = 50;
        }

        public static class Order
        {
            // Cargo
            public const int CargoMinLength = 3;
            public const int CargoMaxLength = 30;

            // Start Point
            public const int StartPointMinLength = 3;
            public const int StartPointMaxLength = 30;

            // End Point
            public const int EndPointMinLength = 3;
            public const int EndPointMaxLength = 30;

            // Delivery Type
            public const int DeliveryTypeMinLength = 3;
            public const int DeliveryTypeMaxLength = 30;
        }

        public static class BankContact
        {
            // Name
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;

            // Email
            public const int EmailMinLength = 3;
            public const int EmailMaxLength = 30;

            // Phone Number
            public const int PhoneNumberMinLength = 10;
            public const int PhoneNumberMaxLength = 20;
        }

        public static class Engine
        {
            // Title
            public const int TitleMinLength = 3;
            public const int TitleMaxLength = 30;
        }

        public static class Transmission
        {
            // Title
            public const int TitleMinLength = 3;
            public const int TitleMaxLength = 30;
        }

        public static class User
        {
            // Status
            public const int StatusMinLength = 3;
            public const int StatusMaxLength = 30;

            // Avatar
            public const int AvatarMinLength = 3;
            public const int AvatarMaxLength = 50;
        }
    }
}

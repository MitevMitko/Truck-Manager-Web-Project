namespace TruckManagerSoftware.Common.DataConstants
{
    using static TruckManagerSoftware.Common.DataConstants.DataConstants;

    public static class DataConstants
    {
        public static class Admin
        {
            // Role Name
            public const string AdminRoleName = "Administrator";

            // Development Email
            public const string AdminDevelopmentEmail = "administrator@mail.com";

            // Area Name
            public const string AdminAreaName = "Administrator";

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

        public static class Garage
        {
            // Country
            public const int CountryMinLength = 5;
            public const int CountryMaxLength = 50;

            // City
            public const int CityMinLength = 3;
            public const int CityMaxLength = 50;

            // Size
            public const int SizeMinLength = 3;
            public const int SizeMaxLength = 30;
        }

        public static class Image
        {
            // Width
            public const int ImageWidth = 1200;

            // Height
            public const int ImageHeight = 600;

            // User's Avatars Path
            public const string UsersAvatarsPath = "wwwroot\\Images\\Avatars";

            // Truck's Images Path
            public const string TrucksImagesPath = "wwwroot\\Images\\Trucks";

            // Trailer's Images Path
            public const string TrailersImagesPath = "wwwroot\\Images\\Trailers";
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

            // Trip Time
            public const int TripTimeMinLength = 3;
            public const int TripTimeMaxLength = 20;

            // RegEx Expression For Validation Of Trip Time
            public const string TripTimeRegExExpression = @"^\d+h\s\d+m$";
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

        public static class Transmission
        {
            // Title
            public const int TitleMinLength = 3;
            public const int TitleMaxLength = 30;
        }

        public static class Truck
        {
            // Brand
            public const int BrandMinLength = 3;
            public const int BrandMaxLength = 30;

            // Series
            public const int SeriesMinLength = 1;
            public const int SeriesMaxLength = 30;

            // Image
            public const int ImageMinLength = 3;
            public const int ImageMaxLength = 50;
        }

        public static class Unauthorized
        {
            // Area Name
            public const string UnauthorizedAreaName = "Unauthorized";
        }

        public static class User
        {
            // UserName
            public const int UserNameMinLength = 5;
            public const int UserNameMaxLength = 20;

            // Email
            public const int EmailMinLength = 10;
            public const int EmailMaxLength = 60;

            // Password
            public const int PasswordMinLength = 5;
            public const int PasswordMaxLength = 20;

            // Status
            public const int StatusMinLength = 3;
            public const int StatusMaxLength = 30;

            // Avatar
            public const int AvatarMinLength = 3;
            public const int AvatarMaxLength = 50;

            // Status Value When Register User
            public const string StatusValueWhenRegisterUser = "roaming";

            // Role Name
            public const string UserRoleName = "User";

            // Development Email
            public const string UserDevelopmentEmail = "user@mail.com";

            // Area Name
            public const string UserAreaName = "User";
        }
    }
}

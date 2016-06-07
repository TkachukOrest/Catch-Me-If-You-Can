namespace CatchMe.Repositories.Sql
{
    public class SpParams
    {
        public class TripCommon
        {
            public const string TripId = "@TripId";
        }

        public class SaveTrip
        {
            public const string TripId = "@TripId";
            public const string UserId = "@UserId";
            public const string Seats = "@Seats";
            public const string Price = "@Price";
            public const string StaticMapUrl = "@StaticMapUrl";
            public const string StartDateTime = "@StartDateTime";
            public const string Manufacturer = "@Manufacturer";
            public const string Model = "@Model";
            public const string Color = "@Color";
            public const string Year = "@Year";
        }

        public class AddPassenger
        {
            public const string TripId = "@TripId";
            public const string PassengerId = "@PassengerId";
            public const string BookedSeats = "@BookedSeats";
        }

        public class AddMapPoint
        {
            public const string TripId = "@TripId";
            public const string Latitude = "@Latitude";
            public const string Longitude = "@Longitude";
            public const string FormattedLongAddress = "@FormattedLongAddress";
            public const string FormattedShortAddress = "@FormattedShortAddress";
            public const string Sequence = "@Sequence";
            public const string City = "@City";
            public const string District = "@District";
            public const string Region = "@Region";
            public const string Country = "@Country";
            public const string StreetName = "@StreetName";
            public const string StreetNumber = "@StreetNumber";
        }

        public class RoleCommon
        {
            public const string RoleId = "@RoleId";
            public const string RoleName = "@RoleName";
        }

        public class UserCommon
        {
            public const string UserId = "@UserId";
            public const string UserName = "@UserName";
            public const string UserEmail = "@UserEmail";
        }

        public class SaveUser
        {
            public const string UserId = "@UserId";
            public const string Email = "@Email";                                     
            public const string EmailConfirmed = "@EmailConfirmed";
            public const string PasswordHash = "@PasswordHash";
            public const string SecurityStamp = "@SecurityStamp";
            public const string UserName = "@UserName";
            public const string CreationTime = "@CreationTime";
            public const string FirstName = "@FirstName";
            public const string LastName = "@LastName";
            public const string PhoneNumber = "@PhoneNumber";
        }                                                      

    }
}

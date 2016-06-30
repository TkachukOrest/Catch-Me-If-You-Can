namespace CatchMe.Repositories.Sql.Concrete
{
    class SpNames
    {
        #region Trips

        public const string AddTrip = "AddTrip";

        public const string AddMapPoint = "AddMapPoint";

        public const string AddPassenger = "AddPassenger";
        
        public const string GetTrips = "GetTrips";

        public const string GetTripById = "GetTripById";

        public const string GetMapPointsByTripId = "GetMapPointsByTripId";

        public const string DeleteTripById = "DeleteTripById";

        public const string UpdateTripById = "UpdateTripById";

        #endregion

        #region Role

        public const string AddRole = "AddRole";

        public const string DeleteRoleById = "DeleteRoleById";

        public const string UpdateRoleById = "UpdateRoleById";

        public const string FindRoleById = "FindRoleById";

        public const string FindRoleByName = "FindRoleByName";

        public const string GetRoles = "GetRoles";

        #endregion

        #region User 

        public const string AddUser = "AddUser";

        public const string UpdateUserById = "UpdateUserById";

        public const string DeleteUserById = "DeleteUserById";

        public const string GetUsers = "GetUsers";

        public const string FindUserById = "FindUserById";

        public const string FindUserByName = "FindUserByName";

        public const string FindUserByEmail = "FindUserByEmail";

        public const string AddUserToRole = "AddUserToRole";

        public const string RemoveUserFromRole = "RemoveUserFromRole";

        public const string GetUserRoles = "GetUserRoles";

        public const string IsUserInRole = "IsUserInRole";

        #endregion
    }
}

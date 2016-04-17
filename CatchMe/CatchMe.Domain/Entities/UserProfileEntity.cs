using System;

namespace CatchMe.Domain.Entities
{
    public class UserProfileEntity
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }        

        public string LastName { get; set; }
    }
}

namespace CatchMe.Domain.Entities
{
    public class UserProfileEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string FirstName { get; set; }        

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
    }
}

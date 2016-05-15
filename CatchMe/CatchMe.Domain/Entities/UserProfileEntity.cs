namespace CatchMe.Domain.Entities
{
    public class UserProfileEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string FirstName { get; set; }        

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public override string ToString()
        {
            return string.Format("First name: {0},\n Last name: {1},\n Phone number: {2}\n",
                FirstName,
                LastName,
                PhoneNumber);
        }
    }
}

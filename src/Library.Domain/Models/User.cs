namespace Library.Domain.Models
{
    public class User : Model
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public UserRole Role { get; set; }
        public Address Address { get; set; }
    }

    public enum UserRole
    {
        student = 0,
        citizen = 1,
        author = 2,
        publishingHouseOwner = 3,
        LibraryEmployee = 4,
    }
}

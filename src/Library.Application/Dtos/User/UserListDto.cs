using Library.Domain.Models;

namespace Library.Application.Dtos
{
    public sealed class UserListDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public bool isAdult { get; set; }
        public UserRole Role { get; set; }

        public static UserListDto CreateFromUser(User user)
        {
            return new UserListDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                isAdult = user.BirthDate.AddYears(18) <= DateTime.Today,
                Role = user.Role,
            };
        }
    }
}

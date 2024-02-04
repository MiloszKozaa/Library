using Library.Domain.Models;

namespace Library.Application.Dtos
{
    public sealed class UserDetailDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }    
        public bool isAdult { get; set; }
        public UserRole Role { get; set; }
        public AddressDto Address { get; set; }

        public static UserDetailDto CreateFromUser(User user)
        {
            return new UserDetailDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                BirthDate = user.BirthDate,
                isAdult = user.BirthDate.AddYears(18) <= DateTime.Today,
                Role = user.Role,
                Address = AddressDto.CreateFromAddress(user.Address),
            };
        }
    }
}

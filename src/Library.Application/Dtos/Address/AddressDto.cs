using Library.Domain.Models;

namespace Library.Application.Dtos
{
    public sealed class AddressDto
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string? FlatNumber { get; set; }

        public static AddressDto CreateFromAddress(Address address)
        {
            return new AddressDto
            {
                Id = address.Id,
                Country = address.Country,
                State = address.State,
                City = address.City,
                PostCode = address.PostCode,
                StreetName = address.StreetName,
                StreetNumber = address.StreetNumber,
                FlatNumber = address.FlatNumber
            };
        }
    }


}

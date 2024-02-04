namespace Library.Domain.Models
{
    public class Address : Model
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostCode {  get; set; }
        public string StreetName {  get; set; }
        public string StreetNumber {  get; set; }
        public string? FlatNumber { get; set; }
    }
}

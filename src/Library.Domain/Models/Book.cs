namespace Library.Domain.Models
{
    public class Book : Model
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public string Note {  get; set; }
        public PublishingHouse PublishingHouse { get; set; }
    }
}

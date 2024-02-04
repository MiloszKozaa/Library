namespace Library.Domain.Models
{
    public class PublishingHouse : Model
    {
        public string Name { get; set; }    
        public string Description { get; set; }
        public DateOnly CreationDate { get; set; }
        public User Owner { get; set; }
    }
}

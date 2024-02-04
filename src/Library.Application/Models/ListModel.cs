namespace Library.Application.Models
{
    public sealed class ListModel<TModel>
    {
        public IEnumerable<TModel> Items { get; set; }

        public static ListModel<TModel> Create(IEnumerable<TModel> items)
        {
            return new ListModel<TModel> { Items = items };
        }
    }
}

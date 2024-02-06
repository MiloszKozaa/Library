namespace Library.Application.Validators
{
    public sealed class Validation
    {
        public Validation() { }
        public static bool IsValidType<TModel>(TModel? model)
        {
            if(model == null)
            {
                return false;
            }

            bool isValid = model.GetType() == typeof(TModel);

            return isValid;
        }
    }
}

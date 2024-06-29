namespace ProductAPI.VSA.Common.Exception
{
    public class ModelNullException : ArgumentNullException
    {
        public ModelNullException(string? paramName, string? message) : base(paramName, message)
        {

        }
    }
}

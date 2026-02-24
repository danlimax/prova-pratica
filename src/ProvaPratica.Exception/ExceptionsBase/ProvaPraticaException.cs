namespace ProvaPratica.Exception.ExceptionsBase
{
    public abstract class ProvaPraticaException : SystemException
    {
        public ProvaPraticaException(string message) : base(message)
        {
            
        }

        public abstract int StatusCode { get; }
        public abstract List<string> GetErrors();
    }
}

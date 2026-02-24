using System.Net;

namespace ProvaPratica.Exception.ExceptionsBase
{
    public class NotFoundException : ProvaPraticaException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public override int StatusCode => (int)HttpStatusCode.NotFound;

        public override List<string> GetErrors()
        {
            return
            [
                Message
            ];
        }
    }
}

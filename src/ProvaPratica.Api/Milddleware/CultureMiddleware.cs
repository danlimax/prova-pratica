using System.Globalization;

namespace ProvaPratica.Api.Milddleware
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;
        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext contex)
        {
           
            var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

            var requestedCulture = contex.Request.Headers.AcceptLanguage.FirstOrDefault();




            var cultureInfo = new CultureInfo("en");

            if (string.IsNullOrWhiteSpace(requestedCulture) == false
                && supportedLanguages.Exists(lang => lang.Name.Equals(requestedCulture))
                )
            {
                cultureInfo = new CultureInfo(requestedCulture);
            }

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            await _next(contex);
        }
    }
}

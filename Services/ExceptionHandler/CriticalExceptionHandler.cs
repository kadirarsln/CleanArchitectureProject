using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace AppServices.ExceptionHandler;

public class CriticalExceptionHandler : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is CriticalException)
        {
            Console.WriteLine("Hata ile ilgili sms gönderildi");
        }

        return ValueTask.FromResult(false);
    }


}
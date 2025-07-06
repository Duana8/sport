using Serilog;

namespace webapi
{
    public class ExLogging
    {
        private readonly RequestDelegate _next;

        public ExLogging(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Пытаемся выполнить следующий этап обработки запроса
                await _next(context);
            }
            catch (Exception ex)
            {
                // Логируем исключение, если оно произошло
                Log.Error(ex, "Произошла ошибка на сервере: {Message}", ex.Message);

                // Можно вернуть ошибку клиенту (например, внутреннюю ошибку сервера)
                context.Response.StatusCode = 500; // Внутренняя ошибка сервера
                await context.Response.WriteAsync("Произошла ошибка на сервере");
            }
        }
    }
}

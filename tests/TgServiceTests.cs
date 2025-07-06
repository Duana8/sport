using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Xunit;
using webapi.Models.Context;

public class TelegramServiceTests
{
	[Fact]
	public async Task SendMessageAsync_ShouldSendRealMessage()
	{
		// Задаем токен напрямую
		string realBotToken = "токен";

		// Строим конфигурацию вручную с этим токеном
		var config = new ConfigurationBuilder()
			.AddInMemoryCollection(new Dictionary<string, string>
			{
				{ "TelegramBotToken", realBotToken }
			})
			.Build();

		// Используем настоящий HttpClient
		var telegramService = new TelegramService(config);

		// Указываем ID чата и сообщение
		var chatId = "чат id";
		var message = "✅ Реальное сообщение отправлено из теста.";

		// Вызываем метод отправки
		var result = await telegramService.SendMessageAsync(chatId, message);

		// Проверяем, что результат true (успешная отправка)
		Assert.True(result);
	}
}
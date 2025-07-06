using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using webapi.Models.Management;
using webapi.Models.Persons;
using webapi.Models.Context._Interface_;
using webapi.Models.Management._Service_;
using webapi.Models.Context;
using webapi.Models.Management.Dao;

public class RealVisitServTest
{
	[Fact]
	public async Task NotificCancelLesson_RealMessageSent_ReturnsTrue()
	{
		// 🔑 Настраиваем TelegramService с реальным токеном
		var config = new ConfigurationBuilder()
			.AddInMemoryCollection(new Dictionary<string, string>
			{
				{ "TelegramBotToken", "токен" }
			})
			.Build();

		var telegramService = new TelegramService(config);

		// 🧪 Мокаем IVisitRepo, чтобы вернуть нужный Visit
		var visitRepoMock = new Mock<IVisitRepo>();
		visitRepoMock.Setup(r => r.GetById(85)).ReturnsAsync(new Visit
		{
			schedule = new Schedule
			{
				date_time = new DateTime(2025, 6, 15, 18, 30, 0),
				type_lesson = new TypeLesson { name = "Йога" },
				staff = new Staff { name = "Ирина", surname = "Петрова" }
			}
		});

		// ✅ Создаём VisitServ с моканным repo и реальным TelegramService
		var visitServ = new VisitServ(visitRepoMock.Object, telegramService);

		var customers = new List<Customer>
		{
			new Customer { chat_id = "чат id" } // 👈 Укажи свой настоящий chat_id
		};

		// 🔄 Вызываем метод, который реально отправит сообщение
		var result = await visitServ.NotificCancelLesson(85, customers);

		// ✅ Проверка успешности
		Assert.True(result);
	}
}

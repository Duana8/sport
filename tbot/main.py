# -*- coding: utf-8 -*-

import asyncio
from aiogram import Bot, Dispatcher
import logging
import urllib3
from api.main import *
from handlers.main import set_commands
import handlers.main
import fms.main
from conf.main import token
import sys
import os

sys.path.append(os.path.dirname(os.path.abspath(__file__)))

urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)
logging.basicConfig(level=logging.INFO)

# Инициализация бота и диспетчера
bot = Bot(token=token)
dp = Dispatcher()

# Регистрация всех хендлеров
handlers.main.register_start_handlers(dp)
fms.main.register_start_handlers(dp)

# Основной запуск бота
async def main():
    try:
        await set_commands(bot)
        logging.info("Бот запущен и ждёт команды...")
        await dp.start_polling(bot)
    except asyncio.CancelledError:
        logging.info("Бот остановлен (CancelledError)")
    except Exception as e:
        logging.error(f"Ошибка при запуске бота: {e}")  # Логируем любую ошибку
        raise  # Прокидываем ошибку вверх, чтобы её можно было обработать

# Перезапуск бота при ошибке
async def run_bot():
    while True:
        try:
            await main()  # Запуск бота
            break  # Если бот завершился без ошибок, выходим из цикла
        except Exception as e:
            logging.error(f"Бот упал с ошибкой: {e}. Перезапуск через 5 секунд...")
            await asyncio.sleep(5)  # Задержка перед перезапуском

# Точка входа
if __name__ == '__main__':
    try:
        asyncio.run(run_bot())  # Запуск с перезапуском
    except KeyboardInterrupt:
        logging.info("Работа бота завершена пользователем (Ctrl+C)")
    except Exception as e:
        logging.error(f"Ошибка при завершении работы бота: {e}")

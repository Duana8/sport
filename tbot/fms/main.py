from aiogram import Dispatcher, types, F
from aiogram.fsm.context import FSMContext
from aiogram.fsm.state import State, StatesGroup
from datetime import datetime
from api.main import get_user_by_login
from handlers.main import get_menu_keyboard,start_command
from api.main import tek_booking
from datetime import timedelta

# Классы состояний для FSM
class AuthState(StatesGroup):
    waiting_for_login = State()
    waiting_for_password = State()

...
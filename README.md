# RegistrationIncomingLetters
Структура проекта
ABDD	Общий функционал для всех модулей.
RegistrationIncomingLetters.Common	Общие классы и Dto.
RegistrationIncomingLetters.DataAccess Доступ до БД и хранение миграции.
RegistrationIncomingLetters.WebAPI Настройки БД и веб сервис
RegistrationIncomingLetters.WPFClient Десктоп клиент wpf, Settings.settings адрес веб сервиса
Создание миграции
Add-Migration init -project RegistrationIncomingLetters.DataAccess  -StartupProject RegistrationIncomingLetters.WebAPI -Context AppDbContext

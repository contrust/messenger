# Messenger
## Список студентов-участников:
- Артем Борисов, ФТ-204
## Технологии:
- ASP .NET Core
## Компоненты системы:
### User Interface:
- Controller
  - AccountController
  - HomeController
- Views
  - Account
  - Home
### Application:
- DuplicateRemovalMessagesHandler
- FrequencyMessagesHandler
- StarReplacingMessagesHandler
- TextConverterMessagesHandler
### Domain:
- User
- Message
- Dialog
## Точки расширения:
- Добавление новых обработчиков сообщений
- Добавление новых ролей для пользователей в чате

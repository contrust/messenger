# MatmexMessenger - веб мессенджер.
## Список студентов-участников:
- Артем Борисов, ФТ-204
- Игорь Наумов, ФТ-204
- Юлия Комиссарова, ФТ-204
## Проблема, которую решает проект:
- Студенты хотят друг с другом познакомиться, но не хватает смелости подойти и заговорить.
## Сценарии:
- Студент хочет пообщаться с другим студентом, для этого он заходит во вкладку поиска студентов, указывает его имя и фамилию, либо ищет по списку, а потом отправляет заявку в друзья. После того, как запрос дружбы будет принят, этот студент сможет зайти во вкладку сообщения и написать другу.
- Студент хочет создать беседу со своими одногруппниками. Для этого он заходит во вкладку сообщения и нажимает кнопку создать беседу, после этого она появится во вкладке сообщения. Нажав на неё, он сможет добавить одногруппников в беседу.
## Технологии:
- ASP .NET Core
- SignalR - для передачи сообщений.
- PostgreSQL
- MicrosoftSQL
## Как передаются сообщения?
- Пользователь отправляет сообщение, оно сохраняется в БД, а после отправляется другому пользователю. Если сообщение не доставилось, то есть скриптик на html страничке, который спрашивает у сервера есть ли сообщение.
## Как слои взаимодействуют друг с другом? 
- Слой Application взаимодействует со слоем Domain и Infrastructure.
## Компоненты системы:
### User Interface:
- Controller
  - AccountController
    * AuthorizeUser
    * GetAccountSettings
    * SetAccountSettings
    * Logout
  - DialogController
    * GetDialog 
    * GetDialogs
    * CreateDialog
    * InviteUserInDialog
    * CreateInviteLink
    * JoinDialog
    * SendMessage
  - UsersController
    * GetUsers
- Views
  - Dialog
  - Dialogs
  - Users
  - Authorization
  - AccountSettings
### Application:
- DialogRecord - анемичная модель диалога, позволяет хранить в себе информацию из БД.
- UserRecord - анемичная модель юзера, позволяет хранить в себе информацию из БД.
- DialogRepository - сущность, имеющая функции сохранения и взятия из БД диалогов.
- UserRepository - сущность, имеющая функции сохранения и взятия из БД юзеров.
- UserAPI
  - AuthorizeUser(...)
  - ChangeUserName(userId, name)
  - ChangeUserSurname(userId, surname)
  - ChangeUserAvatar(userId, avatar)
- DialogAPI
  - CreateDialog(userId, name)
  - ChangeDialogName(userId, dialogId, name)
  - ChangeDialogAvatar(userId, dialogId, avatar)
  - SendInvitationToDialog(userId, dialogId, addingUserId)
  - JoinDialog(userId, dialogId)
  - LeaveDialog(userId, dialogId)
  - SendMessage(userId, dialogId, IContent)
### Domain:
- User - хранит информацию о пользователе, о его имени, фамилии, направлении, курсе, дате рождения, почте. Entity объект.
- Avatar - аватар. ValueType.
- Message - сущность, хранящая в себе информацию в конкретный момент о сообщении пользователя в диалоге. Entity объект.
- Dialog - сущность, оъеденяющая все message пользователей. Entity.
- PictureContent
- TextContent
- IContent
### Infrastructure:
- IDBRecordStorage
- IHasher
- IAuther
## Точки расширения:
- Использование разных баз данных. PostgreSQL, MicrosoftSQL.
- Использование разных алгоритмов шифрования для передачи сообщений. AES, самописанный XOR.
- Использование разных протоколов авторизации. OAuth, openid connect. 
  

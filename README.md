# Электронно-учебное приложение

Добро пожаловать в электронно-учебное приложение, разработанное на ASP.NET Core MVC! Это приложение предназначено для удобного доступа к учебным материалам и взаимодействия между студентами и преподавателями.

## Установка

### Предварительные требования

- [.NET Core SDK](https://dotnet.microsoft.com/download) (версия 6.0 или выше)
- [Visual Studio](https://visualstudio.microsoft.com/) или другой редактор кода
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (или другой совместимый с EF Core)

### Клонирование репозитория

Сначала клонируйте репозиторий на свой локальный компьютер:

bash
git clone https://github.com/DinislamFromUfa/Informatics.git


### Установка зависимостей

Установите необходимые пакеты NuGet:

Примените миграции:

bash
dotnet ef database update

### Запуск приложения

Запустите приложение с помощью команды:

bash
dotnet run

Теперь вы можете открыть браузер и перейти по адресу `http://localhost:#`, чтобы увидеть ваше приложение в действии!

## Использование

После запуска приложения вы сможете:

- Регистрация и вход в систему
- Просмотр учебных материалов
- Загрузка и обмен файлами
- Общение с преподавателями и другими студентами

## Вклад

Если вы хотите внести свой вклад в проект, пожалуйста, создайте форк репозитория, внесите изменения и отправьте пулл-запрос

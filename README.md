# EventManager

## Описание
Телеграм-бот - менеджер событий (календарь).

## Архитектура (по принципам DDD)

### UI
- генерация ответов пользователю полученных из слоя *APP* (в разработке)
- прием сообщений пользователя, отправка их в слой *APP* (в разработке)
Точка расширения: различные виды ответов, будь то кнопки/стикеры/прочее

### APP
- описание классов команд
- команды реализуют базовый класс BaseCommand, регистрируются DI-контейнером в MessageHandler (в разработке)
Точка расширения: создание безграничного количества новых команд, в идеале - расширяется до системы плагинов.


### DOMAIN
- описание класса события
- описание класса пользователя
- описание класса местонахождения
Точка расширения: различные виды метаинформации в событии
Точка расширения: различные способы сериализации события

### REPO
- описание базового класса работы с базой данных (в разработке)
- описание класса сообщения
- классы команд реализуют паттерн команда с некоторыми авторскими улучшениями
- команды распознаются по сигнатуре сообщения, методы-обработчики регистрируются атрибутами с предустановленными шаблонами


### Точка входа приложения
1. Загрузка файлов конфигурации
2. Подключение к базе данных
3. Регистрация команд DI-контейнером
4. Инициализация слушателя телеграм бота

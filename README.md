# finstar-test

Тестовое задание от команды IT Expert 

1. Все необходимые скрипты для создания таблиц и наполнения их тестовыми данными находятся в DB/TestFinStart.sql
2. На локальной машине стоит MSSql, по идее если в наличии PostgreSQL, можно в Program.cs на 26 строке поменять .UseSqlServer на .UseNpgsql, также возможно придется чуть корректировать миграцию учитывая синтаксис PostgreSQL
3. В некоторых местах кода есть мои комментарии относительно некоторых моментов
4. В UI все строки поиска работают как единый запрос, присутствует пагинация
5. Подключен сваггер, достучаться можно по дефолтному адресу localhost:5001/swagger, потыкать запросы и т.п.

База данных StrangeDb
Основная таблица StrangeItems

Состоит из полей:
1. Id int primary key identity(1, 1), 
2. Code int, 
3. Value varchar(500)


Установка:
1. Убедиться что установлена версия node не ниже v16.20.1
2. Открыть cmd из папки ClientApp, написать npm i
3. Сбилдить проект, запустить

Возможные проблемы
1. Если возникает ошибка DeprecationWarning: 'onAfterSetupMiddleware при старте приложения, нужно выполнить инструкцию из ответа Anton: https://stackoverflow.com/questions/70469717/cant-load-a-react-app-after-starting-server. Готовый файл можно скачать по ссылке: https://dropmefiles.com/bIWKZ. Заменить по пути \EmptyUI\ClientApp\node_modules\react-scripts\config.
2. Если возникает проблема с resolve-url-loader, то самый простой способ решения удалить файл EmptyUI\ClientApp\src\shared\ui Search.sass и убрать его из импорта в Search.tsx. В принципе стили только делают правильное расположение иконки и добавляют паддинги. https://github.com/sass/node-sass/issues/2756

Я честно пытался решить эти две проблемы в течении нескольких часов, но не хватило сил и времени :(

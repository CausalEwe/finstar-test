# finstar-test

Тестовое задание от команды IT Expert 

1)Все необходимые скрипты для создания таблиц и наполнения их тестовыми данными находятся в DB/TestFinStart.sql
2)На локальной машине стоит MSSql, по идее если в наличии PostgreSQL, можно в Program.cs на 26 строке поменять .UseSqlServer на .UseNpgsql, также возможно придется чуть корректировать миграцию учитывая синтаксис PostgreSQL
3)В некоторых местах кода есть мои комментарии относительно некоторых моментов
4)В UI поиск по коду и значению работает в синергии, если заполнить оба поля то найдется соответствующий двум запросам результат, по идентификатору - нет

База данных StrangeDb
Основная таблица StrangeItems

Состоит из полей
Id int primary key identity(1, 1), 
Code int, 
Value varchar(500)

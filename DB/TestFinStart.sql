-- БД и таблица для проекта --

if not exists (select * from sys.databases where name = 'StrangeDb')
begin
create database StrangeDb
end

use StrangeDb;

if not exists (select * from sys.tables where name = 'StrangeItems')
begin
Create Table [StrangeDb].[dbo].[StrangeItems] (
Id int primary key identity(1, 1), 
Code int, 
[Value] varchar(500)
)
end

-- Наполнение данными

INSERT INTO [StrangeDb].[dbo].[StrangeItems] (Code, [Value])
VALUES
    (1, 'v1'),
    (2, 'v11'),
    (3, 'v21'),
    (4, 'v16'),
    (5, 'vsd123');

-- Добавление ещё тестовых данных
INSERT INTO [StrangeDb].[dbo].[StrangeItems] (Code, [Value])
VALUES
    (6, 'test'),
    (7, 'helpme'),
    (8, 'moretest'),
    (9, 'aboba');

-- Тестовое задание 2 --

-- Создание таблицы [StrangeDb].[dbo].[test2Clients]
if not exists (select * from sys.tables where name = 'test2Clients')
begin
Create table [StrangeDb].[dbo].[test2Clients] (
Id bigint,
ClientName nvarchar(255)
)
end

-- Создание таблицы [StrangeDb].[dbo].[test2ClientContacts]
if not exists (select * from sys.tables where name = 'test2ClientContacts')
begin
Create table [StrangeDb].[dbo].[test2ClientContacts] (
Id bigint, 
ClientId bigint,
ContactType nvarchar(255),
ContactValue nvarchar(255)
)
end

-- Добавление тестовых данных в таблицу [StrangeDb].[dbo].[test2Clients]
insert into [StrangeDb].[dbo].[test2Clients] (Id, ClientName)
values
    (1, 'Client 1'),
    (2, 'Client 2'),
    (3, 'Client 3'),
    (4, 'Client 4'),
    (5, 'Client 5');

-- Добавление тестовых данных в таблицу [StrangeDb].[dbo].[test2ClientContacts]
insert into [StrangeDb].[dbo].[test2ClientContacts] (Id, ClientId, ContactType, ContactValue)
values
    (1, 1, 'Phone', '111-111-111'),
    (2, 1, 'Email', 'client1@example.com'),
    (3, 2, 'Phone', '222-222-222'),
    (4, 2, 'Email', 'client2@example.com'),
    (5, 3, 'Phone', '333-333-333'),
    (6, 4, 'Email', 'client4@example.com'),
    (7, 5, 'Phone', '555-555-555'),
    (8, 5, 'Email', 'client5@example.com'),
	(9, 5, 'Pager', '312653123');

-- запрос, который возвращает наименование клиентов и кол-во контактов клиентов 

select c.ClientName, COUNT(cc.Id) as NumberOfContacts
from [StrangeDb].[dbo].[test2Clients] c
left join [StrangeDb].[dbo].[test2ClientContacts] cc on c.Id = cc.ClientId
group by c.ClientName;

-- запрос, который возвращает список клиентов, у которых есть более 2 контактов

select c.ClientName
from [StrangeDb].[dbo].[test2Clients] c
inner join (
    select ClientId, COUNT(Id) as ContactCount
    from [StrangeDb].[dbo].[test2ClientContacts]
    group by ClientId
) cc on c.Id = cc.ClientId
where cc.ContactCount > 2;

-- Тестовое задание 3 --

if not exists (select * from sys.tables where name = 'test3')
begin
create table [StrangeDb].[dbo].[test3] (
Id int,
Dt date
)
end

insert into [StrangeDb].[dbo].[test3] values (1, '01.01.2021')
insert into [StrangeDb].[dbo].[test3] values (1, '01.10.2021')
insert into [StrangeDb].[dbo].[test3] values (1, '01.30.2021')
insert into [StrangeDb].[dbo].[test3] values (2, '01.15.2021')
insert into [StrangeDb].[dbo].[test3] values (2, '01.17.2021')
insert into [StrangeDb].[dbo].[test3] values (2, '01.30.2021')
insert into [StrangeDb].[dbo].[test3] values (3, '01.01.2021')
insert into [StrangeDb].[dbo].[test3] values (3, '01.30.2021')

--Вариант 1

with testCTE as (
select
    Id,
    Dt as Sd,
    LEAD(Dt) over (partition by Id order by Dt) as Ed
from
    [StrangeDb].[dbo].[test3]
)
select
    Id,
    Sd,
    case
        when Ed is null or Sd = Ed then null
        else Ed
    end as Ed
from
    testCTE 
where Ed is not null;

--Вариант 2

WITH testCTE AS (
    SELECT
        Id,
        Dt,
        ROW_NUMBER() OVER (PARTITION BY Id ORDER BY Dt) AS RowNum
    FROM
        [StrangeDb].[dbo].[test3]
),
IntervalDates AS (
    SELECT
        nd1.Id,
        nd1.Dt AS Sd,
        MIN(nd2.Dt) AS Ed
    FROM
        testCTE nd1
    LEFT JOIN
        testCTE nd2 ON nd1.Id = nd2.Id AND nd1.RowNum = nd2.RowNum - 1
    GROUP BY
        nd1.Id, nd1.Dt
)
SELECT
    Id,
    Sd,
    Ed
FROM
    IntervalDates
WHERE
    Sd IS NOT NULL AND Ed IS NOT NULL;

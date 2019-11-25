/*Task 1.1*
 Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года
  (колонка ShippedDate) включительно и которые доставлены с ShipVia >= 2. Формат указания даты
  должен быть верным при любых региональных настройках, согласно требованиям статьи
  “Writing International Transact-SQL Statements” в Books Online раздел “Accessing and Changing
  Relational Data Overview”. Этот метод использовать далее для всех заданий. Запрос должен высвечивать
  только колонки OrderID, ShippedDate и ShipVia.
Пояснить почему сюда не попали заказы с NULL-ом в колонке ShippedDate.
 */
select OrderID, ShippedDate, ShipVia from Orders
where ShippedDate >= CONVERT(DATETIME, '19980506', 101)
and ShipVia >= 2;
/*Строки где ShippedDate NULL не показывают потому что они не попадают под сравнение*/
select OrderID, ShippedDate, ShipVia from Orders
where (ShippedDate >= CONVERT(DATETIME, '19980506', 101)
or ShippedDate is NULL)
and ShipVia >= 2;
/*Task 1.2
  Написать запрос, который выводит только недоставленные заказы из таблицы Orders.
  В результатах запроса высвечивать для колонки ShippedDate вместо значений NULL строку
  ‘Not Shipped’ – использовать системную функцию CASЕ. Запрос должен высвечивать только колонки OrderID и ShippedDate.
  */
SELECT OrderID,
       COALESCE(CONVERT(varchar(50), ShippedDate, 121),'Not Shipped')
from Orders
/*Task 1.3
Выбрать в таблице Orders заказы, которые были доставлены после 6 мая 1998 года
  (ShippedDate) не включая эту дату или которые еще не доставлены. В запросе должны
  высвечиваться только колонки OrderID (переименовать в Order Number) и ShippedDate
  (переименовать в Shipped Date). В результатах запроса высвечивать для
  колонки ShippedDate вместо значений NULL строку ‘Not Shipped’, для остальных
  значений высвечивать дату в формате по умолчанию.
  */
SELECT OrderID "Order Number",
       ISNULL(CONVERT(varchar(60),ShippedDate,121), 'Not Shipped') "Shipped Date"
from Orders
where ShippedDate > CONVERT(DATETIME, '19980506', 101) or ShippedDate is null
/*2.1 Выбрать из таблицы Customers всех заказчиков, проживающих в USA и Canada.
  Запрос сделать с только помощью оператора IN. Высвечивать колонки с именем пользователя
  и названием страны в результатах запроса.
  Упорядочить результаты запроса по имени заказчиков и по месту проживания.*/
select ContactName, Country from Customers
where Country in ('USA','Canada')
order by ContactName, Country
/*
2.2 Выбрать из таблицы Customers всех заказчиков, не проживающих в USA и Canada.
Запрос сделать с помощью оператора IN. Высвечивать
колонки с именем пользователя и названием страны в результатах запроса.
Упорядочить результаты запроса по имени заказчиков.
*/
select ContactName, Country from Customers
where Country not in ('USA','Canada')
order by ContactName, Country
/*
2.3 Выбрать из таблицы Customers все страны, в которых проживают заказчики.
Страна должна быть упомянута только один раз и список отсортирован по убыванию.
Не использовать предложение GROUP BY. Высвечивать только одну колонку в результатах запроса.
*/
select distinct Country from Customers
order by Country desc
/*3.1 Выбрать все заказы (OrderID) из таблицы Order Details (заказы не должны повторяться),
  где встречаются продукты с количеством от 3 до 10 включительно – это колонка Quantity
  в таблице Order Details. Использовать оператор BETWEEN. Запрос должен высвечивать только колонку OrderID.
*/
select distinct OrderID from [Order Details]
where Quantity between 3 and 10
/*3.2 Выбрать всех заказчиков из таблицы Customers, у которых название
  страны начинается на буквы из диапазона b и g. Использовать оператор BETWEEN.
Проверить, что в результаты запроса попадает Germany. Запрос должен высвечивать
  только колонки CustomerID и Country и отсортирован по Country.
*/
select CustomerID, Country from Customers
where SUBSTRING(Country, 1, 1) between 'b' and 'g'
order by Country
/*
3.3 Выбрать всех заказчиков из таблицы Customers, у которых название страны
начинается на буквы из диапазона b и g, не используя оператор BETWEEN.
С помощью опции “Execution Plan” определить какой запрос предпочтительнее 3.2 или 3.3
– для этого надо ввести в скрипт выполнение текстового Execution Plan-a для двух этих запросов,
результаты выполнения Execution Plan надо ввести в скрипт в виде комментария и по их
результатам дать ответ на вопрос – по какому параметру было проведено сравнение.
Запрос должен высвечивать только колонки CustomerID и Country и отсортирован по Country.
*/
select CustomerID, Country from Customers
where SUBSTRING(Country, 1, 1) in ('b','c','d','e','f','g')
order by Country
/*4.1 В таблице Products найти все продукты (колонка ProductName),
  где встречается подстрока 'chocolade'. Известно, что в подстроке
  'chocolade' может быть изменена одна буква 'c' в середине - найти
  все продукты, которые удовлетворяют этому условию. Подсказка:
  результаты запроса должны высвечивать 2 строки.*/
select ProductName from Products
where ProductName like ('%cho_olade%')
/*5.1 Найти общую сумму всех заказов из таблицы Order Details с
  учетом количества закупленных товаров и скидок по ним.
  Результат округлить до сотых и высветить в стиле 1 для типа данных money.
  Скидка (колонка Discount) составляет процент из стоимости для данного товара.
  Для определения действительной цены на проданный продукт надо вычесть скидку
  из указанной в колонке UnitPrice цены. Результатом запроса должна быть одна
  запись с одной колонкой с названием колонки 'Totals'.*/
select CONVERT(money,SUM(Quantity*(UnitPrice-(Discount*UnitPrice))),1) "Totals" from [Order Details]
/*
5.2 По таблице Orders найти количество заказов, которые еще не были доставлены
(т.е. в колонке ShippedDate нет значения даты доставки). Использовать при
этом запросе только оператор COUNT. Не использовать предложения WHERE и GROUP.
*/
select count(*) - count(ShippedDate) "Not Shipped" from Orders
/*
5.3 По таблице Orders найти количество различных покупателей (CustomerID),
сделавших заказы. Использовать функцию COUNT и не использовать предложения WHERE и GROUP.
*/
select count(distinct CustomerID) from Orders
/*
6.1 По таблице Orders найти количество заказов с группировкой по годам. В результатах запроса надо высвечивать
две колонки c названиями Year и Total. Написать проверочный запрос, который вычисляет количество всех заказов.
*/
select YEAR(ShippedDate) "Year", count(OrderID) 'Total'  from Orders
group by YEAR(ShippedDate)
/*6.2 По таблице Orders найти количество заказов, cделанных каждым продавцом.
  Заказ для указанного продавца – это любая запись в таблице Orders, где в
  колонке EmployeeID задано значение для данного продавца. В результатах запроса
  надо высвечивать колонку с именем продавца (Должно высвечиваться имя полученное
  конкатенацией LastName & FirstName. Эта строка LastName & FirstName должна быть
  получена отдельным запросом в колонке основного запроса. Также основной запрос
  должен использовать группировку по EmployeeID.) с названием колонки ‘Seller’ и
  колонку c количеством заказов высвечивать с названием 'Amount'. Результаты запроса
  должны быть упорядочены по убыванию количества заказов.
*/
select (select (FirstName+' '+LastName) from Employees
    where EmployeeID = o.EmployeeID) "Seller", count(*) "Amount"
from Orders o
group by o.EmployeeID
order by Amount desc
/*
6.3 По таблице Orders найти количество заказов, cделанных каждым продавцом
и для каждого покупателя. Необходимо определить это только для заказов
сделанных в 1998 году. В результатах запроса надо высвечивать колонку с
именем продавца (название колонки ‘Seller’), колонку с именем покупателя
(название колонки ‘Customer’)  и колонку c количеством заказов высвечивать с
названием 'Amount'. В запросе необходимо использовать специальный оператор
языка T-SQL для работы с выражением GROUP (Этот же оператор поможет выводить
строку “ALL” в результатах запроса). Группировки должны быть сделаны по ID
продавца и покупателя. Результаты запроса должны быть упорядочены по продавцу,
покупателю и по убыванию количества продаж. В результатах должна быть сводная
информация по продажам. Т.е. в резульирующем наборе должны присутствовать дополнительно
к информации о продажах продавца для каждого покупателя следующие строчки:
*/
select (select (FirstName+' '+LastName) from Employees
    where EmployeeID = o.EmployeeID) "Seller",
       (select ContactName from Customers
        where CustomerID = o.CustomerID) "Customer",
       count(*) "Amount"
from Orders o
GROUP BY CUBE (o.EmployeeID, o.CustomerID)
order by o.EmployeeID, o.CustomerID , Amount desc
/*
6.4 Найти покупателей и продавцов, которые живут в одном городе.
Если в городе живут только один или несколько продавцов или только
один или несколько покупателей, то информация о таких покупателя и
продавцах не должна попадать в результирующий набор. Не использовать
конструкцию JOIN. В результатах запроса необходимо вывести следующие
заголовки для результатов запроса: ‘Person’, ‘Type’ (здесь надо выводить
строку ‘Customer’ или  ‘Seller’ в завимости от типа записи), ‘City’.
Отсортировать результаты запроса по колонке ‘City’ и по ‘Person’.
*/
SELECT FirstName + ' ' + LastName AS Person, 'Seller' AS [Type], City
FROM Employees
WHERE EXISTS (SELECT City
              FROM Customers
              WHERE Employees.City = Customers.City)
UNION
SELECT ContactName AS Person, 'Customer' AS [Type], City
FROM Customers
WHERE EXISTS (SELECT City
              FROM Employees
              WHERE Employees.City = Customers.City)
Order by [City], [Person];
/*
6.5 Найти всех покупателей, которые живут в одном городе. В запросе использовать
соединение таблицы Customers c собой - самосоединение. Высветить колонки CustomerID
и City. Запрос не должен высвечивать дублируемые записи. Для проверки написать запрос,
который высвечивает города, которые встречаются более одного раза в таблице Customers.
Это позволит проверить правильность запроса.
*/
SELECT CustomerID, City
FROM Customers AS c1
WHERE EXISTS (SELECT City
              FROM Customers AS c2
              WHERE c1.City = c2.City and not(c1.CustomerID = c2.CustomerID))
Order by [City];

select City
from Customers
group by City
HAVING COUNT(CustomerID) > 1;
/*6.6 По таблице Employees найти для каждого продавца его руководителя,
  т.е. кому он делает репорты. Высветить колонки с именами 'User Name'
  (LastName) и 'Boss'. В колонках должны быть высвечены имена из колонки
  LastName. Высвечены ли все продавцы в этом запросе?*/
select LastName "User Name", (select LastName from Employees where EmployeeID = e.ReportsTo) "Boss"
from Employees e
/*Вывело всех, но у Fuller нет босса*/
/*7.1 Определить продавцов, которые обслуживают регион 'Western' (таблица Region).
  Результаты запроса должны высвечивать два поля: 'LastName' продавца и название
  обслуживаемой территории ('TerritoryDescription' из таблицы Territories).
  Запрос должен использовать JOIN в предложении FROM. Для определения связей\
  между таблицами Employees и Territories надо использовать графические диаграммы для базы Northwind.*/
select LastName, TerritoryDescription from Employees
inner join EmployeeTerritories ET on Employees.EmployeeID = ET.EmployeeID
inner join Territories T on ET.TerritoryID = T.TerritoryID
where T.RegionID = 2
/*8.1 Высветить в результатах запроса имена всех заказчиков из таблицы
  Customers и суммарное количество их заказов из таблицы Orders.
  Принять во внимание, что у некоторых заказчиков нет заказов, но
  они также должны быть выведены в результатах запроса. Упорядочить
  результаты запроса по возрастанию количества заказов.
*/
select ContactName, count(o.CustomerID) "Totals" from Customers
left join Orders O on Customers.CustomerID = O.CustomerID
group by ContactName
order by count(o.CustomerID) desc
/*9.1 Высветить всех поставщиков колонка CompanyName в таблице Suppliers,
  у которых нет хотя бы одного продукта на складе (UnitsInStock в таблице
  Products равно 0). Использовать вложенный SELECT для этого запроса с
  использованием оператора IN. Можно ли использовать вместо оператора IN оператор '=' ?*
*/
select CompanyName from Suppliers
where SupplierID in (select SupplierID from Products where UnitsInStock = 0)
/*Использовать = вместо IN нельзя, так как подзапрос возвращает список значений*/
/*10.1 Высветить всех продавцов, которые имеют более 150 заказов. Использовать вложенный коррелированный SELECT.*/
select LastName from Employees
where EmployeeID in (select EmployeeID from Orders o
group by EmployeeID having count(o.EmployeeID) > 150)
/*11.1 Высветить всех заказчиков (таблица Customers), которые не имеют
  ни одного заказа (подзапрос по таблице Orders). Использовать коррелированный SELECT и оператор EXISTS.
*/
select ContactName from Customers c
where not exists (
select o.CustomerID from Orders o
where c.CustomerID = o.CustomerID
group by CustomerID having count(o.EmployeeID) >= 1)
/*12.1 Для формирования алфавитного указателя Employees высветить из
  таблицы Employees список только тех букв алфавита, с которых
  начинаются фамилии Employees (колонка LastName ) из этой таблицы.
  Алфавитный список должен быть отсортирован по возрастанию.
*/
select distinct SUBSTRING(LastName, 1, 1) "Fletter" from Employees
order by Fletter

exec GreatestOrders 1996
exec ShippedOrdersDiff 35
exec SubordinationInfo 2

select LastName, Northwind.dbo.IsBoss([EmployeeID]) as IsBoss
From Employees
/*13.1 Написать процедуру, которая возвращает самый крупный заказ для каждого
из продавцов за определенный год. В результатах не может быть несколько заказов
  одного продавца, должен быть только один и самый крупный. В результатах запроса
  должны быть выведены следующие колонки: колонка с именем и фамилией продавца
  (FirstName и LastName – пример: Nancy Davolio), номер заказа и его стоимость.
  В запросе надо учитывать Discount при продаже товаров. Процедуре передается год,
  за который надо сделать отчет, и количество возвращаемых записей. Результаты запроса
  должны быть упорядочены по убыванию суммы заказа. Процедура должна быть реализована с
  использованием оператора SELECT и БЕЗ ИСПОЛЬЗОВАНИЯ КУРСОРОВ. Название функции соответственно
  GreatestOrders. Необходимо продемонстрировать использование этих процедур. Также помимо
  демонстрации вызовов процедур в скрипте Query.sql надо написать отдельный ДОПОЛНИТЕЛЬНЫЙ
  проверочный запрос для тестирования правильности работы процедуры GreatestOrders.
  Проверочный запрос должен выводить в удобном для сравнения с результатами работы процедур
  виде для определенного продавца для всех его заказов за определенный указанный год в результатах
  следующие колонки: имя продавца, номер заказа, сумму заказа. Проверочный запрос не должен повторять
  запрос, написанный в процедуре, - он должен выполнять только то, что описано в требованиях по нему.
ВСЕ ЗАПРОСЫ ПО ВЫЗОВУ ПРОЦЕДУР ДОЛЖНЫ БЫТЬ НАПИСАНЫ В ФАЙЛЕ Query.sql – см. пояснение ниже в разделе «Требования к оформлению».
*/
CREATE PROCEDURE GreatestOrders
	@Year int
AS BEGIN
	SELECT LastName + ' ' + FirstName AS 'Name', Price
	FROM Employees e
	INNER JOIN (SELECT EmployeeID, MAX(UnitPrice * Quantity - UnitPrice * Quantity * Discount) AS Price
			FROM [Order Details] od
			INNER JOIN Orders d ON od.OrderID = d.OrderID
			WHERE @Year  = YEAR(OrderDate)
			GROUP BY EmployeeID) AS O ON e.EmployeeID = O.EmployeeID;
END
/*
13.2 Написать процедуру, которая возвращает заказы в таблице Orders,
согласно указанному сроку доставки в днях (разница между OrderDate и ShippedDate).
В результатах должны быть возвращены заказы, срок которых превышает переданное
значение или еще недоставленные заказы. Значению по умолчанию для передаваемого
срока 35 дней. Название процедуры ShippedOrdersDiff. Процедура должна высвечивать
следующие колонки: OrderID, OrderDate, ShippedDate, ShippedDelay (разность в днях между
ShippedDate и OrderDate), SpecifiedDelay (переданное в процедуру значение).
Необходимо продемонстрировать использование этой процедуры.*/
CREATE PROCEDURE ShippedOrdersDiff
	@Time int=35
AS BEGIN
	SELECT *
	FROM (Select OrderID, OrderDate, ShippedDate, DATEDIFF(dd, OrderDate, ShippedDate) AS [ShippedDelay], [SpecifiedDelay] = @Time
		FROM Orders) as SD
	WHERE [ShippedDelay] > @Time OR ShippedDate IS NULL;
END;
/*13.3	Написать процедуру, которая высвечивает всех подчиненных заданного продавца, как непосредственных, так и подчиненных его подчиненных.
 В качестве входного параметра функции используется EmployeeID.
Необходимо распечатать имена подчиненных и выровнять их в тексте (использовать оператор PRINT) согласно иерархии подчинения.
Продавец, для которого надо найти подчиненных также должен быть высвечен. Название процедуры SubordinationInfo.
В качестве алгоритма для решения этой задачи надо использовать пример, приведенный в Books Online и рекомендованный Microsoft для решения подобного типа задач.
Продемонстрировать использование процедуры.*/
CREATE PROCEDURE SubordinationInfo
	@EmployeeID INT
AS
BEGIN
	WITH parent AS (
    SELECT EmployeeID
    FROM Employees
    WHERE EmployeeID = @EmployeeID
), tree AS (
    SELECT x.ReportsTo, x.EmployeeID
    FROM Employees x
    INNER JOIN parent ON x.ReportsTo = parent.EmployeeID
    UNION ALL
    SELECT y.ReportsTo, y.EmployeeID
    FROM Employees y
    INNER JOIN tree t ON y.ReportsTo = t.EmployeeID
)
SELECT e.LastName + ' ' + e.FirstName AS 'Name', x.LastName + ' ' + x.FirstName AS 'Boss'
FROM tree t
inner join Employees e on e.EmployeeID = t.EmployeeID
inner join Employees x on x.EmployeeID = t.ReportsTo
END
-- 13.4 Написать функцию, которая определяет, есть ли у продавца подчиненные.
-- Возвращает тип данных BIT. В качестве входного параметра функции используется EmployeeID.
-- Название функции IsBoss. Продемонстрировать использование функции для всех продавцов из таблицы Employees.
CREATE FUNCTION IsBoss (@EmploeeID INT)
RETURNS BIT
AS
BEGIN
	Declare @ValueToReturn BIT
	if (@EmploeeID in (SELECT Distinct B.EmployeeID
	FROM Employees AS E
	INNER JOIN (SELECT EmployeeID
				FROM Employees) AS B ON B.EmployeeID = E.ReportsTo)) set @ValueToReturn = 1
	else  set @ValueToReturn = 0
	Return @ValueToReturn
END
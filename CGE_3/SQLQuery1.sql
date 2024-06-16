USE CGE

CREATE TABLE [Department] (
	ID numeric IDENTITY,
	Name nvarchar(100) NOT NULL,
  CONSTRAINT [PK_DEPARTMENT] PRIMARY KEY CLUSTERED
  (
  [ID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
GO
CREATE TABLE [Employee] (
	ID numeric IDENTITY,
	DepartmentID numeric,
	ChiefID numeric,
    CHECK(ChiefID != ID),
	Name nvarchar(100) NOT NULL,
	Salary numeric NOT NULL,
  CONSTRAINT [PK_EMPLOYEE] PRIMARY KEY CLUSTERED
  (
  [ID] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)
)
GO


ALTER TABLE [Employee] ADD CONSTRAINT [Employee_fk0] FOREIGN KEY ([DepartmentID]) REFERENCES [Department]([ID])
ON UPDATE cascade
ON delete set null
GO

ALTER TABLE [Employee] ADD CONSTRAINT [Employee_fk1] FOREIGN KEY ([ChiefID]) REFERENCES [Employee]([ID])
GO

INSERT INTO Department(Name)
VALUES
( N'Административно-управленческий отдел'),
( N'Планово-финансовый отдел'),
( N'Отдел кадров'),
( N'Отдел охраны труда, промышленной и пожарной безопасности'),
( N'Служба безопасности'),
( N'Отдел экспертизы ПСД'),
( N'Отдел эксплуатации'),
( N'Отдел информационных технологий'),
( N'Отдел закупок'),
( N'Договорно-коммерческий отдел')

INSERT INTO Employee
VALUES
(1, null, N'Васильев С.В.', 600000),
(1, 1, N'Караваев М.И.', 500000),
(1, 2, N'Красильникова Ю.Г.', 340000),
(1, 2, N'Милютин К.А.', 350000),
(2, 3, N'Григорьева А.В.', 200000),
(2, 5, N'Миронова М.Е.', 140000),
(2, 5, N'Прядкина И.П.', 110000),
(2, 5, N'Бокайчук Е.Н.', 90000),
(2, 6, N'Вдовина А.Д.', 85000),
(3, 3, N'Янина О.П.', 210000),
(3, 10, N'Павлова К.В.', 135000),
(3, 10, N'Графова Л.В.', 127000),
(3, 11, N'Денисова В.Г.', 96000),
(3, 11, N'Доронина Е.А.', 89500),
(4, 2, N'Миронов С.В.', 187000),
(4, 15, N'Кондаков Ю.Н.', 162000),
(4, 15, N'Евграфов К.А.', 112000),
(4, 15, N'Картаполов И.Н.', 87000),
(4, 16, N'Федоров С.В.', 72000),
(5, 2, N'Никитчук А.В.', 150000),
(5, 20, N'Выползов Е.Ф.', 90000),
(5, 20, N'Иванов П.Е.', 90000),
(6, 1, N'Авдеев Н.Б.', 175000),
(6, 23, N'Трегубова П.К.', 120000),
(6, 23, N'Симаненко П.Н.', 110000),
(7, 4, N'Бондаренко С.Н.', 210000),
(7, 26, N'Карпенко А.В.', 190000),
(7, 26, N'Тенешев В.Ф.', 150000),
(7, 27, N'Фомин К.В.', 150000),
(7, 27, N'Павленко А.С.', 150000),
(7, 29, N'Петрунькин С.П.', 90000),
(7, 29, N'Павлов А.В.', 86000),
(7, 29, N'Бояркин М.П.', 84000),
(7, 29, N'Прохоров М.И.', 83000),
(7, 29, N'Куликов А.А.', 79000),
(7, 29, N'Андрейченко В.И.', 79000),
(7, 29, N'Полозов Д.И.', 79000),
(7, 30, N'Константинов К.В.', 90000),
(7, 29, N'Кривоносов П.А.', 86000),
(7, 29, N'Петров Н.Г.', 84000),
(7, 29, N'Шестаков Д.П.', 83000),
(7, 29, N'Галкин Н.Е.', 79000),
(7, 29, N'Михайлов А.В.', 79000),
(7, 29, N'Капустин Э.Н.', 79000),
(8, 4, N'Пустобаев Т.И.', 190000),
(8, 45, N'Кузнецов С.В.', 150000),
(8, 45, N'Макаров О.В.', 134000),
(9, 3, N'Виногорская Г.А.', 200000),
(9, 48, N'Флегантова М.В.', 139000),
(9, 48, N'Криницина С.А.', 123000),
(10, 3, N'Севостьянов А.Н.', 160000),
(10, 51, N'Семенов В.О.', 117000),
(10, 51, N'Рыбин О.П.', 117000)

use CGE
go
CREATE TRIGGER Employee_delete
ON Employee
instead of delete
AS 
begin
update Employee
set ChiefID = null
WHERE ChiefID =(SELECT ID FROM deleted)
delete Employee
where ID = (SELECT ID FROM deleted)
end

go
CREATE TRIGGER Employee_insert_update
ON Employee
after insert, update
AS 
begin
declare @ins table (insID numeric, insDepartmentID numeric, insChiefID numeric, insName nvarchar(100), insSalary numeric)
insert @ins 
	select ID, DepartmentID, ChiefID, [Name], Salary from Employee
declare @updCount int
select @updCount = count(insID) from @ins join Employee on insID=ChiefID and insChiefID = ID
if (@updCount != 0)
	begin
	PRINT N'Внимание! Следующие сотрудники являются начальниками друг друга:'
	select insID, insChiefID, insName from @ins join Employee on insID=ChiefID and insChiefID = ID
	end
end

--Сотрудник с максимальной заработной платой:
declare @maxSalary numeric
select @maxSalary = MAX(Salary) from Employee
select [Name], Salary from Employee where Salary=@maxSalary

--Отдел с самой высокой заработной платой между сотрудниками:
select Top 1 [Department].[Name], Max(Salary)-MIN(Salary) AS Dif from Employee
join Department on DepartmentID = [Department].ID
group by [Department].[Name] 
order by Dif desc 

--Отдел с максимальной суммарной зарплатой сотрудников:
select Top 1 [Department].[Name], SUM(Salary) AS MaxSumSalary from Employee
join Department on DepartmentID = [Department].ID
group by [Department].[Name] 
order by MaxSumSalary desc 

--Сотрудника, чье имя начинается на «Р» и заканчивается на «н»:
select [Name] from Employee
where [Name] like N'Р%н _._.'


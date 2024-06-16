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
( N'���������������-�������������� �����'),
( N'�������-���������� �����'),
( N'����� ������'),
( N'����� ������ �����, ������������ � �������� ������������'),
( N'������ ������������'),
( N'����� ���������� ���'),
( N'����� ������������'),
( N'����� �������������� ����������'),
( N'����� �������'),
( N'���������-������������ �����')

INSERT INTO Employee
VALUES
(1, null, N'�������� �.�.', 600000),
(1, 1, N'�������� �.�.', 500000),
(1, 2, N'������������� �.�.', 340000),
(1, 2, N'������� �.�.', 350000),
(2, 3, N'���������� �.�.', 200000),
(2, 5, N'�������� �.�.', 140000),
(2, 5, N'�������� �.�.', 110000),
(2, 5, N'�������� �.�.', 90000),
(2, 6, N'������� �.�.', 85000),
(3, 3, N'����� �.�.', 210000),
(3, 10, N'������� �.�.', 135000),
(3, 10, N'������� �.�.', 127000),
(3, 11, N'�������� �.�.', 96000),
(3, 11, N'�������� �.�.', 89500),
(4, 2, N'������� �.�.', 187000),
(4, 15, N'�������� �.�.', 162000),
(4, 15, N'�������� �.�.', 112000),
(4, 15, N'���������� �.�.', 87000),
(4, 16, N'������� �.�.', 72000),
(5, 2, N'�������� �.�.', 150000),
(5, 20, N'�������� �.�.', 90000),
(5, 20, N'������ �.�.', 90000),
(6, 1, N'������ �.�.', 175000),
(6, 23, N'��������� �.�.', 120000),
(6, 23, N'��������� �.�.', 110000),
(7, 4, N'���������� �.�.', 210000),
(7, 26, N'�������� �.�.', 190000),
(7, 26, N'������� �.�.', 150000),
(7, 27, N'����� �.�.', 150000),
(7, 27, N'�������� �.�.', 150000),
(7, 29, N'���������� �.�.', 90000),
(7, 29, N'������ �.�.', 86000),
(7, 29, N'������� �.�.', 84000),
(7, 29, N'�������� �.�.', 83000),
(7, 29, N'������� �.�.', 79000),
(7, 29, N'����������� �.�.', 79000),
(7, 29, N'������� �.�.', 79000),
(7, 30, N'������������ �.�.', 90000),
(7, 29, N'���������� �.�.', 86000),
(7, 29, N'������ �.�.', 84000),
(7, 29, N'�������� �.�.', 83000),
(7, 29, N'������ �.�.', 79000),
(7, 29, N'�������� �.�.', 79000),
(7, 29, N'�������� �.�.', 79000),
(8, 4, N'��������� �.�.', 190000),
(8, 45, N'�������� �.�.', 150000),
(8, 45, N'������� �.�.', 134000),
(9, 3, N'����������� �.�.', 200000),
(9, 48, N'���������� �.�.', 139000),
(9, 48, N'��������� �.�.', 123000),
(10, 3, N'����������� �.�.', 160000),
(10, 51, N'������� �.�.', 117000),
(10, 51, N'����� �.�.', 117000)

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
	PRINT N'��������! ��������� ���������� �������� ������������ ���� �����:'
	select insID, insChiefID, insName from @ins join Employee on insID=ChiefID and insChiefID = ID
	end
end

--��������� � ������������ ���������� ������:
declare @maxSalary numeric
select @maxSalary = MAX(Salary) from Employee
select [Name], Salary from Employee where Salary=@maxSalary

--����� � ����� ������� ���������� ������ ����� ������������:
select Top 1 [Department].[Name], Max(Salary)-MIN(Salary) AS Dif from Employee
join Department on DepartmentID = [Department].ID
group by [Department].[Name] 
order by Dif desc 

--����� � ������������ ��������� ��������� �����������:
select Top 1 [Department].[Name], SUM(Salary) AS MaxSumSalary from Employee
join Department on DepartmentID = [Department].ID
group by [Department].[Name] 
order by MaxSumSalary desc 

--����������, ��� ��� ���������� �� �л � ������������� �� ��:
select [Name] from Employee
where [Name] like N'�%� _._.'


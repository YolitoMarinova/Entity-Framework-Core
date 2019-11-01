CREATE DATABASE MiniORM
GO
USE MiniORM
GO
CREATE TABLE Projects
(
Id INT IDENTITY PRIMARY KEY,
Name VARCHAR(50) NOT NULL
)
CREATE TABLE Departments
(
Id INT IDENTITY PRIMARY KEY,
Name VARCHAR(50) NOT NULL
)
CREATE TABLE Employees
(
Id INT IDENTITY PRIMARY KEY,
FirstName VARCHAR(50) NOT NULL,
MiddleName VARCHAR(50),
LastName VARCHAR(50) NOT NULL,
IsEmployed BIT NOT NULL,
DepartmentId INT
CONSTRAINT FK_Employees_Departments FOREIGN KEY
REFERENCES Departments(Id)
)
CREATE TABLE EmployeesProjects
(
ProjectId INT NOT NULL
CONSTRAINT FK_Employees_Projects REFERENCES Projects(Id),
EmployeeId INT NOT NULL
CONSTRAINT FK_Employees_Employee REFERENCES Employees(Id),
CONSTRAINT PK_Projects_Employees
PRIMARY KEY (ProjectId, EmployeeId)
)
GO
INSERT INTO MiniORM.dbo.Departments (Name) VALUES (&#39;Research&#39;);

© Software University Foundation. This work is licensed under the CC-BY-NC-SA license.
Follow us: Page 9 of 21

INSERT INTO MiniORM.dbo.Employees (FirstName, MiddleName, LastName, IsEmployed, DepartmentId) VALUES
(&#39;Stamat&#39;, NULL, &#39;Ivanov&#39;, 1, 1),
(&#39;Petar&#39;, &#39;Ivanov&#39;, &#39;Petrov&#39;, 0, 1),
(&#39;Ivan&#39;, &#39;Petrov&#39;, &#39;Georgiev&#39;, 1, 1),
(&#39;Gosho&#39;, NULL, &#39;Ivanov&#39;, 1, 1);
INSERT INTO MiniORM.dbo.Projects (Name)
VALUES (&#39;C# Project&#39;), (&#39;Java Project&#39;);
INSERT INTO MiniORM.dbo.EmployeesProjects (ProjectId, EmployeeId) VALUES
(1, 1),
(1, 3),
(2, 2),
(2, 3)
--a sample script with the supported syntax
--comment lines, like this one, and empty line will be ignored
--the instructions parsed will be executed when end of file or a go instruction is reached
--instructions end with ';', except for go instruction

create database AcademicManagerDatabase;
go

use AcademicManagerDatabase;

create schema dbo
	create table students (id PK, firstName, lastName)
	create table grades (id PK, studentId, course, value);
go

create table admins (id PK, username, password);
go

insert into students (id, firstName, lastName) values ('0', 'John', 'Doe');

select * from students where id = '0';

update students set firstName = 'Jane' where firstName = 'John';

delete from students where id = '0';

drop students;
drop dbo;
drop AcademicManagerDatabase;
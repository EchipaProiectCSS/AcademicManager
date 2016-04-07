--a sample script with the supported syntax
--comment lines, like this one, and empty line will be ignored
--the instructions parsed will be executed when end of file is reached
--instructions end with ';'

--could also be a full path to the location of the database, including the dabatase name
create database 'AcademicManagerDatabase';

--could also be a full path to the location of the database, including the dabatase name
use 'AcademicManagerDatabase';

create table students (id PK, firstName, lastName);
create table grades (id PK, studentId, course, value);

create table admins (id PK, username, password);

insert into students (id, firstName, lastName) values ('0', 'John', 'Doe');

select * from students where id = '0';

update students set firstName = 'Jane' where firstName = 'John';

delete from students where id = '0';

drop table 'students';
drop database 'AcademicManagerDatabase';
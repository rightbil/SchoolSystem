use [SchoolManagement.SchoolDB.SchoolDbContext]
go
select *
from sys.tables
where name like '__MigrationHistory'
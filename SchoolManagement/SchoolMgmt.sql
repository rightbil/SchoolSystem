use [SchoolSystem]
go
select *
from sys.tables
where name like '__MigrationHistory'


Select *
from tblDepartment


Select t.* , d.[Department Name]
from tblTeacher as t
left join tblDepartment d on t.DepartmentId = d.[Department ID]
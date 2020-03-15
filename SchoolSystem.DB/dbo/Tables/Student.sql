CREATE TABLE [dbo].[Student]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [StudentID] INT NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL,
    [FirstName] NVARCHAR(50) NOT NULL, 
    [EnrollmentDate] NVARCHAR(50) NOT NULL, 
    [EmailAddress] NVARCHAR(50) NOT NULL, 
    [PhoneNumber] NVARCHAR(50) NOT NULL, 
    [DateOfBirth] NVARCHAR(50) NOT NULL, 
    [PostalCode] NVARCHAR(50) NOT NULL, 
    [Comment] NVARCHAR(50) NOT NULL, 
    [ImageUrl] NVARCHAR(50) NOT NULL, 
    [Enrollements] NVARCHAR(50) NULL
    
)

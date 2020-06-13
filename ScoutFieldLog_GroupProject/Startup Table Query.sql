USE ScoutFieldLogDb
CREATE TABLE StartUp(
Id INT IDENTITY (1,1) NOT NULL,
CompanyName NVARCHAR(100),
DateAdded DATE,
Source NVARCHAR(100),
Image NVARCHAR(100),
CompanyWebsite NVARCHAR(100),
City NVARCHAR(100),
State_Province NVARCHAR (100),
Country NVARCHAR(100),
TwoLineSummary NVARCHAR(100),
ContactId NVARCHAR(100),
Themes NVARCHAR(100),
Landscapes NVARCHAR(100),
TechnologyAreas NVARCHAR(100),
Alignments NVARCHAR(100),
DateAssigned NVARCHAR(100),
DateSaved DATE,
Status NVARCHAR(100)
)

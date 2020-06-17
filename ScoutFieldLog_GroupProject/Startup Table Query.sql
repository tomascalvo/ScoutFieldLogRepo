USE ScoutFieldLogDb

DROP TABLE StartUpCompanies;

CREATE TABLE StartUpCompanies (
Id INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
ScoutName NVARCHAR(100),
CompanyName NVARCHAR(100),
CompanyContactName NVARCHAR(100),
CompanyContactPhoneNumber NVARCHAR(100),
CompanyWebsite NVARCHAR(100),
TwoLineSummary NVARCHAR(MAX),
TechnologyAreas NVARCHAR(100),
Image NVARCHAR(100),
City NVARCHAR(100),
State_Province NVARCHAR (100),
Country NVARCHAR(100),
Themes NVARCHAR(100),
Landscapes NVARCHAR(100),
Alignments NVARCHAR(100),
DateReviewed DATE,
DateAssigned DATE,
Uniqueness INT,
Team INT,
Raised INT,
Status NVARCHAR(100),
Keywords NVARCHAR(MAX)
)


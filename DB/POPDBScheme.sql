Create Database TheLanguageSchool

Create Table School
(
	IdentificationNumber Int Primary Key,
	Name Varchar(50),
	SchoolAddress Varchar(100),
	PhoneNumber Varchar(20),
	Email Varchar(30),
	WebSite Varchar(30),
	Pib Varchar(9),
	AccountNumber Varchar(25)
) On School;

Create Table LanguageL
(
	Id Int Primary Key,
	Name Varchar(30),
	Deleted Bit
) On Course;

Create Table CourseType
(
	Id Int Primary Key,
	Name Varchar(30),
	Deleted Bit
) On Course;

Create Table Student
(
	Id Int Primary Key,
	FirstName Varchar(30),
	LastName Varchar(30),
	Jmbg Char(13),
	PersonAddress Varchar(50),
	Deleted Bit
) On People;

Create Table Teacher
(
	Id Int Primary Key,
	FirstName Varchar(30),
	LastName Varchar(30),
	Jmbg Char(13),
	PersonAddress Varchar(50),
	Deleted Bit
) On People;

Create Table UserU
(
	Id Int Primary Key,
	FirstName Varchar(30),
	LastName Varchar(30),
	Jmbg Char(13),
	PersonAddress Varchar(50),
	Deleted Bit,
	UserName Varchar(20),
	PasswordP Varchar(20),
	UserRole Varchar(20)
) On People;

Create Table Course
(
	Id Int Primary Key,
	LanguageId Int Foreign Key References LanguageL(Id),
	CourseTypeId Int Foreign Key References CourseType(Id),
	Price Float,
	TeacherId Int Foreign Key References Teacher(Id),
	StartDate Date,
	EndDate Date,
	Deleted Bit
) On Course;

Create Table Payment
(
	Id Int Primary Key,
	CourseId Int Foreign Key References Course(Id),
	StudentId Int Foreign Key References Student(Id),
	Amount Float,
	PaymentDate Date,
	Deleted Bit
) On School;

Create Table TeacherTeachesLanguage
(
	Id Int Primary Key,
	TeacherId Int Foreign Key References Teacher(Id),
	LanguageId Int Foreign Key References LanguageL(Id),
	Deleted Bit
) On Course;

Create Table StudentAttendsCourse
(
	Id Int Primary Key,
	CourseId Int Foreign Key References Course(Id),
	StudentId Int Foreign Key References Student(Id),
	Deleted Bit
) On Course;

Create Database TheLanguageSchool

Use TheLanguageSchool

Create Table School
(
	School_Id Int Not Null Primary Key,
	School_IdentificationNumber Char(8),
	School_Name Varchar(50),
	School_Address Varchar(100),
	School_PhoneNumber Varchar(15),
	School_Email Varchar(50),
	School_WebSite Varchar(30),
	School_Pib Char(9),
	School_AccountNumber Char(18)
) On School;

Create Table LanguageL
(
	Language_Id Int Primary Key Not Null Identity,
	Language_Name Varchar(30),
	Language_Deleted Bit
) On Course;

Create Table CourseType
(
	CourseType_Id Int Primary Key Not Null Identity,
	CourseType_Name Varchar(30),
	CourseType_Deleted Bit
) On Course;

Create Table Student
(
	Student_Id Int Primary Key Not Null Identity,
	Student_FirstName Varchar(30),
	Student_LastName Varchar(30),
	Student_Jmbg Char(13),
	Student_Address Varchar(50),
	Student_Deleted Bit
) On People;

Create Table Teacher
(
	Teacher_Id Int Primary Key Not Null Identity,
	Teacher_FirstName Varchar(30),
	Teacher_LastName Varchar(30),
	Teacher_Jmbg Char(13),
	Teacher_Address Varchar(50),
	Teacher_Deleted Bit
) On People;

Create Table UserU
(
	UserU_Id Int Primary Key Not Null Identity,
	UserU_FirstName Varchar(30),
	UserU_LastName Varchar(30),
	UserU_Jmbg Char(13),
	UserU_Address Varchar(50),
	UserU_Deleted Bit,
	UserU_UserName Varchar(20) Unique,
	UserU_PasswordP Varchar(20),
	UserU_UserRole Varchar(20)
) On People;

Create Table Course
(
	Course_Id Int Primary Key Not Null Identity,
	Course_LanguageId Int Foreign Key References LanguageL(Language_Id),
	Course_CourseTypeId Int Foreign Key References CourseType(CourseType_Id),
	Course_Price Float,
	Course_TeacherId Int Foreign Key References Teacher(Teacher_Id),
	Course_StartDate Date,
	Course_EndDate Date,
	Course_Deleted Bit
) On Course;

Create Table Payment
(
	Payment_Id Int Primary Key Not Null Identity,
	Payment_CourseId Int Foreign Key References Course(Course_Id),
	Payment_StudentId Int Foreign Key References Student(Student_Id),
	Payment_Amount Float,
	Payment_Date Date,
	Payment_Deleted Bit
) On School;

Create Table TeacherTeachesLanguage
(
	Teaches_Id Int Primary Key Not Null Identity,
	Teaches_TeacherId Int Foreign Key References Teacher(Teacher_Id),
	Teaches_LanguageId Int Foreign Key References LanguageL(Language_Id),
	Teaches_Deleted Bit
) On Course;

Create Table StudentAttendsCourse
(
	Attends_Id Int Primary Key Not Null Identity,
	Attends_CourseId Int Foreign Key References Course(Course_Id),
	Attends_StudentId Int Foreign Key References Student(Student_Id),
	Attends_Deleted Bit
) On Course;

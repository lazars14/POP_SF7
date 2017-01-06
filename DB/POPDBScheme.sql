Create Database TheLanguageSchool

Use TheLanguageSchool

Create Table School
(
	School_Id Int Not Null Primary Key,
	School_IdentificationNumber Char(8) Not Null,
	School_Name Varchar(50) Not Null,
	School_Address Varchar(100) Not Null,
	School_PhoneNumber Varchar(15) Not Null,
	School_Email Varchar(50) Not Null,
	School_WebSite Varchar(30) Not Null,
	School_Pib Char(9) Not Null,
	School_AccountNumber Char(20) Not Null
);

Create Table LanguageL
(
	Language_Id Int Primary Key Not Null Identity,
	Language_Name Varchar(30) Not Null,
	Language_Deleted Bit Not Null
);

Create Table CourseType
(
	CourseType_Id Int Primary Key Not Null Identity,
	CourseType_Name Varchar(30) Not Null,
	CourseType_Deleted Bit Not Null
);

Create Table Student
(
	Student_Id Int Primary Key Not Null Identity,
	Student_FirstName Varchar(30) Not Null,
	Student_LastName Varchar(30) Not Null,
	Student_Jmbg Char(13) Not Null,
	Student_Address Varchar(50) Not Null,
	Student_Deleted Bit Not Null
);

Create Table Teacher
(
	Teacher_Id Int Primary Key Not Null Identity,
	Teacher_FirstName Varchar(30) Not Null,
	Teacher_LastName Varchar(30) Not Null,
	Teacher_Jmbg Char(13) Not Null,
	Teacher_Address Varchar(50) Not Null,
	Teacher_Deleted Bit Not Null
);

Create Table UserU
(
	UserU_Id Int Primary Key Not Null Identity,
	UserU_FirstName Varchar(30) Not Null,
	UserU_LastName Varchar(30) Not Null,
	UserU_Jmbg Char(13) Not Null,
	UserU_Address Varchar(50) Not Null,
	UserU_Deleted Bit Not Null,
	UserU_UserName Varchar(20) Not Null Unique,
	UserU_PasswordP Varchar(20) Not Null,
	UserU_UserRole Varchar(20) Not Null
);

Create Table Course
(
	Course_Id Int Primary Key Not Null Identity,
	Course_LanguageId Int Not Null Foreign Key References LanguageL(Language_Id),
	Course_CourseTypeId Int Not Null Foreign Key References CourseType(CourseType_Id),
	Course_Price Float Not Null,
	Course_TeacherId Int Not Null Foreign Key References Teacher(Teacher_Id),
	Course_StartDate Date Not Null,
	Course_EndDate Date Not Null,
	Course_Deleted Bit Not Null
);

Create Table Payment
(
	Payment_Id Int Primary Key Not Null Identity,
	Payment_CourseId Int Not Null Foreign Key References Course(Course_Id),
	Payment_StudentId Int Not Null Foreign Key References Student(Student_Id),
	Payment_Amount Float Not Null,
	Payment_Date Date Not Null,
	Payment_Deleted Bit Not Null
);

Create Table TeacherTeachesLanguage
(
	Teaches_Id Int Primary Key Not Null Identity,
	Teaches_TeacherId Int Not Null Foreign Key References Teacher(Teacher_Id),
	Teaches_LanguageId Int Not Null Foreign Key References LanguageL(Language_Id),
	Teaches_Deleted Bit Not Null
);

Create Table StudentAttendsCourse
(
	Attends_Id Int Primary Key Not Null Identity,
	Attends_CourseId Int Not Null Foreign Key References Course(Course_Id),
	Attends_StudentId Int Not Null Foreign Key References Student(Student_Id),
	Attends_Deleted Bit Not Null
);

Create Table TeacherTeachesCourse
(
	TCourse_Id Int Primary Key Not Null Identity,
	TCourse_TeacherId Int Not Null Foreign Key References Teacher(Teacher_Id),
	TCourse_CourseId Int Not Null Foreign Key References Course(Course_Id),
	TCourse_Deleted Bit Not Null
);

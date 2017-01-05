Insert into School values(1, '12345678', 'The Language School', 'Pap Pavla 23', '021/123-456', 'theLanguageSchool@gmail.com', 'www.thelanguageschool.com', '123456789', '840-2133234231234-23');

Insert into LanguageL values('Engleski', 0);
Insert into LanguageL values('Spanski', 0);
Insert into LanguageL values('Nemacki', 0);
Insert into LanguageL values('Ruski', 0);
Insert into LanguageL values('Francuski', 0);
Insert into LanguageL values('Grcki', 0);
Insert into LanguageL values('Svedski', 0);

Insert into CourseType values('Pocetni', 0);
Insert into CourseType values('Srednji', 0);
Insert into CourseType values('Napredni', 0);

Insert into Student values('Luka', 'Maksimovic', '1234567891011', 'Adresa 1', 0);
Insert into Student values('Bogdan', 'Dragovic', '1234567891011', 'Adresa 2', 0);
Insert into Student values('Igor', 'Jevdjenic', '1234567891011', 'Adresa 3', 0);
Insert into Student values('Dejan', 'Pejic', '1234567891011', 'Adresa 4', 0);
Insert into Student values('Nemanja', 'Vucicevic', '1234567891011', 'Adresa 5', 0);
Insert into Student values('Nenad', 'Karanovic', '1234567891011', 'Adresa 6', 0);
Insert into Student values('Nemanja', 'Kapetanovic', '1234567891011', 'Adresa 7', 0);
Insert into Student values('Stefan', 'Fundic', '1234567891011', 'Adresa 8', 0);
Insert into Student values('Nenad', 'Nerandzic', '1234567891011', 'Adresa 9', 0);
Insert into Student values('Filip', 'Zekavcic', '1234567891011', 'Adresa 10', 0);

Insert into Teacher values('Uros', 'Carapic', '1234567891011', 'Adresa 1', 0);
Insert into Teacher values('Stefan', 'Mitrovic', '1234567891011', 'Adresa 2', 0);
Insert into Teacher values('Matija', 'Popovic', '1234567891011', 'Adresa 3', 0);
Insert into Teacher values('Marko', 'Radovanovic', '1234567891011', 'Adresa 4', 0);
Insert into Teacher values('Lazar', 'Markovic', '1234567891011', 'Adresa 5', 0);

Insert into UserU values('Marko', 'Radonjic', '1234567891011', 'Adresa 1', 0, 'marko', 'radonjic', 'ADMINISTRATOR');
Insert into UserU values('Aleksandar', 'Todorovic', '1234567891011', 'Adresa 2', 0, 'aleksandar', 'todorovic', 'EMPLOYEE');
Insert into UserU values('Drasko', 'Radovic', '1234567891011', 'Adresa 3', 0, 'drasko', 'radovic', 'EMPLOYEE');

Insert into Course(Course_LanguageId,Course_CourseTypeId,Course_Price,Course_TeacherId,Course_StartDate,Course_EndDate,Course_Deleted) values (1, 1, 24000.00, 1, '2017-01-01', '2018-01-01', 0);
Insert into Course(Course_LanguageId,Course_CourseTypeId,Course_Price,Course_TeacherId,Course_StartDate,Course_EndDate,Course_Deleted) values (2, 2, 34000.00, 2, '2017-03-01', '2018-03-01', 0);
Insert into Course(Course_LanguageId,Course_CourseTypeId,Course_Price,Course_TeacherId,Course_StartDate,Course_EndDate,Course_Deleted) values (3, 3, 30000.00, 3, '2017-02-01', '2018-02-01', 0);
Insert into Course(Course_LanguageId,Course_CourseTypeId,Course_Price,Course_TeacherId,Course_StartDate,Course_EndDate,Course_Deleted) values (4, 2, 27000.00, 4, '2017-04-01', '2018-04-01', 0);
Insert into Course(Course_LanguageId,Course_CourseTypeId,Course_Price,Course_TeacherId,Course_StartDate,Course_EndDate,Course_Deleted) values (5, 2, 28000.00, 5, '2017-05-01', '2018-05-01', 0);
Insert into Course(Course_LanguageId,Course_CourseTypeId,Course_Price,Course_TeacherId,Course_StartDate,Course_EndDate,Course_Deleted) values (6, 3, 31000.00, 1, '2017-06-01', '2018-06-01', 0);
Insert into Course(Course_LanguageId,Course_CourseTypeId,Course_Price,Course_TeacherId,Course_StartDate,Course_EndDate,Course_Deleted) values (7, 3, 32000.00, 4, '2017-07-01', '2018-07-01', 0);

Insert into TeacherTeachesLanguage(Teaches_TeacherId,Teaches_LanguageId,Teaches_Deleted) values (1,1,0);
Insert into TeacherTeachesLanguage(Teaches_TeacherId,Teaches_LanguageId,Teaches_Deleted) values (2,2,0);
Insert into TeacherTeachesLanguage(Teaches_TeacherId,Teaches_LanguageId,Teaches_Deleted) values (3,3,0);
Insert into TeacherTeachesLanguage(Teaches_TeacherId,Teaches_LanguageId,Teaches_Deleted) values (4,4,0);
Insert into TeacherTeachesLanguage(Teaches_TeacherId,Teaches_LanguageId,Teaches_Deleted) values (5,5,0);
Insert into TeacherTeachesLanguage(Teaches_TeacherId,Teaches_LanguageId,Teaches_Deleted) values (1,6,0);
Insert into TeacherTeachesLanguage(Teaches_TeacherId,Teaches_LanguageId,Teaches_Deleted) values (4,7,0);

Insert into TeacherTeachesCourse values (1,1,0);
Insert into TeacherTeachesCourse values (2,2,0);
Insert into TeacherTeachesCourse values (3,3,0);
Insert into TeacherTeachesCourse values (4,4,0);
Insert into TeacherTeachesCourse values (5,5,0);

Insert into StudentAttendsCourse(Attends_CourseId,Attends_StudentId,Attends_Deleted) values (1,1,0);
Insert into StudentAttendsCourse(Attends_CourseId,Attends_StudentId,Attends_Deleted) values (1,2,0);
Insert into StudentAttendsCourse(Attends_CourseId,Attends_StudentId,Attends_Deleted) values (1,3,0);
Insert into StudentAttendsCourse(Attends_CourseId,Attends_StudentId,Attends_Deleted) values (1,4,0);
Insert into StudentAttendsCourse(Attends_CourseId,Attends_StudentId,Attends_Deleted) values (1,5,0);
Insert into StudentAttendsCourse(Attends_CourseId,Attends_StudentId,Attends_Deleted) values (1,6,0);

Insert into Payment(Payment_CourseId,Payment_StudentId,Payment_Amount,Payment_Date,Payment_Deleted) values (1,1,1200.00,'2017-02-02', 0);
Insert into Payment(Payment_CourseId,Payment_StudentId,Payment_Amount,Payment_Date,Payment_Deleted) values (2,2,1200.00,'2017-03-02', 0);
Insert into Payment(Payment_CourseId,Payment_StudentId,Payment_Amount,Payment_Date,Payment_Deleted) values (3,3,1200.00,'2017-03-02', 0);
Insert into Payment(Payment_CourseId,Payment_StudentId,Payment_Amount,Payment_Date,Payment_Deleted) values (1,1,1200.00,'2017-05-02', 0);
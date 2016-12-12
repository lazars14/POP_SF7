Insert into School values(3245, 'The Language School', 'Pap Pavla 23', '021/123-456', 'theLanguageSchool@gmail.com', 'www.thelanguageschool.com', '123456789', '840-213323423-23');

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

Insert into Student values('Luka', 'Maksimovic', 'Adresa 1', '1234567891011', 0);
Insert into Student values('Bogdan', 'Dragovic', 'Adresa 2', '1234567891011', 0);
Insert into Student values('Igor', 'Jevdjenic', 'Adresa 3', '1234567891011', 0);
Insert into Student values('Dejan', 'Pejic', 'Adresa 4', '1234567891011', 0);
Insert into Student values('Nemanja', 'Vucicevic', 'Adresa 5', '1234567891011', 0);
Insert into Student values('Nenad', 'Karanovic', 'Adresa 6', '1234567891011', 0);
Insert into Student values('Nemanja', 'Kapetanovic', 'Adresa 7', '1234567891011', 0);
Insert into Student values('Stefan', 'Fundic', 'Adresa 8', '1234567891011', 0);
Insert into Student values('Nenad', 'Nerandzic', 'Adresa 9', '1234567891011', 0);
Insert into Student values('Filip', 'Zekavcic', 'Adresa 10', '1234567891011', 0);
Insert into Student values('Ugljesa', 'Bogdanovic', 'Adresa 11', '1234567891011', 0);
Insert into Student values('Stevan', 'Jeremic', 'Adresa 12', '1234567891011', 0);
Insert into Student values('Milan', 'Radivojevic', 'Adresa 13', '1234567891011', 0);
Insert into Student values('Aleksa', 'Jelic', 'Adresa 14', '1234567891011', 0);
Insert into Student values('Milos', 'Jankovic', 'Adresa 15', '1234567891011', 0);
Insert into Student values('Marko', 'Sarenac', 'Adresa 16', '1234567891011', 0);
Insert into Student values('Brano', 'Djukanovic', 'Adresa 17', '1234567891011', 0);
Insert into Student values('Aleksandar', 'Vasic', 'Adresa 18', '1234567891011', 0);
Insert into Student values('Nikola', 'Milutinovic', 'Adresa 19', '1234567891011', 0);
Insert into Student values('Marko', 'Dujkovic', 'Adresa 20', '1234567891011', 0);
Insert into Student values('Bojan', 'Bogdanovic', 'Adresa 21', '1234567891011', 0);
Insert into Student values('Antonije', 'Dimitrijevic', 'Adresa 22', '1234567891011', 0);
Insert into Student values('Danilo', 'Ostojic', 'Adresa 23', '1234567891011', 0);
Insert into Student values('Dusan', 'Hukic', 'Adresa 24', '1234567891011', 0);

Insert into Teacher values('Uros', 'Carapic', 'Adresa 1', '1234567891011', 0);
Insert into Teacher values('Stefan', 'Mitrovic', 'Adresa 2', '1234567891011', 0);
Insert into Teacher values('Matija', 'Popovic', 'Adresa 3', '1234567891011', 0);
Insert into Teacher values('Marko', 'Radovanovic', 'Adresa 4', '1234567891011', 0);
Insert into Teacher values('Lazar', 'Markovic', 'Adresa 5', '1234567891011', 0);

Insert into UserU values('Marko', 'Radonjic', 'Adresa 1', '1234567891011', 0, 'marko', 'radonjic', 'Administrator');
Insert into UserU values('Aleksandar', 'Todorovic', 'Adresa 2', '1234567891011', 0, 'aleksandar', 'todorovic', 'Employee');
Insert into UserU values('Drasko', 'Radovic', 'Adresa 3', '1234567891011', 0, 'drasko', 'radovic', 'Employee');

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

Insert into StudentAttendsCourse(Attends_CourseId,Attends_StudentId,Attends_Deleted) values (1,1,0);
Insert into StudentAttendsCourse(Attends_CourseId,Attends_StudentId,Attends_Deleted) values (1,2,0);
Insert into StudentAttendsCourse(Attends_CourseId,Attends_StudentId,Attends_Deleted) values (1,3,0);
Insert into StudentAttendsCourse(Attends_CourseId,Attends_StudentId,Attends_Deleted) values (1,4,0);
Insert into StudentAttendsCourse(Attends_CourseId,Attends_StudentId,Attends_Deleted) values (1,5,0);
Insert into StudentAttendsCourse(Attends_CourseId,Attends_StudentId,Attends_Deleted) values (1,6,0);

Insert into Payment(Payment_CourseId,Payment_StudentId,Payment_Amount,Payment_Date,Payment_Deleted) values (1,1,1200.00,'2017-02-02', 0);
Insert into Payment(Payment_CourseId,Payment_StudentId,Payment_Amount,Payment_Date,Payment_Deleted) values (1,1,1200.00,'2017-02-02', 0);
Insert into Payment(Payment_CourseId,Payment_StudentId,Payment_Amount,Payment_Date,Payment_Deleted) values (1,1,1200.00,'2017-02-02', 0);
Insert into Payment(Payment_CourseId,Payment_StudentId,Payment_Amount,Payment_Date,Payment_Deleted) values (1,1,1200.00,'2017-02-02', 0);

Insert into Payment(Payment_CourseId,Payment_StudentId,Payment_Amount,Payment_Date,Payment_Deleted) values (1,2,1200.00,'2017-03-02', 0);
Insert into Payment(Payment_CourseId,Payment_StudentId,Payment_Amount,Payment_Date,Payment_Deleted) values (1,2,1200.00,'2017-03-02', 0);
Insert into Payment(Payment_CourseId,Payment_StudentId,Payment_Amount,Payment_Date,Payment_Deleted) values (1,2,1200.00,'2017-03-02', 0);
Insert into Payment(Payment_CourseId,Payment_StudentId,Payment_Amount,Payment_Date,Payment_Deleted) values (1,2,1200.00,'2017-03-02', 0);
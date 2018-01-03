DROP DATABASE IF EXISTS StudyGroup;
CREATE DATABASE StudyGroup;
USE StudyGroup;

DROP TABLE IF EXISTS Students;
CREATE TABLE Students (
  Id int NOT NULL AUTO_INCREMENT,
  Username varchar(50) NOT NULL,
  Password varchar(50) NOT NULL,
  Email varchar(50) NOT NULL,
  Fname varchar(30) NOT NULL,
  Lname varchar(30) NOT NULL,
  Major varchar(30) DEFAULT NULL,
  Phone varchar(11) DEFAULT NULL,
  UNIQUE(Username),
  UNIQUE(Email),
  UNIQUE(Phone),
  PRIMARY KEY (Id)

) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS Courses;
CREATE TABLE Courses (
  Id int NOT NULL AUTO_INCREMENT,
  Name varchar(50) DEFAULT NULL,
  UNIQUE(Name),
  PRIMARY KEY (Id)

) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS Sections;
CREATE TABLE Sections (
  Id int NOT NULL AUTO_INCREMENT,
  Course_Id int NOT NULL,
  Section_Name varchar(50) DEFAULT NULL,
  PRIMARY KEY (Id),
  FOREIGN KEY (Course_Id)
      REFERENCES Courses (Id)

) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS Groups;
CREATE TABLE Groups (
  Id int NOT NULL AUTO_INCREMENT,
  Size int NOT NULL DEFAULT 5,
  Private bit NOT NULL DEFAULT FALSE,
  PRIMARY KEY (Id)

) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS StudentGroups;
CREATE TABLE StudentGroups (
  Student_Id int NOT NULL,
  Group_Id int NOT NULL,
  PRIMARY KEY (Student_Id, Group_Id),
  FOREIGN KEY (Group_Id)
      REFERENCES Groups(Id),
      ON DELETE CASCADE,
  FOREIGN KEY (Student_Id)
      REFERENCES Students(Id)
      ON DELETE CASCADE

) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS StudentGroupRequests;
CREATE TABLE StudentGroupRequests (
  Inviter_Id int NOT NULL,
  Student_Id int NOT NULL,
  Group_Id int NOT NULL,
  PRIMARY KEY (Student_Id, Group_Id),
  FOREIGN KEY (Group_Id)
      REFERENCES Groups(Id)
      ON DELETE CASCADE,
  FOREIGN KEY (Student_Id)
      REFERENCES Students(Id)
      ON DELETE CASCADE

) ENGINE=InnoDB DEFAULT CHARSET=latin1;
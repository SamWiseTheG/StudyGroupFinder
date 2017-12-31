DROP DATABASE IF EXISTS StudyGroup;
CREATE DATABASE StudyGroup;
USE StudyGroup;

DROP TABLE IF EXISTS Students;
CREATE TABLE Students (
  ID int NOT NULL AUTO_INCREMENT,
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
  PRIMARY KEY (ID)

) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS Courses;
CREATE TABLE Courses (
  ID int NOT NULL AUTO_INCREMENT,
  Name varchar(50) DEFAULT NULL,
  PRIMARY KEY (ID)

) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS Sections;
CREATE TABLE Sections (
  ID int NOT NULL AUTO_INCREMENT,
  Course_ID int NOT NULL,
  Section_Name varchar(50) DEFAULT NULL,
  PRIMARY KEY (ID),
  FOREIGN KEY (Course_ID)
      REFERENCES Courses (ID)

) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS Groups;
CREATE TABLE Groups (
  ID int NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (ID)

) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS StudentGroups;
CREATE TABLE StudentGroups (
  Student_ID int NOT NULL,
  Group_ID int NOT NULL,
  PRIMARY KEY (Student_ID, Group_ID),
  FOREIGN KEY (Group_ID)
      REFERENCES Groups(ID),
  FOREIGN KEY (Student_ID)
      REFERENCES Students(ID)

) ENGINE=InnoDB DEFAULT CHARSET=latin1;
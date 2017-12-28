DROP DATABASE IF EXISTS StudyGroup;
CREATE DATABASE StudyGroup;
USE StudyGroup;

DROP TABLE IF EXISTS Students;
CREATE TABLE Students (
  student_id int NOT NULL AUTO_INCREMENT,
  student_username varchar(50) NOT NULL,
  student_password varchar(50) NOT NULL,
  student_email varchar(50) NOT NULL,
  student_fname varchar(30) DEFAULT NULL,
  student_lname varchar(30) DEFAULT NULL,
  student_major varchar(30) DEFAULT NULL,  
  student_phone varchar(11) DEFAULT NULL,
  UNIQUE(student_username),
  UNIQUE(student_email),
  PRIMARY KEY (student_id)

) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS Courses;
CREATE TABLE Courses (
  course_id int NOT NULL,
  course_name varchar(50) DEFAULT NULL,
  PRIMARY KEY (course_id)

) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS Sections;
CREATE TABLE Sections (
  section_id int NOT NULL,
  course_id int NOT NULL,
  section_name varchar(50) DEFAULT NULL,
  PRIMARY KEY (section_id),
  FOREIGN KEY (course_id)
      REFERENCES Courses (course_id)

) ENGINE=InnoDB DEFAULT CHARSET=latin1; 

DROP TABLE IF EXISTS Groups;
CREATE TABLE Groups (
  group_id int NOT NULL,
  PRIMARY KEY (group_id)

) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS StudentGroups;
CREATE TABLE StudentGroups ( 
  student_id int NOT NULL, 
  group_id int NOT NULL,
  PRIMARY KEY (student_id, group_id),
  FOREIGN KEY (group_id) 
      REFERENCES Groups(group_id),
  FOREIGN KEY (student_id) 
      REFERENCES Students(student_id)

) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS StudentSections;
CREATE TABLE StudentClasses ( 
  student_id int NOT NULL, 
  section_id int NOT NULL,
  PRIMARY KEY (student_id, section_id),
  FOREIGN KEY (section_id) 
      REFERENCES Sections(section_id),
  FOREIGN KEY (student_id) 
      REFERENCES Students(student_id)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

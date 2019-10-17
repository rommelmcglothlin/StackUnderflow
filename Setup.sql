CREATE TABLE IF NOT EXISTS users
(
  id VARCHAR (255) NOT NULL,
  email VARCHAR(255) NOT NULL UNIQUE,
  username VARCHAR(255)NOT NULL,
  hash VARCHAR(255) NOT NULL,

  PRIMARY KEY(id)
);

CREATE TABLE IF NOT EXISTS questions
(
  id VARCHAR(255) NOT NULL,
  title VARCHAR(255) NOT NULL, 
  body VARCHAR(255)NOT NULL,
  authorid VARCHAR(255) NOT NULL, 
  answerid VARCHAR(255),
  questioncreated DATETIME,
  questionupdated DATETIME,

  FOREIGN KEY (authorid)
  REFERENCES users(id),
  PRIMARY KEY(id)
);

CREATE TABLE IF NOT EXISTS responses
(

  id VARCHAR(255) NOT NULL,
  body VARCHAR(255) NOT NULL,
  questionid VARCHAR(255) NOT NULL,
  authorid VARCHAR(255) NOT NULL,
  updated DATETIME,

  FOREIGN KEY (questionid)
  REFERENCES questions(id),
  FOREIGN KEY (authorid)
  REFERENCES users(id),
  PRIMARY KEY(id)
);

CREATE TABLE IF NOT EXISTS categories
(
  id VARCHAR(255) NOT NULL,
  name VARCHAR(255) NOT NULL UNIQUE,
  
  PRIMARY KEY(id)
);

CREATE TABLE IF NOT EXISTS categoryquestions
(
  id VARCHAR(255) NOT NULL,
  categoryid VARCHAR(255) NOT NULL,
  questionid VARCHAR(255) NOT NULL,

  FOREIGN KEY (questionid)
  REFERENCES questions(id),
  FOREIGN KEY (categoryid)
  REFERENCES categories(id),
  PRIMARY KEY (id)

);

-- DROP TABLE categoryquestions; 

-- DROP TABLE categories;

-- ALTER TABLE categories DROP UNIQUE name;
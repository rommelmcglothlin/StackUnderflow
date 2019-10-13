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
  answerid VARCHAR(255) NOT NULL,
  answered TINYINT DEFAULT 0,
  questioncreated DATETIME,
  questionupdated DATETIME,

  FOREIGN KEY (authorid)
  REFERENCES users(id),
  FOREIGN KEY (answerid)
  REFERENCES responses(id),
  PRIMARY KEY(id)
);

CRATE TABLE IF NOT EXISTS responses
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
  name VARCHAR(255) NOT NULL,
  
  PRIMARY KEY(id)
);

-- CREATE TABLE IF NOT EXISTS 
-- (
--   authorid VARCHAR(255) NOT NULL,
--   categoryid VARCHAR(255) NOT NULL,
--   questionid VARCHAR(255) NOT NULL,
--   answerid VARCHAR(255) NOT NULL,

--   FOREIGN KEY (questionid)
--   REFERENCES questions(id),
--   FOREIGN KEY (answerid)
--   REFERENCES responses(id),
--   FOREIGN KEY (authorid)
--   REFERENCES users(id),

-- )
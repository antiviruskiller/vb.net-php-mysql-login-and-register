never checked if mysql injection will work
dont for get to create new db and name it then add create table
databse: CREATE TABLE users (
  id varchar(45) NOT NULL,
  username varchar(45) NOT NULL,
  password varchar(45) NOT NULL,
  last_activity date NOT NULL,
  PRIMARY KEY (MANAGER_ID)
);
this should work

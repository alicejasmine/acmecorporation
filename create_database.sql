CREATE DATABASE AcmeDB;

USE AcmeDB
CREATE TABLE DrawEntries
(
    entry_ID      INT PRIMARY KEY IDENTITY (1,1),
    first_name    NVARCHAR(50)  NOT NULL,
    last_name     NVARCHAR(50)  NOT NULL,
    email_address NVARCHAR(255) NOT NULL,
    serial_number NVARCHAR(20)  NOT NULL
);

USE AcmeDB
CREATE TABLE ProductSerialNumbers
(
    Id            INT IDENTITY (1,1) PRIMARY KEY,
    serial_number NVARCHAR(20) NOT NULL UNIQUE
);
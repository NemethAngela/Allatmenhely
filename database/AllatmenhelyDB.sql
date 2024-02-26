DROP DATABASE IF EXISTS `ironbot1970`;
CREATE DATABASE `ironbot1970`;

USE `ironbot1970`;

DROP TABLE IF EXISTS `Users`;
CREATE TABLE `Users` (
  `Id` integer PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `Username` varchar(50) NOT NULL,
  `PasswordHash` varchar(100) NOT NULL COMMENT 'MD5 hashed pswd',
  `PasswordSalt` varchar(100) NOT NULL COMMENT 'MD5 hash-salt',
  `Email` varchar(50) NOT NULL,
  `Phone` varchar(20),
  `Mobile` varchar(20) NOT NULL,
  `FirstName` varchar(50) NOT NULL,
  `LastName` varchar(50) NOT NULL,
  `Country` varchar(50) NOT NULL,
  `City` varchar(30) NOT NULL,
  `Zip` varchar(5) NOT NULL,
  `Address` varchar(50) NOT NULL,
  `BillingAddressId` integer DEFAULT null,
  `Status` int NOT NULL DEFAULT 0 COMMENT '0: Validating, 1: Enabled, 2: Deleted',
  `IsValidated` bit NOT NULL DEFAULT 0,
  `ValidationUrl` varchar(250) NOT NULL,
  `IsAdmin` bit NOT NULL DEFAULT 0,
  `TimeStamp` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
);

DROP TABLE IF EXISTS `BillingAddresses`;
CREATE TABLE `BillingAddresses` (
  `Id` integer PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `FullName` varchar(100) NOT NULL,
  `VATNumber` varchar(20),
  `Country` varchar(50) NOT NULL,
  `City` varchar(30) NOT NULL,
  `Zip` varchar(5) NOT NULL,
  `Address` varchar(50) NOT NULL
);

DROP TABLE IF EXISTS `Robots`;
CREATE TABLE `Robots` (
  `Id` integer PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `Description` text DEFAULT null,
  `ThumbnailUrl` varchar(250) DEFAULT null,
  `PhotoUrl` varchar(250) DEFAULT null,
  `CategoryId` integer NOT NULL,
  `Manufacturer` varchar(50) DEFAULT null,
  `ManufactureYear` integer DEFAULT null,
  `RentalFee` decimal NOT NULL,
  `Deposit` decimal NOT NULL,
  `Status` int NOT NULL DEFAULT 0 COMMENT '0: Available, 1: OutOfOrder, 2: NotAvailable, 3: Retired'
);

DROP TABLE IF EXISTS `Categories`;
CREATE TABLE `Categories` (
  `Id` integer PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL COMMENT '0: Vacuum cleaner, 1: Drone, ..., 1000: Other',
  `Description` text DEFAULT null,
  `ImageUrl` varchar(250) DEFAULT null,
  `IsDeleted` bit NOT NULL DEFAULT 0
);

DROP TABLE IF EXISTS `Cart`;
CREATE TABLE `Cart` (
  `Id` integer PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `UserId` integer NOT NULL,
  `RobotId` integer NOT NULL,
  `StartDate` date NOT NULL,
  `EndDate` date NOT NULL,
  `TimeStamp` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
);

DROP TABLE IF EXISTS `Rentals`;
CREATE TABLE `Rentals` (
  `Id` integer PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `RentalGroup` varchar(25) NOT NULL COMMENT 'Generated from timestamp, should be the same on records in one rental transaction',
  `UserId` integer NOT NULL,
  `RobotId` integer NOT NULL,
  `StartDate` date NOT NULL,
  `EndDate` date NOT NULL,
  `RentalFee` decimal NOT NULL,
  `RentalDeposit` decimal NOT NULL,
  `RentalTotalAmount` decimal NOT NULL,
  `IsDepositPaid` bit NOT NULL DEFAULT 0,
  `IsRentalTotalAmountPaid` bit NOT NULL DEFAULT 0,
  `IsDepositRefunded` bit NOT NULL DEFAULT 0,
  `Status` integer NOT NULL DEFAULT 0 COMMENT '0: Open, 1: Closed, 2: Cancelled',
  `TimeStamp` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
);

ALTER TABLE `Users` ADD FOREIGN KEY (`BillingAddressId`) REFERENCES `BillingAddresses` (`Id`);

ALTER TABLE `Robots` ADD FOREIGN KEY (`CategoryId`) REFERENCES `Categories` (`Id`);

ALTER TABLE `Cart` ADD FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`);

ALTER TABLE `Cart` ADD FOREIGN KEY (`RobotId`) REFERENCES `Robots` (`Id`);

ALTER TABLE `Rentals` ADD FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`);

ALTER TABLE `Rentals` ADD FOREIGN KEY (`RobotId`) REFERENCES `Robots` (`Id`);
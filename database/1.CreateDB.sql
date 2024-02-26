
/*
DROP DATABASE IF EXISTS `defaultdb`;
CREATE DATABASE `defaultdb`;

USE `defaultdb`;
*/

DROP TABLE IF EXISTS `Animal`;
CREATE TABLE `Animal` (
  `Id` integer PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `KindId` integer NOT NULL,
  `Age` integer NOT NULL,
  `IsMale` bit NULL DEFAULT null,
  `IsNeutered` bit NOT NULL DEFAULT 0,
  `Description` text DEFAULT null,
  `Photo` text DEFAULT null,
  `IsActive` bit NOT NULL DEFAULT 1,
  `TimeStamp` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
);

DROP TABLE IF EXISTS `Kind`;
CREATE TABLE `Kind` (
  `Id` integer PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `Kind` varchar(50) NOT NULL
);

DROP TABLE IF EXISTS `Admin`;
CREATE TABLE `Admin` (
  `Id` integer PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `Email` varchar(50) NOT NULL,
  `PasswordHash` varchar(100) NOT NULL,
  `PasswordSalt` varchar(100) NOT NULL
);

DROP TABLE IF EXISTS `Enquery`;
CREATE TABLE `Enquery` (
  `Id` integer PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `TimeStamp` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Phone` varchar(20),
  `AnimalId` integer NOT NULL,
  `Email` varchar(50) NOT NULL
);

ALTER TABLE `Animal` ADD FOREIGN KEY (`KindId`) REFERENCES `Kind` (`Id`);

ALTER TABLE `Enquery` ADD FOREIGN KEY (`AnimalId`) REFERENCES `Animal` (`Id`);

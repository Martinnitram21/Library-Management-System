-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: librarydb
-- ------------------------------------------------------
-- Server version	8.0.36

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `transactions_tbl`
--

DROP TABLE IF EXISTS `transactions_tbl`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `transactions_tbl` (
  `transaction_id` int NOT NULL AUTO_INCREMENT,
  `member_id` int DEFAULT NULL,
  `book_id` int DEFAULT NULL,
  `borrow_date` datetime DEFAULT NULL,
  `due_date` datetime DEFAULT NULL,
  `return_date` datetime DEFAULT NULL,
  `fine` decimal(10,2) DEFAULT NULL,
  `status` enum('Borrowed','Returned','Overdue') DEFAULT 'Borrowed',
  PRIMARY KEY (`transaction_id`),
  KEY `FK_member_idx` (`member_id`),
  KEY `FK_book_id_idx` (`book_id`),
  CONSTRAINT `FK_book_id` FOREIGN KEY (`book_id`) REFERENCES `books_tbl` (`book_id`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `FK_member_id` FOREIGN KEY (`member_id`) REFERENCES `members_tbl` (`member_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `transactions_tbl`
--

LOCK TABLES `transactions_tbl` WRITE;
/*!40000 ALTER TABLE `transactions_tbl` DISABLE KEYS */;
INSERT INTO `transactions_tbl` VALUES (1,1,1,'1000-01-01 00:00:00','1000-01-01 00:00:00',NULL,100.00,'Borrowed');
/*!40000 ALTER TABLE `transactions_tbl` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-11-28  9:46:05

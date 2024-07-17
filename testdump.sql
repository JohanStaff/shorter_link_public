-- MySQL dump 10.13  Distrib 8.3.0, for macos13.6 (arm64)
--
-- Host: localhost    Database: shorter_links
-- ------------------------------------------------------
-- Server version	8.3.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Current Database: `shorter_links`
--

CREATE DATABASE /*!32312 IF NOT EXISTS*/ `shorter_links` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `shorter_links`;

--
-- Table structure for table `actual_subscriptions`
--

DROP TABLE IF EXISTS `actual_subscriptions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `actual_subscriptions` (
  `user_id` bigint unsigned NOT NULL,
  `subscription_id` bigint unsigned NOT NULL DEFAULT '0',
  `updated_ts` datetime DEFAULT CURRENT_TIMESTAMP,
  `expires_ts` datetime NOT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `actual_subscriptions`
--

LOCK TABLES `actual_subscriptions` WRITE;
/*!40000 ALTER TABLE `actual_subscriptions` DISABLE KEYS */;
/*!40000 ALTER TABLE `actual_subscriptions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `banned_domains`
--

DROP TABLE IF EXISTS `banned_domains`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `banned_domains` (
  `domain` varchar(2048) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `banned_domains`
--

LOCK TABLES `banned_domains` WRITE;
/*!40000 ALTER TABLE `banned_domains` DISABLE KEYS */;
/*!40000 ALTER TABLE `banned_domains` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `devices`
--

DROP TABLE IF EXISTS `devices`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `devices` (
  `user_id` bigint unsigned NOT NULL,
  `device_id` varchar(128) NOT NULL,
  `added_ts` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `devices`
--

LOCK TABLES `devices` WRITE;
/*!40000 ALTER TABLE `devices` DISABLE KEYS */;
INSERT INTO `devices` VALUES (0,'c7b8afd0-74d8-4918-95ea-caa574fa799e','2024-07-14 12:53:31'),(0,'2444c406-1bf7-4939-85c1-4df6ee9513ce','2024-07-14 13:04:07'),(4,'2444c406-1bf7-4939-85c1-4df6ee9513ce','2024-07-14 13:06:33'),(0,'9426b518-b014-4644-99e4-fef1af06c462','2024-07-14 17:17:15'),(4,'9426b518-b014-4644-99e4-fef1af06c462','2024-07-14 17:19:01');
/*!40000 ALTER TABLE `devices` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `events`
--

DROP TABLE IF EXISTS `events`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `events` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `user_id` bigint unsigned NOT NULL,
  `action_id` int NOT NULL,
  `message` varchar(2048) DEFAULT NULL,
  `device_id` varchar(38) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `added_ts` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `events`
--

LOCK TABLES `events` WRITE;
/*!40000 ALTER TABLE `events` DISABLE KEYS */;
INSERT INTO `events` VALUES (2,0,100,'Shorted https://backrooms.ru/index.php/База_данных_Закулисья into localhost:5027/go/t36dAiw3OU','fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-13 18:19:49'),(3,0,100,'Shorted https://google.com/ into localhost:5027/go/89exjE_9g0','fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-13 18:20:20'),(4,4,100,'Shorted https://en.wikipedia.org/wiki/California into localhost:5027/go/Zv_gi4PtMk','fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-13 18:40:55'),(5,4,100,'Shorted https://en.wikipedia.org/wiki/Sacramento,_California into localhost:5027/go/yMK1KrZXHk','fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-13 19:12:16'),(6,4,100,'Shorted www.google.com into localhost:5027/go/moxBQ_K2Ak','fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-13 23:10:15'),(7,4,100,'Shorted https://en.wikipedia.org/wiki/Wikipedia:Six_degrees_of_Wikipedia into localhost:5027/go/IgF3UO6Z00','fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-13 23:40:38'),(8,0,100,'Shorted www.google.com into localhost:5027/go/xJ9OO_ldVk','fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-14 11:30:00'),(9,0,100,'Shorted www.google.com into localhost:5027/go/x3Xon4dx90','fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-14 11:33:08'),(10,0,100,'Shorted www.google.com into localhost:5027/go/0FHjeK8e_E','fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-14 11:33:13'),(11,0,100,'Shorted www.google.com into localhost:5027/go/cayg7mcW5k','fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-14 11:33:13'),(12,0,100,'Shorted www.google.com into localhost:5027/go/9ZK8n_D4eU','fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-14 11:33:14'),(13,0,100,'Shorted www.google.com into localhost:5027/go/FBsV6rguJ0','fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-14 11:33:14'),(14,0,100,'Shorted www.google.com into localhost:5027/go/Uq7URUUvXk','fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-14 11:33:15'),(15,0,100,'Shorted www.google.com into localhost:5027/go/wC-dkLAi2E','fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-14 11:33:15'),(16,0,100,'Shorted www.google.com into localhost:5027/go/eDIwaUqwiU','fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-14 11:33:16'),(17,0,100,'Shorted www.google.com into localhost:5027/go/tqyEIAt070','fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-14 11:33:16'),(18,0,100,'Shorted www.google.com into localhost:5027/go/qKhaCXMG-k','fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-14 11:33:17'),(19,0,100,'Shorted www.google.com into localhost:5027/go/qPC2ZoHQcE','fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-14 11:33:17'),(20,0,100,'Shorted www.google.com into localhost:5027/go/c4AQCos7Z0','fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-14 11:33:18'),(21,0,100,'Shorted www.google.com into localhost:5027/go/tjRG6NhinU','fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-14 11:33:18'),(22,0,100,'Shorted www.google.com into localhost:5027/go/ibTnbfnA1U','fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-14 11:33:18'),(23,4,100,'Shorted www.google.com into localhost:5027/go/88A4KE1080','fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-14 11:33:51'),(24,4,100,'Shorted www.google.com into localhost:5027/go/nBE8ldG_cU','fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-14 11:33:51'),(25,0,100,'Shorted www.google.com into localhost:5027/go/HMViP1Xfg0','9426b518-b014-4644-99e4-fef1af06c462','2024-07-14 17:30:13'),(26,0,100,'Shorted www.google.com into localhost:5027/go/0xXWtyKQtU','9426b518-b014-4644-99e4-fef1af06c462','2024-07-14 17:55:19'),(27,0,100,'Shorted https://www.spiralclick.com/ into localhost:5027/go/6Y9wWIw4a0','9426b518-b014-4644-99e4-fef1af06c462','2024-07-14 17:56:51'),(28,0,100,'Shorted https://www.spiralclick.com/jsakfdjaksdjfkjskdfjkasdfjkasdf into localhost:5027/go/R1C8nectWE','9426b518-b014-4644-99e4-fef1af06c462','2024-07-14 17:56:58'),(29,0,100,'Shorted google.com into localhost:5027/go/ibthNnLgDU','9426b518-b014-4644-99e4-fef1af06c462','2024-07-14 19:25:29'),(30,0,100,'Shorted google.com into localhost:5027/go/RjPJpZ178U','9426b518-b014-4644-99e4-fef1af06c462','2024-07-14 19:26:07');
/*!40000 ALTER TABLE `events` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `link_stat`
--

DROP TABLE IF EXISTS `link_stat`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `link_stat` (
  `link_id` bigint unsigned NOT NULL,
  `visits` int unsigned DEFAULT '0',
  PRIMARY KEY (`link_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `link_stat`
--

LOCK TABLES `link_stat` WRITE;
/*!40000 ALTER TABLE `link_stat` DISABLE KEYS */;
INSERT INTO `link_stat` VALUES (1,0),(2,0),(3,0),(4,13),(5,2),(6,2),(7,3),(8,1),(9,1),(10,0),(11,0),(12,0),(13,0),(14,0),(15,0),(16,0),(17,0),(18,0),(19,0),(20,0),(21,0),(22,0),(23,0),(24,1),(25,1),(26,1),(28,1);
/*!40000 ALTER TABLE `link_stat` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `links`
--

DROP TABLE IF EXISTS `links`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `links` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `original_url` varchar(2048) NOT NULL,
  `hash_url` varchar(2048) NOT NULL,
  `mask_url` varchar(2048) DEFAULT NULL,
  `user_id` bigint unsigned DEFAULT NULL,
  `name` varchar(2048) NOT NULL,
  `active` tinyint(1) NOT NULL DEFAULT '1',
  `added_ts` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `links`
--

LOCK TABLES `links` WRITE;
/*!40000 ALTER TABLE `links` DISABLE KEYS */;
INSERT INTO `links` VALUES (1,'https://www.spiralclick.com/','f7FZgK3EP0','',0,'https://www.spiralclick.com/',1,'2024-07-13 12:34:15'),(2,'https://backrooms.ru/index.php/База_данных_Закулисья','t36dAiw3OU','',0,'https://backrooms.ru/index.php/База_данных_Закулисья',1,'2024-07-13 18:19:49'),(3,'https://google.com/','89exjE_9g0','',0,'https://google.com/',1,'2024-07-13 18:20:20'),(4,'https://en.wikipedia.org/wiki/California','Zv_gi4PtMk','',4,'https://en.wikipedia.org/wiki/California',1,'2024-07-13 18:40:55'),(5,'https://en.wikipedia.org/wiki/Sacramento,_California','yMK1KrZXHk','',4,'https://en.wikipedia.org/wiki/Sacramento,_California',1,'2024-07-13 19:12:16'),(6,'www.google.com','moxBQ_K2Ak','',4,'www.google.com',1,'2024-07-13 23:10:15'),(7,'https://en.wikipedia.org/wiki/Wikipedia:Six_degrees_of_Wikipedia','IgF3UO6Z00','',4,'https://en.wikipedia.org/wiki/Wikipedia:Six_degrees_of_Wikipedia',1,'2024-07-13 23:40:38'),(8,'www.google.com','xJ9OO_ldVk','',0,'www.google.com',1,'2024-07-14 11:30:00'),(9,'www.google.com','x3Xon4dx90','',0,'www.google.com',1,'2024-07-14 11:33:08'),(10,'www.google.com','0FHjeK8e_E','',0,'www.google.com',1,'2024-07-14 11:33:13'),(11,'www.google.com','cayg7mcW5k','',0,'www.google.com',1,'2024-07-14 11:33:13'),(12,'www.google.com','9ZK8n_D4eU','',0,'www.google.com',1,'2024-07-14 11:33:14'),(13,'www.google.com','FBsV6rguJ0','',0,'www.google.com',1,'2024-07-14 11:33:14'),(14,'www.google.com','Uq7URUUvXk','',0,'www.google.com',1,'2024-07-14 11:33:15'),(15,'www.google.com','wC-dkLAi2E','',0,'www.google.com',1,'2024-07-14 11:33:15'),(16,'www.google.com','eDIwaUqwiU','',0,'www.google.com',1,'2024-07-14 11:33:16'),(17,'www.google.com','tqyEIAt070','',0,'www.google.com',1,'2024-07-14 11:33:16'),(18,'www.google.com','qKhaCXMG-k','',0,'www.google.com',1,'2024-07-14 11:33:17'),(19,'www.google.com','qPC2ZoHQcE','',0,'www.google.com',1,'2024-07-14 11:33:17'),(20,'www.google.com','c4AQCos7Z0','',0,'www.google.com',1,'2024-07-14 11:33:18'),(21,'www.google.com','tjRG6NhinU','',0,'www.google.com',1,'2024-07-14 11:33:18'),(22,'www.google.com','ibTnbfnA1U','',0,'www.google.com',1,'2024-07-14 11:33:18'),(23,'www.google.com','88A4KE1080','',4,'www.google.com',1,'2024-07-14 11:33:51'),(24,'www.google.com','nBE8ldG_cU','',4,'www.google.com',1,'2024-07-14 11:33:51'),(25,'www.google.com','HMViP1Xfg0','',0,'www.google.com',1,'2024-07-14 17:30:13'),(26,'www.google.com','0xXWtyKQtU','',0,'www.google.com',1,'2024-07-14 17:55:19'),(27,'https://www.spiralclick.com/','6Y9wWIw4a0','',0,'https://www.spiralclick.com/',1,'2024-07-14 17:56:51'),(28,'https://www.spiralclick.com/jsakfdjaksdjfkjskdfjkasdfjkasdf','R1C8nectWE','',0,'https://www.spiralclick.com/jsakfdjaksdjfkjskdfjkasdfjkasdf',1,'2024-07-14 17:56:58'),(29,'google.com','ibthNnLgDU','',0,'google.com',1,'2024-07-14 19:25:29'),(30,'google.com','RjPJpZ178U','',0,'google.com',1,'2024-07-14 19:26:07');
/*!40000 ALTER TABLE `links` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `subscription_plans`
--

DROP TABLE IF EXISTS `subscription_plans`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `subscription_plans` (
  `id` bigint unsigned NOT NULL,
  `title` varchar(128) DEFAULT NULL,
  `description` longtext,
  `period` int unsigned DEFAULT NULL,
  `group` int NOT NULL,
  `price` float NOT NULL DEFAULT '0',
  `links_per_month` int unsigned NOT NULL DEFAULT '5',
  `alias_edition` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `subscription_plans`
--

LOCK TABLES `subscription_plans` WRITE;
/*!40000 ALTER TABLE `subscription_plans` DISABLE KEYS */;
INSERT INTO `subscription_plans` VALUES (0,'Free Plan','Free!',10000,-1,0,5,0),(100,'Basic','Paid plan',31,1,3,20,0),(150,'Premium','Paid plan',31,1,5,30,1);
/*!40000 ALTER TABLE `subscription_plans` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `id` bigint unsigned NOT NULL AUTO_INCREMENT,
  `username` varchar(128) NOT NULL,
  `email` varchar(320) NOT NULL,
  `password` varchar(256) NOT NULL,
  `active` tinyint(1) NOT NULL DEFAULT '1',
  `device_id` varchar(38) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `added_ts` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `users_unique` (`username`),
  UNIQUE KEY `users_unique_1` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'test@test.com','test@test.com','$P$CeWKo4.xqYZ/L3x4AtVj5pzT5tesGe/',1,'fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-13 18:32:09'),(2,'test1@test.com','test1@test.com','$P$C4ZGfQNdQbXJ5zi8p1na4XYlX3XCGh.',1,'fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-13 18:36:46'),(3,'test2@test.com','test2@test.com','$P$C/ik9BcTxqEd4I0Ns9HKcNtxCl2GVw0',1,'fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-13 18:38:25'),(4,'test3@test.com','test3@test.com','$P$C6zDNh974S1yFr82RB/rdKkd/mgJnY/',1,'fa981eae-fc21-4ef7-89c7-d1bffe19edec','2024-07-13 18:38:52');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-07-15  8:52:57

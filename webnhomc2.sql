-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               10.4.32-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win64
-- HeidiSQL Version:             12.6.0.6765
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for webnhomc2
CREATE DATABASE IF NOT EXISTS `webnhomc2` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;
USE `webnhomc2`;

-- Dumping structure for table webnhomc2.categorys
CREATE TABLE IF NOT EXISTS `categorys` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext NOT NULL,
  `Thumbnail` longtext NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table webnhomc2.categorys: ~6 rows (approximately)
INSERT INTO `categorys` (`Id`, `Name`, `Thumbnail`) VALUES
	(1, 'Nhẫn bạc', 'https://cdn.pnj.io/images/promo/148/button-nhan.png'),
	(2, 'Dây chuyền bạc', 'https://cdn.pnj.io/images/promo/148/button-daychuyen.png'),
	(3, 'Lắc tay bạc', 'https://cdn.pnj.io/images/promo/148/button-lac.png'),
	(4, 'Bông tai bạc', 'https://cdn.pnj.io/images/promo/148/button-bongtai.png'),
	(5, 'Lắc chân bạc', 'https://cdn.pnj.io/images/promo/148/button-lac.png'),
	(6, 'Charm bạc', 'https://tse4.mm.bing.net/th?id=OIP.7iXJQQ_-51C33Hl2autDyQHaE8&pid=Api&P=0&h=220');

-- Dumping structure for table webnhomc2.discounts
CREATE TABLE IF NOT EXISTS `discounts` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext NOT NULL,
  `Discount_Value` double NOT NULL,
  `Start_Date` datetime(6) NOT NULL,
  `End_Date` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table webnhomc2.discounts: ~4 rows (approximately)
INSERT INTO `discounts` (`Id`, `Name`, `Discount_Value`, `Start_Date`, `End_Date`) VALUES
	(1, 'Giảm giá Black Friday', 50, '2024-11-25 00:00:00.000000', '2024-11-30 00:00:00.000000'),
	(3, 'Giảm giá khách hàng mới', 15, '2024-11-01 00:00:00.000000', '2025-01-01 00:00:00.000000'),
	(4, 'Ưu đãi VIP - Giảm giá 30%', 30, '2024-11-20 00:00:00.000000', '2024-12-31 00:00:00.000000'),
	(5, 'Giảm giá Flash Sale', 75.5, '2024-11-23 00:00:00.000000', '2024-11-23 00:00:00.000000');

-- Dumping structure for table webnhomc2.images
CREATE TABLE IF NOT EXISTS `images` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Pid` int(11) NOT NULL,
  `Image1` longtext DEFAULT NULL,
  `Image2` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_images_Pid` (`Pid`),
  CONSTRAINT `FK_images_products_Pid` FOREIGN KEY (`Pid`) REFERENCES `products` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table webnhomc2.images: ~29 rows (approximately)
INSERT INTO `images` (`Id`, `Pid`, `Image1`, `Image2`) VALUES
	(1, 1, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/216/sp-snxmxmk000167-nhan-nam-bac-dinh-da-pnjsilver-1.png', 'https://cdn.pnj.io/images/detailed/228/sp-snxm00w060021-nhan-bac-dinh-da-pnjsilver-2.png'),
	(2, 2, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/114/snxmxmw060030-nhan-bac-dinh-da-pnjsilver-1.png', 'https://cdn.pnj.io/images/detailed/228/sp-snxm00w060021-nhan-bac-dinh-da-pnjsilver-2.png'),
	(3, 3, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/226/sp-snxm00w060022-nhan-nam-bac-dinh-da-pnjsilver-1.png', 'https://cdn.pnj.io/images/detailed/226/sp-snxm00w060022-nhan-nam-bac-dinh-da-pnjsilver-2.png'),
	(4, 4, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/226/sp-snxm00w060014-nhan-bac-dinh-da-pnjsilver-1.png', 'https://cdn.pnj.io/images/detailed/226/sp-snxm00w060014-nhan-bac-dinh-da-pnjsilver-2.png'),
	(5, 5, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/226/sp-snxmxmw060186-nhan-bac-dinh-da-pnjsilver-1.png', 'https://cdn.pnj.io/images/detailed/226/sp-snxmxmw060186-nhan-bac-dinh-da-pnjsilver-2.png'),
	(6, 6, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/218/sp-sd0000w060105-day-chuyen-nam-bac-y-dinh-pnjsilver-1.png', 'https://cdn.pnj.io/images/detailed/218/sp-sd0000w060105-day-chuyen-nam-bac-y-dinh-pnjsilver-2.png'),
	(7, 7, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/199/sp-sm0000y000033-mat-day-chuyen-bac-dinh-da-pnjsilver-kim-bao-nhu-y-1.png', 'https://cdn.pnj.io/images/detailed/199/sp-sm0000y000033-mat-day-chuyen-bac-dinh-da-pnjsilver-kim-bao-nhu-y-2.png'),
	(8, 8, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/199/sp-smxmxmy000009-mat-day-chuyen-bac-dinh-da-pnjsilver-kim-bao-nhu-y-1.png', 'https://cdn.pnj.io/images/detailed/197/sp-smxmxmy000009-mat-day-chuyen-bac-dinh-da-pnjsilver-kim-bao-nhu-y-2.png'),
	(9, 9, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/217/sp-smzt00w060009-mat-day-chuyen-bac-dinh-da-disney-pnj-inside-out-2-1.png', 'https://cdn.pnj.io/images/detailed/217/sp-smzt00w060009-mat-day-chuyen-bac-dinh-da-disney-pnj-inside-out-2-2.png'),
	(10, 10, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/202/sp-smxm00w000035-mat-day-chuyen-bac-dinh-ngoc-trai-pnjsilver-1.png', 'https://cdn.pnj.io/images/detailed/202/sp-smxm00w000035-mat-day-chuyen-bac-dinh-ngoc-trai-pnjsilver-2.png'),
	(11, 11, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/227/sp-slxmxmw060232-lac-tay-bac-dinh-da-pnjsilver-1.png', 'https://cdn.pnj.io/images/detailed/227/sp-slxmxmw060232-lac-tay-bac-dinh-da-pnjsilver-2.png'),
	(12, 12, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/227/sp-slztmxw000010-lac-tay-bac-dinh-da-pnjsilver-1.png', 'https://cdn.pnj.io/images/detailed/227/sp-slztmxw000010-lac-tay-bac-dinh-da-pnjsilver-2.png'),
	(13, 13, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/226/sp-slxm00w060039-lac-tay-bac-y-dinh-pnjsilver-1.png', 'https://cdn.pnj.io/images/detailed/226/sp-slxm00w060039-lac-tay-bac-y-dinh-pnjsilver-2.png'),
	(14, 14, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/225/sp-slxmxmw060211-lac-tay-bac-dinh-da-pnjsilver-1.png', 'https://cdn.pnj.io/images/detailed/225/sp-slxmxmw060211-lac-tay-bac-dinh-da-pnjsilver-2.png'),
	(15, 15, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/222/sp-slxm00w060038-lac-tay-bac-dinh-da-pnjsilver-1.png', 'https://cdn.pnj.io/images/detailed/222/sp-slxm00w060038-lac-tay-bac-dinh-da-pnjsilver-2.png'),
	(16, 16, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/225/sp-sbxmxmw060230-bong-tai-bac-dinh-da-pnjsilver-1.png', ''),
	(17, 17, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/225/sp-sbxmxmw060229-bong-tai-bac-dinh-da-pnjsilver-1.png', 'https://cdn.pnj.io/images/detailed/225/sp-sbxmxmw060229-bong-tai-bac-dinh-da-pnjsilver-2.png'),
	(18, 18, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/225/sp-sbxmxmw060227-bong-tai-bac-dinh-da-pnjsilver-1.png', 'https://cdn.pnj.io/images/detailed/225/sp-sbxmxmw060227-bong-tai-bac-dinh-da-pnjsilver-2.png'),
	(19, 19, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/221/sp-sbxmxmw000089-bong-tai-bac-dinh-da-pnjsilver-1.png', 'https://cdn.pnj.io/images/detailed/221/sp-sbxmxmw000089-bong-tai-bac-dinh-da-pnjsilver-2.png'),
	(20, 20, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/221/sp-sbpfxmw000027-bong-tai-bac-dinh-da-pnjsilver-1.png', 'https://cdn.pnj.io/images/detailed/221/sp-sbpfxmw000027-bong-tai-bac-dinh-da-pnjsilver-2.png'),
	(21, 21, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/219/sp-sl0000w060223-lac-chan-bac-dinh-da-pnjsilver.png', 'https://cdn.pnj.io/images/detailed/218/sp-sl0000w060223-lac-chan-bac-dinh-da-pnjsilver-2.png'),
	(22, 22, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/209/sp-slxm00k000021-lac-chan-bac-dinh-da-pnjsilver-hinh-trai-tim-1.png', 'https://cdn.pnj.io/images/detailed/209/sp-slxm00k000021-lac-chan-bac-dinh-da-pnjsilver-hinh-trai-tim-2.png'),
	(23, 23, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/142/slxmxmk000042-lac-chan-bac-dinh-da-pnjsilver.png', 'https://cdn.pnj.io/images/detailed/142/slxmxmk000042-lac-chan-bac-dinh-da-pnjsilver-01.png'),
	(24, 24, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/131/gl0000w000652-lac-chan-vang-trang-18k-pnj-1.png', 'https://cdn.pnj.io/images/detailed/131/gl0000w000652-lac-chan-vang-trang-18k-pnj-2.png'),
	(25, 25, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/99/ti0000x000002-phu-kien-charm-style-by-pnj-001.png', 'https://cdn.pnj.io/images/detailed/99/ti0000x000002-phu-kien-charm-style-by-pnj-002.png'),
	(26, 26, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/83/0000y000053-hat-charm-bac-treo-pnjstyle-1.png', 'https://cdn.pnj.io/images/detailed/83/0000y000053-hat-charm-bac-treo-pnjstyle-2.png'),
	(27, 27, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/83/si0000y000034-hat-charm-treo-bac-pnjstyle-dna-chu-g.png', 'https://cdn.pnj.io/images/detailed/83/si0000y000034-hat-charm-treo-bac-pnjstyle-dna-chu-g-01.png'),
	(28, 28, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/83/0000y000050-hat-charm-bac-treo-pnjstyle-3_9mmi-ur.png', 'https://cdn.pnj.io/images/detailed/83/0000y000050-hat-charm-bac-treo-pnjstyle-2.png'),
	(29, 29, 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/83/0000y000052-hat-charm-bac-treo-pnjstyle-2_wf7x-1l.png', 'https://cdn.pnj.io/images/detailed/83/0000y000052-hat-charm-bac-treo-pnjstyle-3_mbzc-tb.png');

-- Dumping structure for table webnhomc2.orderdetails
CREATE TABLE IF NOT EXISTS `orderdetails` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `OrderId` int(11) NOT NULL,
  `ProductId` int(11) NOT NULL,
  `Price` double NOT NULL,
  `Quantity` int(11) NOT NULL,
  `SubTotal` double NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_orderDetails_OrderId` (`OrderId`),
  KEY `IX_orderDetails_ProductId` (`ProductId`),
  CONSTRAINT `FK_orderDetails_orders_OrderId` FOREIGN KEY (`OrderId`) REFERENCES `orders` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_orderDetails_products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table webnhomc2.orderdetails: ~4 rows (approximately)
INSERT INTO `orderdetails` (`Id`, `OrderId`, `ProductId`, `Price`, `Quantity`, `SubTotal`) VALUES
	(7, 4, 1, 100, 1, 100),
	(8, 4, 2, 100, 1, 100),
	(25, 13, 1, 100, 1, 100),
	(26, 13, 2, 100, 1, 100);

-- Dumping structure for table webnhomc2.orders
CREATE TABLE IF NOT EXISTS `orders` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` int(11) NOT NULL,
  `FullName` longtext NOT NULL,
  `Email` longtext NOT NULL,
  `PhoneNumber` longtext NOT NULL,
  `Address` longtext NOT NULL,
  `Note` longtext DEFAULT NULL,
  `OrderDate` datetime(6) NOT NULL,
  `Status` longtext NOT NULL,
  `TotalMoney` double NOT NULL,
  `PaymentMethod` longtext NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_orders_UserId` (`UserId`),
  CONSTRAINT `FK_orders_users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table webnhomc2.orders: ~2 rows (approximately)
INSERT INTO `orders` (`Id`, `UserId`, `FullName`, `Email`, `PhoneNumber`, `Address`, `Note`, `OrderDate`, `Status`, `TotalMoney`, `PaymentMethod`) VALUES
	(4, 1, 'Lê Tuấn Phát', 'ltphat2401034444@gmail.com', '0935822771', 'Ninh hòa', NULL, '2024-12-15 06:51:11.305327', 'confirmed', 200, 'Tiền mặt'),
	(13, 1, 'Lê Tuấn Phát', 'ltphat2401034444@gmail.com', '0935822771', 'Ninh hòa', NULL, '2024-12-15 07:50:31.605118', 'pending', 200, 'Ví VNPay');

-- Dumping structure for table webnhomc2.products
CREATE TABLE IF NOT EXISTS `products` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` longtext NOT NULL,
  `Brand` longtext NOT NULL,
  `Price` double NOT NULL,
  `Discount` int(11) NOT NULL,
  `Warranty` int(11) NOT NULL,
  `InventoryNumber` int(11) NOT NULL,
  `Description` longtext NOT NULL,
  `Thumbnail` longtext NOT NULL,
  `CreateAt` datetime(6) NOT NULL,
  `UpdateAt` datetime(6) NOT NULL,
  `Cid` int(11) NOT NULL,
  `NumOfPur` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_products_Cid` (`Cid`),
  CONSTRAINT `FK_products_categorys_Cid` FOREIGN KEY (`Cid`) REFERENCES `categorys` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table webnhomc2.products: ~29 rows (approximately)
INSERT INTO `products` (`Id`, `Title`, `Brand`, `Price`, `Discount`, `Warranty`, `InventoryNumber`, `Description`, `Thumbnail`, `CreateAt`, `UpdateAt`, `Cid`, `NumOfPur`) VALUES
	(1, 'Nhẫn bạc đính kim cương', 'pnj', 100, 0, 2, 100, 'Nhẫn bạc tinh tế đính kim cương sang trọng, mang lại vẻ đẹp quý phái cho người sử dụng.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/216/sp-snxmxmk000167-nhan-nam-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 1, 100),
	(2, 'Nhẫn bạc đính đá', 'pnj', 100, 0, 2, 99, 'Nhẫn bạc đẹp mắt với đá quý đính kèm, hoàn hảo cho mọi dịp lễ tết.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/114/snxmxmw060030-nhan-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 1, 101),
	(3, 'Nhẫn bạc có logo hoạt hình', 'pnj', 100, 0, 2, 100, 'Nhẫn bạc với thiết kế logo hoạt hình vui nhộn, phù hợp cho những người yêu thích phong cách trẻ trung.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/226/sp-snxm00w060022-nhan-nam-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 1, 100),
	(4, 'Nhẫn bạc hình bông hoa', 'pnj', 100, 0, 2, 100, 'Nhẫn bạc thiết kế bông hoa tinh tế, mang đến vẻ đẹp dịu dàng cho người đeo.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/226/sp-snxm00w060014-nhan-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 1, 100),
	(5, 'Nhẫn bạc hình hình bướm', 'pnj', 100, 0, 2, 100, 'Nhẫn bạc hình bướm, thể hiện sự thanh thoát và tinh tế, phù hợp với những cô nàng yêu thích sự nhẹ nhàng.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/226/sp-snxmxmw060186-nhan-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 1, 100),
	(6, 'Dây chuyền mắc xích', 'pnj', 100, 0, 2, 100, 'Dây chuyền bạc mắc xích hiện đại, dễ dàng phối hợp với nhiều kiểu trang phục khác nhau.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/218/sp-sd0000w060105-day-chuyen-nam-bac-y-dinh-pnjsilver-1.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 2, 100),
	(7, 'Dây chuyền kim bảo như ý', 'pnj', 100, 0, 2, 100, 'Dây chuyền kim bảo mang ý nghĩa may mắn, giúp người đeo thêm phần tự tin và thịnh vượng.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/199/sp-sm0000y000033-mat-day-chuyen-bac-dinh-da-pnjsilver-kim-bao-nhu-y-1.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 2, 100),
	(8, 'Dây chuyền hình chìa khóa', 'pnj', 100, 0, 2, 100, 'Dây chuyền với mặt chìa khóa, biểu tượng của sự mở ra những cơ hội mới.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/199/sp-smxmxmy000009-mat-day-chuyen-bac-dinh-da-pnjsilver-kim-bao-nhu-y-1.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 2, 100),
	(9, 'Dây chuyền hình con rắn', 'pnj', 100, 0, 2, 100, 'Dây chuyền thiết kế hình con rắn, mang ý nghĩa sự tái sinh và phát triển mạnh mẽ.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/217/sp-smzt00w060009-mat-day-chuyen-bac-dinh-da-disney-pnj-inside-out-2-1.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 2, 100),
	(10, 'Dây chuyền mặt trái tim', 'pnj', 100, 0, 2, 100, 'Dây chuyền với mặt trái tim ngọt ngào, thích hợp cho những ai yêu thích sự lãng mạn.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/202/sp-smxm00w000035-mat-day-chuyen-bac-dinh-ngoc-trai-pnjsilver-1.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 2, 100),
	(11, 'Lắc tay có hình trái tim', 'pnj', 100, 0, 2, 100, 'Lắc tay thiết kế hình trái tim, tượng trưng cho tình yêu và sự kết nối bền chặt.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/227/sp-slxmxmw060232-lac-tay-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 3, 100),
	(12, 'Lắc tay hình móc câu', 'pnj', 100, 0, 2, 100, 'Lắc tay thiết kế móc câu tinh xảo, tượng trưng cho sự bảo vệ và may mắn.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/227/sp-slztmxw000010-lac-tay-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 3, 100),
	(13, 'Lắc tay hình móc khóa', 'pnj', 100, 0, 2, 100, 'Lắc tay hình móc khóa, mang lại cảm giác an toàn và mở ra những cơ hội mới.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/226/sp-slxm00w060039-lac-tay-bac-y-dinh-pnjsilver-1.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 3, 100),
	(14, 'Lắc tay hình bông hoa', 'pnj', 100, 0, 2, 100, 'Lắc tay thiết kế bông hoa xinh xắn, thích hợp cho những cô gái yêu thích sự dịu dàng.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/225/sp-slxmxmw060211-lac-tay-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 3, 100),
	(15, 'Lăc tay hình trái tim bự', 'pnj', 100, 0, 2, 100, 'Lắc tay với hình trái tim to lớn, thể hiện tình yêu mãnh liệt và sự gắn kết sâu sắc.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/222/sp-slxm00w060038-lac-tay-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 3, 100),
	(16, 'Bông tai hình nơ', 'pnj', 100, 0, 2, 100, 'Bông tai hình nơ duyên dáng, mang đến vẻ ngoài thanh lịch và tinh tế.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/225/sp-sbxmxmw060230-bong-tai-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 4, 100),
	(17, 'Bông tai hình bông tuyết', 'pnj', 100, 0, 2, 100, 'Bông tai hình bông tuyết, biểu tượng của sự tinh khiết và vẻ đẹp tự nhiên.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/225/sp-sbxmxmw060229-bong-tai-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 4, 100),
	(18, 'Bông tai hình chữ thập', 'pnj', 100, 0, 2, 100, 'Bông tai thiết kế hình chữ thập, mang ý nghĩa tôn vinh đức tin và sự bảo vệ.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/225/sp-sbxmxmw060227-bong-tai-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 4, 100),
	(19, 'Bông tai rong biển', 'pnj', 100, 0, 2, 100, 'Bông tai thiết kế hình rong biển, mang lại cảm giác tươi mới và tự nhiên.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/221/sp-sbxmxmw000089-bong-tai-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 4, 100),
	(20, 'Bông tai ngọc trai', 'pnj', 100, 0, 2, 100, 'Bông tai ngọc trai sang trọng, hoàn hảo cho những dịp đặc biệt và sự kiện trang trọng.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/221/sp-sbpfxmw000027-bong-tai-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 4, 100),
	(21, 'Lắc chân hình cỏ bốn lá', 'pnj', 100, 0, 2, 100, 'Lắc chân hình cỏ bốn lá mang ý nghĩa may mắn, giúp người đeo luôn gặp thuận lợi.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/219/sp-sl0000w060223-lac-chan-bac-dinh-da-pnjsilver.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 5, 100),
	(22, 'Lắc chân trái tim', 'pnj', 100, 0, 2, 100, 'Lắc chân hình trái tim, tượng trưng cho tình yêu và sự chăm sóc.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/209/sp-slxm00k000021-lac-chan-bac-dinh-da-pnjsilver-hinh-trai-tim-1.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 5, 100),
	(23, 'Lắc chân hình vô cực', 'pnj', 100, 0, 2, 100, 'Lắc chân với thiết kế hình vô cực, biểu tượng của sự vĩnh cửu và bền vững.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/142/slxmxmk000042-lac-chan-bac-dinh-da-pnjsilver.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 5, 100),
	(24, 'Lặc chân đính chuông', 'pnj', 100, 0, 2, 100, 'Lắc chân đính chuông, tạo âm thanh nhẹ nhàng và dễ chịu mỗi khi di chuyển.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/131/gl0000w000652-lac-chan-vang-trang-18k-pnj-1.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 5, 100),
	(25, 'Charm hình nơ', 'pnj', 100, 0, 2, 100, 'Charm hình nơ xinh xắn, dễ dàng kết hợp với nhiều loại vòng tay.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/99/ti0000x000002-phu-kien-charm-style-by-pnj-001.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 6, 100),
	(26, 'Charm chữ Z', 'pnj', 100, 0, 2, 91, 'Charm chữ Z độc đáo, giúp bạn thể hiện cá tính riêng biệt.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/83/0000y000053-hat-charm-bac-treo-pnjstyle-1.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 6, 103),
	(27, 'Charm chữ G', 'pnj', 100, 0, 2, 88, 'Charm chữ G, là lựa chọn tuyệt vời cho những người yêu thích chữ cái cá nhân.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/83/si0000y000034-hat-charm-treo-bac-pnjstyle-dna-chu-g.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 6, 103),
	(28, 'Charm chữ W', 'pnj', 100, 0, 2, 93, 'Charm chữ W, phù hợp với những ai mang tên hoặc sở thích về chữ W.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/83/0000y000050-hat-charm-bac-treo-pnjstyle-3_9mmi-ur.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 6, 102),
	(29, 'Charm chữ Y', 'pnj', 100, 0, 2, 96, 'Charm chữ Y, đại diện cho sự mạnh mẽ và độc đáo của người sở hữu.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/83/0000y000052-hat-charm-bac-treo-pnjstyle-2_wf7x-1l.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 6, 101);

-- Dumping structure for table webnhomc2.users
CREATE TABLE IF NOT EXISTS `users` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `User1` longtext NOT NULL,
  `Fullname` longtext NOT NULL,
  `Password` longtext NOT NULL,
  `IsGender` tinyint(1) NOT NULL,
  `BirthDay` datetime(6) NOT NULL,
  `Email` longtext NOT NULL,
  `PhoneNumber` longtext NOT NULL,
  `Address` longtext NOT NULL,
  `createdAt` datetime(6) NOT NULL,
  `updatedAt` datetime(6) NOT NULL,
  `IsAdmin` tinyint(1) NOT NULL,
  `RandomKey` longtext NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table webnhomc2.users: ~2 rows (approximately)
INSERT INTO `users` (`Id`, `User1`, `Fullname`, `Password`, `IsGender`, `BirthDay`, `Email`, `PhoneNumber`, `Address`, `createdAt`, `updatedAt`, `IsAdmin`, `RandomKey`) VALUES
	(1, 'phat12', 'Lê Tuấn Phát', '8990e9314217c96dfb06931178d7f396', 0, '2024-12-12 00:00:00.000000', 'ltphat2401034444@gmail.com', '0935822771', 'Ninh hòa', '2024-12-10 07:49:31.428458', '2024-12-10 07:49:31.428461', 0, '23j@fd2djL'),
	(2, 'ti12', 'Lê Tuấn Phát', '8990e9314217c96dfb06931178d7f396', 0, '2021-12-12 00:00:00.000000', 'ltphat2401034444@gmail.com', '0935822771', 'Ninh hòa', '2023-12-10 00:00:00.000000', '2021-11-10 00:00:00.000000', 1, '23j@fd2djL');

-- Dumping structure for table webnhomc2.__efmigrationshistory
CREATE TABLE IF NOT EXISTS `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table webnhomc2.__efmigrationshistory: ~0 rows (approximately)
INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
	('20241209163013_InitialCreate', '8.0.2'),
	('20241210003617_AddRandomKeyToUser', '8.0.2'),
	('20241214154102_themcothenullchoNote', '8.0.2');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;

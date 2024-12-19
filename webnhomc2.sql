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
	(1, 'Nhẫn bạc', 'button-nhan.png'),
	(2, 'Dây chuyền bạc', 'button-daychuyen.png'),
	(3, 'Lắc tay bạc', 'button-lac.png'),
	(4, 'Bông tai bạc', 'button-bongtai.png'),
	(5, 'Lắc chân bạc', 'button-lac.png'),
	(6, 'Charm bạc', 'th.jpg');

-- Dumping structure for table webnhomc2.discounts
CREATE TABLE IF NOT EXISTS `discounts` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext NOT NULL,
  `Discount_Value` double NOT NULL,
  `Start_Date` datetime(6) NOT NULL,
  `End_Date` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table webnhomc2.discounts: ~3 rows (approximately)
INSERT INTO `discounts` (`Id`, `Name`, `Discount_Value`, `Start_Date`, `End_Date`) VALUES
	(1, 'Giảm giá Black Friday', 50, '2024-11-25 00:00:00.000000', '2024-11-30 00:00:00.000000'),
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
	(1, 1, 'sanpham1.png', 'sanpham1-2.png'),
	(2, 2, 'sanpham2.png', 'sanpham2-2.png'),
	(3, 3, 'sanpham3.png', 'sanpham3-2.png'),
	(4, 4, 'sanpham4.png', 'sanpham4-2.png'),
	(5, 5, 'sanpham5.png', 'sanpham5-2.png'),
	(6, 6, 'sanpham6.png', 'sanpham6-2.png'),
	(7, 7, 'sanpham7.png', 'sanpham7-2.png'),
	(8, 8, 'sanpham8.png', 'sanpham8-2.png'),
	(9, 9, 'sanpham9.png', 'sanpham9-2.png'),
	(10, 10, 'sanpham10.png', 'sanpham10-2.png'),
	(11, 11, 'sanpham11.png', 'sanpham11-2.png'),
	(12, 12, 'sanpham12.png', 'sanpham12-2.png'),
	(13, 13, 'sanpham13.png', 'sanpham13-2.png'),
	(14, 14, 'sanpham14.png', 'sanpham14-2.png'),
	(15, 15, 'sanpham15.png', 'sanpham15-2.png'),
	(16, 16, 'sanpham16.png', ''),
	(17, 17, 'sanpham17.png', 'sanpham17-2.png'),
	(18, 18, 'sanpham18.png', 'sanpham18-2.png'),
	(19, 19, 'sanpham19.png', 'sanpham19-2.png'),
	(20, 20, 'sanpham20.png', 'sanpham20-2.png'),
	(21, 21, 'sanpham21.png', 'sanpham21-2.png'),
	(22, 22, 'sanpham22.png', 'sanpham22-2.png'),
	(23, 23, 'sanpham23.png', 'sanpham23-2.png'),
	(24, 24, 'sanpham24.png', 'sanpham24-2.png'),
	(25, 25, 'sanpham25.png', 'sanpham25-2.png'),
	(26, 26, 'sanpham26.png', 'sanpham26-2.png'),
	(27, 27, 'sanpham27.png', 'sanpham27-2.png'),
	(28, 28, 'sanpham28.png', 'sanpham28-2.png'),
	(29, 29, 'sanpham29.png', 'sanpham29-2.png');

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
) ENGINE=InnoDB AUTO_INCREMENT=47 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table webnhomc2.orderdetails: ~0 rows (approximately)

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
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table webnhomc2.orders: ~0 rows (approximately)

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
	(1, 'Nhẫn bạc đính kim cương', 'pnj', 100, 0, 2, 100, 'Nhẫn bạc tinh tế đính kim cương sang trọng, mang lại vẻ đẹp quý phái cho người sử dụng.', 'sanpham1.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 1, 100),
	(2, 'Nhẫn bạc đính đá', 'pnj', 100, 0, 2, 99, 'Nhẫn bạc đẹp mắt với đá quý đính kèm, hoàn hảo cho mọi dịp lễ tết.', 'sanpham2.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 1, 101),
	(3, 'Nhẫn bạc có logo hoạt hình', 'pnj', 100, 0, 2, 100, 'Nhẫn bạc với thiết kế logo hoạt hình vui nhộn, phù hợp cho những người yêu thích phong cách trẻ trung.', 'sanpham3.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 1, 100),
	(4, 'Nhẫn bạc hình bông hoa', 'pnj', 100, 0, 2, 100, 'Nhẫn bạc thiết kế bông hoa tinh tế, mang đến vẻ đẹp dịu dàng cho người đeo.', 'sanpham4.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 1, 100),
	(5, 'Nhẫn bạc hình hình bướm', 'pnj', 100, 0, 2, 100, 'Nhẫn bạc hình bướm, thể hiện sự thanh thoát và tinh tế, phù hợp với những cô nàng yêu thích sự nhẹ nhàng.', 'sanpham5.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 1, 100),
	(6, 'Dây chuyền mắc xích', 'pnj', 100, 0, 2, 100, 'Dây chuyền bạc mắc xích hiện đại, dễ dàng phối hợp với nhiều kiểu trang phục khác nhau.', 'sanpham6.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 2, 100),
	(7, 'Dây chuyền kim bảo như ý', 'pnj', 100, 0, 2, 100, 'Dây chuyền kim bảo mang ý nghĩa may mắn, giúp người đeo thêm phần tự tin và thịnh vượng.', 'sanpham7.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 2, 100),
	(8, 'Dây chuyền hình chìa khóa', 'pnj', 100, 0, 2, 100, 'Dây chuyền với mặt chìa khóa, biểu tượng của sự mở ra những cơ hội mới.', 'sanpham8.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 2, 100),
	(9, 'Dây chuyền hình con rắn', 'pnj', 100, 0, 2, 100, 'Dây chuyền thiết kế hình con rắn, mang ý nghĩa sự tái sinh và phát triển mạnh mẽ.', 'sanpham9.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 2, 100),
	(10, 'Dây chuyền mặt trái tim', 'pnj', 100, 0, 2, 100, 'Dây chuyền với mặt trái tim ngọt ngào, thích hợp cho những ai yêu thích sự lãng mạn.', 'sanpham10.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 2, 100),
	(11, 'Lắc tay có hình trái tim', 'pnj', 100, 0, 2, 100, 'Lắc tay thiết kế hình trái tim, tượng trưng cho tình yêu và sự kết nối bền chặt.', 'sanpham11.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 3, 100),
	(12, 'Lắc tay hình móc câu', 'pnj', 100, 0, 2, 100, 'Lắc tay thiết kế móc câu tinh xảo, tượng trưng cho sự bảo vệ và may mắn.', 'sanpham12.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 3, 100),
	(13, 'Lắc tay hình móc khóa', 'pnj', 100, 0, 2, 100, 'Lắc tay hình móc khóa, mang lại cảm giác an toàn và mở ra những cơ hội mới.', 'sanpham13.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 3, 100),
	(14, 'Lắc tay hình bông hoa', 'pnj', 100, 0, 2, 100, 'Lắc tay thiết kế bông hoa xinh xắn, thích hợp cho những cô gái yêu thích sự dịu dàng.', 'sanpham14.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 3, 100),
	(15, 'Lăc tay hình trái tim bự', 'pnj', 100, 0, 2, 100, 'Lắc tay với hình trái tim to lớn, thể hiện tình yêu mãnh liệt và sự gắn kết sâu sắc.', 'sanpham15.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 3, 100),
	(16, 'Bông tai hình nơ', 'pnj', 100, 0, 2, 100, 'Bông tai hình nơ duyên dáng, mang đến vẻ ngoài thanh lịch và tinh tế.', 'sanpham16.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 4, 100),
	(17, 'Bông tai hình bông tuyết', 'pnj', 100, 0, 2, 100, 'Bông tai hình bông tuyết, biểu tượng của sự tinh khiết và vẻ đẹp tự nhiên.', 'sanpham17.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 4, 100),
	(18, 'Bông tai hình chữ thập', 'pnj', 100, 0, 2, 100, 'Bông tai thiết kế hình chữ thập, mang ý nghĩa tôn vinh đức tin và sự bảo vệ.', 'sanpham18.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 4, 100),
	(19, 'Bông tai rong biển', 'pnj', 100, 0, 2, 100, 'Bông tai thiết kế hình rong biển, mang lại cảm giác tươi mới và tự nhiên.', 'sanpham19.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 4, 100),
	(20, 'Bông tai ngọc trai', 'pnj', 100, 0, 2, 100, 'Bông tai ngọc trai sang trọng, hoàn hảo cho những dịp đặc biệt và sự kiện trang trọng.', 'sanpham20.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 4, 100),
	(21, 'Lắc chân hình cỏ bốn lá', 'pnj', 100, 0, 2, 100, 'Lắc chân hình cỏ bốn lá mang ý nghĩa may mắn, giúp người đeo luôn gặp thuận lợi.', 'sanpham21.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 5, 100),
	(22, 'Lắc chân trái tim', 'pnj', 100, 0, 2, 100, 'Lắc chân hình trái tim, tượng trưng cho tình yêu và sự chăm sóc.', 'sanpham22.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 5, 100),
	(23, 'Lắc chân hình vô cực', 'pnj', 100, 0, 2, 100, 'Lắc chân với thiết kế hình vô cực, biểu tượng của sự vĩnh cửu và bền vững.', 'sanpham23.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 5, 100),
	(24, 'Lặc chân đính chuông', 'pnj', 100, 0, 2, 100, 'Lắc chân đính chuông, tạo âm thanh nhẹ nhàng và dễ chịu mỗi khi di chuyển.', 'sanpham24.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 5, 100),
	(25, 'Charm hình nơ', 'pnj', 100, 0, 2, 100, 'Charm hình nơ xinh xắn, dễ dàng kết hợp với nhiều loại vòng tay.', 'sanpham25.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 6, 100),
	(26, 'Charm chữ Z', 'pnj', 100, 0, 2, 91, 'Charm chữ Z độc đáo, giúp bạn thể hiện cá tính riêng biệt.', 'sanpham26.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 6, 103),
	(27, 'Charm chữ G', 'pnj', 100, 0, 2, 88, 'Charm chữ G, là lựa chọn tuyệt vời cho những người yêu thích chữ cái cá nhân.', 'sanpham27.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 6, 103),
	(28, 'Charm chữ W', 'pnj', 100, 0, 2, 93, 'Charm chữ W, phù hợp với những ai mang tên hoặc sở thích về chữ W.', 'sanpham28.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 6, 102),
	(29, 'Charm chữ Y', 'pnj', 100, 0, 2, 96, 'Charm chữ Y, đại diện cho sự mạnh mẽ và độc đáo của người sở hữu.', 'sanpham29.png', '2024-11-10 21:14:55.000000', '2024-11-10 21:14:55.000000', 6, 101);

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
  `IsConfirmEmail` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table webnhomc2.users: ~2 rows (approximately)
INSERT INTO `users` (`Id`, `User1`, `Fullname`, `Password`, `IsGender`, `BirthDay`, `Email`, `PhoneNumber`, `Address`, `createdAt`, `updatedAt`, `IsAdmin`, `RandomKey`, `IsConfirmEmail`) VALUES
	(7, 'ti12', 'le tuyet quyen', '82ed29ed3b1743dfe22c89e0c97c6ae9', 1, '2024-12-14 00:00:00.000000', '21129858@st.hcmuaf.edu.vn', '0935822771', 'Ninh hòa', '2024-12-18 19:00:07.148543', '2024-12-18 19:00:07.148546', 1, 'sai&K31JK5', 1),
	(12, 'phat12', 'Lê Tuấn Phát', '37a2d12285380b5d3754a0f306db22cc', 1, '2024-12-06 00:00:00.000000', 'ltphat240103@gmail.com', '0935822771', 'Ninh hòa', '2024-12-18 19:21:07.759957', '2024-12-18 19:21:07.759960', 0, 'fsMsLkAAfd', 1);

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
	('20241214154102_themcothenullchoNote', '8.0.2'),
	('20241218112713_themthuoctinhchouser', '8.0.2');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;

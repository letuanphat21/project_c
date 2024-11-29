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


-- Dumping database structure for webnhomc
CREATE DATABASE IF NOT EXISTS `webnhomc` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;
USE `webnhomc`;

-- Dumping structure for table webnhomc.category
CREATE TABLE IF NOT EXISTS `category` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) DEFAULT NULL,
  `thumb` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table webnhomc.category: ~6 rows (approximately)
INSERT INTO `category` (`id`, `name`, `thumb`) VALUES
	(1, 'Nhẫn bạc', 'https://cdn.pnj.io/images/promo/148/button-nhan.png'),
	(2, 'Dây chuyền bạc', 'https://cdn.pnj.io/images/promo/148/button-daychuyen.png'),
	(3, 'Lắc tay bạc', 'https://cdn.pnj.io/images/promo/148/button-lac.png'),
	(4, 'Bông tai bạc', 'https://cdn.pnj.io/images/promo/148/button-bongtai.png'),
	(5, 'Lắc chân bạc', 'https://cdn.pnj.io/images/promo/148/button-lac.png'),
	(6, 'Charm bạc', 'https://tse4.mm.bing.net/th?id=OIP.7iXJQQ_-51C33Hl2autDyQHaE8&pid=Api&P=0&h=220');

-- Dumping structure for table webnhomc.discount
CREATE TABLE IF NOT EXISTS `discount` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) DEFAULT NULL,
  `discount` decimal(20,6) DEFAULT NULL,
  `start_date` date DEFAULT NULL,
  `end_date` date DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table webnhomc.discount: ~2 rows (approximately)
INSERT INTO `discount` (`id`, `name`, `discount`, `start_date`, `end_date`) VALUES
	(1, 'Giảm giá Black Friday', 50.000000, '2024-11-25', '2024-11-21'),
	(5, 'Giảm giá Flash Sale', 75.500000, '2024-11-23', '2024-11-23');

-- Dumping structure for table webnhomc.image
CREATE TABLE IF NOT EXISTS `image` (
  `mid` int(11) NOT NULL,
  `pid` int(11) DEFAULT NULL,
  `image1` varchar(200) DEFAULT NULL,
  `image2` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`mid`),
  KEY `FK__product` (`pid`),
  CONSTRAINT `FK__product` FOREIGN KEY (`pid`) REFERENCES `product` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table webnhomc.image: ~29 rows (approximately)
INSERT INTO `image` (`mid`, `pid`, `image1`, `image2`) VALUES
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

-- Dumping structure for table webnhomc.order
CREATE TABLE IF NOT EXISTS `order` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `userId` int(11) DEFAULT NULL,
  `fullname` varchar(50) DEFAULT NULL,
  `email` varchar(150) DEFAULT NULL,
  `phoneNumber` varchar(20) DEFAULT NULL,
  `address` varchar(200) DEFAULT NULL,
  `note` varchar(255) DEFAULT NULL,
  `orderDate` date DEFAULT NULL,
  `status` varchar(50) DEFAULT NULL,
  `totalMoney` double DEFAULT NULL,
  `paymentmethod` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `userId` (`userId`),
  CONSTRAINT `order_ibfk_1` FOREIGN KEY (`userId`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table webnhomc.order: ~6 rows (approximately)
INSERT INTO `order` (`id`, `userId`, `fullname`, `email`, `phoneNumber`, `address`, `note`, `orderDate`, `status`, `totalMoney`, `paymentmethod`) VALUES
	(1, 1, 'Lê Tuấn Phát', 'ltphat@gmail.com', '0934221332', 'Ninh hòa', '', '2024-11-24', 'pending', 510, 'Tiền mặt'),
	(2, 1, 'Lê Tuấn Phát', 'ltphat@gmail.com', '0934221332', 'Ninh hòa', '', '2024-11-24', 'pending', 560, 'Tiền mặt'),
	(3, 1, 'Lê Tuấn Phát', 'ltphat@gmail.com', '0934221332', 'Ninh hòa', '', '2024-11-24', 'pending', 350, 'Tiền mặt'),
	(4, 1, 'Lê Tuấn Phát', 'ltphat@gmail.com', '0934221332', 'Ninh hòa', '', '2024-11-24', 'pending', 500, 'Tiền mặt'),
	(5, 1, 'Lê Tuấn Phát', 'ltphat@gmail.com', '0934221332', 'Ninh hòa', '', '2024-11-25', 'pending', 560, 'Tiền mặt'),
	(6, 1, 'Lê Tuấn Phát', 'ltphat@gmail.com', '0934221332', 'Ninh hòa', '', '2024-11-26', 'pending', 720, 'Tiền mặt');

-- Dumping structure for table webnhomc.orderdetails
CREATE TABLE IF NOT EXISTS `orderdetails` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `orderId` int(11) DEFAULT NULL,
  `productId` int(11) DEFAULT NULL,
  `price` double DEFAULT NULL,
  `quantity` int(11) DEFAULT NULL,
  `totalMoney` double DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `orderId` (`orderId`),
  KEY `productId` (`productId`),
  CONSTRAINT `orderdetails_ibfk_1` FOREIGN KEY (`orderId`) REFERENCES `order` (`id`),
  CONSTRAINT `orderdetails_ibfk_2` FOREIGN KEY (`productId`) REFERENCES `product` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table webnhomc.orderdetails: ~12 rows (approximately)
INSERT INTO `orderdetails` (`id`, `orderId`, `productId`, `price`, `quantity`, `totalMoney`) VALUES
	(1, 1, 29, 100, 4, 400),
	(2, 1, 28, 100, 2, 200),
	(3, 2, 29, 100, 4, 400),
	(4, 2, 28, 100, 3, 300),
	(5, 3, 27, 100, 4, 400),
	(6, 3, 2, 100, 1, 100),
	(7, 4, 28, 100, 4, 400),
	(8, 4, 26, 100, 1, 100),
	(9, 5, 26, 100, 4, 400),
	(10, 5, 27, 100, 3, 300),
	(11, 6, 27, 100, 5, 500),
	(12, 6, 26, 100, 4, 400);

-- Dumping structure for table webnhomc.product
CREATE TABLE IF NOT EXISTS `product` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `categoryid` int(11) DEFAULT NULL,
  `title` varchar(350) DEFAULT NULL,
  `brand` varchar(50) DEFAULT NULL,
  `price` double DEFAULT NULL,
  `discount` int(11) DEFAULT NULL,
  `warranty` int(11) DEFAULT NULL,
  `inventoryNumber` int(11) DEFAULT NULL,
  `description` longtext DEFAULT NULL,
  `thumbnail` varchar(500) DEFAULT NULL,
  `createdAt` datetime DEFAULT NULL,
  `updatedAt` datetime DEFAULT NULL,
  `numOfPur` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `categoryId` (`categoryid`) USING BTREE,
  CONSTRAINT `product_ibfk_1` FOREIGN KEY (`categoryid`) REFERENCES `category` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table webnhomc.product: ~29 rows (approximately)
INSERT INTO `product` (`id`, `categoryid`, `title`, `brand`, `price`, `discount`, `warranty`, `inventoryNumber`, `description`, `thumbnail`, `createdAt`, `updatedAt`, `numOfPur`) VALUES
	(1, 1, 'Nhẫn bạc đính kim cương', 'pnj', 100, 0, 2, 100, 'Nhẫn bạc tinh tế đính kim cương sang trọng, mang lại vẻ đẹp quý phái cho người sử dụng.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/216/sp-snxmxmk000167-nhan-nam-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 100),
	(2, 1, 'Nhẫn bạc đính đá', 'pnj', 100, 0, 2, 99, 'Nhẫn bạc đẹp mắt với đá quý đính kèm, hoàn hảo cho mọi dịp lễ tết.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/114/snxmxmw060030-nhan-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 101),
	(3, 1, 'Nhẫn bạc có logo hoạt hình', 'pnj', 100, 0, 2, 100, 'Nhẫn bạc với thiết kế logo hoạt hình vui nhộn, phù hợp cho những người yêu thích phong cách trẻ trung.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/226/sp-snxm00w060022-nhan-nam-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 100),
	(4, 1, 'Nhẫn bạc hình bông hoa', 'pnj', 100, 0, 2, 100, 'Nhẫn bạc thiết kế bông hoa tinh tế, mang đến vẻ đẹp dịu dàng cho người đeo.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/226/sp-snxm00w060014-nhan-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 100),
	(5, 1, 'Nhẫn bạc hình hình bướm', 'pnj', 100, 0, 2, 100, 'Nhẫn bạc hình bướm, thể hiện sự thanh thoát và tinh tế, phù hợp với những cô nàng yêu thích sự nhẹ nhàng.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/226/sp-snxmxmw060186-nhan-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 100),
	(6, 2, 'Dây chuyền mắc xích', 'pnj', 100, 0, 2, 100, 'Dây chuyền bạc mắc xích hiện đại, dễ dàng phối hợp với nhiều kiểu trang phục khác nhau.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/218/sp-sd0000w060105-day-chuyen-nam-bac-y-dinh-pnjsilver-1.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 100),
	(7, 2, 'Dây chuyền kim bảo như ý', 'pnj', 100, 0, 2, 100, 'Dây chuyền kim bảo mang ý nghĩa may mắn, giúp người đeo thêm phần tự tin và thịnh vượng.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/199/sp-sm0000y000033-mat-day-chuyen-bac-dinh-da-pnjsilver-kim-bao-nhu-y-1.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 100),
	(8, 2, 'Dây chuyền hình chìa khóa', 'pnj', 100, 0, 2, 100, 'Dây chuyền với mặt chìa khóa, biểu tượng của sự mở ra những cơ hội mới.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/199/sp-smxmxmy000009-mat-day-chuyen-bac-dinh-da-pnjsilver-kim-bao-nhu-y-1.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 100),
	(9, 2, 'Dây chuyền hình con rắn', 'pnj', 100, 0, 2, 100, 'Dây chuyền thiết kế hình con rắn, mang ý nghĩa sự tái sinh và phát triển mạnh mẽ.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/217/sp-smzt00w060009-mat-day-chuyen-bac-dinh-da-disney-pnj-inside-out-2-1.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 100),
	(10, 2, 'Dây chuyền mặt trái tim', 'pnj', 100, 0, 2, 100, 'Dây chuyền với mặt trái tim ngọt ngào, thích hợp cho những ai yêu thích sự lãng mạn.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/202/sp-smxm00w000035-mat-day-chuyen-bac-dinh-ngoc-trai-pnjsilver-1.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 100),
	(11, 3, 'Lắc tay có hình trái tim', 'pnj', 100, 0, 2, 100, 'Lắc tay thiết kế hình trái tim, tượng trưng cho tình yêu và sự kết nối bền chặt.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/227/sp-slxmxmw060232-lac-tay-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 100),
	(12, 3, 'Lắc tay hình móc câu', 'pnj', 100, 0, 2, 100, 'Lắc tay thiết kế móc câu tinh xảo, tượng trưng cho sự bảo vệ và may mắn.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/227/sp-slztmxw000010-lac-tay-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 100),
	(13, 3, 'Lắc tay hình móc khóa', 'pnj', 100, 0, 2, 100, 'Lắc tay hình móc khóa, mang lại cảm giác an toàn và mở ra những cơ hội mới.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/226/sp-slxm00w060039-lac-tay-bac-y-dinh-pnjsilver-1.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 100),
	(14, 3, 'Lắc tay hình bông hoa', 'pnj', 100, 0, 2, 100, 'Lắc tay thiết kế bông hoa xinh xắn, thích hợp cho những cô gái yêu thích sự dịu dàng.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/225/sp-slxmxmw060211-lac-tay-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 100),
	(15, 3, 'Lăc tay hình trái tim bự', 'pnj', 100, 0, 2, 100, 'Lắc tay với hình trái tim to lớn, thể hiện tình yêu mãnh liệt và sự gắn kết sâu sắc.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/222/sp-slxm00w060038-lac-tay-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 100),
	(16, 4, 'Bông tai hình nơ', 'pnj', 100, 0, 2, 100, 'Bông tai hình nơ duyên dáng, mang đến vẻ ngoài thanh lịch và tinh tế.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/225/sp-sbxmxmw060230-bong-tai-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 100),
	(17, 4, 'Bông tai hình bông tuyết', 'pnj', 100, 0, 2, 100, 'Bông tai hình bông tuyết, biểu tượng của sự tinh khiết và vẻ đẹp tự nhiên.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/225/sp-sbxmxmw060229-bong-tai-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 100),
	(18, 4, 'Bông tai hình chữ thập', 'pnj', 100, 0, 2, 100, 'Bông tai thiết kế hình chữ thập, mang ý nghĩa tôn vinh đức tin và sự bảo vệ.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/225/sp-sbxmxmw060227-bong-tai-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 100),
	(19, 4, 'Bông tai rong biển', 'pnj', 100, 0, 2, 100, 'Bông tai thiết kế hình rong biển, mang lại cảm giác tươi mới và tự nhiên.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/221/sp-sbxmxmw000089-bong-tai-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 100),
	(20, 4, 'Bông tai ngọc trai', 'pnj', 100, 0, 2, 100, 'Bông tai ngọc trai sang trọng, hoàn hảo cho những dịp đặc biệt và sự kiện trang trọng.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/221/sp-sbpfxmw000027-bong-tai-bac-dinh-da-pnjsilver-1.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 100),
	(21, 5, 'Lắc chân hình cỏ bốn lá', 'pnj', 100, 0, 2, 100, 'Lắc chân hình cỏ bốn lá mang ý nghĩa may mắn, giúp người đeo luôn gặp thuận lợi.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/219/sp-sl0000w060223-lac-chan-bac-dinh-da-pnjsilver.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 100),
	(22, 5, 'Lắc chân trái tim', 'pnj', 100, 0, 2, 100, 'Lắc chân hình trái tim, tượng trưng cho tình yêu và sự chăm sóc.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/209/sp-slxm00k000021-lac-chan-bac-dinh-da-pnjsilver-hinh-trai-tim-1.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 100),
	(23, 5, 'Lắc chân hình vô cực', 'pnj', 100, 0, 2, 100, 'Lắc chân với thiết kế hình vô cực, biểu tượng của sự vĩnh cửu và bền vững.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/142/slxmxmk000042-lac-chan-bac-dinh-da-pnjsilver.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 100),
	(24, 5, 'Lặc chân đính chuông', 'pnj', 100, 0, 2, 100, 'Lắc chân đính chuông, tạo âm thanh nhẹ nhàng và dễ chịu mỗi khi di chuyển.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/131/gl0000w000652-lac-chan-vang-trang-18k-pnj-1.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 100),
	(25, 6, 'Charm hình nơ', 'pnj', 100, 0, 2, 100, 'Charm hình nơ xinh xắn, dễ dàng kết hợp với nhiều loại vòng tay.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/99/ti0000x000002-phu-kien-charm-style-by-pnj-001.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 100),
	(26, 6, 'Charm chữ Z', 'pnj', 100, 0, 2, 91, 'Charm chữ Z độc đáo, giúp bạn thể hiện cá tính riêng biệt.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/83/0000y000053-hat-charm-bac-treo-pnjstyle-1.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 103),
	(27, 6, 'Charm chữ G', 'pnj', 100, 0, 2, 88, 'Charm chữ G, là lựa chọn tuyệt vời cho những người yêu thích chữ cái cá nhân.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/83/si0000y000034-hat-charm-treo-bac-pnjstyle-dna-chu-g.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 103),
	(28, 6, 'Charm chữ W', 'pnj', 100, 0, 2, 93, 'Charm chữ W, phù hợp với những ai mang tên hoặc sở thích về chữ W.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/83/0000y000050-hat-charm-bac-treo-pnjstyle-3_9mmi-ur.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 102),
	(29, 6, 'Charm chữ Y', 'pnj', 100, 0, 2, 96, 'Charm chữ Y, đại diện cho sự mạnh mẽ và độc đáo của người sở hữu.', 'https://cdn.pnj.io/images/thumbnails/300/300/detailed/83/0000y000052-hat-charm-bac-treo-pnjstyle-2_wf7x-1l.png', '2024-11-10 21:14:55', '2024-11-10 21:14:55', 101);

-- Dumping structure for table webnhomc.user
CREATE TABLE IF NOT EXISTS `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user` varchar(250) DEFAULT NULL,
  `fullname` varchar(50) DEFAULT NULL,
  `password` varchar(200) DEFAULT NULL,
  `gender` bit(1) DEFAULT NULL,
  `birthday` date DEFAULT NULL,
  `email` varchar(150) DEFAULT NULL,
  `phoneNumber` varchar(20) DEFAULT NULL,
  `address` varchar(200) DEFAULT NULL,
  `createdAt` datetime DEFAULT NULL,
  `updatedAt` datetime DEFAULT NULL,
  `isAdmin` bit(1) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table webnhomc.user: ~6 rows (approximately)
INSERT INTO `user` (`id`, `user`, `fullname`, `password`, `gender`, `birthday`, `email`, `phoneNumber`, `address`, `createdAt`, `updatedAt`, `isAdmin`) VALUES
	(1, 'phat12', 'Lê Tuấn Phát21', '1234', b'1', '2017-02-20', 'ltphat32@gmail.com', '09342213321221', 'Ninh hòa khánh hòa', '2024-11-11 00:00:00', '2024-11-27 13:13:15', b'0'),
	(2, 'tuyet21', 'le tuyet quyen', '123', b'0', '2024-11-23', 'ltphat240103@gmail.com', '0935822771', 'Ninh hòa', '2024-11-21 00:00:00', '2024-11-21 00:00:00', b'0'),
	(3, 'ti12', 'le tuan ti', '123', b'1', '2024-09-01', 'lt@gmai.com', '0935822771', 'innh hoa', '2024-11-21 20:47:52', '2024-11-21 20:47:52', b'1'),
	(4, 'phat1', 'le tuan q', '123', b'1', '2024-11-27', 'lt1@gmail.com', '1313123123', 'Ninh hòa', '2024-11-27 17:46:58', '2024-11-27 17:47:00', b'0'),
	(5, 'phat2', 'le tuan 2', '123', b'1', '2024-11-27', 'lt1@gmail.com', '1313123123', 'Ninh hòa', '2024-11-27 17:47:28', '2024-11-27 17:47:28', b'0'),
	(6, 'phat3', 'le ta', '123', b'1', '2024-11-27', 'lt1@gmail.com', '13212312312', 'NInh hòa', '2024-11-27 17:47:49', '2024-11-27 17:47:50', b'0');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;

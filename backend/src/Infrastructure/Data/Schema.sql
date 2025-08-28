-- MySQL schema for Real Estate portal
CREATE TABLE IF NOT EXISTS Users (
  Id BIGINT PRIMARY KEY AUTO_INCREMENT,
  Email VARCHAR(255) NOT NULL UNIQUE,
  PasswordHash VARCHAR(255) NOT NULL,
  CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS Properties (
  Id BIGINT PRIMARY KEY AUTO_INCREMENT,
  Title VARCHAR(255) NOT NULL,
  Address VARCHAR(255) NOT NULL,
  City VARCHAR(100) NOT NULL,
  Price DECIMAL(18,2) NOT NULL,
  ListingType TINYINT NOT NULL,
  Bedrooms INT NOT NULL,
  Bathrooms INT NOT NULL,
  CarSpots INT NOT NULL,
  Description TEXT NULL
);

CREATE TABLE IF NOT EXISTS Favorites (
  UserId BIGINT NOT NULL,
  PropertyId BIGINT NOT NULL,
  CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (UserId, PropertyId),
  FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
  FOREIGN KEY (PropertyId) REFERENCES Properties(Id) ON DELETE CASCADE
);

-- Seed sample properties
INSERT INTO Properties (Title, Address, City, Price, ListingType, Bedrooms, Bathrooms, CarSpots, Description) VALUES
('Sunny Family Home', '12 Oak St', 'Springfield', 650000, 1, 4, 2, 2, 'Spacious family home with a large backyard.'),
('Modern Apartment', '99 King Rd Apt 8', 'Riverton', 520, 0, 2, 1, 1, 'Close to shops and public transport.'),
('Beachside Cottage', '5 Ocean View', 'Seabreeze', 1200000, 1, 3, 2, 2, 'Stunning ocean views.');

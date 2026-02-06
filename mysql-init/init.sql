CREATE DATABASE IF NOT EXISTS petworld;
USE petworld;

CREATE TABLE IF NOT EXISTS Products (
    Id CHAR(36) PRIMARY KEY,
    Name VARCHAR(200) NOT NULL,
    Category VARCHAR(100) NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    Description VARCHAR(500) NOT NULL
);

CREATE TABLE IF NOT EXISTS ChatConversations (
    Id CHAR(36) PRIMARY KEY,
    CreatedAt DATETIME NOT NULL,
    Question VARCHAR(2000) NOT NULL,
    Answer VARCHAR(4000) NOT NULL,
    IterationCount INT NOT NULL
);

INSERT INTO Products (Id, Name, Category, Price, Description) VALUES
('550e8400-e29b-41d4-a716-446655440001', 'Royal Canin Adult Dog 15kg', 'Karma dla psow', 289.00, 'Premium karma dla doroslych psow srednich ras'),
('550e8400-e29b-41d4-a716-446655440002', 'Whiskas Adult Kurczak 7kg', 'Karma dla kotow', 129.00, 'Sucha karma dla doroslych kotow z kurczakiem'),
('550e8400-e29b-41d4-a716-446655440003', 'Tetra AquaSafe 500ml', 'Akwarystyka', 45.00, 'Uzdatniacz wody do akwarium, neutralizuje chlor'),
('550e8400-e29b-41d4-a716-446655440004', 'Trixie Drapak XL 150cm', 'Akcesoria dla kotow', 399.00, 'Wysoki drapak z platformami i domkiem'),
('550e8400-e29b-41d4-a716-446655440005', 'Kong Classic Large', 'Zabawki dla psow', 69.00, 'Wytrzymala zabawka do napelniania smakolykami'),
('550e8400-e29b-41d4-a716-446655440006', 'Ferplast Klatka dla chomika', 'Gryzonie', 189.00, 'Klatka 60x40cm z wyposazeniem'),
('550e8400-e29b-41d4-a716-446655440007', 'Flexi Smycz automatyczna 8m', 'Akcesoria dla psow', 119.00, 'Smycz zwijana dla psow do 50kg'),
('550e8400-e29b-41d4-a716-446655440008', 'Brit Premium Kitten 8kg', 'Karma dla kotow', 159.00, 'Karma dla kociat do 12 miesiaca zycia'),
('550e8400-e29b-41d4-a716-446655440009', 'JBL ProFlora CO2 Set', 'Akwarystyka', 549.00, 'Kompletny zestaw CO2 dla roslin akwariowych'),
('550e8400-e29b-41d4-a716-446655440034', 'Vitapol Siano dla krolikow 1kg', 'Gryzonie', 25.00, 'Naturalne siano lakowe, podstawa diety');

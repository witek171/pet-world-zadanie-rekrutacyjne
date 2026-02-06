CREATE DATABASE IF NOT EXISTS petworld;
USE petworld;

CREATE TABLE IF NOT EXISTS Products (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(200) NOT NULL,
    Category VARCHAR(100) NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    Description VARCHAR(500) NOT NULL
);

CREATE TABLE IF NOT EXISTS ChatConversations (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    CreatedAt DATETIME NOT NULL,
    Question VARCHAR(2000) NOT NULL,
    Answer VARCHAR(4000) NOT NULL,
    IterationCount INT NOT NULL
);

INSERT INTO Products (Name, Category, Price, Description) VALUES
('Royal Canin Adult Dog 15kg', 'Karma dla psow', 289.00, 'Premium karma dla doroslych psow srednich ras'),
('Whiskas Adult Kurczak 7kg', 'Karma dla kotow', 129.00, 'Sucha karma dla doroslych kotow z kurczakiem'),
('Tetra AquaSafe 500ml', 'Akwarystyka', 45.00, 'Uzdatniacz wody do akwarium, neutralizuje chlor'),
('Trixie Drapak XL 150cm', 'Akcesoria dla kotow', 399.00, 'Wysoki drapak z platformami i domkiem'),
('Kong Classic Large', 'Zabawki dla psow', 69.00, 'Wytrzymala zabawka do napelniania smakolykami'),
('Ferplast Klatka dla chomika', 'Gryzonie', 189.00, 'Klatka 60x40cm z wyposazeniem'),
('Flexi Smycz automatyczna 8m', 'Akcesoria dla psow', 119.00, 'Smycz zwijana dla psow do 50kg'),
('Brit Premium Kitten 8kg', 'Karma dla kotow', 159.00, 'Karma dla kociat do 12 miesiaca zycia'),
('JBL ProFlora CO2 Set', 'Akwarystyka', 549.00, 'Kompletny zestaw CO2 dla roslin akwariowych'),
('Vitapol Siano dla krolikow 1kg', 'Gryzonie', 25.00, 'Naturalne siano lakowe, podstawa diety');

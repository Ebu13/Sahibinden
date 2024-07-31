create database Sahibinden;
go
use Sahibinden;
go
-- Kullanýcýlar Tablosu
CREATE TABLE Users (
    user_id INT IDENTITY(1,1) PRIMARY KEY,
    username NVARCHAR(50) NOT NULL,
    password NVARCHAR(50) NOT NULL,
    email NVARCHAR(100) NOT NULL
);

-- Menü Tablosu
CREATE TABLE Menu (
    menu_id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(50) NOT NULL,
    parent_id INT NULL,
    FOREIGN KEY (parent_id) REFERENCES Menu(menu_id) ON DELETE NO ACTION
);

-- Arabalar Tablosu
CREATE TABLE Cars (
    car_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL,
    menu_id INT NOT NULL,
    year INT NOT NULL,
    price DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE CASCADE,
    FOREIGN KEY (menu_id) REFERENCES Menu(menu_id) ON DELETE NO ACTION
);

-- Evler Tablosu
CREATE TABLE Homes (
    home_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL,
    menu_id INT NOT NULL,
    location NVARCHAR(100) NOT NULL,
    size INT NOT NULL,
    price DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE CASCADE,
    FOREIGN KEY (menu_id) REFERENCES Menu(menu_id) ON DELETE NO ACTION
);

-- Menüye Ana Baþlýklar Ekleme
INSERT INTO Menu (name, parent_id) VALUES ('Araba', NULL);
INSERT INTO Menu (name, parent_id) VALUES ('Ev', NULL);

-- Menüye Arabalarý Ekleyin
INSERT INTO Menu (name, parent_id) VALUES ('BMW', (SELECT menu_id FROM Menu WHERE name = 'Araba'));
INSERT INTO Menu (name, parent_id) VALUES ('Audi', (SELECT menu_id FROM Menu WHERE name = 'Araba'));
INSERT INTO Menu (name, parent_id) VALUES ('Mercedes', (SELECT menu_id FROM Menu WHERE name = 'Araba'));

-- BMW Alt Modelleri Ekleyin
INSERT INTO Menu (name, parent_id) VALUES ('BMW Seri 3', (SELECT menu_id FROM Menu WHERE name = 'BMW'));
INSERT INTO Menu (name, parent_id) VALUES ('BMW Seri 5', (SELECT menu_id FROM Menu WHERE name = 'BMW'));

-- Audi Alt Modelleri Ekleyin
INSERT INTO Menu (name, parent_id) VALUES ('Audi A4', (SELECT menu_id FROM Menu WHERE name = 'Audi'));
INSERT INTO Menu (name, parent_id) VALUES ('Audi A6', (SELECT menu_id FROM Menu WHERE name = 'Audi'));

-- Mercedes Alt Modelleri Ekleyin
INSERT INTO Menu (name, parent_id) VALUES ('Mercedes C Serisi', (SELECT menu_id FROM Menu WHERE name = 'Mercedes'));
INSERT INTO Menu (name, parent_id) VALUES ('Mercedes E Serisi', (SELECT menu_id FROM Menu WHERE name = 'Mercedes'));

-- Menüye Ev Tiplerini Ekleyin
INSERT INTO Menu (name, parent_id) VALUES ('Müstakil', (SELECT menu_id FROM Menu WHERE name = 'Ev'));
INSERT INTO Menu (name, parent_id) VALUES ('Villa', (SELECT menu_id FROM Menu WHERE name = 'Ev'));
INSERT INTO Menu (name, parent_id) VALUES ('Apartman', (SELECT menu_id FROM Menu WHERE name = 'Ev'));

INSERT INTO Users (username, password, email) VALUES 
('Ebubekir', '13042003', 'ebu@sampas.com'),
('Ýlker', 'senel12345678', 'ilker_senel@sampas.com');

INSERT INTO Cars (user_id, menu_id, year, price) VALUES 
(1, (SELECT menu_id FROM Menu WHERE name = 'BMW Seri 3'), 2020, 25000.00),
(2, (SELECT menu_id FROM Menu WHERE name = 'BMW Seri 5'), 2021, 35000.00),
(2, (SELECT menu_id FROM Menu WHERE name = 'Audi A4'), 2019, 30000.00),
(2, (SELECT menu_id FROM Menu WHERE name = 'Audi A6'), 2022, 40000.00),
(2, (SELECT menu_id FROM Menu WHERE name = 'Mercedes C Serisi'), 2018, 32000.00),
(1, (SELECT menu_id FROM Menu WHERE name = 'Mercedes E Serisi'), 2023, 45000.00),
(1, (SELECT menu_id FROM Menu WHERE name = 'BMW Seri 3'), 2017, 22000.00),
(1, (SELECT menu_id FROM Menu WHERE name = 'BMW Seri 5'), 2020, 34000.00),
(2, (SELECT menu_id FROM Menu WHERE name = 'Audi A4'), 2021, 31000.00),
(1, (SELECT menu_id FROM Menu WHERE name = 'Mercedes C Serisi'), 2022, 36000.00);


INSERT INTO Homes (user_id, menu_id, location, size, price) VALUES 
(1, (SELECT menu_id FROM Menu WHERE name = 'Müstakil'), 'Ýstanbul', 150, 300000.00),
(2, (SELECT menu_id FROM Menu WHERE name = 'Villa'), 'Ankara', 200, 500000.00),
(1, (SELECT menu_id FROM Menu WHERE name = 'Apartman'), 'Ýzmir', 100, 250000.00),
(1, (SELECT menu_id FROM Menu WHERE name = 'Müstakil'), 'Bursa', 120, 280000.00),
(1, (SELECT menu_id FROM Menu WHERE name = 'Villa'), 'Antalya', 250, 700000.00),
(1, (SELECT menu_id FROM Menu WHERE name = 'Apartman'), 'Adana', 90, 220000.00),
(2, (SELECT menu_id FROM Menu WHERE name = 'Müstakil'), 'Konya', 130, 320000.00),
(2, (SELECT menu_id FROM Menu WHERE name = 'Villa'), 'Trabzon', 180, 550000.00),
(2, (SELECT menu_id FROM Menu WHERE name = 'Apartman'), 'Sakarya', 110, 240000.00),
(1, (SELECT menu_id FROM Menu WHERE name = 'Müstakil'), 'Kocaeli', 160, 310000.00);


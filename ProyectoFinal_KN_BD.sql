CREATE DATABASE ProyectoFinal_KN_BD
Go

DROP DATABASE ProyectoFinal_KN_BD
Go

USE ProyectoFinal_KN_BD


--TABLES--------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE Store (
    id_store int IDENTITY (1,1) NOT NULL CONSTRAINT pk_store PRIMARY KEY,
	name varchar(250) NOT NULL,
);


CREATE TABLE Product(
	id_product int IDENTITY (1,1) NOT NULL CONSTRAINT pk_product PRIMARY KEY,
	name varchar(40) NOT NULL,
	description varchar(100) NOT NULL,
	stock int NOT NULL,
	price decimal NOT NULL,
	picture_url varchar(500) NOT NULL,
	id_store int NOT NULL,
);


CREATE TABLE User_tb(
	id_user int IDENTITY (1,1) NOT NULL CONSTRAINT pk_User_tb PRIMARY KEY,
	name varchar(255) NOT NULL,
	email varchar(255) NOT NULL UNIQUE,
	password varchar(500) NOT NULL,
	active bit NOT NULL,
	id_role int NOT NULL,
	token_recovery varchar(200),
	identification varchar (20) NOT NULL,
	id_province int NOT NULL,
);

CREATE TABLE Role(
	id_role int IDENTITY (1,1) NOT NULL CONSTRAINT pk_Role PRIMARY KEY,
	role_name varchar(40) NOT NULL,
);

CREATE TABLE Province(
	id_province int IDENTITY (1,1) NOT NULL CONSTRAINT pk_Province PRIMARY KEY,
	province_name varchar(40) NOT NULL,
);

CREATE TABLE Invoice(
	id_invoice int IDENTITY (1,1) NOT NULL CONSTRAINT pk_invoice PRIMARY KEY,
	total decimal NOT NULL,
	date DATETIME NOT NULL,
	id_user int NOT NULL,
);


CREATE TABLE  Invoice_details (
	id_invoice_detail int IDENTITY (1,1) NOT NULL CONSTRAINT pk_invoice_details PRIMARY KEY,
	id_product int NOT NULL,
	quantity decimal NOT NULL,
	price decimal NOT NULL,
	id_invoice int NOT NULL,
);

CREATE TABLE  Binnacle (
	id_error bigint  NOT NULL CONSTRAINT pk_binnacle PRIMARY KEY,
	description varchar(500) NOT NULL,
	date_time datetime NOT NULL,
	origin varchar(100) NOT NULL,
);

CREATE TABLE  Errors (
	id_error bigint  NOT NULL CONSTRAINT pk_errors PRIMARY KEY,
	description varchar(500) NOT NULL,
	date_time datetime NOT NULL,
	origin varchar(100) NOT NULL,
	id_user int NOT NULL
);


ALTER TABLE Product ADD CONSTRAINT fk_store FOREIGN KEY (id_store)
REFERENCES store (id_store);

ALTER TABLE Invoice  ADD CONSTRAINT fk_user FOREIGN KEY (id_user) 
REFERENCES User_tb(id_user);

ALTER TABLE User_tb ADD CONSTRAINT fk_user_province FOREIGN KEY (id_province) 
REFERENCES Province(id_province);

ALTER TABLE User_tb ADD CONSTRAINT fk_user_role FOREIGN KEY (id_role) 
REFERENCES Role(id_role)

ALTER TABLE Invoice_details  ADD CONSTRAINT fk_product FOREIGN KEY (id_product) 
REFERENCES Product(id_product);

ALTER TABLE Invoice_details  ADD CONSTRAINT fk_invoice FOREIGN KEY (id_invoice)
REFERENCES Invoice(id_invoice);

ALTER TABLE Errors  ADD CONSTRAINT fk_user2 FOREIGN KEY (id_user)
REFERENCES User_tb(id_user);



--INSERTS PROVINCIAS----------------------------------------------------------------------------------------------------------------------------


INSERT INTO [dbo].[Province]
           ([province_name])
     VALUES
           ('San Jos�')

INSERT INTO [dbo].[Province]
           ([province_name])
     VALUES
           ('Alajuela')

INSERT INTO [dbo].[Province]
           ([province_name])
     VALUES
           ('Cartago')

INSERT INTO [dbo].[Province]
           ([province_name])
     VALUES
           ('Heredia')

INSERT INTO [dbo].[Province]
           ([province_name])
     VALUES
           ('Guanacaste')

INSERT INTO [dbo].[Province]
           ([province_name])
     VALUES
           ('Puntarenas')

INSERT INTO [dbo].[Province]
           ([province_name])
     VALUES
           ('Lim�n')

INSERT INTO [dbo].[Province]
           ([province_name])
     VALUES
           ('Otro')

 --INSERTS ROLES----------------------------------------------------------------------------------------------------------------------------

INSERT INTO [dbo].[Role]
           ([role_name])
     VALUES
           ('Administrador')

INSERT INTO [dbo].[Role]
           ([role_name])
     VALUES
           ('Cliente')

--INSERTS USUARIOS----------------------------------------------------------------------------------------------------------------------------


INSERT INTO [dbo].[User_tb]
           ([name]
           ,[email]
           ,[password]
           ,[active]
           ,[id_role]
           ,[identification]
           ,[id_province])
     VALUES
           ('DAVID CARDENAS O','davidcardenasorozco@gmail.com','123','1','1','118790058','1')

INSERT INTO [dbo].[User_tb]
           ([name]
           ,[email]
           ,[password]
           ,[active]
           ,[id_role]
           ,[identification]
           ,[id_province])
     VALUES
           ('USUARIO DE PRUEBA','usuariodeprueba@gmail.com','123','0','2','118790056','6')

--INSERTS TIENDAS--------------------------------------------------------------------------------------------------------------------


INSERT INTO [dbo].[Store]
           ([name])
     VALUES
           ('Snap Tools San Jos�')

INSERT INTO [dbo].[Store]
           ([name])
     VALUES
           ('Snap Tools Heredia')


--INSERTS PRODUCTOS------------------------------------------------------------------------------------------------------------------


INSERT INTO [dbo].[Product]
           ([name]
           ,[description]
           ,[stock]
           ,[price]
           ,[picture_url]
           ,[id_store])
     VALUES
           ('Truper MA-16F','Martillo pulido, u�a curva 16 oz y mango de fibra de vidrio','5',
		    '7000.00','https://m.media-amazon.com/images/I/61hhOHoEaCL._AC_SL1500_.jpg','1')

INSERT INTO [dbo].[Product]
           ([name]
           ,[description]
           ,[stock]
           ,[price]
           ,[picture_url]
           ,[id_store])
     VALUES
           ('KOHAM','Tijeras de podar el�ctricas profesionales inal�mbricas con 2 piezas de respaldo recargables','10',
		    '40000.00','https://m.media-amazon.com/images/W/IMAGERENDERING_521856-T1/images/I/71cbIrZGxOL._AC_UF894,1000_QL80_.jpg','2')

--SELECTS----------------------------------------------------------------------------------------------------------------------------

Select * from User_tb;

Select * from Province; 

Select * from Role; 

Select * from Store;

Select * from Product;

--DELETS----------------------------------------------------------------------------------------------------------------------------

DELETE FROM [dbo].[User_tb] 

DELETE FROM [dbo].[Role] 
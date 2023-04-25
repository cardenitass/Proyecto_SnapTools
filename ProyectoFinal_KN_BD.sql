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


CREATE TABLE Cart(
	id_cart int IDENTITY (1,1) NOT NULL CONSTRAINT pk_cart PRIMARY KEY,
	id_user int NOT NULL,
	id_product int NOT NULL,
	quantity int NOT NULL, 
	date_time datetime NOT NULL,
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
    date_time datetime NOT NULL,
	sub_total decimal NOT NULL,
	tax decimal NOT NULL, 
	total decimal NOT NULL,
	id_user int NOT NULL,
);


CREATE TABLE  Invoice_details (
	id_invoice_detail int IDENTITY (1,1) NOT NULL CONSTRAINT pk_invoice_details PRIMARY KEY,
	id_invoice int NOT NULL,
	id_product int NOT NULL,
	quantity int NOT NULL,
	price decimal NOT NULL,
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



ALTER TABLE Product ADD CONSTRAINT fk_product_store FOREIGN KEY (id_store)
REFERENCES store (id_store);

ALTER TABLE Invoice  ADD CONSTRAINT fk_invoice_user FOREIGN KEY (id_user) 
REFERENCES User_tb(id_user);

ALTER TABLE User_tb ADD CONSTRAINT fk_user_province FOREIGN KEY (id_province) 
REFERENCES Province(id_province);

ALTER TABLE User_tb ADD CONSTRAINT fk_user_role FOREIGN KEY (id_role) 
REFERENCES Role(id_role)

ALTER TABLE Cart ADD CONSTRAINT fk_cart_user FOREIGN KEY (id_user) 
REFERENCES User_tb(id_user)

ALTER TABLE Cart ADD CONSTRAINT fk_cart_product FOREIGN KEY (id_product) 
REFERENCES Product(id_product)

ALTER TABLE Invoice_details  ADD CONSTRAINT fk_invoice_details_product FOREIGN KEY (id_product) 
REFERENCES Product(id_product);

ALTER TABLE Invoice_details  ADD CONSTRAINT fk_invoice_details_invoice FOREIGN KEY (id_invoice)
REFERENCES Invoice(id_invoice);

ALTER TABLE Errors  ADD CONSTRAINT fk_errors_user FOREIGN KEY (id_user)
REFERENCES User_tb(id_user);
GO

--STORE PROCEDURES----------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE ShowTemporalCart
    @IdUsuario int
AS
BEGIN 
  DECLARE @IVA DECIMAL(10,2) = 0.13

    SELECT ISNULL(SUM(C.quantity),0)                                               CartQuantity,
		   ISNULL(SUM(C.quantity * P.price) + (SUM(C.quantity * P.price)* @IVA),0) CartPrice
	FROM Cart C INNER JOIN Product P 
	     ON C.id_product = P.id_product
	WHERE id_user = @IdUsuario
END
GO

-----------------------------------

CREATE PROCEDURE ShowTotalCart
    @IdUsuario int
AS
BEGIN 
 DECLARE @IVA DECIMAL(10,2) = 0.13

    SELECT CONVERT(VARCHAR,P.id_product) +'-> '+ P.name             ProductName,
	       C.quantity                                               CartQuantity,
	       P.price                                                  ProductPrice,
		   C.quantity * P.price                                     SubTotal,
		   (C.quantity * P.price) * @IVA                            Tax,		                                                           
		   C.quantity * P.price + ((C.quantity * P.price) * @IVA)   Total
	FROM Cart C INNER JOIN Product P 
	     ON C.id_product = P.id_product
	WHERE id_user = @IdUsuario
END
GO

-----------------------------------

CREATE PROCEDURE ConfirmPayment
    @IdUsuario int
AS
BEGIN

 -- SI HAY UN PRODUCTO QUE SUPERA EL STOCK NO EJECUTAR EL PROCEDURE

 IF NOT EXISTS(SELECT C.id_product
			   FROM Cart C
			   INNER JOIN Product P ON C.id_product = P.id_product
			   WHERE id_user = @IdUsuario 
			   AND C.quantity > P.stock)

   BEGIN 
		DECLARE @IVA DECIMAL(10,2) = 0.13

		-- ENCABEZADO DE LA FACTURA
		INSERT INTO [dbo].[Invoice]
			   ([date_time]
			   ,[sub_total]
			   ,[tax]
			   ,[total]
			   ,[id_user])
		 SELECT GETDATE(), 
		  SUM(C.quantity * P.price),
		  SUM((C.quantity * P.price) * @IVA),
		  SUM(C.quantity * P.price + ((C.quantity * P.price) * @IVA)),
		  C.id_user
		  FROM Cart C INNER JOIN Product P ON C.id_product = P.id_product
		  WHERE id_user = @IdUsuario
		  GROUP BY C.id_user

		  -- DETALLE DE LA FACTURA

	INSERT INTO [dbo].[Invoice_details]
			   ([id_invoice]
			   ,[id_product]
			   ,[quantity]
			   ,[price])
				SELECT @@Identity, 
				  C.id_product,
				  C.quantity,
				  C.quantity * P.price
				  FROM Cart C INNER JOIN Product P ON C.id_product = P.id_product
				  WHERE id_user = @IdUsuario

			-- ELIMINAR PRODUCTOS COMPRADOS DEL STOCK

			UPDATE P 
			SET P.stock = P.stock - C.quantity
			FROM Product P
			INNER JOIN Cart C ON P.id_product = C.id_product
			WHERE id_user = @IdUsuario

			-- LIMPIAR EL CARRITO
			DELETE FROM Cart 
			WHERE id_user = @IdUsuario

    END
	ELSE
	BEGIN 
	    -- LIMPIAR DEL CARRITO LOS PRODUCTOS QUE SUPERAN EL STOCK 
		DELETE C  
		FROM Cart C INNER JOIN Product P 
		ON C.id_product = P.id_product
		WHERE id_user = @IdUsuario 
		      AND C.quantity > P.stock
	END
END


-----------------------------------

CREATE PROCEDURE ViewInvoice
    @IdUsuario int
AS
BEGIN 
    SELECT I.id_invoice,
	       I.date_time,
	       I.sub_total,
		   I.tax, 
		   I.total
	FROM Invoice I 
	WHERE id_user = @IdUsuario
END
GO

-----------------------------------

ALTER PROCEDURE ShowInvoiceDetails 
@IdInvoice INT 
AS
BEGIN
       
	SELECT id_invoice
		  ,id_product
		  ,quantity
		  ,price
	  FROM Invoice_details
	  WHERE id_invoice =  @IdInvoice
END
GO


--INSERTS PROVINCIAS----------------------------------------------------------------------------------------------------------------------------


INSERT INTO [dbo].[Province]
           ([province_name])
     VALUES
           ('San José')

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
           ('Limón')

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
           ('Snap Tools San José')

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
           ('Truper MA-16F','Martillo pulido, uña curva 16 oz y mango de fibra de vidrio','5',
		    '7000.00','\img\Martillo.jpg','1')

INSERT INTO [dbo].[Product]
           ([name]
           ,[description]
           ,[stock]
           ,[price]
           ,[picture_url]
           ,[id_store])
     VALUES
           ('KOHAM','Tijera de podar eléctrica profesionale inalámbrica con 2 piezas de respaldo recargables','10',
		    '40000.00','\img\Tijera.jpg','2')
		   


--SELECTS----------------------------------------------------------------------------------------------------------------------------

Select * from User_tb;

Select * from Product;

Select * from Cart; 

Select * from Invoice;

Select * from Invoice_details;

Select * from Province; 

Select * from Role; 

Select * from Store;


--DELETS----------------------------------------------------------------------------------------------------------------------------

DELETE FROM [dbo].[User_tb] 

DELETE FROM [dbo].[Role] 

DELETE FROM [dbo].[Product] 

DELETE FROM [dbo].[Cart] 

DELETE FROM [dbo].[Invoice]

DELETE FROM [dbo].[Invoice_details] 




--Eliminar los productos del carrito despues de 5 dias-----------------------------------------------------------------------------

DELETE FROM Cart
WHERE date_time > GETDATE() + 5

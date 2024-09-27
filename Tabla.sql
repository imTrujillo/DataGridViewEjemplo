Use master
Go
Create Database   Almacen

use Almacen
Go 
Create table Productos(
	Nombre varchar(500),
	Descripcion varchar(500),
	FechaVencimiento Date,
	Cantidad Integer,
	Precio Numeric(10,2),
	Id integer Primary Key identity 
);

INSERT INTO PRODUCTOS(Nombre, Descripcion,FechaVencimiento,Cantidad,Precio)VALUES ('dsfdsf', 'sdfdsfdsfd','2024-09-18', 34, 324)
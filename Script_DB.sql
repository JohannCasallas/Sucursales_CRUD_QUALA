-- Crear la base de datos TestDB
CREATE DATABASE TestDB;
GO

-- Usar la base de datos TestDB
USE TestDB;
GO

-- Creacion tabla Moneda_JC
CREATE TABLE Moneda_JC (
    MonedaId INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion NVARCHAR(50) NOT NULL
);
GO

-- Datos de ejemplo en la tabla Moneda_JC
INSERT INTO Moneda_JC (Descripcion) VALUES ('USD'); -- Dólar estadounidense
INSERT INTO Moneda_JC (Descripcion) VALUES ('COP'); -- Peso colombiano
INSERT INTO Moneda_JC (Descripcion) VALUES ('EUR'); -- Euro
-- Puedes insertar más monedas según sea necesario

-- Creacion tabla Sucursal_JC
CREATE TABLE Sucursal_JC (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion NVARCHAR(250) NOT NULL,
    Direccion NVARCHAR(250) NOT NULL,
    Identificacion NVARCHAR(50) NOT NULL,
    FechaCreacion DATETIME NOT NULL CONSTRAINT CHK_FechaCreacion CHECK (FechaCreacion >= GETDATE()),
    MonedaId INT NOT NULL,
    CONSTRAINT FK_Sucursal_JC_Moneda FOREIGN KEY (MonedaId) REFERENCES Moneda_JC(MonedaId)
);
GO

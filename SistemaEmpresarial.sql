-- Crear la base de datos
CREATE DATABASE SistemaEmpresa;
USE SistemaEmpresa;

-- Crear tabla Empresa
CREATE TABLE Empresa (
    ID VARCHAR(70) PRIMARY KEY,
    Nombre VARCHAR(100),
	numTelefono INT,
	email VARCHAR(100)
);

-- Crear tabla MetodoPago
CREATE TABLE MetodoPago (
    ID VARCHAR(70) PRIMARY KEY,
    descripcion VARCHAR(50)
);

-- Crear tabla Provincia
CREATE TABLE Provincia (
    ID VARCHAR(70) PRIMARY KEY,
    Nombre VARCHAR(50)
);

-- Crear tabla Canton
CREATE TABLE Canton (
    ID VARCHAR(70) PRIMARY KEY,
    Nombre VARCHAR(50),
    ProvinciaID VARCHAR(70),
    CONSTRAINT FK_PROVINCIA FOREIGN KEY (ProvinciaID) REFERENCES Provincia(ID)
);

-- Crear tabla Lenguaje
CREATE TABLE Lenguaje (
    ID VARCHAR(70) PRIMARY KEY,
    Nombre VARCHAR(50)
);

-- Crear tabla Moneda
CREATE TABLE Moneda (
    ID VARCHAR(70) PRIMARY KEY,
    Nombre VARCHAR(50)
);

-- Crear tabla Cliente
CREATE TABLE Cliente (
    ID VARCHAR(70) PRIMARY KEY,
	Cedula INT,
    Nombre VARCHAR(100),
	Email VARCHAR(100),
	numTelefono INT,
    EmpresaID VARCHAR(70),
    ProvinciaID VARCHAR(70),
    CantonID VARCHAR(70),
    CONSTRAINT FK_EMPRESA FOREIGN KEY (EmpresaID) REFERENCES Empresa(ID),
    CONSTRAINT FK_PROVINCIA_CLIENTE FOREIGN KEY (ProvinciaID) REFERENCES Provincia(ID),
    CONSTRAINT FK_CANTON FOREIGN KEY (CantonID) REFERENCES Canton(ID)
);

-- Crear tabla Transacciones
CREATE TABLE Transacciones (
    ID VARCHAR(70) PRIMARY KEY,
    ClienteID VARCHAR(70),
    LenguajeID VARCHAR(70),
    MonedaID VARCHAR(70),
    MetodoPagoID VARCHAR(70),
    Monto DECIMAL(18,2),
    Fecha DATETIME,
    CONSTRAINT FK_CLIENTE FOREIGN KEY (ClienteID) REFERENCES Cliente(ID),
    CONSTRAINT FK_LENGUAJE FOREIGN KEY (LenguajeID) REFERENCES Lenguaje(ID),
    CONSTRAINT FK_MONEDA FOREIGN KEY (MonedaID) REFERENCES Moneda(ID),
    CONSTRAINT FK_METODOPAGO FOREIGN KEY (MetodoPagoID) REFERENCES MetodoPago(ID)
);

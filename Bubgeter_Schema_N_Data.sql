CREATE DATABASE Budgeter;
GO

USE Budgeter;
GO

CREATE TABLE Usuarios (
    IdUsuario INT PRIMARY KEY IDENTITY(1,1),
    NombreUsuario VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    ContraseñaHash NVARCHAR(MAX) NOT NULL,
    FechaRegistro DATETIME DEFAULT GETDATE(),
    EstadoUsuario BIT DEFAULT 1
);

CREATE TABLE Personas (
    IdPersona INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT NOT NULL UNIQUE FOREIGN KEY REFERENCES Usuarios(IdUsuario),
    NombreCompleto VARCHAR(100) NOT NULL,
    Telefono VARCHAR(15),
    Direccion VARCHAR(255),
    FechaNacimiento DATE
);

CREATE TABLE Presupuestos (
    IdPresupuesto INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT NOT NULL FOREIGN KEY REFERENCES Usuarios(IdUsuario),
    NombrePresupuesto VARCHAR(100) NOT NULL,
    MontoTotal DECIMAL(18, 2) NOT NULL,
    FechaInicio DATE NOT NULL,
    FechaFin DATE NOT NULL,
    Descripcion VARCHAR(255),
    FechaCreacion DATETIME DEFAULT GETDATE(),
    EstadoPresupuesto BIT DEFAULT 1 -- 1: Activo, 0: Inactivo
);

CREATE TABLE TipoCategorias (
    IdTipoCategoria INT PRIMARY KEY IDENTITY(1,1),
    NombreTipo VARCHAR(50) NOT NULL UNIQUE, -- Ejemplo: Necesidades, Deseos
    Descripcion VARCHAR(255),
    FechaCreacion DATETIME DEFAULT GETDATE()
);

CREATE TABLE CategoriasGastos (
    IdCategoria INT PRIMARY KEY IDENTITY(1,1),
    IdTipoCategoria INT NOT NULL FOREIGN KEY REFERENCES TipoCategorias(IdTipoCategoria),
    NombreCategoria VARCHAR(50) NOT NULL UNIQUE,
    Descripcion VARCHAR(255),
    FechaCreacion DATETIME DEFAULT GETDATE()
);

CREATE TABLE Gastos (
    IdGasto INT PRIMARY KEY IDENTITY(1,1),
    IdPresupuesto INT NOT NULL FOREIGN KEY REFERENCES Presupuestos(IdPresupuesto),
    IdCategoria INT NOT NULL FOREIGN KEY REFERENCES CategoriasGastos(IdCategoria),
    Monto DECIMAL(18, 2) NOT NULL,
    FechaGasto DATETIME NOT NULL,
    Descripcion VARCHAR(255),
    Notas TEXT,
    FechaCreacion DATETIME DEFAULT GETDATE()
);

CREATE TABLE Ingresos (
    IdIngreso INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT NOT NULL FOREIGN KEY REFERENCES Usuarios(IdUsuario),
    Monto DECIMAL(18, 2) NOT NULL,
    FechaIngreso DATETIME NOT NULL,
    Descripcion VARCHAR(255),
    Fuente VARCHAR(100), -- Ejemplo: Salario, Freelance, etc.
    Estado BIT DEFAULT 1, -- 1: Recibido, 0: Pendiente
    FechaCreacion DATETIME DEFAULT GETDATE()
);

CREATE TABLE MetasAhorro (
    IdMeta INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT NOT NULL FOREIGN KEY REFERENCES Usuarios(IdUsuario),
    NombreMeta VARCHAR(100) NOT NULL,
    MontoMeta DECIMAL(18, 2) NOT NULL,
    FechaInicio DATE NOT NULL,
    FechaMeta DATE NOT NULL,
    MontoAhorrado DECIMAL(18, 2) DEFAULT 0,
    Descripcion VARCHAR(255),
    FechaCreacion DATETIME DEFAULT GETDATE()
);

CREATE TABLE AportacionesAhorro (
    IdAportacion INT PRIMARY KEY IDENTITY(1,1),
    IdMeta INT NOT NULL FOREIGN KEY REFERENCES MetasAhorro(IdMeta),
    MontoAportacion DECIMAL(18, 2) NOT NULL,
    FechaAportacion DATETIME NOT NULL,
    Notas TEXT,
    FechaCreacion DATETIME DEFAULT GETDATE()
);

CREATE TABLE MetodosPago (
    IdMetodo INT PRIMARY KEY IDENTITY(1,1),
    NombreMetodo VARCHAR(50) NOT NULL UNIQUE,
    Descripcion VARCHAR(255),
    FechaCreacion DATETIME DEFAULT GETDATE()
);

-- Insertando datos iniciales
INSERT INTO Usuarios (NombreUsuario, Email, ContraseñaHash) 
VALUES ('andre123', 'andre@email.com', 'hashed_password');

INSERT INTO Personas (IdUsuario, NombreCompleto, Telefono, Direccion, FechaNacimiento) 
VALUES (1, 'André Pérez', '50312345678', 'San Salvador, El Salvador', '1995-05-15');

INSERT INTO TipoCategorias (NombreTipo, Descripcion) 
VALUES	('Necesidades', 'Gastos esenciales para la vida diaria'),
		('Deseos', 'Gastos no esenciales relacionados con entretenimiento y ocio');

INSERT INTO CategoriasGastos (IdTipoCategoria, NombreCategoria, Descripcion) 
VALUES	(1, 'Renta', 'Pago de vivienda'),
		(1, 'Luz', 'Pago de electricidad'),
		(1, 'Alimentos', 'Compra de alimentos y bebidas'),
		(1, 'Transporte', 'Gastos en transporte'),
		(2, 'Entretenimiento', 'Actividades recreativas');

INSERT INTO MetodosPago (NombreMetodo, Descripcion) 
VALUES	('Efectivo', 'Pago en efectivo'),
		('Tarjeta de crédito', 'Pago con tarjeta de crédito'),
		('Transferencia bancaria', 'Pago mediante transferencia bancaria');

INSERT INTO Presupuestos (IdUsuario, NombrePresupuesto, MontoTotal, FechaInicio, FechaFin, Descripcion) 
VALUES (1, 'Presupuesto Mensual Enero', 800.00, '2025-01-01', '2025-01-31', 'Presupuesto para el mes de enero');

INSERT INTO Gastos (IdPresupuesto, IdCategoria, Monto, FechaGasto, Descripcion) 
VALUES	(1, 1, 300.00, '2025-01-05', 'Pago de renta de enero'),
		(1, 2, 50.00, '2025-01-06', 'Pago de electricidad de enero'),
		(1, 3, 150.00, '2025-01-07', 'Compra de alimentos para la semana');

INSERT INTO Ingresos (IdUsuario, Monto, FechaIngreso, Descripcion, Fuente) 
VALUES (1, 1000.00, '2025-01-01', 'Salario mensual', 'Trabajo Full-Time');

INSERT INTO MetasAhorro (IdUsuario, NombreMeta, MontoMeta, FechaInicio, FechaMeta, Descripcion) 
VALUES (1, 'Viaje a Europa', 5000.00, '2025-01-01', '2025-12-31', 'Ahorro para viajar a Europa a fin de año');

INSERT INTO AportacionesAhorro (IdMeta, MontoAportacion, FechaAportacion, Notas) 
VALUES (1, 100.00, '2025-01-10', 'Primera aportación al fondo de ahorro');
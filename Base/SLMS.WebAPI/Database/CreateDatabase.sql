-- Script para crear la base de datos LigaFutbolDB
-- Ejecutar este script en SQL Server Management Studio

USE master;
GO

-- Crear la base de datos si no existe
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'LigaFutbolDB')
BEGIN
    CREATE DATABASE LigaFutbolDB;
END
GO

USE LigaFutbolDB;
GO

-- Crear tabla Rol
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Rol')
BEGIN
    CREATE TABLE Rol (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Tipo NVARCHAR(50) NOT NULL,
        Nombre NVARCHAR(100) NOT NULL
    );
END
GO

-- Crear tabla Usuario
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Usuario')
BEGIN
    CREATE TABLE Usuario (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Email NVARCHAR(100) NOT NULL UNIQUE,
        PasswordHash NVARCHAR(MAX) NOT NULL,
        Estado BIT NOT NULL DEFAULT 1,
        Creado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        Modificado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        RolId INT NOT NULL,
        CONSTRAINT FK_Usuario_Rol FOREIGN KEY (RolId) REFERENCES Rol(Id)
    );
END
GO

-- Crear tabla Liga
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Liga')
BEGIN
    CREATE TABLE Liga (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Nombre NVARCHAR(200) NOT NULL,
        Logo NVARCHAR(500),
        Estado BIT NOT NULL DEFAULT 1,
        Creado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        Modificado DATETIME2 NOT NULL DEFAULT GETUTCDATE()
    );
END
GO

-- Crear tabla Torneo
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Torneo')
BEGIN
    CREATE TABLE Torneo (
        Id INT PRIMARY KEY IDENTITY(1,1),
        LigaId INT NOT NULL,
        Nombre NVARCHAR(200) NOT NULL,
        Logo NVARCHAR(500),
        Estado BIT NOT NULL DEFAULT 1,
        Creado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        Modificado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        CONSTRAINT FK_Torneo_Liga FOREIGN KEY (LigaId) REFERENCES Liga(Id)
    );
END
GO

-- Crear tabla Equipo
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Equipo')
BEGIN
    CREATE TABLE Equipo (
        Id INT PRIMARY KEY IDENTITY(1,1),
        TorneoId INT NOT NULL,
        LigaId INT NOT NULL,
        Nombre NVARCHAR(200) NOT NULL,
        Logo NVARCHAR(500),
        Estado BIT NOT NULL DEFAULT 1,
        Creado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        Modificado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        CONSTRAINT FK_Equipo_Torneo FOREIGN KEY (TorneoId) REFERENCES Torneo(Id),
        CONSTRAINT FK_Equipo_Liga FOREIGN KEY (LigaId) REFERENCES Liga(Id)
    );
END
GO

-- Crear tabla Jugador
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Jugador')
BEGIN
    CREATE TABLE Jugador (
        Id INT PRIMARY KEY IDENTITY(1,1),
        EquipoId INT NOT NULL,
        TorneoId INT NOT NULL,
        LigaId INT NOT NULL,
        Nombre NVARCHAR(200) NOT NULL,
        Edad INT NOT NULL,
        Nacimiento DATE NOT NULL,
        Estado BIT NOT NULL DEFAULT 1,
        Creado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        Modificado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        RolId INT NOT NULL,
        CONSTRAINT FK_Jugador_Equipo FOREIGN KEY (EquipoId) REFERENCES Equipo(Id),
        CONSTRAINT FK_Jugador_Torneo FOREIGN KEY (TorneoId) REFERENCES Torneo(Id),
        CONSTRAINT FK_Jugador_Liga FOREIGN KEY (LigaId) REFERENCES Liga(Id),
        CONSTRAINT FK_Jugador_Rol FOREIGN KEY (RolId) REFERENCES Rol(Id)
    );
END
GO

-- Crear tabla Partido
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Partido')
BEGIN
    CREATE TABLE Partido (
        Id INT PRIMARY KEY IDENTITY(1,1),
        LocalId INT NOT NULL,
        VisitanteId INT NOT NULL,
        Numero INT NOT NULL,
        TorneoId INT NOT NULL,
        CONSTRAINT FK_Partido_Local FOREIGN KEY (LocalId) REFERENCES Equipo(Id),
        CONSTRAINT FK_Partido_Visitante FOREIGN KEY (VisitanteId) REFERENCES Equipo(Id),
        CONSTRAINT FK_Partido_Torneo FOREIGN KEY (TorneoId) REFERENCES Torneo(Id)
    );
END
GO

-- Crear tabla TipoEstadistica
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TipoEstadistica')
BEGIN
    CREATE TABLE TipoEstadistica (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Nombre NVARCHAR(100) NOT NULL
    );
END
GO

-- Crear tabla Estadistica
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Estadistica')
BEGIN
    CREATE TABLE Estadistica (
        Id INT PRIMARY KEY IDENTITY(1,1),
        TipoId INT NOT NULL,
        Minuto INT NOT NULL,
        JugadorId INT NOT NULL,
        PartidoId INT NOT NULL,
        CONSTRAINT FK_Estadistica_Tipo FOREIGN KEY (TipoId) REFERENCES TipoEstadistica(Id),
        CONSTRAINT FK_Estadistica_Jugador FOREIGN KEY (JugadorId) REFERENCES Jugador(Id),
        CONSTRAINT FK_Estadistica_Partido FOREIGN KEY (PartidoId) REFERENCES Partido(Id)
    );
END
GO

-- Crear tabla TipoCastigo
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TipoCastigo')
BEGIN
    CREATE TABLE TipoCastigo (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Nombre NVARCHAR(100) NOT NULL
    );
END
GO

-- Crear tabla Castigo
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Castigo')
BEGIN
    CREATE TABLE Castigo (
        Id INT PRIMARY KEY IDENTITY(1,1),
        TipoId INT NOT NULL,
        JugadorId INT NOT NULL,
        Partidos INT NOT NULL,
        CONSTRAINT FK_Castigo_Tipo FOREIGN KEY (TipoId) REFERENCES TipoCastigo(Id),
        CONSTRAINT FK_Castigo_Jugador FOREIGN KEY (JugadorId) REFERENCES Jugador(Id)
    );
END
GO

-- Crear tabla ProgramacionJuego
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ProgramacionJuego')
BEGIN
    CREATE TABLE ProgramacionJuego (
        Id INT PRIMARY KEY IDENTITY(1,1),
        LocalId INT NOT NULL,
        VisitanteId INT NOT NULL,
        FechaHora DATETIME2 NOT NULL,
        CONSTRAINT FK_ProgramacionJuego_Local FOREIGN KEY (LocalId) REFERENCES Equipo(Id),
        CONSTRAINT FK_ProgramacionJuego_Visitante FOREIGN KEY (VisitanteId) REFERENCES Equipo(Id)
    );
END
GO

-- Insertar datos iniciales en Rol
IF NOT EXISTS (SELECT * FROM Rol)
BEGIN
    INSERT INTO Rol (Tipo, Nombre) VALUES 
    ('Admin', 'Administrador'),
    ('DT', 'Director Técnico'),
    ('Arbitro', 'Árbitro'),
    ('Jugador', 'Jugador');
END
GO

-- Insertar tipos de estadísticas
IF NOT EXISTS (SELECT * FROM TipoEstadistica)
BEGIN
    INSERT INTO TipoEstadistica (Nombre) VALUES 
    ('Gol'),
    ('Tarjeta Amarilla'),
    ('Tarjeta Roja'),
    ('Falta');
END
GO

-- Insertar tipos de castigos
IF NOT EXISTS (SELECT * FROM TipoCastigo)
BEGIN
    INSERT INTO TipoCastigo (Nombre) VALUES 
    ('Suspensión'),
    ('Multa'),
    ('Expulsión Temporal');
END
GO

-- Crear usuario administrador por defecto
-- Password: Admin123 (en producción cambiar esto)
IF NOT EXISTS (SELECT * FROM Usuario WHERE Email = 'admin@ligafutbol.com')
BEGIN
    INSERT INTO Usuario (Email, PasswordHash, Estado, Creado, Modificado, RolId)
    VALUES ('admin@ligafutbol.com', 'JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=', 1, GETUTCDATE(), GETUTCDATE(), 1);
END
GO

PRINT 'Base de datos creada exitosamente';
PRINT 'Usuario admin creado: admin@ligafutbol.com / Admin123';
GO
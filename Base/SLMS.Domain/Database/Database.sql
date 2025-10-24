-- =============================================
-- Script de Creación de Base de Datos
-- Liga Fútbol API - Versión Simplificada
-- =============================================

USE LigaFutbolDB;
GO

-- =============================================
-- 1. TABLA: RolUsuario
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'RolUsuario')
BEGIN
    CREATE TABLE RolUsuario (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Nombre NVARCHAR(100) NOT NULL,
        Descripcion NVARCHAR(500),
        Tipo INT NOT NULL, -- Enum: 1=Administrador, 2=Operador, 3=Consultor
        
        -- Auditoría
        Creado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        CreadoPor NVARCHAR(100) DEFAULT 'Sistema',
        Modificado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        ModificadoPor NVARCHAR(100) DEFAULT 'Sistema',
        Eliminado BIT NOT NULL DEFAULT 0,
        FechaEliminacion DATETIME2 NULL,
        EliminadoPor NVARCHAR(100) NULL
    );
    
    PRINT '✅ Tabla RolUsuario creada';
END
GO

-- =============================================
-- 2. TABLA: Usuario
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Usuario')
BEGIN
    CREATE TABLE Usuario (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Email NVARCHAR(100) NOT NULL UNIQUE,
        GoogleId NVARCHAR(100) NULL UNIQUE,
        NombreCompleto NVARCHAR(200) NOT NULL,
        FotoPerfil NVARCHAR(500) NULL,
        RolUsuarioId INT NOT NULL,
        Estado INT NOT NULL DEFAULT 1, -- Enum: 1=Activo, 2=Inactivo, 3=Suspendido
        
        -- Auditoría
        Creado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        CreadoPor NVARCHAR(100) DEFAULT 'Sistema',
        Modificado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        ModificadoPor NVARCHAR(100) DEFAULT 'Sistema',
        Eliminado BIT NOT NULL DEFAULT 0,
        FechaEliminacion DATETIME2 NULL,
        EliminadoPor NVARCHAR(100) NULL,
        
        CONSTRAINT FK_Usuario_RolUsuario FOREIGN KEY (RolUsuarioId) 
            REFERENCES RolUsuario(Id)
    );
    
    CREATE INDEX IX_Usuario_Email ON Usuario(Email);
    CREATE INDEX IX_Usuario_GoogleId ON Usuario(GoogleId);
    
    PRINT '✅ Tabla Usuario creada';
END
GO

-- =============================================
-- 3. TABLA: Liga
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Liga')
BEGIN
    CREATE TABLE Liga (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Nombre NVARCHAR(200) NOT NULL,
        Logo NVARCHAR(500) NULL,
        Descripcion NVARCHAR(1000) NULL,
        Pais NVARCHAR(100) NULL,
        Estado INT NOT NULL DEFAULT 1,
        
        -- Auditoría
        Creado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        CreadoPor NVARCHAR(100) DEFAULT 'Sistema',
        Modificado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        ModificadoPor NVARCHAR(100) DEFAULT 'Sistema',
        Eliminado BIT NOT NULL DEFAULT 0,
        FechaEliminacion DATETIME2 NULL,
        EliminadoPor NVARCHAR(100) NULL
    );
    
    PRINT '✅ Tabla Liga creada';
END
GO

-- =============================================
-- 4. TABLA: Torneo
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Torneo')
BEGIN
    CREATE TABLE Torneo (
        Id INT PRIMARY KEY IDENTITY(1,1),
        LigaId INT NOT NULL,
        Nombre NVARCHAR(200) NOT NULL,
        Logo NVARCHAR(500) NULL,
        FechaInicio DATETIME2 NOT NULL,
        FechaFin DATETIME2 NOT NULL,
        NumeroJornadas INT NOT NULL DEFAULT 17,
        Estado INT NOT NULL DEFAULT 1,
        
        -- Auditoría
        Creado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        CreadoPor NVARCHAR(100) DEFAULT 'Sistema',
        Modificado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        ModificadoPor NVARCHAR(100) DEFAULT 'Sistema',
        Eliminado BIT NOT NULL DEFAULT 0,
        FechaEliminacion DATETIME2 NULL,
        EliminadoPor NVARCHAR(100) NULL,
        
        CONSTRAINT FK_Torneo_Liga FOREIGN KEY (LigaId) 
            REFERENCES Liga(Id)
    );
    
    CREATE INDEX IX_Torneo_LigaId ON Torneo(LigaId);
    
    PRINT '✅ Tabla Torneo creada';
END
GO

-- =============================================
-- 5. TABLA: Equipo
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Equipo')
BEGIN
    CREATE TABLE Equipo (
        Id INT PRIMARY KEY IDENTITY(1,1),
        LigaId INT NOT NULL,
        Nombre NVARCHAR(200) NOT NULL,
        NombreCorto NVARCHAR(50) NULL,
        Logo NVARCHAR(500) NULL,
        Estadio NVARCHAR(200) NULL,
        Ciudad NVARCHAR(100) NULL,
        AñoFundacion INT NULL,
        ColorPrincipal NVARCHAR(50) NULL,
        ColorSecundario NVARCHAR(50) NULL,
        Estado INT NOT NULL DEFAULT 1,
        
        -- Auditoría
        Creado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        CreadoPor NVARCHAR(100) DEFAULT 'Sistema',
        Modificado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        ModificadoPor NVARCHAR(100) DEFAULT 'Sistema',
        Eliminado BIT NOT NULL DEFAULT 0,
        FechaEliminacion DATETIME2 NULL,
        EliminadoPor NVARCHAR(100) NULL,
        
        CONSTRAINT FK_Equipo_Liga FOREIGN KEY (LigaId) 
            REFERENCES Liga(Id)
    );
    
    CREATE INDEX IX_Equipo_LigaId ON Equipo(LigaId);
    
    PRINT '✅ Tabla Equipo creada';
END
GO

-- =============================================
-- 6. TABLA: EquipoTorneo (Tabla Intermedia)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'EquipoTorneo')
BEGIN
    CREATE TABLE EquipoTorneo (
        Id INT PRIMARY KEY IDENTITY(1,1),
        EquipoId INT NOT NULL,
        TorneoId INT NOT NULL,
        FechaInscripcion DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        DirectorTecnico NVARCHAR(200) NULL,
        
        -- Auditoría
        Creado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        CreadoPor NVARCHAR(100) DEFAULT 'Sistema',
        Modificado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        ModificadoPor NVARCHAR(100) DEFAULT 'Sistema',
        Eliminado BIT NOT NULL DEFAULT 0,
        FechaEliminacion DATETIME2 NULL,
        EliminadoPor NVARCHAR(100) NULL,
        
        CONSTRAINT FK_EquipoTorneo_Equipo FOREIGN KEY (EquipoId) 
            REFERENCES Equipo(Id),
        CONSTRAINT FK_EquipoTorneo_Torneo FOREIGN KEY (TorneoId) 
            REFERENCES Torneo(Id),
        CONSTRAINT UQ_EquipoTorneo_EquipoTorneo UNIQUE (EquipoId, TorneoId)
    );
    
    CREATE INDEX IX_EquipoTorneo_EquipoId ON EquipoTorneo(EquipoId);
    CREATE INDEX IX_EquipoTorneo_TorneoId ON EquipoTorneo(TorneoId);
    
    PRINT '✅ Tabla EquipoTorneo creada';
END
GO

-- =============================================
-- 7. TABLA: Jugador
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Jugador')
BEGIN
    CREATE TABLE Jugador (
        Id INT PRIMARY KEY IDENTITY(1,1),
        EquipoTorneoId INT NOT NULL,
        Nombre NVARCHAR(100) NOT NULL,
        Apellidos NVARCHAR(100) NOT NULL,
        FechaNacimiento DATE NOT NULL,
        Nacionalidad NVARCHAR(100) NULL,
        NumeroCamiseta INT NULL,
        Posicion INT NOT NULL, -- Enum: 1=Portero, 2=Defensa, 3=Medio, 4=Delantero
        Foto NVARCHAR(500) NULL,
        Altura DECIMAL(5,2) NULL, -- en metros
        Peso DECIMAL(5,2) NULL, -- en kg
        Estado INT NOT NULL DEFAULT 1,
        
        -- Auditoría
        Creado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        CreadoPor NVARCHAR(100) DEFAULT 'Sistema',
        Modificado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        ModificadoPor NVARCHAR(100) DEFAULT 'Sistema',
        Eliminado BIT NOT NULL DEFAULT 0,
        FechaEliminacion DATETIME2 NULL,
        EliminadoPor NVARCHAR(100) NULL,
        
        CONSTRAINT FK_Jugador_EquipoTorneo FOREIGN KEY (EquipoTorneoId) 
            REFERENCES EquipoTorneo(Id)
    );
    
    CREATE INDEX IX_Jugador_EquipoTorneoId ON Jugador(EquipoTorneoId);
    
    PRINT '✅ Tabla Jugador creada';
END
GO

-- =============================================
-- 8. TABLA: Partido
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Partido')
BEGIN
    CREATE TABLE Partido (
        Id INT PRIMARY KEY IDENTITY(1,1),
        TorneoId INT NOT NULL,
        LocalId INT NOT NULL, -- EquipoTorneo
        VisitanteId INT NOT NULL, -- EquipoTorneo
        Jornada INT NOT NULL,
        FechaHora DATETIME2 NOT NULL,
        Estadio NVARCHAR(200) NULL,
        Estado INT NOT NULL DEFAULT 1, -- Enum: 1=Programado, 2=EnJuego, 3=Finalizado, 4=Cancelado
        
        -- Resultado
        GolesLocal INT NULL,
        GolesVisitante INT NULL,
        AsistenciaPublico INT NULL,
        
        -- Árbitros
        ArbitroPrincipal NVARCHAR(200) NULL,
        Arbitro1 NVARCHAR(200) NULL,
        Arbitro2 NVARCHAR(200) NULL,
        CuartoArbitro NVARCHAR(200) NULL,
        
        -- Auditoría
        Creado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        CreadoPor NVARCHAR(100) DEFAULT 'Sistema',
        Modificado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        ModificadoPor NVARCHAR(100) DEFAULT 'Sistema',
        Eliminado BIT NOT NULL DEFAULT 0,
        FechaEliminacion DATETIME2 NULL,
        EliminadoPor NVARCHAR(100) NULL,
        
        CONSTRAINT FK_Partido_Torneo FOREIGN KEY (TorneoId) 
            REFERENCES Torneo(Id),
        CONSTRAINT FK_Partido_Local FOREIGN KEY (LocalId) 
            REFERENCES EquipoTorneo(Id),
        CONSTRAINT FK_Partido_Visitante FOREIGN KEY (VisitanteId) 
            REFERENCES EquipoTorneo(Id)
    );
    
    CREATE INDEX IX_Partido_TorneoId ON Partido(TorneoId);
    CREATE INDEX IX_Partido_LocalId ON Partido(LocalId);
    CREATE INDEX IX_Partido_VisitanteId ON Partido(VisitanteId);
    CREATE INDEX IX_Partido_FechaHora ON Partido(FechaHora);
    
    PRINT '✅ Tabla Partido creada';
END
GO

-- =============================================
-- 9. TABLA: Estadistica
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Estadistica')
BEGIN
    CREATE TABLE Estadistica (
        Id INT PRIMARY KEY IDENTITY(1,1),
        PartidoId INT NOT NULL,
        JugadorId INT NOT NULL,
        Tipo INT NOT NULL, -- Enum: 1=Gol, 2=TarjetaAmarilla, 3=TarjetaRoja, 4=Asistencia, 5=AutoGol
        Minuto INT NOT NULL,
        Descripcion NVARCHAR(500) NULL,
        JugadorAsistenteId INT NULL, -- Para goles con asistencia
        
        -- Auditoría
        Creado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        CreadoPor NVARCHAR(100) DEFAULT 'Sistema',
        Modificado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        ModificadoPor NVARCHAR(100) DEFAULT 'Sistema',
        Eliminado BIT NOT NULL DEFAULT 0,
        FechaEliminacion DATETIME2 NULL,
        EliminadoPor NVARCHAR(100) NULL,
        
        CONSTRAINT FK_Estadistica_Partido FOREIGN KEY (PartidoId) 
            REFERENCES Partido(Id),
        CONSTRAINT FK_Estadistica_Jugador FOREIGN KEY (JugadorId) 
            REFERENCES Jugador(Id),
        CONSTRAINT FK_Estadistica_JugadorAsistente FOREIGN KEY (JugadorAsistenteId) 
            REFERENCES Jugador(Id)
    );
    
    CREATE INDEX IX_Estadistica_PartidoId ON Estadistica(PartidoId);
    CREATE INDEX IX_Estadistica_JugadorId ON Estadistica(JugadorId);
    
    PRINT '✅ Tabla Estadistica creada';
END
GO

-- =============================================
-- 10. TABLA: Alineacion
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Alineacion')
BEGIN
    CREATE TABLE Alineacion (
        Id INT PRIMARY KEY IDENTITY(1,1),
        PartidoId INT NOT NULL,
        JugadorId INT NOT NULL,
        EsTitular BIT NOT NULL DEFAULT 1,
        MinutoEntrada INT NULL,
        MinutoSalida INT NULL,
        
        -- Auditoría
        Creado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        CreadoPor NVARCHAR(100) DEFAULT 'Sistema',
        Modificado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        ModificadoPor NVARCHAR(100) DEFAULT 'Sistema',
        Eliminado BIT NOT NULL DEFAULT 0,
        FechaEliminacion DATETIME2 NULL,
        EliminadoPor NVARCHAR(100) NULL,
        
        CONSTRAINT FK_Alineacion_Partido FOREIGN KEY (PartidoId) 
            REFERENCES Partido(Id),
        CONSTRAINT FK_Alineacion_Jugador FOREIGN KEY (JugadorId) 
            REFERENCES Jugador(Id)
    );
    
    CREATE INDEX IX_Alineacion_PartidoId ON Alineacion(PartidoId);
    CREATE INDEX IX_Alineacion_JugadorId ON Alineacion(JugadorId);
    
    PRINT '✅ Tabla Alineacion creada';
END
GO

-- =============================================
-- 11. TABLA: Castigo
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Castigo')
BEGIN
    CREATE TABLE Castigo (
        Id INT PRIMARY KEY IDENTITY(1,1),
        JugadorId INT NOT NULL,
        Tipo INT NOT NULL, -- Enum: 1=Suspension, 2=Multa, 3=Amonestacion
        PartidosSuspension INT NOT NULL DEFAULT 0,
        Motivo NVARCHAR(1000) NULL,
        FechaInicio DATETIME2 NOT NULL,
        FechaFin DATETIME2 NULL,
        Activo BIT NOT NULL DEFAULT 1,
        
        -- Auditoría
        Creado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        CreadoPor NVARCHAR(100) DEFAULT 'Sistema',
        Modificado DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        ModificadoPor NVARCHAR(100) DEFAULT 'Sistema',
        Eliminado BIT NOT NULL DEFAULT 0,
        FechaEliminacion DATETIME2 NULL,
        EliminadoPor NVARCHAR(100) NULL,
        
        CONSTRAINT FK_Castigo_Jugador FOREIGN KEY (JugadorId) 
            REFERENCES Jugador(Id)
    );
    
    CREATE INDEX IX_Castigo_JugadorId ON Castigo(JugadorId);
    
    PRINT '✅ Tabla Castigo creada';
END
GO

-- =============================================
-- DATOS INICIALES
-- =============================================

-- Insertar Roles
IF NOT EXISTS (SELECT * FROM RolUsuario)
BEGIN
    INSERT INTO RolUsuario (Nombre, Descripcion, Tipo) VALUES
    ('Administrador', 'Acceso completo al sistema', 1),
    ('Operador', 'Puede gestionar ligas, torneos, equipos y partidos', 2),
    ('Consultor', 'Solo lectura de información', 3);
    
    PRINT '✅ Roles de usuario insertados';
END
GO

PRINT '';
PRINT '🎉 ¡BASE DE DATOS CREADA EXITOSAMENTE!';
PRINT '';
PRINT '📊 Tablas creadas:';
PRINT '  1. RolUsuario';
PRINT '  2. Usuario';
PRINT '  3. Liga';
PRINT '  4. Torneo';
PRINT '  5. Equipo';
PRINT '  6. EquipoTorneo';
PRINT '  7. Jugador';
PRINT '  8. Partido';
PRINT '  9. Estadistica';
PRINT '  10. Alineacion';
PRINT '  11. Castigo';
PRINT '';
PRINT '✅ Sistema listo para usar';
GO
USE [BdiExamen];
GO

-- Drop condicional para permitir re-ejecutar el script durante desarrollo/pruebas
IF OBJECT_ID('dbo.tblExamen', 'U') IS NOT NULL
    DROP TABLE dbo.tblExamen;
GO

-- Catálogo principal del examen (altas/bajas/modificaciones)
CREATE TABLE dbo.tblExamen (
    Id INT IDENTITY(1,1) NOT NULL,   -- autogenerado; el SP no lo recibe en el INSERT
    Nombre NVARCHAR(100) NOT NULL,   -- 100 chars es margen suficiente para nombres de catálogo
    Descripcion NVARCHAR(500) NULL,  -- opcional, no toda entrada del catálogo trae descripción
    CONSTRAINT PK_tblExamen PRIMARY KEY (Id)
);
GO
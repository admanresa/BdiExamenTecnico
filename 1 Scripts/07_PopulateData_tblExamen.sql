USE [BdiExamen];
GO

-- Datos de prueba para poder validar el listado/búsqueda sin capturar manualmente
INSERT INTO dbo.tblExamen (Nombre, Descripcion) VALUES
    (N'Producto Demo 1', N'Descripción de ejemplo para pruebas'),
    (N'Producto Demo 2', N'Otro registro de prueba'),
    (N'Producto Demo 3', NULL); -- valido el caso de descripción opcional
GO
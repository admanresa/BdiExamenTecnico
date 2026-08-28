-- Creación de la base solo si no existe, para poder correr el script
-- las veces que haga falta sin tronar en ambientes ya inicializados
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'BdiExamen')
    CREATE DATABASE BdiExamen;
GO
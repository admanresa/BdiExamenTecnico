USE [BdiExamen];
GO

-- Consulta con filtros opcionales (Id, Nombre, Descripcion) para que el WebService
-- arme distintos criterios de búsqueda sin necesitar varios SPs.
-- No maneja CodigoRetorno/DescripcionRetorno como los otros SPs porque aquí
-- el resultado (con o sin filas) ya es suficiente respuesta para el consumidor.
IF OBJECT_ID('dbo.spConsultarExamenes', 'P') IS NOT NULL
    DROP PROCEDURE dbo.spConsultarExamenes;
GO

CREATE PROCEDURE dbo.spConsultarExamenes
    @Id          INT = NULL,
    @Nombre      NVARCHAR(100) = NULL,
    @Descripcion NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Sql NVARCHAR(MAX);
    DECLARE @Params NVARCHAR(MAX);

    SET @Sql = N'SELECT Id, Nombre, Descripcion FROM dbo.tblExamen WHERE 1=1';

    IF @Id IS NOT NULL
        SET @Sql += N' AND Id = @Id';

    IF NULLIF(@Nombre, N'') IS NOT NULL
        SET @Sql += N' AND Nombre LIKE N''%'' + @Nombre + N''%''';

    IF NULLIF(@Descripcion, N'') IS NOT NULL
        SET @Sql += N' AND Descripcion LIKE N''%'' + @Descripcion + N''%''';

    SET @Sql += N' ORDER BY Id;';

    SET @Params = N'@Id INT, @Nombre NVARCHAR(100), @Descripcion NVARCHAR(MAX)';

    EXEC sp_executesql @Sql, @Params, @Id = @Id, @Nombre = @Nombre, @Descripcion = @Descripcion;
END
GO
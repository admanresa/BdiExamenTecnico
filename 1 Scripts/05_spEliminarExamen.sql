USE [BdiExamen];
GO

IF OBJECT_ID('dbo.spEliminarExamen', 'P') IS NOT NULL
    DROP PROCEDURE dbo.spEliminarExamen;
GO

CREATE PROCEDURE dbo.spEliminarExamen
    @Id                 INT, -- Requerido desde C#
    @CodigoRetorno      INT OUTPUT,
    @DescripcionRetorno NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET @CodigoRetorno = 0;
    SET @DescripcionRetorno = N'';

    BEGIN TRY
        -- Validar existencia del registro
        IF NOT EXISTS (SELECT 1 FROM dbo.tblExamen WHERE Id = @Id)
        BEGIN
            SET @CodigoRetorno = 1;
            SET @DescripcionRetorno = N'No existe un registro con el Id indicado.';
            RETURN;
        END

        -- Eliminación física
        DELETE FROM dbo.tblExamen WHERE Id = @Id;

        SET @DescripcionRetorno = N'Se eliminó el registro.';
    END TRY
    BEGIN CATCH
        -- Captura errores de base de datos (ej. Llave foránea / FK constraint)
        SET @CodigoRetorno = ERROR_NUMBER();
        SET @DescripcionRetorno = ERROR_MESSAGE();
    END CATCH
END
GO
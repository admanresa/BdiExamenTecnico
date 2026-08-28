USE [BdiExamen];
GO

-- Actualiza los datos del examen; si no existe, devuelve el código de error 1.
IF OBJECT_ID('dbo.spActualizarExamen', 'P') IS NOT NULL
    DROP PROCEDURE dbo.spActualizarExamen;
GO

CREATE PROCEDURE dbo.spActualizarExamen
    @Id                 INT,
    @Nombre             NVARCHAR(100),
    @Descripcion        NVARCHAR(500) = NULL,
    @CodigoRetorno      INT OUTPUT,
    @DescripcionRetorno NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET @CodigoRetorno = 0;
    SET @DescripcionRetorno = N'';

    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.tblExamen WHERE Id = @Id)
        BEGIN
            SET @CodigoRetorno = 1;
            SET @DescripcionRetorno = N'No existe un registro con el Id indicado.';
            RETURN;
        END

        UPDATE dbo.tblExamen
        SET Nombre = @Nombre,
            Descripcion = @Descripcion
        WHERE Id = @Id;

        SET @DescripcionRetorno = N'Se actualizó el registro.';
    END TRY
    BEGIN CATCH
        SET @CodigoRetorno = ERROR_NUMBER();
        SET @DescripcionRetorno = ERROR_MESSAGE();
    END CATCH
END
GO
USE [BdiExamen];
GO

-- Alta de un registro en el catálogo.
-- Devolvemos el Id generado (IDENTITY) y controlamos cualquier excepción
-- para que el WebService siempre reciba código/mensaje, nunca una excepción cruda.
IF OBJECT_ID('dbo.spAgregarExamen', 'P') IS NOT NULL
    DROP PROCEDURE dbo.spAgregarExamen;
GO

CREATE PROCEDURE dbo.spAgregarExamen
    @Nombre             NVARCHAR(100),
    @Descripcion        NVARCHAR(500) = NULL,
    @IdGenerado         INT OUTPUT,
    @CodigoRetorno      INT OUTPUT,
    @DescripcionRetorno NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET @CodigoRetorno = 0;
    SET @DescripcionRetorno = N'';

    BEGIN TRY
        INSERT INTO dbo.tblExamen (Nombre, Descripcion)
        VALUES (@Nombre, @Descripcion);

        SET @IdGenerado = SCOPE_IDENTITY();
        SET @DescripcionRetorno = N'Se insertó el registro.';
    END TRY
    BEGIN CATCH
        SET @CodigoRetorno = ERROR_NUMBER();
        SET @DescripcionRetorno = ERROR_MESSAGE();
    END CATCH
END
GO
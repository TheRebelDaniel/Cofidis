CREATE PROCEDURE sp_GetClientById
	@ClientID NVARCHAR(50)
AS
BEGIN
	SELECT * FROM dbo.Clients WHERE ClientId = @ClientID;
END;
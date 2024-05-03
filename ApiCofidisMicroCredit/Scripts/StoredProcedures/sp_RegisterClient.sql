CREATE PROCEDURE sp_RegisterClient
	@FiscalNumber NVARCHAR(50),
	@MonthlyIncome DECIMAL(12,2),
	@ActualEconomicSituation DECIMAL(12,2),
	@CreditHistories AS CreditHistoriesType READONLY,
	@ClientDebts AS ClientDebtsType READONLY
AS
BEGIN
	SET NOCOUNT ON;

	--Create new user
	INSERT INTO dbo.Clients (FiscalNumber, MonthlyIncome, ActualEconomicSituation)
	VALUES (@FiscalNumber,@MonthlyIncome, @ActualEconomicSituation);

	DECLARE @ClientId INT;
    SET @ClientId = SCOPE_IDENTITY();

	IF @ClientId IS NOT NULL
	BEGIN
		--Insert Client credit history
		INSERT INTO dbo.CreditHistories (ClientId, CreditAmount)
		SELECT @ClientId, CreditAmount
		FROM @CreditHistories

		--Insert client debts
		INSERT INTO dbo.ClientDebts (ClientId, DebtAmount)
		SELECT @ClientId, DebtAmount
		FROM @ClientDebts
	END
	--return clientID
	SELECT @ClientId;
END;


CREATE PROCEDURE sp_ClientCreditLimitDetermination
	@ClientID NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @LastMonthIncome DECIMAL(10,2);
	DECLARE @MaxCreditLimit INT;

	-- Get client's sum of the last month incomes
	SELECT @LastMonthIncome = SUM(cth.IncomeValue)
	FROM dbo.ClientTransactionHisory AS cth
	WHERE
	cth.ClientID = @ClientID AND
	cth.TransactionType = 'Income'
	AND MONTH(cth.TransactionDateTime) = MONTH(GETDATE()) - 1

	-- Calculate client's max credit limit based on the last month incomes
	IF @LastMonthIncome <= 1000
	BEGIN
		SET @MaxCreditLimit = 1000;
	END
	ELSE IF @LastMonthIncome > 1000 AND @LastMonthIncome <= 2000
	BEGIN
		SET @MaxCreditLimit = 2000
	END
	ELSE
	BEGIN
		SET @MaxCreditLimit = 5000
	END

	-- Returns max credit limit
	RETURN @MaxCreditLimit;
END;
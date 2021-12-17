USE [CoinBaseDB]
GO

/****** Object:  StoredProcedure [dbo].[SaveCoinBaseExchangeRates]    Script Date: 12/17/2021 2:59:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create PROCEDURE [dbo].[SaveCoinBaseExchangeRates]
@ExchangeRate VarChar(80),
@Key VarChar(80),
@Value VarChar(80)
AS 
BEGIN 
     SET NOCOUNT ON 

     INSERT INTO dbo.requests
          ( 
		    exchangerate,                 
            [Key] ,
            value                              
          ) 
     VALUES 
          (
		    @ExchangeRate,
            @Key,
            @Value
          ) 

END 
GO


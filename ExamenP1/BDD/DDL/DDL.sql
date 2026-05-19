IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'FederacionFutbol')
BEGIN
    CREATE DATABASE FederacionFutbol;
END
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'TicketPremium')
BEGIN
    CREATE DATABASE TicketPremium;
END
GO
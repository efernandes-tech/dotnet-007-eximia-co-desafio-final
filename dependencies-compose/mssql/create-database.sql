IF NOT EXISTS (SELECT *
FROM sys.databases
WHERE name = 'EscolaEximiaDB')
BEGIN
    CREATE DATABASE EscolaEximiaDB;
END

IF NOT EXISTS (SELECT *
FROM sys.databases
WHERE name = 'PlataformaClienteFinalDB')
BEGIN
    CREATE DATABASE PlataformaClienteFinalDB;
END

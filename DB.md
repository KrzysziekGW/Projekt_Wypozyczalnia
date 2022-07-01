# Wypozyczalnia_DB
--////////////////////////////////-- 1.Tworzenie i uzupełnianie bazy danych --\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

USE master;
GO
IF DB_ID (N'Wypozyczalnia') IS NOT NULL
DROP DATABASE Wypozyczalnia; 

CREATE DATABASE Wypozyczalnia
COLLATE Polish_100_CI_AS; 
GO
IF DB_ID (N'Wypozyczalnia') IS NOT NULL 
SELECT 'Pomyślnie utworzono bazę danych'


USE [Wypozyczalnia]

CREATE TABLE [dbo].[Klienci](
	[ID] [int] PRIMARY KEY IDENTITY,
	[Imie] [nvarchar] (50) NOT NULL,
	[Nazwisko] [nvarchar] (100) NOT NULL,
	[PESEL] [nvarchar] (20) NOT NULL,
	[Email] [nvarchar] (250) NULL,
	[Telefon] [nvarchar] (20) NOT NULL)

CREATE TABLE [dbo].[Statusy](
	[ID] [int] PRIMARY KEY IDENTITY,
	[Nazwa] [nvarchar] (250) UNIQUE NOT NULL)


CREATE TABLE [dbo].[Pracownicy](
	[ID] [int] PRIMARY KEY IDENTITY,
	[Imie] [nvarchar] (50) NOT NULL,
	[Nazwisko] [nvarchar] (100) NOT NULL,
	[Telefon] [nvarchar] (20) NOT NULL)


CREATE TABLE [dbo].[Narty](
	[ID] [int] PRIMARY KEY IDENTITY,
	[Nazwa] [nvarchar] (250) NOT NULL,
	[Długość] [int] NOT NULL,
	[StatusID] [int] FOREIGN KEY REFERENCES Statusy(ID),
	[Cena] [decimal] (8,2) NOT NULL)


CREATE TABLE [dbo].[Buty](
	[ID] [int] PRIMARY KEY IDENTITY,
	[Nazwa] [nvarchar] (250) NOT NULL,
	[Rozmiar] [int] NOT NULL,
	[StatusID] [int] FOREIGN KEY REFERENCES Statusy(ID),
	[Cena] [decimal] (8,2) NOT NULL)
	

CREATE TABLE [dbo].[Kaski](
	[ID] [int] PRIMARY KEY IDENTITY,
	[Nazwa] [nvarchar] (250) NOT NULL,
	[Rozmiar] [nvarchar] (20) NOT NULL,
	[StatusID] [int] FOREIGN KEY REFERENCES Statusy(ID),
	[Cena] [decimal] (8,2) NOT NULL)



CREATE TABLE [dbo].[Kijki](
	[ID] [int] PRIMARY KEY IDENTITY,
	[Nazwa] [nvarchar] (250) NOT NULL,
	[Długość] [int] NOT NULL,
	[StatusID] [int] FOREIGN KEY REFERENCES Statusy(ID),
	[Cena] [decimal] (8,2) NOT NULL)



CREATE TABLE [dbo].[Zestaw](
	[ID] [int] PRIMARY KEY IDENTITY,
	[NartyID] [int] FOREIGN KEY REFERENCES Narty(ID),
	[ButyID] [int] FOREIGN KEY REFERENCES Buty(ID),
	[KaskiID] [int] FOREIGN KEY REFERENCES Kaski(ID),
	[KijkiID] [int] FOREIGN KEY REFERENCES Kijki(ID),
	[CenaZestawu] [decimal] (8,2))


CREATE TABLE [dbo].[Wypożyczone](
	[ID] [int] PRIMARY KEY IDENTITY,
	[KlienciID] [int] FOREIGN KEY REFERENCES Klienci(ID),
	[ZestawID] [int] FOREIGN KEY REFERENCES Zestaw(ID),
	[DataWypożyczenia] [datetime] NOT NULL, 
	[DataOddania] [datetime],
	[PracownikID] [int] FOREIGN KEY REFERENCES Pracownicy(ID),
	[KwotaDoZapłaty] [decimal] (8,2))

INSERT INTO Statusy (Nazwa)
VALUES ('Dostępne'),
('Wyporzyczone')

INSERT INTO Pracownicy (Imie, Nazwisko, Telefon) 
VALUES ('Wiktor', 'Dudziak','885114851'),
('Zbigniew', 'Chrupek','512516925'),
('Andrzej', 'Twardowski','512617626')

INSERT INTO Narty(Nazwa, Długość, StatusID, Cena) 
VALUES ('Fischer RC4', '160', 1, '20'),
('Fischer RC4 RCS Black', '140', 1, '20'),
('Head Worldcup', '175', 1, '20'),
('Head Supershape E-Titan', '155', 1, '20'),
('Head Supershape E-Original', '145', 1, '20'),
('Rossignol Elite ST TI', '165', 1, '20'),
('Rossignol Elite Plus TI', '150', 1, '20'),
('Fischer RC4 The Curv DTX', '160', 1, '20'),
('Fischer RC4 Worldcup CT', '150', 1, '20'),
('Fischer RC4 The Curv', '170', 1, '20'),
('Head Worldcup Rebels', '160', 1, '20'),
('Fischer RC4 Worldcup SC', '155', 1, '20'),
('Fischer RC4 RCS', '140', 1, '20'),
('Rossignol Elite Plus TI', '165', 1, '20'),
('Fischer RC4 The Curv DTX', '170', 1, '20'),
('Head Supershape E-Original', '155', 1, '20'),
('Rossignol Elite ST TI', '175', 1, '20'),
('Fischer RC4', '165', 1, '20'),
('Fischer RC4 RCS Black', '155', 1, '20'),
('Rossignol Elite Plus TI', '150', 1, '20')


INSERT INTO Buty(Nazwa, Rozmiar,StatusID, Cena) 
VALUES ('Rossignol Alias', '38', 1, '10'),
('Head Advant Edge', '41', 1, '10'),
('Rossignol Allspeed', '43', 1, '10'),
('Fischer Cruzar X', '42', 1, '10'),
('Rossignol Speed', '39', 1, '10'),
('Salomon X Pro Cruise', '40', 1, '10'),
('Rossignol Track', '42', 1, '10'),
('Fischer Cruzar', '41', 1, '10'),
('Fischer RC Pro', '38', 1, '10'),
('Head Edge LYT', '37', 1, '10'),
('Fischer RC One', '36', 1, '10'),
('Head Cube', '41', 1, '10'),
('Nordica Cruise', '40', 1, '10'),
('Rossignol Allspeed Pro', '44', 1, '10'),
('Dalbello Panterra', '45', 1, '10'),
('Salomon X Max', '42', 1, '10'),
('Head Nexo LYT', '42', 1, '10'),
(' Fischer RC One', '41', 1, '10'),
('Fischer RC4 The Curv', '38', 1, '10'),
('Head Vector', '39', 1, '10')


INSERT INTO Kaski(Nazwa, Rozmiar,StatusID, Cena) 
VALUES ('Marker Consort', 'M', 1, '5'),
(' Uvex Skid', 'M', 1, '5'),
('Head Vico', 'L', 1, '5'),
('Head Trex', 'S', 1, '5'),
('P1us Rent', 'L', 1, '5'),
('Salomon Pioneer LT', 'M', 1, '5'),
('Salomon Cruiser2', 'M', 1, '5'),
('Salomon Pioneer Red Beluga', 'S', 1, '5'),
('Sinner Titan Matte', 'L', 1, '5'),
('Head Varius', 'M', 1, '5'),
('Head Compact Anthracite', 'M', 1, '5'),
('Head Compact Dusky', 'S', 1, '5'),
('Uvex Fierce', 'S', 2, '5'),
('Uvex Fierce Strato', 'L', 1, '5'),
('Uvex P1us', 'L', 1, '5'),
('Uvex Jimm Octo', 'S', 1, '5'),
('Uvex Jakk', 'M', 1, '5'),
('Sinner Moonstone', 'L', 1, '5'),
('Head Varius Anthracite', 'M', 1, '5'),
('Giro Range Mips', 'S', 1, '5')


INSERT INTO Kijki(Nazwa, Długość,StatusID, Cena) 
VALUES ('Komperdell Camaro Mix', '120', 1, '5'),
('Gabel Speed', '125', 1, '5'),
('Swix Techlite MS', '130', 1, '5'),
('Head Multi S', '110', 1, '5'),
('Salomon X North', '120', 1, '5'),
('Rossignol Tactic', '130', 1, '5'),
('Head Multi', '110', 1, '5'),
('Fischer Unlimited', '135', 1, '5'),
('Salomon Arctic', '115', 1, '5'),
('Komperdell Carv', '125', 2, '5'),
('Leki Vista', '125', 1, '5'),
('Head Multi Alu', '130', 1, '5'),
('Atomic Black', '110', 1, '5'),
('One Way GT', '115', 1, '5'),
('Fischer Progressor Neutral', '130', 1, '5'),
('Fischer Pro Alu', '125', 1, '5'),
('Swix Plus', '135', 1, '5'),
('Head Kore', '135', 1, '5'),
('Fischer Pro Turn XTD', '120', 1, '5'),
('Head Airfoil', '110', 1, '5')


--////////////////////////////////-- 2. Tworzenie widoków --\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\


--Widok 1
CREATE VIEW dbo.v_PokażKlientów
AS
	select Imie,Nazwisko,Telefon,KlienciID,ZestawID,DataWypożyczenia,DataOddania,KwotaDoZapłaty from Klienci
	inner join Wypożyczone on Wypożyczone.KlienciID = Klienci.ID

--Widok 2
CREATE VIEW dbo.v_DzisiejszeWypożyczenia
AS
Select * from v_PokażKlientów where DataWypożyczenia = Convert(date, getdate())

--Widok 3
CREATE VIEW dbo.v_DzisiejszeZwroty
AS
Select * from v_PokażKlientów where DataOddania = Convert(date, getdate())

/*Wartości null w tabeli są spowodowane, tym że jeszcze klienci nie oddali sprzętu i nie możemy obliczyć Kwoty do zapłaty,
ponieważ nie wiemy ile dni zestaw jest na wypożyczeniu*/

--////////////////////////////////-- 3. Tworzenie Funkcji --\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

--Funkcja 1
CREATE FUNCTION dbo.ObliczCeneZestawu
	(@ZestawID INT)
RETURNS decimal (8,2) 
AS
BEGIN
	DECLARE @CenaZestawu decimal (8,2)
	SET @CenaZestawu = (
		select SUM(Cena) from(
				Select cena from Narty inner join Zestaw on Zestaw.NartyID = narty.id 
				where Zestaw.id = @ZestawID
				union all
				Select cena from Buty inner join Zestaw on Zestaw.ButyID = Buty.id
				where Zestaw.id = @ZestawID
				union all
				Select cena from Kaski inner join Zestaw on Zestaw.KaskiID = Kaski.id 
				where Zestaw.id = @ZestawID
				union all
				Select cena from Kijki inner join Zestaw on Zestaw.KijkiID = Kijki.id
				where Zestaw.id = @ZestawID
				)AS Cena
			)
	RETURN @CenaZestawu 
END

/* Ceny pojedyńczych przedmiotów są z góry ustalone:
-wszystkie narty po 20zl
-wszystkie buty po 10zl
-wszystkie kask po 5zl
-wszystkie kijki po 5zl
*/

--Funkcja 2

CREATE FUNCTION dbo.ObliczIlośćDni
	(@KlientID INT,@ZestawID INT)
RETURNS INT 
AS
BEGIN
	DECLARE @IlośćDni INT
	SET @IlośćDni = (
		SELECT (DATEDIFF(DAY,DataWypożyczenia,DataOddania)) 
		FROM Wypożyczone
		WHERE KlienciID = @KlientID AND ZestawID = @ZestawID 
	)			
	RETURN @IlośćDni 
END

Select dbo.ObliczIlośćDni(4,5)
Select * from Wypożyczone
--Funkcja 3
CREATE FUNCTION WyszukajKlienta
	(@Imie nvarchar(50),@Nazwisko nvarchar(50),@Telefon nvarchar(10))	
RETURNS TABLE
AS
RETURN 
select Imie,Nazwisko,Telefon,KlienciID,ZestawID,DataWypożyczenia,DataOddania,KwotaDoZapłaty from Klienci
	inner join Wypożyczone on Wypożyczone.KlienciID = Klienci.ID
	where Imie = @Imie AND Nazwisko = @Nazwisko AND Telefon = @Telefon

--Funkcja 4
CREATE FUNCTION PokażZestawKlienta
	(@KlientID int)	
RETURNS TABLE
AS
RETURN 
	Select Wypożyczone.ZestawID,Nazwa,convert(nvarchar,Długość) AS Rozmiar from Wypożyczone 
	inner join Zestaw on Zestaw.ID = Wypożyczone.ZestawID 
	inner join Narty on Narty.id = Zestaw.NartyID
	where KlienciID = @KlientID
		union all
	Select Wypożyczone.ZestawID,Nazwa,convert(nvarchar,Rozmiar)AS Rozmiar from Wypożyczone 
	inner join Zestaw on Zestaw.ID = Wypożyczone.ZestawID 
	inner join Buty on Buty.id = Zestaw.ButyID
	where KlienciID = @KlientID
		union all
	Select Wypożyczone.ZestawID,Nazwa,Rozmiar AS Rozmiar from Wypożyczone 
	inner join Zestaw on Zestaw.ID = Wypożyczone.ZestawID 
	inner join Kaski on Kaski.id = Zestaw.KaskiID
	where KlienciID = @KlientID
		union all
	Select Wypożyczone.ZestawID,Nazwa,convert(nvarchar,Długość)AS Rozmiar from Wypożyczone 
	inner join Zestaw on Zestaw.ID = Wypożyczone.ZestawID 
	inner join Kijki on Kijki.id = Zestaw.KijkiID
	where KlienciID = @KlientID


--////////////////////////////////-- 4. Tworzenie Procedur --\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

--Procedura 1
CREATE PROCEDURE DodajKlienta
	@Imie nvarchar (50),
	@Nazwisko nvarchar (100),
	@PESEL nvarchar (20),
	@Email nvarchar (250),
	@Telefon nvarchar (20)

AS
BEGIN
	INSERT Klienci
	VALUES (@Imie, @Nazwisko, @PESEL, @Email, @Telefon)
END

--Procedura 2
CREATE PROCEDURE DodajZestaw
	@NartyID int,
	@ButyID int,
	@KaskiID int,
	@KijkiID int
AS
BEGIN
	INSERT Zestaw(NartyID,ButyID,KaskiID,KijkiID)
	VALUES (@NartyID, @ButyID, @KaskiID, @KijkiID)
END

--Procedura 3
CREATE PROCEDURE DodajCeneZestawu
	@ZestawID INT
AS
BEGIN	
		UPDATE Zestaw
		SET CenaZestawu = (dbo.ObliczCeneZestawu(@ZestawID))
		WHERE id = @ZestawID
END

--Procedura 4
CREATE PROCEDURE Wypożycz
	@KlientID int,
	@ZestawID int,
	@DataWypożyczenia Datetime,
	@DataOddania Datetime,
	@PracownikID int
AS
BEGIN
	INSERT Wypożyczone
	VALUES (@KlientID, @ZestawID, @DataWypożyczenia, @DataOddania, @PracownikID, null)
END

--Procedura 5
CREATE PROCEDURE ZmieńStatus
	@ZestawID INT,
	@StausID INT
AS
BEGIN
		UPDATE Narty 
		Set StatusID = @StausID
		from Narty inner join Zestaw on Zestaw.NartyID = narty.id 
		where Zestaw.id = @ZestawID
		UPDATE Buty
		Set StatusID = @StausID
		from Buty inner join Zestaw on Zestaw.ButyID = Buty.id
		where Zestaw.id = @ZestawID
		UPDATE Kaski
		Set StatusID = @StausID
		from Kaski inner join Zestaw on Zestaw.KaskiID = Kaski.id 
		where Zestaw.id = @ZestawID
		UPDATE Kijki
		Set StatusID = @StausID
		from Kijki inner join Zestaw on Zestaw.KijkiID = Kijki.id
		where Zestaw.id = @ZestawID
END

--Procedura 6
CREATE PROCEDURE ZwrotZestawu
	@KlientID INT,
	@ZestawID INT,
	@DataOddania DATETIME
AS
BEGIN	
		UPDATE Wypożyczone
		SET DataOddania = @DataOddania
		WHERE KlienciID = @KlientID AND ZestawID = @ZestawID
END

--Procedura 7
CREATE PROCEDURE DodajKwoteDoZapłaty
	@KlientID INT,
	@ZestawID INT
AS
BEGIN	
		UPDATE Wypożyczone
		SET KwotaDoZapłaty = (dbo.ObliczIlośćDni(@KlientID,@ZestawID) * CenaZestawu)
		from Wypożyczone inner join Zestaw on Zestaw.ID = Wypożyczone.ZestawID
		WHERE Wypożyczone.KlienciID = @KlientID AND Wypożyczone.ZestawID = @ZestawID 
END

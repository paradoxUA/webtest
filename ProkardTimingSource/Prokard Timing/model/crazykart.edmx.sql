
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 09/12/2014 09:32:32
-- Generated from EDMX file: E:\inetpub\wwwroot\finished\crazyCart\source\ProkardTimingSource\Prokard Timing\model\crazykart.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [crazykart];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_certificateusers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[certificates] DROP CONSTRAINT [FK_certificateusers];
GO
IF OBJECT_ID(N'[dbo].[FK_fuelkarts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[fuel] DROP CONSTRAINT [FK_fuelkarts];
GO
IF OBJECT_ID(N'[dbo].[FK_usersgroups]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[users] DROP CONSTRAINT [FK_usersgroups];
GO
IF OBJECT_ID(N'[dbo].[FK_racestracks]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[races] DROP CONSTRAINT [FK_racestracks];
GO
IF OBJECT_ID(N'[dbo].[FK_jurnalraces]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[jurnal] DROP CONSTRAINT [FK_jurnalraces];
GO
IF OBJECT_ID(N'[dbo].[FK_jurnalusers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[jurnal] DROP CONSTRAINT [FK_jurnalusers];
GO
IF OBJECT_ID(N'[dbo].[FK_noracekartraces]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[noracekart] DROP CONSTRAINT [FK_noracekartraces];
GO
IF OBJECT_ID(N'[dbo].[FK_pricesrace_modes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[prices] DROP CONSTRAINT [FK_pricesrace_modes];
GO
IF OBJECT_ID(N'[dbo].[FK_race_dataraces]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[race_data] DROP CONSTRAINT [FK_race_dataraces];
GO
IF OBJECT_ID(N'[dbo].[FK_race_datausers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[race_data] DROP CONSTRAINT [FK_race_datausers];
GO
IF OBJECT_ID(N'[dbo].[FK_race_datakarts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[race_data] DROP CONSTRAINT [FK_race_datakarts];
GO
IF OBJECT_ID(N'[dbo].[FK_user_cashusers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[user_cash] DROP CONSTRAINT [FK_user_cashusers];
GO
IF OBJECT_ID(N'[dbo].[FK_loginsprogram_users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[logins] DROP CONSTRAINT [FK_loginsprogram_users];
GO
IF OBJECT_ID(N'[dbo].[FK_user_cashjurnal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[user_cash] DROP CONSTRAINT [FK_user_cashjurnal];
GO
IF OBJECT_ID(N'[dbo].[FK_certificatecertificate_type]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[certificates] DROP CONSTRAINT [FK_certificatecertificate_type];
GO
IF OBJECT_ID(N'[dbo].[FK_cassajurnal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[cassa] DROP CONSTRAINT [FK_cassajurnal];
GO
IF OBJECT_ID(N'[dbo].[FK_race_timesrace_data]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[race_times] DROP CONSTRAINT [FK_race_timesrace_data];
GO
IF OBJECT_ID(N'[dbo].[FK_messagesusers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[messages] DROP CONSTRAINT [FK_messagesusers];
GO
IF OBJECT_ID(N'[dbo].[FK_messageskarts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[messages] DROP CONSTRAINT [FK_messageskarts];
GO
IF OBJECT_ID(N'[dbo].[FK_messagesprogram_users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[messages] DROP CONSTRAINT [FK_messagesprogram_users];
GO
IF OBJECT_ID(N'[dbo].[FK_DiscountCardusers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DiscountCards] DROP CONSTRAINT [FK_DiscountCardusers];
GO
IF OBJECT_ID(N'[dbo].[FK_DiscountCardprogram_users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DiscountCards] DROP CONSTRAINT [FK_DiscountCardprogram_users];
GO
IF OBJECT_ID(N'[dbo].[FK_DiscountCardDiscountCardGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DiscountCards] DROP CONSTRAINT [FK_DiscountCardDiscountCardGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_race_dataDiscountCard]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[race_data] DROP CONSTRAINT [FK_race_dataDiscountCard];
GO
IF OBJECT_ID(N'[dbo].[FK_Petrolprogram_users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Petroleums] DROP CONSTRAINT [FK_Petrolprogram_users];
GO
IF OBJECT_ID(N'[dbo].[FK_race_datarace_modes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[race_data] DROP CONSTRAINT [FK_race_datarace_modes];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[cassa]', 'U') IS NOT NULL
    DROP TABLE [dbo].[cassa];
GO
IF OBJECT_ID(N'[dbo].[certificates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[certificates];
GO
IF OBJECT_ID(N'[dbo].[users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[users];
GO
IF OBJECT_ID(N'[dbo].[certificate_type]', 'U') IS NOT NULL
    DROP TABLE [dbo].[certificate_type];
GO
IF OBJECT_ID(N'[dbo].[fuel]', 'U') IS NOT NULL
    DROP TABLE [dbo].[fuel];
GO
IF OBJECT_ID(N'[dbo].[karts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[karts];
GO
IF OBJECT_ID(N'[dbo].[groups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[groups];
GO
IF OBJECT_ID(N'[dbo].[jurnal]', 'U') IS NOT NULL
    DROP TABLE [dbo].[jurnal];
GO
IF OBJECT_ID(N'[dbo].[races]', 'U') IS NOT NULL
    DROP TABLE [dbo].[races];
GO
IF OBJECT_ID(N'[dbo].[tracks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tracks];
GO
IF OBJECT_ID(N'[dbo].[logins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[logins];
GO
IF OBJECT_ID(N'[dbo].[messages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[messages];
GO
IF OBJECT_ID(N'[dbo].[noracekart]', 'U') IS NOT NULL
    DROP TABLE [dbo].[noracekart];
GO
IF OBJECT_ID(N'[dbo].[prices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[prices];
GO
IF OBJECT_ID(N'[dbo].[race_modes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[race_modes];
GO
IF OBJECT_ID(N'[dbo].[program_users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[program_users];
GO
IF OBJECT_ID(N'[dbo].[race_data]', 'U') IS NOT NULL
    DROP TABLE [dbo].[race_data];
GO
IF OBJECT_ID(N'[dbo].[race_times]', 'U') IS NOT NULL
    DROP TABLE [dbo].[race_times];
GO
IF OBJECT_ID(N'[dbo].[settings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[settings];
GO
IF OBJECT_ID(N'[dbo].[user_cash]', 'U') IS NOT NULL
    DROP TABLE [dbo].[user_cash];
GO
IF OBJECT_ID(N'[dbo].[DiscountCards]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DiscountCards];
GO
IF OBJECT_ID(N'[dbo].[DiscountCardGroups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DiscountCardGroups];
GO
IF OBJECT_ID(N'[dbo].[Petroleums]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Petroleums];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'cassa'
CREATE TABLE [dbo].[cassa] (
    [id] int IDENTITY(1,1) NOT NULL,
    [sum] float  NULL,
    [sign] int  NOT NULL,
    [date] datetime  NULL,
    [doc_id] int  NOT NULL
);
GO

-- Creating table 'certificates'
CREATE TABLE [dbo].[certificates] (
    [id] int IDENTITY(1,1) NOT NULL,
    [bar_number] nvarchar(max)  NOT NULL,
    [user_id] int  NULL,
    [count] int  NULL,
    [created] datetime  NULL,
    [modified] datetime  NOT NULL,
    [date_end] datetime  NOT NULL,
    [active] bit  NOT NULL,
    [c_id] int  NOT NULL
);
GO

-- Creating table 'users'
CREATE TABLE [dbo].[users] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NULL,
    [surname] nvarchar(max)  NULL,
    [gender] bit  NOT NULL,
    [birthday] datetime  NOT NULL,
    [created] datetime  NULL,
    [modified] datetime  NULL,
    [nickname] nvarchar(max)  NULL,
    [banned] bit  NOT NULL,
    [date_banned] datetime  NULL,
    [email] nvarchar(max)  NOT NULL,
    [tel] nvarchar(max)  NOT NULL,
    [message_id] int  NULL,
    [barcode] nvarchar(max)  NOT NULL,
    [gr] int  NULL,
    [deleted] bit  NOT NULL,
    [date_deleted] datetime  NULL
);
GO

-- Creating table 'certificate_type'
CREATE TABLE [dbo].[certificate_type] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [nominal] nvarchar(max)  NULL,
    [cost] float  NOT NULL,
    [created] datetime  NOT NULL,
    [deleted] bit  NOT NULL,
    [c_type] bit  NOT NULL
);
GO

-- Creating table 'fuel'
CREATE TABLE [dbo].[fuel] (
    [id] int IDENTITY(1,1) NOT NULL,
    [car_id] int  NULL,
    [fuel_value] float  NOT NULL,
    [created] datetime  NOT NULL,
    [sign] int  NOT NULL,
    [comment] nvarchar(max)  NULL
);
GO

-- Creating table 'karts'
CREATE TABLE [dbo].[karts] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NULL,
    [number] nvarchar(max)  NULL,
    [transponder] nvarchar(max)  NOT NULL,
    [color] nvarchar(max)  NULL,
    [created] datetime  NOT NULL,
    [modified] datetime  NULL,
    [repair] bit  NOT NULL,
    [message_id] int  NULL,
    [wait] bit  NOT NULL
);
GO

-- Creating table 'groups'
CREATE TABLE [dbo].[groups] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [sale] nvarchar(max)  NULL,
    [created] datetime  NULL,
    [modified] datetime  NOT NULL
);
GO

-- Creating table 'jurnal'
CREATE TABLE [dbo].[jurnal] (
    [id] int IDENTITY(1,1) NOT NULL,
    [tp] int  NOT NULL,
    [comment] nvarchar(max)  NULL,
    [created] datetime  NOT NULL,
    [race_id] int  NULL,
    [user_id] int  NULL
);
GO

-- Creating table 'races'
CREATE TABLE [dbo].[races] (
    [id] int IDENTITY(1,1) NOT NULL,
    [racedate] datetime  NULL,
    [raceid] nvarchar(max)  NOT NULL,
    [created] datetime  NOT NULL,
    [modified] datetime  NOT NULL,
    [stat] int  NULL,
    [track_id] int  NOT NULL,
    [light_mode] bit  NOT NULL,
    --[is_race] bit  NULL
);
GO

-- Creating table 'tracks'
CREATE TABLE [dbo].[tracks] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NULL,
    [length] float  NULL,
    [file] nvarchar(max)  NULL,
    [created] datetime  NOT NULL,
    [is_deleted] bit  NOT NULL
);
GO

-- Creating table 'logins'
CREATE TABLE [dbo].[logins] (
    [id] int IDENTITY(1,1) NOT NULL,
    [stat] int  NULL,
    [created] datetime  NOT NULL,
    [user_id] int  NOT NULL
);
GO

-- Creating table 'messages'
CREATE TABLE [dbo].[messages] (
    [id] int IDENTITY(1,1) NOT NULL,
    [m_type] int  NOT NULL,
    [message] nvarchar(max)  NULL,
    [created] datetime  NOT NULL,
    [modified] datetime  NOT NULL,
    [date] datetime  NULL,
    [subject] nvarchar(max)  NOT NULL,
    [id_pilot] int  NULL,
    [id_kart] int  NULL,
    [id_program_user] int  NULL
);
GO

-- Creating table 'noracekart'
CREATE TABLE [dbo].[noracekart] (
    [id] int IDENTITY(1,1) NOT NULL,
    [transponder] nvarchar(max)  NULL,
    [race_id] int  NULL,
    [created] datetime  NOT NULL
);
GO

-- Creating table 'prices'
CREATE TABLE [dbo].[prices] (
    [id] int IDENTITY(1,1) NOT NULL,
    [week] int  NOT NULL,
    [d1] nvarchar(max)  NULL,
    [d2] nvarchar(max)  NULL,
    [d3] nvarchar(max)  NULL,
    [d4] nvarchar(max)  NULL,
    [d5] nvarchar(max)  NULL,
    [d6] nvarchar(max)  NULL,
    [d7] nvarchar(max)  NULL,
    [idRaceMode] int  NOT NULL
);
GO

-- Creating table 'race_modes'
CREATE TABLE [dbo].[race_modes] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [length] int  NOT NULL,
    [is_deleted] bit  NOT NULL
);
GO

-- Creating table 'program_users'
CREATE TABLE [dbo].[program_users] (
    [id] int IDENTITY(1,1) NOT NULL,
    [login] nvarchar(max)  NOT NULL,
    [password] nvarchar(max)  NOT NULL,
    [stat] int  NULL,
    [created] datetime  NOT NULL,
    [modified] datetime  NOT NULL,
    [deleted] bit  NOT NULL,
    [name] nvarchar(max)  NULL,
    [surname] nvarchar(max)  NULL,
    [barcode] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'race_data'
CREATE TABLE [dbo].[race_data] (
    [id] int IDENTITY(1,1) NOT NULL,
    [race_id] int  NULL,
    [pilot_id] int  NULL,
    [car_id] int  NULL,
    [created] datetime  NOT NULL,
    [modified] datetime  NOT NULL,
    [reserv] bit  NULL,
    [monthrace] bit  NULL,
    [race_month_date] datetime  NULL,
    [light_mode] bit  NOT NULL,
    [paid_amount] float  NULL,
    [id_discount_card] int  NULL,
    [id_race_mode] int  NOT NULL
);
GO

-- Creating table 'race_times'
CREATE TABLE [dbo].[race_times] (
    [id] int IDENTITY(1,1) NOT NULL,
    [lap] int  NULL,
    [seconds] decimal(18,7)  NOT NULL,
    [created] datetime  NOT NULL,
    [member_id] int  NOT NULL
);
GO

-- Creating table 'settings'
CREATE TABLE [dbo].[settings] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [val] nvarchar(max)  NOT NULL,
    [description] nvarchar(max)  NULL
);
GO

-- Creating table 'user_cash'
CREATE TABLE [dbo].[user_cash] (
    [id] int IDENTITY(1,1) NOT NULL,
    [user_id] int  NULL,
    [sum] float  NULL,
    [sign] bit  NOT NULL,
    [created] datetime  NOT NULL,
    [doc_id] int  NOT NULL
);
GO

-- Creating table 'DiscountCards'
CREATE TABLE [dbo].[DiscountCards] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [idOwner] int  NULL,
    [idSeller] int  NULL,
    [Number] nvarchar(max)  NOT NULL,
    [Created] datetime  NOT NULL,
    [ValidUntil] datetime  NOT NULL,
    [SalePlace] nvarchar(max)  NULL,
    [IdDiscountCardGroup] int  NOT NULL,
    [IsBlocked] bit  NOT NULL
);
GO

-- Creating table 'DiscountCardGroups'
CREATE TABLE [dbo].[DiscountCardGroups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PercentOfDiscount] smallint  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'Petroleums'
CREATE TABLE [dbo].[Petroleums] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [litres] float  NOT NULL,
    [program_users_id] int  NOT NULL,
    [Date] datetime  NOT NULL,
    [Price] float  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'cassa'
ALTER TABLE [dbo].[cassa]
ADD CONSTRAINT [PK_cassa]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'certificates'
ALTER TABLE [dbo].[certificates]
ADD CONSTRAINT [PK_certificates]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'users'
ALTER TABLE [dbo].[users]
ADD CONSTRAINT [PK_users]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'certificate_type'
ALTER TABLE [dbo].[certificate_type]
ADD CONSTRAINT [PK_certificate_type]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'fuel'
ALTER TABLE [dbo].[fuel]
ADD CONSTRAINT [PK_fuel]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'karts'
ALTER TABLE [dbo].[karts]
ADD CONSTRAINT [PK_karts]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'groups'
ALTER TABLE [dbo].[groups]
ADD CONSTRAINT [PK_groups]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'jurnal'
ALTER TABLE [dbo].[jurnal]
ADD CONSTRAINT [PK_jurnal]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'races'
ALTER TABLE [dbo].[races]
ADD CONSTRAINT [PK_races]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'tracks'
ALTER TABLE [dbo].[tracks]
ADD CONSTRAINT [PK_tracks]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'logins'
ALTER TABLE [dbo].[logins]
ADD CONSTRAINT [PK_logins]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'messages'
ALTER TABLE [dbo].[messages]
ADD CONSTRAINT [PK_messages]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'noracekart'
ALTER TABLE [dbo].[noracekart]
ADD CONSTRAINT [PK_noracekart]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'prices'
ALTER TABLE [dbo].[prices]
ADD CONSTRAINT [PK_prices]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'race_modes'
ALTER TABLE [dbo].[race_modes]
ADD CONSTRAINT [PK_race_modes]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'program_users'
ALTER TABLE [dbo].[program_users]
ADD CONSTRAINT [PK_program_users]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'race_data'
ALTER TABLE [dbo].[race_data]
ADD CONSTRAINT [PK_race_data]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'race_times'
ALTER TABLE [dbo].[race_times]
ADD CONSTRAINT [PK_race_times]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'settings'
ALTER TABLE [dbo].[settings]
ADD CONSTRAINT [PK_settings]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'user_cash'
ALTER TABLE [dbo].[user_cash]
ADD CONSTRAINT [PK_user_cash]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [Id] in table 'DiscountCards'
ALTER TABLE [dbo].[DiscountCards]
ADD CONSTRAINT [PK_DiscountCards]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DiscountCardGroups'
ALTER TABLE [dbo].[DiscountCardGroups]
ADD CONSTRAINT [PK_DiscountCardGroups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Petroleums'
ALTER TABLE [dbo].[Petroleums]
ADD CONSTRAINT [PK_Petroleums]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [user_id] in table 'certificates'
ALTER TABLE [dbo].[certificates]
ADD CONSTRAINT [FK_certificateusers]
    FOREIGN KEY ([user_id])
    REFERENCES [dbo].[users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_certificateusers'
CREATE INDEX [IX_FK_certificateusers]
ON [dbo].[certificates]
    ([user_id]);
GO

-- Creating foreign key on [car_id] in table 'fuel'
ALTER TABLE [dbo].[fuel]
ADD CONSTRAINT [FK_fuelkarts]
    FOREIGN KEY ([car_id])
    REFERENCES [dbo].[karts]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_fuelkarts'
CREATE INDEX [IX_FK_fuelkarts]
ON [dbo].[fuel]
    ([car_id]);
GO

-- Creating foreign key on [gr] in table 'users'
ALTER TABLE [dbo].[users]
ADD CONSTRAINT [FK_usersgroups]
    FOREIGN KEY ([gr])
    REFERENCES [dbo].[groups]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_usersgroups'
CREATE INDEX [IX_FK_usersgroups]
ON [dbo].[users]
    ([gr]);
GO

-- Creating foreign key on [track_id] in table 'races'
ALTER TABLE [dbo].[races]
ADD CONSTRAINT [FK_racestracks]
    FOREIGN KEY ([track_id])
    REFERENCES [dbo].[tracks]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_racestracks'
CREATE INDEX [IX_FK_racestracks]
ON [dbo].[races]
    ([track_id]);
GO

-- Creating foreign key on [race_id] in table 'jurnal'
ALTER TABLE [dbo].[jurnal]
ADD CONSTRAINT [FK_jurnalraces]
    FOREIGN KEY ([race_id])
    REFERENCES [dbo].[races]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_jurnalraces'
CREATE INDEX [IX_FK_jurnalraces]
ON [dbo].[jurnal]
    ([race_id]);
GO

-- Creating foreign key on [user_id] in table 'jurnal'
ALTER TABLE [dbo].[jurnal]
ADD CONSTRAINT [FK_jurnalusers]
    FOREIGN KEY ([user_id])
    REFERENCES [dbo].[users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_jurnalusers'
CREATE INDEX [IX_FK_jurnalusers]
ON [dbo].[jurnal]
    ([user_id]);
GO

-- Creating foreign key on [race_id] in table 'noracekart'
ALTER TABLE [dbo].[noracekart]
ADD CONSTRAINT [FK_noracekartraces]
    FOREIGN KEY ([race_id])
    REFERENCES [dbo].[races]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_noracekartraces'
CREATE INDEX [IX_FK_noracekartraces]
ON [dbo].[noracekart]
    ([race_id]);
GO

-- Creating foreign key on [idRaceMode] in table 'prices'
ALTER TABLE [dbo].[prices]
ADD CONSTRAINT [FK_pricesrace_modes]
    FOREIGN KEY ([idRaceMode])
    REFERENCES [dbo].[race_modes]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_pricesrace_modes'
CREATE INDEX [IX_FK_pricesrace_modes]
ON [dbo].[prices]
    ([idRaceMode]);
GO

-- Creating foreign key on [race_id] in table 'race_data'
ALTER TABLE [dbo].[race_data]
ADD CONSTRAINT [FK_race_dataraces]
    FOREIGN KEY ([race_id])
    REFERENCES [dbo].[races]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_race_dataraces'
CREATE INDEX [IX_FK_race_dataraces]
ON [dbo].[race_data]
    ([race_id]);
GO

-- Creating foreign key on [pilot_id] in table 'race_data'
ALTER TABLE [dbo].[race_data]
ADD CONSTRAINT [FK_race_datausers]
    FOREIGN KEY ([pilot_id])
    REFERENCES [dbo].[users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_race_datausers'
CREATE INDEX [IX_FK_race_datausers]
ON [dbo].[race_data]
    ([pilot_id]);
GO

-- Creating foreign key on [car_id] in table 'race_data'
ALTER TABLE [dbo].[race_data]
ADD CONSTRAINT [FK_race_datakarts]
    FOREIGN KEY ([car_id])
    REFERENCES [dbo].[karts]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_race_datakarts'
CREATE INDEX [IX_FK_race_datakarts]
ON [dbo].[race_data]
    ([car_id]);
GO

-- Creating foreign key on [user_id] in table 'user_cash'
ALTER TABLE [dbo].[user_cash]
ADD CONSTRAINT [FK_user_cashusers]
    FOREIGN KEY ([user_id])
    REFERENCES [dbo].[users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_user_cashusers'
CREATE INDEX [IX_FK_user_cashusers]
ON [dbo].[user_cash]
    ([user_id]);
GO

-- Creating foreign key on [user_id] in table 'logins'
ALTER TABLE [dbo].[logins]
ADD CONSTRAINT [FK_loginsprogram_users]
    FOREIGN KEY ([user_id])
    REFERENCES [dbo].[program_users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_loginsprogram_users'
CREATE INDEX [IX_FK_loginsprogram_users]
ON [dbo].[logins]
    ([user_id]);
GO

-- Creating foreign key on [doc_id] in table 'user_cash'
ALTER TABLE [dbo].[user_cash]
ADD CONSTRAINT [FK_user_cashjurnal]
    FOREIGN KEY ([doc_id])
    REFERENCES [dbo].[jurnal]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_user_cashjurnal'
CREATE INDEX [IX_FK_user_cashjurnal]
ON [dbo].[user_cash]
    ([doc_id]);
GO

-- Creating foreign key on [c_id] in table 'certificates'
ALTER TABLE [dbo].[certificates]
ADD CONSTRAINT [FK_certificatecertificate_type]
    FOREIGN KEY ([c_id])
    REFERENCES [dbo].[certificate_type]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_certificatecertificate_type'
CREATE INDEX [IX_FK_certificatecertificate_type]
ON [dbo].[certificates]
    ([c_id]);
GO

-- Creating foreign key on [doc_id] in table 'cassa'
ALTER TABLE [dbo].[cassa]
ADD CONSTRAINT [FK_cassajurnal]
    FOREIGN KEY ([doc_id])
    REFERENCES [dbo].[jurnal]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_cassajurnal'
CREATE INDEX [IX_FK_cassajurnal]
ON [dbo].[cassa]
    ([doc_id]);
GO

-- Creating foreign key on [member_id] in table 'race_times'
ALTER TABLE [dbo].[race_times]
ADD CONSTRAINT [FK_race_timesrace_data]
    FOREIGN KEY ([member_id])
    REFERENCES [dbo].[race_data]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_race_timesrace_data'
CREATE INDEX [IX_FK_race_timesrace_data]
ON [dbo].[race_times]
    ([member_id]);
GO

-- Creating foreign key on [id_pilot] in table 'messages'
ALTER TABLE [dbo].[messages]
ADD CONSTRAINT [FK_messagesusers]
    FOREIGN KEY ([id_pilot])
    REFERENCES [dbo].[users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_messagesusers'
CREATE INDEX [IX_FK_messagesusers]
ON [dbo].[messages]
    ([id_pilot]);
GO

-- Creating foreign key on [id_kart] in table 'messages'
ALTER TABLE [dbo].[messages]
ADD CONSTRAINT [FK_messageskarts]
    FOREIGN KEY ([id_kart])
    REFERENCES [dbo].[karts]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_messageskarts'
CREATE INDEX [IX_FK_messageskarts]
ON [dbo].[messages]
    ([id_kart]);
GO

-- Creating foreign key on [id_program_user] in table 'messages'
ALTER TABLE [dbo].[messages]
ADD CONSTRAINT [FK_messagesprogram_users]
    FOREIGN KEY ([id_program_user])
    REFERENCES [dbo].[program_users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_messagesprogram_users'
CREATE INDEX [IX_FK_messagesprogram_users]
ON [dbo].[messages]
    ([id_program_user]);
GO

-- Creating foreign key on [idOwner] in table 'DiscountCards'
ALTER TABLE [dbo].[DiscountCards]
ADD CONSTRAINT [FK_DiscountCardusers]
    FOREIGN KEY ([idOwner])
    REFERENCES [dbo].[users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DiscountCardusers'
CREATE INDEX [IX_FK_DiscountCardusers]
ON [dbo].[DiscountCards]
    ([idOwner]);
GO

-- Creating foreign key on [idSeller] in table 'DiscountCards'
ALTER TABLE [dbo].[DiscountCards]
ADD CONSTRAINT [FK_DiscountCardprogram_users]
    FOREIGN KEY ([idSeller])
    REFERENCES [dbo].[program_users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DiscountCardprogram_users'
CREATE INDEX [IX_FK_DiscountCardprogram_users]
ON [dbo].[DiscountCards]
    ([idSeller]);
GO

-- Creating foreign key on [IdDiscountCardGroup] in table 'DiscountCards'
ALTER TABLE [dbo].[DiscountCards]
ADD CONSTRAINT [FK_DiscountCardDiscountCardGroup]
    FOREIGN KEY ([IdDiscountCardGroup])
    REFERENCES [dbo].[DiscountCardGroups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DiscountCardDiscountCardGroup'
CREATE INDEX [IX_FK_DiscountCardDiscountCardGroup]
ON [dbo].[DiscountCards]
    ([IdDiscountCardGroup]);
GO

-- Creating foreign key on [id_discount_card] in table 'race_data'
ALTER TABLE [dbo].[race_data]
ADD CONSTRAINT [FK_race_dataDiscountCard]
    FOREIGN KEY ([id_discount_card])
    REFERENCES [dbo].[DiscountCards]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_race_dataDiscountCard'
CREATE INDEX [IX_FK_race_dataDiscountCard]
ON [dbo].[race_data]
    ([id_discount_card]);
GO

-- Creating foreign key on [program_users_id] in table 'Petroleums'
ALTER TABLE [dbo].[Petroleums]
ADD CONSTRAINT [FK_Petrolprogram_users]
    FOREIGN KEY ([program_users_id])
    REFERENCES [dbo].[program_users]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Petrolprogram_users'
CREATE INDEX [IX_FK_Petrolprogram_users]
ON [dbo].[Petroleums]
    ([program_users_id]);
GO

-- Creating foreign key on [id_race_mode] in table 'race_data'
ALTER TABLE [dbo].[race_data]
ADD CONSTRAINT [FK_race_datarace_modes]
    FOREIGN KEY ([id_race_mode])
    REFERENCES [dbo].[race_modes]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_race_datarace_modes'
CREATE INDEX [IX_FK_race_datarace_modes]
ON [dbo].[race_data]
    ([id_race_mode]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
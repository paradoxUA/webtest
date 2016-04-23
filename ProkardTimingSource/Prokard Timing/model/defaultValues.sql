use [crazykart]
GO


 INSERT INTO program_users ([login], [password], stat, created, modified,
  deleted, name, surname, barcode ) VALUES ('sega', 'jHd/cCQz8+k=', 0, GETDATE(), GETDATE(),
  0, 'Sergey', 'Gavrilenko', '1')

  INSERT INTO program_users ([login], [password], stat, created, modified,
  deleted, name, surname, barcode ) VALUES ('sega1', 'jHd/cCQz8+k=', 1, GETDATE(), GETDATE(),
  0, 'Sergey', 'Gavrilenko', '1')

INSERT INTO program_users ([login], [password], stat, created, modified,
  deleted, name, surname, barcode ) VALUES ('sega2', 'jHd/cCQz8+k=', 2, GETDATE(), GETDATE(),
  0, 'Sergey', 'Gavrilenko', '1') 



  INSERT INTO [crazykart].[dbo].[karts]
           ([name]
           ,[number]
           ,[transponder]
           ,[color]
           ,[created]
           ,[modified]
           ,[repair]
           ,[message_id]
           ,[wait])
     VALUES
           ('карт 1',
		   1,
           '01',
           NULL,
           GETDATE(),
           GETDATE(),
           0,
           null,
           0)
GO


INSERT INTO [crazykart].[dbo].[karts]
           ([name]
           ,[number]
           ,[transponder]
           ,[color]
           ,[created]
           ,[modified]
           ,[repair]
           ,[message_id]
           ,[wait])
     VALUES
           ('карт 2',
		   2,
           '02',
           NULL,
           GETDATE(),
           GETDATE(),
           0,
           null,
           0)
GO


INSERT INTO [crazykart].[dbo].[karts]
           ([name]
           ,[number]
           ,[transponder]
           ,[color]
           ,[created]
           ,[modified]
           ,[repair]
           ,[message_id]
           ,[wait])
     VALUES
           ('карт 3',
		   3,
           '03',
           NULL,
           GETDATE(),
           GETDATE(),
           0,
           null,
           0)
GO


INSERT INTO [crazykart].[dbo].[karts]           ([name]           ,[number]           ,[transponder]
           ,[color]           ,[created]           ,[modified]           ,[repair]
           ,[message_id]           ,[wait])
     VALUES
           ('карт 4',		   4,           '04',           NULL,           GETDATE(),
           GETDATE(),           0,           null,           0)
GO

INSERT INTO [crazykart].[dbo].[karts]           ([name]           ,[number]           ,[transponder]
           ,[color]           ,[created]           ,[modified]           ,[repair]
           ,[message_id]           ,[wait])
     VALUES
           ('карт 5',		   5,           '05',           NULL,           GETDATE(),
           GETDATE(),           0,           null,           0)
GO
INSERT INTO [crazykart].[dbo].[karts]           ([name]           ,[number]           ,[transponder]
           ,[color]           ,[created]           ,[modified]           ,[repair]
           ,[message_id]           ,[wait])
     VALUES
           ('карт 6',		   6,           '06',           NULL,           GETDATE(),
           GETDATE(),           0,           null,           0)
GO

INSERT INTO [crazykart].[dbo].[karts]           ([name]           ,[number]           ,[transponder]
           ,[color]           ,[created]           ,[modified]           ,[repair]
           ,[message_id]           ,[wait])
     VALUES
           ('карт 7',		   7,           '07',           NULL,           GETDATE(),
           GETDATE(),           0,           null,           0)
GO

INSERT INTO [crazykart].[dbo].[karts]           ([name]           ,[number]           ,[transponder]
           ,[color]           ,[created]           ,[modified]           ,[repair]
           ,[message_id]           ,[wait])
     VALUES
           ('карт 8',		   8,           '08',           NULL,           GETDATE(),
           GETDATE(),           0,           null,           0)
GO

INSERT INTO [crazykart].[dbo].[karts]           ([name]           ,[number]           ,[transponder]
           ,[color]           ,[created]           ,[modified]           ,[repair]
           ,[message_id]           ,[wait])
     VALUES
           ('карт 9',		   9,           '09',           NULL,           GETDATE(),
           GETDATE(),           0,           null,           0)
GO

INSERT INTO [crazykart].[dbo].[karts]           ([name]           ,[number]           ,[transponder]
           ,[color]           ,[created]           ,[modified]           ,[repair]
           ,[message_id]           ,[wait])
     VALUES
           ('карт 10',		   10,           '10',           NULL,           GETDATE(),
           GETDATE(),           0,           null,           0)
GO

INSERT INTO [crazykart].[dbo].[karts]           ([name]           ,[number]           ,[transponder]
           ,[color]           ,[created]           ,[modified]           ,[repair]
           ,[message_id]           ,[wait])
     VALUES
           ('карт 11',		   11,           '11',           NULL,           GETDATE(),
           GETDATE(),           0,           null,           0)
GO

INSERT INTO [crazykart].[dbo].[karts]           ([name]           ,[number]           ,[transponder]
           ,[color]           ,[created]           ,[modified]           ,[repair]
           ,[message_id]           ,[wait])
     VALUES
           ('карт 12',		   12,           '12',           NULL,           GETDATE(),
           GETDATE(),           0,           null,           0)
GO



INSERT INTO [crazykart].[dbo].[race_modes]
           ([name]
           ,[length]
		   , is_deleted
		   )
     VALUES
           ('10 минут', 10, 0)
           
GO

INSERT INTO [crazykart].[dbo].[race_modes]
           ([name]
           ,[length]
		   , is_deleted
		   )
     VALUES
           ('5 минут', 5, 0)
           
GO


INSERT INTO [crazykart].[dbo].[race_modes]
           ([name]
           ,[length]
		   , is_deleted
		   )
     VALUES
           ('15 минут', 15, 0)
           
GO



INSERT INTO [crazykart].[dbo].[race_modes]
           ([name]
           ,[length], 
		   is_deleted)
     VALUES
           ('20 минут', 20, 0)
           
GO


INSERT INTO [crazykart].[dbo].[tracks]
           ([name]
           ,[length]
           ,[file]
           ,[created])
     VALUES
           ('трасса 1', 1100, '', GETDATE())
GO

INSERT INTO [crazykart].[dbo].[tracks]
           ([name]
           ,[length]
           ,[file]
           ,[created])
     VALUES
           ('трасса 2', 1200, '', GETDATE())
GO

INSERT INTO [crazykart].[dbo].[tracks]
           ([name]
           ,[length]
           ,[file]
           ,[created])
     VALUES
           ('трасса 3', 1300, '', GETDATE())
GO

INSERT INTO [crazykart].[dbo].[groups]
           ([name]
           ,[sale]
           ,[created]
           ,[modified])
     VALUES
           ('группа 1', 0, GETDATE(),GETDATE()           )
GO

INSERT INTO [crazykart].[dbo].[groups]
           ([name]
           ,[sale]
           ,[created]
           ,[modified])
     VALUES
           ('группа 2', 0, GETDATE(),GETDATE()           )
GO

INSERT INTO [crazykart].[dbo].[groups]
           ([name]
           ,[sale]
           ,[created]
           ,[modified])
     VALUES
           ('группа 3', 0, GETDATE(),GETDATE()           )
GO






/****** Object:  StoredProcedure [dbo].[GetPilots]    Script Date: 03/08/2012 18:59:28 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPilots]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetPilots]
GO



/****** Object:  StoredProcedure [dbo].[GetPilots]    Script Date: 03/08/2012 18:59:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetPilots]
@PageIndex int,
@PageSize int,
@filter nvarchar(500)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
 

WITH usersTable AS
(
   SELECT ROW_NUMBER() OVER(ORDER BY id) AS RowNum,
         *         
     FROM users WHERE deleted = 0 AND (    
     name LIKE '%' + @filter + '%' OR
      surname LIKE '%' + @filter + '%' OR
       nickname LIKE '%' + @filter + '%' OR
        email LIKE '%' + @filter + '%' OR
         tel LIKE '%' + @filter + '%' OR
          barcode LIKE '%' + @filter + '%')     
     
)

SELECT *, (SELECT TOP 1 Id FROM DiscountCards WHERE idOwner = usersTable.id) as 'discount_card'
 
 FROM usersTable
WHERE RowNum BETWEEN (@PageIndex - 1) * @PageSize + 1 
                 AND @PageIndex * @PageSize
ORDER BY nickname;

END

GO









/****** Object:  StoredProcedure [dbo].[GetPilotsByGroup]    Script Date: 03/08/2012 19:00:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPilotsByGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetPilotsByGroup]
GO



/****** Object:  StoredProcedure [dbo].[GetPilotsByGroup]    Script Date: 03/08/2012 19:00:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetPilotsByGroup]
@PageIndex int,
@PageSize int,
@filter nvarchar(500),
@group_id int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
 
 IF(@group_id = -1)
	 BEGIN
		 EXEC GetPilots @PageIndex, @PageSize, @filter
		 RETURN;
	 END;
 

WITH usersTable AS
(
   SELECT ROW_NUMBER() OVER(ORDER BY id) AS RowNum,
         *         
     FROM users WHERE deleted = 0 AND gr = @group_id AND 
     (     
     name LIKE '%' + @filter + '%' OR
      surname LIKE '%' + @filter + '%' OR
       nickname LIKE '%' + @filter + '%' OR
        email LIKE '%' + @filter + '%' OR
         tel LIKE '%' + @filter + '%' OR
          barcode LIKE '%' + @filter + '%'     
      )
)

SELECT *, (SELECT TOP 1 Id FROM DiscountCards WHERE idOwner = usersTable.id) as 'discount_card'
  
 FROM usersTable
WHERE RowNum BETWEEN (@PageIndex - 1) * @PageSize + 1 
                 AND @PageIndex * @PageSize
ORDER BY nickname;

END

GO




/****** Object:  StoredProcedure [dbo].[GetPilotsCount]    Script Date: 03/08/2012 19:00:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPilotsCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetPilotsCount]
GO



/****** Object:  StoredProcedure [dbo].[GetPilotsCount]    Script Date: 03/08/2012 19:00:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetPilotsCount]
@filter nvarchar(500),
@group_id int,
@with_phones_only bit


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	IF @with_phones_only = 1
	BEGIN
	
		IF(@group_id > -1)
			BEGIN
				SELECT COUNT(*)         
			 FROM users WHERE len(tel) > 0 AND deleted = 0 AND gr = @group_id AND (    
			 name LIKE '%' + @filter + '%' OR
			  surname LIKE '%' + @filter + '%' OR
			   nickname LIKE '%' + @filter + '%' OR
				email LIKE '%' + @filter + '%' OR
				 tel LIKE '%' + @filter + '%' OR
				  barcode LIKE '%' + @filter + '%')
			END
		ELSE
		BEGIN
			SELECT COUNT(*)         
			 FROM users WHERE len(tel) >0 AND deleted = 0 AND (     
			 name LIKE '%' + @filter + '%' OR
			  surname LIKE '%' + @filter + '%' OR
			   nickname LIKE '%' + @filter + '%' OR
				email LIKE '%' + @filter + '%' OR
				 tel LIKE '%' + @filter + '%' OR
				  barcode LIKE '%' + @filter + '%')
		END
	END
	ELSE
	BEGIN
		IF(@group_id > -1)
			BEGIN
				SELECT COUNT(*)         
			 FROM users WHERE deleted = 0 AND gr = @group_id AND (    
			 name LIKE '%' + @filter + '%' OR
			  surname LIKE '%' + @filter + '%' OR
			   nickname LIKE '%' + @filter + '%' OR
				email LIKE '%' + @filter + '%' OR
				 tel LIKE '%' + @filter + '%' OR
				  barcode LIKE '%' + @filter + '%')
			END
		ELSE
		BEGIN
			SELECT COUNT(*)         
			 FROM users WHERE deleted = 0 AND (     
			 name LIKE '%' + @filter + '%' OR
			  surname LIKE '%' + @filter + '%' OR
			   nickname LIKE '%' + @filter + '%' OR
				email LIKE '%' + @filter + '%' OR
				 tel LIKE '%' + @filter + '%' OR
				  barcode LIKE '%' + @filter + '%')
		END
	END
END

GO





/****** Object:  StoredProcedure [dbo].[GetRacesJournal]    Script Date: 03/10/2012 19:29:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetRacesJournal]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetRacesJournal]
GO



/****** Object:  StoredProcedure [dbo].[GetRacesJournal]    Script Date: 03/10/2012 19:29:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetRacesJournal]
@PageIndex int,
@PageSize int,
@startDate DATETIME,
@endDate DATETIME


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
 

WITH racesTable AS
(
   SELECT ROW_NUMBER() OVER(ORDER BY created ASC) AS RowNum,
         *         
     FROM jurnal WHERE    
    (tp=10 or tp=11 or tp=12 or tp=13) AND (created >=  @startDate
		 AND created <= @endDate) 	      
)

SELECT * 
 FROM racesTable
WHERE RowNum BETWEEN (@PageIndex - 1) * @PageSize + 1 
                 AND @PageIndex * @PageSize
ORDER BY created ASC;

END


GO











/****** Object:  StoredProcedure [dbo].[GetRacesByEventJournal]    Script Date: 03/10/2012 19:29:04 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetRacesByEventJournal]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetRacesByEventJournal]
GO


/****** Object:  StoredProcedure [dbo].[GetRacesByEventJournal]    Script Date: 03/10/2012 19:29:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetRacesByEventJournal]
@PageIndex int,
@PageSize int,
@idEvent int,
@startDate DATETIME,
@endDate DATETIME


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
 
 IF @idEvent = -1 
 BEGIN
	EXEC GetRacesJournal @PageIndex,@PageSize ,@startDate,@endDate 
 RETURN;
 END;
 

WITH racesTable AS
(
   SELECT ROW_NUMBER() OVER(ORDER BY created ASC) AS RowNum,
         *         
     FROM jurnal WHERE    
     tp = @idEvent AND created >=  @startDate
     AND created <= @endDate     
)

SELECT * 
 FROM racesTable
WHERE RowNum BETWEEN (@PageIndex - 1) * @PageSize + 1 
                 AND @PageIndex * @PageSize
ORDER BY created ASC;

END


GO













/****** Object:  StoredProcedure [dbo].[GetRacesCount]    Script Date: 03/10/2012 19:30:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetRacesCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetRacesCount]
GO


/****** Object:  StoredProcedure [dbo].[GetRacesCount]    Script Date: 03/10/2012 19:30:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetRacesCount]
@idEvent int,
@startDate DATETIME,
@endDate DATETIME


AS
BEGIN
	
	SET NOCOUNT ON;
 
 IF @idEvent = -1 
	 BEGIN
		SELECT COUNT(*) FROM jurnal WHERE (tp=10 or tp=11 or tp=12 or tp=13) AND (created >=  @startDate
		 AND created <= @endDate) 	
	 END
 ELSE
	 BEGIN
		SELECT COUNT(*) FROM jurnal WHERE tp = @idEvent AND created >=  @startDate
		 AND created <= @endDate 	
	 END
	 
END
 
 




GO





USE [crazykart]
GO

/****** Object:  StoredProcedure [dbo].[GetCassaReport]    Script Date: 03/21/2012 18:37:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCassaReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetCassaReport]
GO





/****** Object:  StoredProcedure [dbo].[GetCassaReport]    Script Date: 03/21/2012 18:37:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetCassaReport]
	@PageIndex int,
	@PageSize int,
	@startDate DATETIME,
	@endDate DATETIME	

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

 WITH resultTable AS
(
   SELECT ROW_NUMBER() OVER(ORDER BY j.id) AS RowNum,
         j.id, j.created as 'date', j.comment, 
j.user_id,j.tp,j.race_id, c.sum,
 c.sign from jurnal j, cassa c 
 where c.doc_id=j.id and j.created between @startDate
  and @endDate       
     
)

SELECT * 
 FROM resultTable
WHERE RowNum BETWEEN (@PageIndex - 1) * @PageSize + 1 
                 AND @PageIndex * @PageSize
ORDER BY id;

 
 
END

GO






/****** Object:  StoredProcedure [dbo].[GetVirtualCassaReport]    Script Date: 03/21/2012 18:49:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetVirtualCassaReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetVirtualCassaReport]
GO

USE [crazykart]
GO

/****** Object:  StoredProcedure [dbo].[GetVirtualCassaReport]    Script Date: 03/21/2012 18:49:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetVirtualCassaReport]
	@PageIndex int,
	@PageSize int,
	@startDate DATETIME,
	@endDate DATETIME	

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

 WITH resultTable AS
(
   SELECT ROW_NUMBER() OVER(ORDER BY j.id) AS RowNum,
   j.id, j.created as 'date', j.comment,
j.user_id,j.tp,j.race_id, u.sum,
 u.sign from jurnal j, user_cash u 
 where u.doc_id = j.id and j.created between @startDate
  and @endDate       
     
)

SELECT * 
 FROM resultTable
WHERE RowNum BETWEEN (@PageIndex - 1) * @PageSize + 1 
                 AND @PageIndex * @PageSize
ORDER BY id;

 
 
END

GO



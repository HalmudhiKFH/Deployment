/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

INSERT INTO
  [dbo].[Users] (
    [Username],
    [Email],
    [Password],
    [Type],
    [CreatedAt],
    [ModifiedAt]
  )
VALUES
  ('Admin','admin@llpa.com','4a57245268e64025e33a299bd45c48b7d1c6b164c66ffe9b4efcd35e34dace55', 1, '2021-12-20 12:50:13.850', NULL),
  ('TestUser','user@llpa.com','4a57245268e64025e33a299bd45c48b7d1c6b164c66ffe9b4efcd35e34dace55', 2, '2021-12-20 12:50:13.850', NULL)
GO

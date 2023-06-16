EXEC sp_configure 'show advanced options', 1;
RECONFIGURE;
GO
EXEC sp_configure 'clr enable' ,1;
RECONFIGURE;
GO
EXEC sp_configure 'clr strict security', 0;
RECONFIGURE;
GO

-- cleanup
IF OBJECT_ID('dbo.CreateTableUsingXmlSchema') IS NOT NULL
  DROP PROCEDURE CreateTableUsingXmlSchema
GO
IF OBJECT_ID('dbo.InsertXML') IS NOT NULL
  DROP PROCEDURE InsertXML
GO
IF OBJECT_ID('dbo.DeleteXML') IS NOT NULL
  DROP PROCEDURE DeleteXML
GO
IF OBJECT_ID('dbo.FindXML') IS NOT NULL
  DROP FUNCTION FindXML
GO
IF OBJECT_ID('dbo.UpdateXML') IS NOT NULL
  DROP PROCEDURE UpdateXML
GO
IF  EXISTS (SELECT * FROM sys.assemblies asms WHERE asms.name = N'CLR_XML')
	DROP ASSEMBLY [CLR_XML] WITH NO DEPENDENTS;
GO

-- create
CREATE ASSEMBLY CLR_XML FROM 'C:\Users\Aleksander\Desktop\AleksanderBartoszek_XML\bin\Debug\AleksanderBartoszek_XML.dll' WITH PERMISSION_SET = EXTERNAL_ACCESS;
GO
CREATE PROCEDURE dbo.CreateTableUsingXmlSchema(@tableName NVARCHAR(128), @xmlSchema NVARCHAR(4000)) AS EXTERNAL NAME CLR_XML.CreateTable.CreateTableUsingXmlSchema;
GO
CREATE PROCEDURE dbo.InsertXML(@tableName NVARCHAR(128), @xmlData NVARCHAR(4000)) AS EXTERNAL NAME CLR_XML.InsertXML.InsertXmlData;
GO
CREATE PROCEDURE dbo.DeleteXML(@tableName NVARCHAR(128), @condition NVARCHAR(4000)) AS EXTERNAL NAME CLR_XML.DeleteXML.DeleteXMLData;
GO
CREATE FUNCTION dbo.FindXML(@tableName NVARCHAR(128), @elementName NVARCHAR(128), @elementValue NVARCHAR(4000)) RETURNS XML AS EXTERNAL NAME CLR_XML.FindXML.FindXMLData;
GO
CREATE PROCEDURE dbo.UpdateXML(@tableName NVARCHAR(128), @elementName NVARCHAR(128), @newElementValue NVARCHAR(4000),  @condition NVARCHAR(4000)) AS EXTERNAL NAME CLR_XML.UpdateXML.UpdateXMLData;
GO

-- check
SELECT A.assembly_id,A.name as SQLCLRDemo,
B.object_id, C. name as CLR, C.type, C.type_desc
FROM Sys.Assemblies A
INNER JOIN SYS.ASSEMBLY_MODULES B oN a.assembly_id=B.assembly_id
INNER JOIN SYS.OBJECTS C ON B.object_id = C.object_id
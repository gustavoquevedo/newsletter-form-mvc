# newsletter-form-mvc
<<<<<<< HEAD
Small ASP.NET MVC project with a newsletter form
=======

Greenfinch Newsletter Form project

Tools and frameworks
--------------------
Developed with:
- Visual Studio 2017 Community edition
- ASP.NET MVC 5
- Entity Framework 6 (Code first approach)

Design
------
The application design and styling is based on the Greenfinch's website.
The newsletter form panel has been built as a panel that could located on a sidebar.

Database
--------
The database script to generate the schema and add some data is included.
The DB schema should be automatically created in the first execution into a SQL Server instance located in localhost.

There is only one relevant table in the DB, Subscriptions with the following fields:
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmailAddress] [nvarchar](max) NOT NULL,
	[Source] [int] NOT NULL,
	[SourceOther] [nvarchar](max) NULL,
	[Reason] [nvarchar](max) NULL,
	[DateTime] [datetime] NOT NULL
	
Entity framework will also create a __MigrationHistory table that can be ignored.
	
Test
----
The solution includes a test project apart from the main web project.
This test project could be improved by using a mocking framework.

Deployment
----------
A deployed version of the web application can be found at:
http://gtdoro.com/newsletter
>>>>>>> Initial commit

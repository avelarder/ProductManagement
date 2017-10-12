CREATE TABLE [dbo].[Products] (
    [Number] VARCHAR (10)   NOT NULL,
    [Tittle] VARCHAR (255)  NOT NULL,
    [Price]  DECIMAL (9, 2) NOT NULL,
    CONSTRAINT [Products_PK] PRIMARY KEY CLUSTERED ([Number] ASC)
);


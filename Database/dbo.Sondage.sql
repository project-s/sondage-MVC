CREATE TABLE [dbo].[Sondage] (
    [numeroDeSondage]           INT       IDENTITY (1, 1) NOT NULL,
    [dateDeCreationSondage]     DATE      NOT NULL,
    [question]                  VARCHAR (50) NOT NULL,
    [lienVote]                  VARCHAR (15) NULL,
    [lienDesactivation]         VARCHAR (15) NULL,
    [FK_idValiditeSondage]      CHAR (1)  NOT NULL,
    [FK_idTypeDeChoixDuSondage] CHAR (1)  NOT NULL,
    PRIMARY KEY CLUSTERED ([numeroDeSondage] ASC),
    CONSTRAINT [FK_idValiditeSondage] FOREIGN KEY ([FK_idValiditeSondage]) REFERENCES [dbo].[ValiditeSondage] ([idValiditeSondage]),
	CONSTRAINT [FK_idTypeDeChoixDuSondage] FOREIGN KEY ([FK_idTypeDeChoixDuSondage]) REFERENCES [dbo].[TypeDeChoixDuSondage] ([idTypeDeChoixDuSondage])
);


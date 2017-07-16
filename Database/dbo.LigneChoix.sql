CREATE TABLE [dbo].[LigneChoix] (
    [numeroDeChoix]      INT        IDENTITY (1, 1) NOT NULL,
    [indexChoix]         INT        NOT NULL,
    [texteChoix]         CHAR (200) NOT NULL,
    [FK_numeroDeSondage] INT        NOT NULL,
    PRIMARY KEY CLUSTERED ([numeroDeChoix] ASC), 
    CONSTRAINT [FK_numeroDeSondage] FOREIGN KEY ([FK_numeroDeSondage]) REFERENCES [Sondage]([numeroDeSondage])
);


CREATE TABLE [dbo].[Vote]
(
	[numeroDeVote] INT NOT NULL PRIMARY KEY IDENTITY, 
    [dateDeVote] DATE NOT NULL, 
    [FK_numeroDeChoix] INT NOT NULL,
	CONSTRAINT [FK_numeroDeChoix] FOREIGN KEY ([FK_numeroDeChoix]) REFERENCES [dbo].[LigneChoix] ([numeroDeChoix])
)


/****** Script for SelectTopNRows command from SSMS  ******/
SELECT *
  FROM [IMDB].[dbo].[MovieScores]
  where 1=1
	and Available=1
	--and Genre like '%fantasy%' 
	--and Genre like '%family%' 
	--and Genre like '%Horror%' 
	--and Genre like '%thriller%' 
	--and Genre like '%Sci-Fi%' 
	and Genre like '%Action%' 
	--and Genre like '%Adventure%' 
	--and Genre like '%Mystery%' 
	--and Genre like '%Crime%' 
	--and Title like '%rat%' 
  order by UltimateScore desc
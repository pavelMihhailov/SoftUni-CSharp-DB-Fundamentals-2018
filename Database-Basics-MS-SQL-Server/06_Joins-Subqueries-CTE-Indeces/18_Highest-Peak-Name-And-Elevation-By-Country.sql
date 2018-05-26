WITH CTE_PeaksMountains (Country, PeakName, Elevation, Mountain) AS (
	 SELECT c.CountryName, p.PeakName, p.Elevation, m.MountainRange
	   FROM Countries AS c
  LEFT JOIN MountainsCountries as mc ON c.CountryCode = mc.CountryCode
  LEFT JOIN Mountains AS m ON mc.MountainId = m.Id
  LEFT JOIN Peaks AS p ON p.MountainId = m.Id
)
SELECT TOP 5
  TopElevations.Country AS Country,
  ISNULL(pm.PeakName, '(no highest peak)') AS HighestPeakName,
  ISNULL(TopElevations.HighestElevation, 0) AS HighestPeakElevation,	
  ISNULL(pm.Mountain, '(no mountain)') AS Mountain
FROM 
(  SELECT Country, MAX(Elevation) AS HighestElevation
     FROM CTE_PeaksMountains
 GROUP BY Country) AS TopElevations
LEFT JOIN CTE_PeaksMountains AS pm 
	   ON (TopElevations.Country = pm.Country AND TopElevations.HighestElevation = pm.Elevation)
 ORDER BY Country, HighestPeakName
SELECT ct.ContinentCode, ct.CurrencyCode, ct.Usages FROM
(
	   SELECT con.ContinentCode,
	  	      cu.CurrencyCode,
		      COUNT(cu.CurrencyCode) AS Usages,
		      DENSE_RANK() OVER(PARTITION BY (con.ContinentCode)
		      ORDER BY COUNT(cu.CurrencyCode) DESC) AS Rank
		 FROM Continents AS con
		 JOIN Countries AS c
		   ON c.ContinentCode = con.ContinentCode
		 JOIN Currencies AS cu
		   ON cu.CurrencyCode = c.CurrencyCode
	 GROUP BY con.ContinentCode, cu.CurrencyCode
	   HAVING COUNT(cu.CurrencyCode) > 1
) AS ct 
WHERE ct.Rank = 1
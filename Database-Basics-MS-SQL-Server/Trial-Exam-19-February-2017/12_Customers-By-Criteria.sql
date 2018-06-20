SELECT
  c.FirstName,
  c.Age,
  c.PhoneNumber 
  FROM Customers AS c
WHERE (c.Age >= 21 AND c.FirstName LIKE '%an%') OR (RIGHT(c.PhoneNumber, 2) = '38' AND c.CountryId <> 31)
ORDER BY c.FirstName, c.Age DESC
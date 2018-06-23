SELECT DISTINCT u.Username FROM Users AS u
JOIN Reports AS r ON r.UserId = u.Id
WHERE 
(ISNUMERIC(LEFT(u.Username, 1)) = 1 AND r.CategoryId = LEFT(u.Username, 1))
OR
(ISNUMERIC(RIGHT(u.Username, 1)) = 1 AND r.CategoryId = RIGHT(u.Username, 1))
ORDER BY u.Username
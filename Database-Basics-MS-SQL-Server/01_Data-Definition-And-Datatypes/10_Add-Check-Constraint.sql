ALTER TABLE Users
ADD CONSTRAINT CHK_Password CHECK (len([Password]) >= 5)
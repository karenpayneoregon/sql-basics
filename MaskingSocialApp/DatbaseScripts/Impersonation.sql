USE [ComputedSample4]

---ALTER TABLE dbo.Taxpayer ALTER COLUMN SocialSecurityNumber [NCHAR](9) MASKED WITH (FUNCTION = 'partial(0,"XXXXX",4)');
---ALTER TABLE dbo.Taxpayer ALTER COLUMN PhoneNumber [NCHAR](12) MASKED WITH (FUNCTION = 'partial(0,"XXXXX",4)');

--- ALTER TABLE dbo.Taxpayer ALTER COLUMN [Name] DROP MASKED


---ALTER TABLE dbo.Taxpayer ALTER COLUMN BirthDate DATE MASKED WITH (FUNCTION = 'default()');

---ALTER TABLE dbo.Taxpayer ALTER COLUMN BirthDay DATE MASKED WITH (FUNCTION = 'default()')

-- Create a non-privileged user
CREATE USER NonPrivilegedUser WITHOUT LOGIN;
-- Grant SELECT permission
GRANT SELECT ON dbo.Taxpayer TO NonPrivilegedUser;
-- Impersonate the user to show initial masked view
EXECUTE AS USER = 'NonPrivilegedUser';
SELECT t.Id,t.FirstName,t.LastName,t.SocialSecurityNumber, t.PhoneNumber, t.BirthDay, t.BirthYear FROM dbo.Taxpayer AS t
-- Revert impersonation
REVERT;
-- Grant UNMASK permission
GRANT UNMASK TO NonPrivilegedUser;
-- Impersonate again to show unmasked view
EXECUTE AS USER = 'NonPrivilegedUser';
-- Revert impersonation
REVERT;
-- Remove the user
DROP USER NonPrivilegedUser;

SELECT t.Id,t.FirstName,t.LastName,t.SocialSecurityNumber, t.PhoneNumber, t.BirthDay, t.BirthYear FROM dbo.Taxpayer AS t

EXECUTE AS USER = 'NonPrivilegedUser';
SELECT	t.Id,
        t.FirstName,
        t.LastName,
        t.SocialSecurityNumber,
        t.PhoneNumber,
        t.BirthDay,
        t.BirthYear
  FROM dbo.Taxpayer AS t;

REVERT;

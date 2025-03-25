CREATE PROCEDURE GetTaxpayersAsNonPrivilegedUser
AS
BEGIN
    EXECUTE AS USER = 'NonPrivilegedUser';
    SELECT * FROM Taxpayer;
    REVERT;
END

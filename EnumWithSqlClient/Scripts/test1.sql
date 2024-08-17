


-- Create the stored procedure
CREATE PROCEDURE GetProductsForOrder
    @OrderID INT
AS
BEGIN
    -- Select all products for the given order
    SELECT p.ProductName, od.Quantity, od.UnitPrice
    FROM OrderDetail od
    INNER JOIN Products p ON od.ProductID = p.ProductID
    WHERE od.OrderID = @OrderID
END

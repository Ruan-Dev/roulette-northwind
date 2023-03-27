CREATE PROCEDURE pr_GetOrderSummary 
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL,
    @EmployeeID INT = NULL,
    @CustomerID VARCHAR(5) = NULL
AS
BEGIN
    SELECT 
        CONCAT(e.TitleOfCourtesy, ' ', e.FirstName, ' ', e.LastName) AS EmployeeFullName,
        s.CompanyName AS ShipperCompanyName,
        c.CompanyName AS CustomerCompanyName,
        COUNT(DISTINCT o.OrderID) AS NumberOfOrders,
        CONVERT(DATE, o.OrderDate) AS OrderDate,
        SUM(o.Freight) AS TotalFreightCost,
        COUNT(DISTINCT od.ProductID) AS NumberOfDifferentProducts,
        SUM(od.UnitPrice * od.Quantity) AS TotalOrderValue
    FROM Orders o
    JOIN Employees e ON o.EmployeeID = e.EmployeeID
    JOIN Shippers s ON o.ShipVia = s.ShipperID
    JOIN Customers c ON o.CustomerID = c.CustomerID
    JOIN [Order Details] od ON o.OrderID = od.OrderID
    WHERE (@StartDate IS NULL OR o.OrderDate >= @StartDate)
      AND (@EndDate IS NULL OR o.OrderDate <= @EndDate)
      AND (@EmployeeID IS NULL OR o.EmployeeID = @EmployeeID)
      AND (@CustomerID IS NULL OR o.CustomerID = @CustomerID)
    GROUP BY CONVERT(DATE, o.OrderDate), e.EmployeeID, e.TitleOfCourtesy, e.FirstName, e.LastName, 
             c.CustomerID, c.CompanyName, s.ShipperID, s.CompanyName
    ORDER BY CONVERT(DATE, o.OrderDate), e.EmployeeID, c.CustomerID, s.ShipperID
END
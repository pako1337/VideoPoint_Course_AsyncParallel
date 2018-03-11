SELECT top 10 prod.ProductID, prod.Name, SUM(th.ActualCost*th.Quantity+tha.ActualCost*tha.Quantity)
FROM Production.TransactionHistory th
INNER JOIN Production.TransactionHistoryArchive tha ON th.ProductID = tha.ProductID
INNER JOIN Production.Product prod ON prod.ProductId = th.ProductID
WHERE th.TransactionDate > '2014-03-01'
group by prod.ProductID, prod.Name
order by SUM(th.ActualCost*th.Quantity+tha.ActualCost*tha.Quantity) desc

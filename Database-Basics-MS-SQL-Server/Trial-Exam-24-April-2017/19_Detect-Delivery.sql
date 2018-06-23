CREATE TRIGGER tr_DetectDelivery ON Orders AFTER UPDATE
AS
DECLARE @OldStatus INT = (SELECT Delivered FROM deleted)
DECLARE @NewStatus INT = (SELECT Delivered FROM inserted)

IF(@OldStatus = 0 AND @NewStatus = 1)
BEGIN
     UPDATE Parts
     SET StockQty += op.Quantity
     FROM Parts AS p
     JOIN OrderParts AS op ON op.PartId = p.PartId
     JOIN Orders AS o ON o.OrderId = op.OrderId
     JOIN inserted AS i ON i.OrderId = o.OrderId
     JOIN deleted AS d ON d.OrderId = i.OrderId
END
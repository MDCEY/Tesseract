 SELECT Audit_Move_Date,
        Audit_Part_Num,
        Audit_Type,
        Audit_Dest_Site_Num,
        Audit_Qty,
        Part_Desc,
        Stock_Total_Qty
 FROM   SCPart
     INNER JOIN SCAudit ON Part_Num = Audit_Part_Num
     INNER JOIN SPStock ON Audit_Part_Num = Stock_Part_Num AND Audit_Dest_Site_Num = Stock_Site_Num
 WHERE  Audit_Dest_Site_Num = 'STOWPARTS'
   AND Audit_Type IN ('PO','SC')
   AND Audit_Qty > 0
   AND DATEDIFF(dd,Audit_Move_Date,GETDATE()) <= 7
 ORDER BY Audit_Move_Date DESC , Part_Desc



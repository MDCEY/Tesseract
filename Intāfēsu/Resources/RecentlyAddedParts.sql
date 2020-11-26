 SELECT "AddedOnDate" = Audit_Move_Date,
        "PartNumber" = Audit_Part_Num,
        "Description" = Part_Desc,
        "CurrentStock" = Stock_Total_Qty
 FROM   COOPESOLBRANCHLIVE.dbo.SCPart
     INNER JOIN COOPESOLBRANCHLIVE.dbo.SCAudit ON Part_Num = Audit_Part_Num
     INNER JOIN COOPESOLBRANCHLIVE.dbo.SPStock ON Audit_Part_Num = Stock_Part_Num AND Audit_Dest_Site_Num = Stock_Site_Num
 WHERE  Audit_Dest_Site_Num = 'STOWPARTS'
   AND Audit_Type IN ('PO','SC')
   AND Audit_Qty > 0
   AND DATEDIFF(dd,Audit_Move_Date,GETDATE()) <= 7
   AND Stock_Total_Qty > 0
 ORDER BY [AddedOnDate] DESC , [Description]



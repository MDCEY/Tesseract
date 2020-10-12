SELECT Audit_Part_Num as 'PartNumber', Part_Desc as 'Description', Audit_Last_Update as 'MovedAt', Employ_Name 'Engineer', Stock_Bin 'Location', Audit_Qty 'Quantity'   FROM COOPESOLBRANCHLIVE.dbo.SCAudit
  JOIN COOPESOLBRANCHLIVE.dbo.SCPart on Part_Num = Audit_Part_Num
  JOIN COOPESOLBRANCHLIVE.dbo.SPStock ON Audit_Part_Num = Stock_Part_Num AND Audit_Source_Site_Num = Stock_Site_Num
  JOIN COOPESOLBRANCHLIVE.dbo.SCEmploy ON Employ_Para = Audit_Dest_Site_Num
  WHERE
  Audit_Move_Date between
      Convert(DateTime, DATEDIFF(DAY, 0, GETDATE())) and
      Dateadd(day, 1, DATEDIFF(DAY, 0, GETDATE()))
  AND Audit_Part_Num is not null
  AND Audit_Source_Site_Num = 'STOWPARTS'
  AND Audit_Dest_Site_Num LIKE '%BK'
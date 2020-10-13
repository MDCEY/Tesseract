SELECT count(Part_Num) 'Total',isnull(Part_Desc, Prod_Desc) 'partDescription',
       MAX(Call_InDate) 'LastBookedIn' FROM COOPESOLBRANCHLIVE.dbo.SCCall 
left join COOPESOLBRANCHLIVE.dbo.SCPart ON Job_Part_Num = Part_Num
LEFT JOIN COOPESOLBRANCHLIVE.dbo.SCProd ON Job_Part_Num = Prod_Desc
WHERE DATEDIFF(hh,Call_InDate, GETDATE()) < 16
AND Job_CDate is null
AND Call_CalT_Code = 'ZR1'
GROUP BY Part_Desc, Prod_Desc
ORDER BY [LastBookedIn] DESC, [partDescription]
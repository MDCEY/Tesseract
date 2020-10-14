SELECT Job_CDate as DateAdded,
 Call_Employ_Num as EngineerNumber,
 Employ_Name as EngineerName,
 Call_Ser_Num as SerialNumber,
 isnull(Part_Desc, Prod_Desc) as ProductDescription
 from COOPESOLBRANCHLIVE.dbo.SCCall
LEFT JOIN COOPESOLBRANCHLIVE.dbo.SCPART on isnull(Call_Prod_Num,Job_Part_Num) = Part_Num
LEFT JOIN COOPESOLBRANCHLIVE.dbo.SCPROD on isnull(Call_Prod_Num,Job_Part_Num) = Prod_Num
LEFT JOIN COOPESOLBRANCHLIVE.dbo.SCEMPLOY ON Call_Employ_Num = Employ_Num
WHERE datediff(day,Job_CDate,getdate()) = 0
group by Job_CDate, Call_Ser_Num, isnull(Part_Desc, Prod_Desc), Call_Employ_Num, Employ_Name
order by Job_CDate DESC, Call_Ser_Num
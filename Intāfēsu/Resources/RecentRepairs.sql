SELECT Job_CDate as DateAdded,
 Call_Employ_Num as EngineerNumber,
 Call_Ser_Num as SerialNumber,
 isnull(Part_Desc, Prod_Desc) as ProductDescription
 from COOPESOLBRANCHLIVE.dbo.SCCall
LEFT JOIN COOPESOLBRANCHLIVE.dbo.SCFSRL on FSRL_Call_Num = Call_Num
LEFT JOIN COOPESOLBRANCHLIVE.dbo.SCPART on isnull(Call_Prod_Num,Job_Part_Num) = Part_Num
LEFT JOIN COOPESOLBRANCHLIVE.dbo.SCPROD on isnull(Call_Prod_Num,Job_Part_Num) = Prod_Num
WHERE datediff(day,Job_CDate,getdate()) = 0
group by Job_CDate, Call_Ser_Num, isnull(Part_Desc, Prod_Desc), isnull(FSRL_Cost, 0), Call_Employ_Num
order by Job_CDate DESC, Call_Ser_Num
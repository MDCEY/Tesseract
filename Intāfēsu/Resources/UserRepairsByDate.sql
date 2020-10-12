SELECT ISNULL(COUNT(Call_Num),0) as 'Repairs'
FROM COOPESOLBRANCHLIVE.dbo.SCCall
WHERE Job_CDate between Convert(DateTime, DATEDIFF(DAY, 0, GETDATE())) and Dateadd(day, 1, DATEDIFF(DAY, 0, GETDATE()))
AND Call_Employ_Num = @User
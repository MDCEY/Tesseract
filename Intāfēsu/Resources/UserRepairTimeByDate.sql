SELECT isnull(SUM(FSR_Work_Time), 0)
FROM COOPESOLBRANCHLIVE.dbo.SCCall
         JOIN COOPESOLBRANCHLIVE.dbo.SCFSR ON
        FSR_Call_Num = Call_Num AND
        Call_FSR_Count = FSR_Num
         JOIN COOPESOLBRANCHLIVE.dbo.SCEmploy ON
    Call_Employ_Num = Employ_Num
WHERE Employ_Para like '%BK'
  AND Job_CDate between Convert(DateTime, DATEDIFF(DAY, 0, GETDATE())) and
    Dateadd(day, 1, DATEDIFF(DAY, 0, GETDATE()))
  AND Call_Employ_Num = @User
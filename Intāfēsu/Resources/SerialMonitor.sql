SELECT
 call_num, Call_User FROM COOPESOLBRANCHLIVE.dbo.SCCALL
 WHERE Call_Ser_Num = @SerialNumber
 AND Call_InDate between dateadd(day,-5,GetDate()) and dateadd(day,5,GetDate())
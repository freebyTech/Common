@ECHO OFF
IF %1.==. GOTO ExecuteStatements 
IF %1=="" GOTO ExecuteStatements 

SET Repository_Location=%1

:ExecuteStatements
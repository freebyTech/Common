@ECHO OFF
IF %1.==. GOTO ExecuteStatements 
IF %1=="" GOTO ExecuteStatements 

SET Repository_Location=%1

:ExecuteStatements
.\.build\RepoGet.exe .\.build "NuGet" /VERBOSE /BT:Release /ONLYFILES:*.dll+*.exe
.\.build\RepoGet.exe .\.build\temp "RepoGet" /VERBOSE /BT:Release /ONLYFILES:*.dll+*.exe
robocopy .\.build\temp .\.build *.* /MOVE /IS
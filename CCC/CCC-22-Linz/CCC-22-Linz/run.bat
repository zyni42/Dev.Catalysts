@ECHO OFF
CLS

SETLOCAL

IF "%1" == "?" SET USAGE=YES
IF "%1" == ""  SET USAGE=YES

IF "%USAGE%" == "YES" (
	ECHO :
	ECHO : USAGE: %0
	ECHO :
	ECHO :            LEVEL = 1, 2, 3, 4, 5, 6, 7
	ECHO :
	ECHO : EXAMPLE : %0 6 ..... run the 6th level
	ECHO :
	GOTO END
)

set LEVEL=level6.in

IF NOT "%1" == "" set LEVEL=level%1.in

REM cd bin\debug

ECHO java  -jar simulator.jar --exec="bin\debug\CCC-22-Linz.exe %1" --level=Level%1\%LEVEL%
     java  -jar simulator.jar --exec="bin\debug\CCC-22-Linz.exe %1" --level=Level%1\%LEVEL%

REM cd ..\..

:END

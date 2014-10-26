@ECHO OFF

SETLOCAL

IF "%1" == "?" SET USAGE=YES
IF "%1" == ""  SET USAGE=YES

IF "%USAGE%" == "YES" (
	ECHO :
	ECHO : USAGE: %0  LEVEL [ SPEED ]
	ECHO :
	ECHO :            LEVEL = 1, 2, 3, 4, 5, 6, 7
	ECHO :            SPEED = 1, 2, 3, ... 100
	ECHO :
	ECHO : EXAMPLE : %0 6 10 ..... run the 6th level at speed 10
	ECHO :
	GOTO END
)

set LEVEL=level6.in
set SPEED=1

IF NOT "%1" == "" set LEVEL=level%1.in
IF NOT "%2" == "" set SPEED=%2

cd bin\debug

ECHO java  -jar simulator.jar --exec="CCC-21-Rathaus.exe %1" --level=%LEVEL% --speed=%SPEED%
     java  -jar simulator.jar --exec="CCC-21-Rathaus.exe %1" --level=%LEVEL% --speed=%SPEED%

cd ..\..

:END
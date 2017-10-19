@ECHO OFF
CLS

SETLOCAL

REM *** 
REM *** IF "%1" == "?" SET USAGE=YES
REM *** IF "%1" == ""  SET USAGE=YES
REM *** 
REM *** IF "%USAGE%" == "YES" (
REM *** 	ECHO :
REM *** 	ECHO : USAGE: %0
REM *** 	ECHO :
REM *** 	ECHO :            LEVEL = 1, 2, 3, 4, 5, 6, 7
REM *** 	ECHO :
REM *** 	ECHO : EXAMPLE : %0 6 ..... run the 6th level
REM *** 	ECHO :
REM *** 	GOTO END
REM *** )
REM *** 
REM *** set LEVEL=level6.in
REM *** 
REM *** IF NOT "%1" == "" set LEVEL=level%1\level%1.in
REM *** IF NOT "%2" == "" set LEVEL=level%2\level%2.in
REM *** 

cd bin\debug

SHIFT
ECHO CCC-23-Rathaus.exe %0 %1 %2 %3 %4 %5 %6 %7 %8 %9
     CCC-23-Rathaus.exe %0 %1 %2 %3 %4 %5 %6 %7 %8 %9

cd ..\..

:END

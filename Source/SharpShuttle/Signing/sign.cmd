@echo off
set timestampurl="http://timestamp.comodoca.com/authenticode"
set pfxcontainer="sharpshuttle_signcontainer2.pfx"
set signtoolpath=C:\Program Files\Microsoft SDKs\Windows\v6.0A\bin

echo RUNNING POSTBUILD SIGNING

if /I "%1" == "Release" goto sign

echo SIGNING IGNORED DUE TO %1 CONFIGURATION

goto end



:sign
REM start nu het signen
ECHO WARNING: Private key must be present in %%private_key_password%% system variable!
ECHO.
ECHO RUNNING: signtool.exe sign 
ECHO            /f %pfxcontainer%
ECHO            /p "%%private_key_password%%" 
ECHO            /t %timestampurl% 
ECHO            /v %2
ECHO.

"%signtoolpath%\signtool.exe" sign /f %pfxcontainer% /p "%private_key_password%" /t %timestampurl% /v %2

:end
echo FINISHED POSTBUILD
exit
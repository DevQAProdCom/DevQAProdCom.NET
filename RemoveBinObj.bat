@echo off
echo Searching for "bin", "obj" folders and "chromedriver.exe" files to delete...

:: Recursively search and delete ".git" folders
for /d /r %%i in (.git) do (
    echo Deleting folder: %%i
    rd /s /q "%%i"
)

:: Recursively search and delete "bin" folders
for /d /r %%i in (bin) do (
    echo Deleting folder: %%i
    rd /s /q "%%i"
)

:: Recursively search and delete "obj" folders
for /d /r %%i in (obj) do (
    echo Deleting folder: %%i
    rd /s /q "%%i"
)

:: Recursively search and delete "chromedriver.exe" files 
for /r %%i in (chromedriver.exe) do ( 
     echo Deleting file: %%i 
     del /f /q "%%i" 
)

echo All "bin", "obj" folders, "chromedriver.exe" files, and ".git" folders deleted.
pause

SET /P _inputserver= Please enter a MS SQL Server:

for %%G in (0000*.sql) do sqlcmd /S "%_inputname%" -E -i"%%G"

for %%G in (*.sql) do sqlcmd /S "%_inputname%" /d PandemicaDB -E -i"%%G"
pause
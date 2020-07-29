for %%G in (0000*.sql) do sqlcmd /S localhost -E -i"%%G"

for %%G in (*.sql) do sqlcmd /S localhost /d PandemicaDB -E -i"%%G"
pause
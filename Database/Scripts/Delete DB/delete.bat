for %%G in (*.sql) do sqlcmd /S localhost /d PandemicaDB -E -i"%%G"
pause
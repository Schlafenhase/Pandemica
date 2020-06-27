for %%G in (*.sql) do sqlcmd /S DESKTOP-VGQH22C /d PandemicaDB -E -i"%%G"
pause
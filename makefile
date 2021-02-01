CC=csc
FILENAME=Conecta4.cs
.DEFAULT_GOAL=run

run:
	${CC} ${FILENAME}
	# ./Conecta4.exe < diagonal.txt
	# ./Conecta4.exe < empate.txt
	# ./Conecta4.exe < gana_el_primero.txt
/*

  ____          _____               _ _           _       
 |  _ \        |  __ \             (_) |         | |      
 | |_) |_   _  | |__) |_ _ _ __ _____| |__  _   _| |_ ___ 
 |  _ <| | | | |  ___/ _` | '__|_  / | '_ \| | | | __/ _ \
 | |_) | |_| | | |  | (_| | |   / /| | |_) | |_| | ||  __/
 |____/ \__, | |_|   \__,_|_|  /___|_|_.__/ \__, |\__\___|
         __/ |                               __/ |        
        |___/                               |___/         
    
____________________________________
/ Si necesitas ayuda, contáctame en \
\ https://parzibyte.me               /
 ------------------------------------
        \   ^__^
         \  (oo)\_______
            (__)\       )\/\
                ||----w |
                ||     ||
Creado por Parzibyte (https://parzibyte.me).
------------------------------------------------------------------------------------------------
Si el código es útil para ti, puedes agradecerme siguiéndome: https://parzibyte.me/blog/sigueme/
Y compartiendo mi blog con tus amigos
También tengo canal de YouTube: https://www.youtube.com/channel/UCroP4BTWjfM0CkGB6AFUoBg?sub_confirmation=1
------------------------------------------------------------------------------------------------
*/
class Conecta4
{
    static int FILAS = 10;
    static int COLUMNAS = 10;
    static int CONECTA = 7;
    static string JUGADOR_1 = "o";
    static string JUGADOR_2 = "x";
    static string ESPACIO_VACIO = " ";
    // Opciones del menú
    static int MODO_HUMANO_CONTRA_HUMANO = 1;
    static int MODO_HUMANO_CONTRA_CPU = 2;
    static int MODO_CPU_CONTRA_CPU = 3;
    static int OPCION_MENU_SALIR = 4;
    // Otras constantes
    static int ERROR_FILA_INVALIDA = 24000;
    static int ERROR_COLUMNA_LLENA = 24001;
    static int ERROR_NINGUNO = 24002;
    static int CONECTA_ARRIBA = 24003;
    static int CONECTA_ABAJO_DERECHA = 25000;
    static int CONECTA_ARRIBA_DERECHA = 25001;
    static int NO_CONECTA = 25002;
    static int FILA_NO_ENCONTRADA = 25003;
    static int COLUMNA_GANADORA_NO_ENCONTRADA = 25004;
    static int CONECTA_DERECHA = 25005;
    static string JUGADOR_CPU_2 = JUGADOR_2;
    static string JUGADOR_CPU_1 = JUGADOR_1;

    static string[,] clonarMatriz(string[,] tableroOriginal)
    {
        return tableroOriginal.Clone() as string[,];
    }

    static int obtenerColumnaGanadora(string jugador, string[,] tableroOriginal)
    {
        string[,] tablero = new string[FILAS, COLUMNAS];
        int i;
        for (i = 0; i < COLUMNAS; i++)
        {
            tablero = clonarMatriz(tableroOriginal);
            int resultado = colocarPieza(jugador, i, tablero);
            if (resultado == ERROR_NINGUNO)
            {
                int gana = ganador(jugador, tablero);
                if (gana != NO_CONECTA)
                {
                    return i;
                }
            }
        }
        return COLUMNA_GANADORA_NO_ENCONTRADA;
    }

    static int obtenerPrimeraFilaLlena(int columna, string[,] tablero)
    {
        int i;
        for (i = 0; i < FILAS; ++i)
        {
            if (tablero[i, columna] != ESPACIO_VACIO)
            {
                return i;
            }
        }
        return FILA_NO_ENCONTRADA;
    }

    static (int, int) obtenerColumnaEnLaQueSeObtieneMayorPuntaje(string jugador, string[,] tableroOriginal)
    {

        int conteoMayor = 0, indiceColumnaConConteoMayor = -1;
        string[,] tablero = new string[FILAS, COLUMNAS];
        int i;
        for (i = 0; i < COLUMNAS; ++i)
        {
            tablero = clonarMatriz(tableroOriginal);
            int estado = colocarPieza(jugador, i, tablero);
            if (estado == ERROR_NINGUNO)
            {
                int filaDePiezaRecienColocada = obtenerPrimeraFilaLlena(i, tablero);
                if (filaDePiezaRecienColocada != FILA_NO_ENCONTRADA)
                {
                    int c = contarArriba(i, filaDePiezaRecienColocada, jugador, tablero);
                    if (c > conteoMayor)
                    {
                        conteoMayor = c;
                        indiceColumnaConConteoMayor = i;
                    }
                    c = contarArribaDerecha(i, filaDePiezaRecienColocada, jugador, tablero);
                    if (c > conteoMayor)
                    {
                        conteoMayor = c;
                        indiceColumnaConConteoMayor = i;
                    }
                    c = contarDerecha(i, filaDePiezaRecienColocada, jugador, tablero);
                    if (c > conteoMayor)
                    {
                        conteoMayor = c;
                        indiceColumnaConConteoMayor = i;
                    }
                    c = contarAbajoDerecha(i, filaDePiezaRecienColocada, jugador, tablero);
                    if (c > conteoMayor)
                    {
                        conteoMayor = c;
                        indiceColumnaConConteoMayor = i;
                    }
                }
            }
        }
        return (conteoMayor, indiceColumnaConConteoMayor);
    }



    static int obtenerColumnaAleatoria(string jugador, string[,] tableroOriginal)
    {
        while (true)
        {
            string[,] tablero = new string[FILAS, COLUMNAS];
            tablero = clonarMatriz(tableroOriginal);
            int columna = aleatorio_en_rango(0, COLUMNAS - 1);
            int resultado = colocarPieza(jugador, columna, tablero);
            if (resultado == ERROR_NINGUNO)
            {
                return columna;
            }
        }
    }

    static int obtenerColumnaCentral(string jugador, string[,] tableroOriginal)
    {
        string[,] tablero = new string[FILAS, COLUMNAS];
        tablero = clonarMatriz(tableroOriginal);
        int mitad = (COLUMNAS - 1) / 2;
        int resultado = colocarPieza(jugador, mitad, tablero);
        if (resultado == ERROR_NINGUNO)
        {
            return mitad;
        }
        return COLUMNA_GANADORA_NO_ENCONTRADA;
    }

    static int elegirColumnaCpu(string jugador, string[,] tablero)
    {
        // Voy a comprobar si puedo ganar...
        int posibleColumnaGanadora = obtenerColumnaGanadora(jugador, tablero);
        if (posibleColumnaGanadora != COLUMNA_GANADORA_NO_ENCONTRADA)
        {
            System.Console.Write("*elijo ganar*\n");
            return posibleColumnaGanadora;
        }
        // Si no, voy a comprobar si mi oponente gana con el siguiente movimiento, para evitarlo
        string oponente = obtenerOponente(jugador);
        int posibleColumnaGanadoraDeOponente = obtenerColumnaGanadora(oponente, tablero);
        if (posibleColumnaGanadoraDeOponente != COLUMNA_GANADORA_NO_ENCONTRADA)
        {
            System.Console.Write("*elijo evitar que mi oponente gane*\n");
            return posibleColumnaGanadoraDeOponente;
        }
        // En caso de que nadie pueda ganar en el siguiente movimiento, buscaré en dónde se obtiene el mayor
        // puntaje al colocar la pieza
        var (conteoCpu, columnaCpu) = obtenerColumnaEnLaQueSeObtieneMayorPuntaje(jugador, tablero);
        var (conteoOponente, columnaOponente) = obtenerColumnaEnLaQueSeObtieneMayorPuntaje(oponente, tablero);
        if (conteoOponente > conteoCpu)
        {
            System.Console.Write("*elijo quitarle el puntaje a mi oponente*\n");
            return columnaOponente;
        }
        else if (conteoCpu > 1)
        {
            System.Console.Write("*elijo colocarla en donde obtengo un mayor puntaje*\n");
            return columnaCpu;
        }
        // Si no, regresar la central por si está desocupada

        int columnaCentral = obtenerColumnaCentral(jugador, tablero);
        if (columnaCentral != COLUMNA_GANADORA_NO_ENCONTRADA)
        {
            System.Console.Write("*elijo ponerla en el centro*\n");
            return columnaCentral;
        }
        // Finalmente, devolver la primera disponible de manera aleatoria
        int columna = obtenerColumnaAleatoria(jugador, tablero);
        if (columna != FILA_NO_ENCONTRADA)
        {
            System.Console.Write("*elijo la primera vacía aleatoria*\n");
            return columna;
        }
        System.Console.Write("Esto no debería suceder\n");
        return 0;
    }



    static int solicitarColumnaAJugador()
    {
        System.Console.Write("Escribe la columna en donde colocar la pieza: ");
        int columna = System.Convert.ToInt32(System.Console.ReadLine());
        // Necesitamos índices de arreglos
        columna--;
        return columna;
    }
    static string elegirJugadorAlAzar()
    {
        int numero = aleatorio_en_rango(0, 1);
        if (numero == 1)
        {
            return JUGADOR_1;
        }
        else
        {
            return JUGADOR_2;
        }
    }
    static string obtenerOponente(string jugador)
    {
        if (jugador == JUGADOR_1)
        {
            return JUGADOR_2;
        }
        else
        {
            return JUGADOR_1;
        }
    }
    static int aleatorio_en_rango(int minimo, int maximo)
    {
        System.Random rnd = new System.Random();
        return rnd.Next(minimo, maximo + 1);
    }

    static int obtenerFilaDesocupada(int columna, string[,] tablero)
    {
        int i;
        for (i = FILAS - 1; i >= 0; i--)
        {
            if (tablero[i, columna] == ESPACIO_VACIO)
            {
                return i;
            }
        }
        return FILA_NO_ENCONTRADA;
    }
    static int colocarPieza(string jugador, int columna, string[,] tablero)
    {
        if (columna < 0 || columna >= COLUMNAS)
        {
            return ERROR_FILA_INVALIDA;
        }
        int fila = obtenerFilaDesocupada(columna, tablero);
        if (fila == FILA_NO_ENCONTRADA)
        {
            return ERROR_COLUMNA_LLENA;
        }
        tablero[fila, columna] = jugador;
        return ERROR_NINGUNO;
    }
    static void limpiarTablero(string[,] tablero)
    {
        int i;
        for (i = 0; i < FILAS; ++i)
        {
            int j;
            for (j = 0; j < COLUMNAS; ++j)
            {
                tablero[i, j] = ESPACIO_VACIO;
            }
        }
    }
    static void dibujarEncabezado(int columnas)
    {
        System.Console.Write("\n");
        int i;
        for (i = 0; i < columnas; ++i)
        {
            System.Console.Write("|" + (i + 1));
            if (i + 1 >= columnas)
            {
                System.Console.Write("|");
            }

        }
    }

    static int dibujarTablero(string[,] tablero)
    {
        dibujarEncabezado(COLUMNAS);
        System.Console.Write("\n");
        int i;
        for (i = 0; i < FILAS; ++i)
        {
            int j;
            for (j = 0; j < COLUMNAS; ++j)
            {
                System.Console.Write("|" + tablero[i, j]);
                if (j + 1 >= COLUMNAS)
                {
                    System.Console.Write("|");
                }
            }
            System.Console.Write("\n");
        }
        return 0;
    }
    static bool esEmpate(string[,] tablero)
    {
        int i;
        for (i = 0; i < COLUMNAS; ++i)
        {
            int resultado = obtenerFilaDesocupada(i, tablero);
            if (resultado != FILA_NO_ENCONTRADA)
            {
                return false;
            }
        }
        return true;
    }
    static int contarArriba(int x, int y, string jugador, string[,] tablero)
    {
        int yInicio = (y - CONECTA >= 0) ? y - CONECTA + 1 : 0;
        int contador = 0;
        for (; yInicio <= y; yInicio++)
        {
            if (tablero[yInicio, x] == jugador)
            {
                contador++;
            }
            else
            {
                contador = 0;
            }
        }
        return contador;
    }

    static int contarDerecha(int x, int y, string jugador, string[,] tablero)
    {
        int xFin = (x + CONECTA < COLUMNAS) ? x + CONECTA - 1 : COLUMNAS - 1;
        int contador = 0;
        for (; x <= xFin; x++)
        {
            if (tablero[y, x] == jugador)
            {
                contador++;
            }
            else
            {
                contador = 0;
            }
        }
        return contador;
    }

    static int contarArribaDerecha(int x, int y, string jugador, string[,] tablero)
    {
        int xFin = (x + CONECTA < COLUMNAS) ? x + CONECTA - 1 : COLUMNAS - 1;
        int yInicio = (y - CONECTA >= 0) ? y - CONECTA + 1 : 0;
        int contador = 0;
        while (x <= xFin && yInicio <= y)
        {
            if (tablero[y, x] == jugador)
            {
                contador++;
            }
            else
            {
                contador = 0;
            }
            x++;
            y--;
        }
        return contador;
    }

    static int contarAbajoDerecha(int x, int y, string jugador, string[,] tablero)
    {
        int xFin = (x + CONECTA < COLUMNAS) ? x + CONECTA - 1 : COLUMNAS - 1;
        int yFin = (y + CONECTA < FILAS) ? y + CONECTA - 1 : FILAS - 1;
        int contador = 0;
        while (x <= xFin && y <= yFin)
        {
            if (tablero[y, x] == jugador)
            {
                contador++;
            }
            else
            {
                contador = 0;
            }
            x++;
            y++;
        }
        return contador;
    }
    static void jugar(int modo)
    {
        string[,] tablero = new string[FILAS, COLUMNAS];
        limpiarTablero(tablero);
        string jugadorActual = elegirJugadorAlAzar();
        System.Console.WriteLine("Comienza el jugador " + jugadorActual);
        while (true)
        {
            int columna = 0;
            System.Console.WriteLine("\nTurno del jugador " + jugadorActual);
            dibujarTablero(tablero);
            if (modo == MODO_HUMANO_CONTRA_CPU)
            {
                if (jugadorActual == JUGADOR_CPU_2)
                {
                    System.Console.Write("CPU 2 pensando...");
                    columna = elegirColumnaCpu(jugadorActual, tablero);
                }
                else
                {
                    columna = solicitarColumnaAJugador();
                }
            }
            else if (modo == MODO_CPU_CONTRA_CPU)
            {

                System.Console.Write($"CPU {(jugadorActual == JUGADOR_CPU_1 ? "1" : "2")} pensando...");
                columna = elegirColumnaCpu(jugadorActual, tablero);
            }
            else if (modo == MODO_HUMANO_CONTRA_HUMANO)
            {
                columna = solicitarColumnaAJugador();
            }
            int estado = colocarPieza(jugadorActual, columna, tablero);
            if (estado == ERROR_COLUMNA_LLENA)
            {
                System.Console.Write("Error: columna llena");
            }
            else if (estado == ERROR_FILA_INVALIDA)
            {
                System.Console.Write("Fila no correcta");
            }
            else if (estado == ERROR_NINGUNO)
            {
                int g = ganador(jugadorActual, tablero);
                if (g != NO_CONECTA)
                {
                    dibujarTablero(tablero);
                    System.Console.WriteLine("Gana el jugador " + jugadorActual);
                    break;
                }
                else if (esEmpate(tablero))
                {
                    dibujarTablero(tablero);
                    System.Console.Write("Empate");
                    break;
                }
            }
            jugadorActual = obtenerOponente(jugadorActual);
        }
    }
    static int ganador(string jugador, string[,] tablero)
    {
        /*
         * Solo necesitamos
         * Arriba
         * Derecha
         * Arriba derecha
         * Abajo derecha
         *
         * */
        int y;
        for (y = 0; y < FILAS; y++)
        {
            int x;
            for (x = 0; x < COLUMNAS; x++)
            {
                int conteoArriba = contarArriba(x, y, jugador, tablero);
                if (conteoArriba >= CONECTA)
                {
                    return CONECTA_ARRIBA;
                }
                if (contarDerecha(x, y, jugador, tablero) >= CONECTA)
                {
                    return CONECTA_DERECHA;
                }
                if (contarArribaDerecha(x, y, jugador, tablero) >= CONECTA)
                {
                    return CONECTA_ARRIBA_DERECHA;
                }
                if (contarAbajoDerecha(x, y, jugador, tablero) >= CONECTA)
                {
                    return CONECTA_ABAJO_DERECHA;
                }
            }
        }
        return NO_CONECTA;
    }



    static void Main(string[] args)
    {
        System.Console.WriteLine("Parzibyte presenta: Conecta 4 en C#");
        System.Console.WriteLine("  https://parzibyte.me/blog");
        System.Console.WriteLine($"{MODO_HUMANO_CONTRA_HUMANO} => Modo humano contra humano");
        System.Console.WriteLine($"{MODO_HUMANO_CONTRA_CPU} => Modo humano contra CPU");
        System.Console.WriteLine($"{MODO_CPU_CONTRA_CPU} => Modo CPU contra CPU");
        System.Console.WriteLine($"{OPCION_MENU_SALIR} => Salir");
        System.Console.Write("Elige: ");
        int modo = System.Convert.ToInt32(System.Console.ReadLine());
        // Perdonar por el if tan largo
        if (modo != MODO_HUMANO_CONTRA_HUMANO && modo != MODO_HUMANO_CONTRA_CPU && modo != MODO_CPU_CONTRA_CPU)
        {
            System.Console.WriteLine("Saliendo...");
            return;
        }
        // Si todo va bien...
        jugar(modo);
    }
}
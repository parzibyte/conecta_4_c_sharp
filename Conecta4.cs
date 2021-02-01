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
    static int FILAS = 4;
    static int COLUMNAS = 4;
    static int CONECTA = 4;
    static string JUGADOR_1 = "o";
    static string JUGADOR_2 = "x";
    static string ESPACIO_VACIO = " ";
    static int ERROR_FILA_INVALIDA = 0;
    static int ERROR_COLUMNA_LLENA = 1;
    static int ERROR_NINGUNO = 2;
    static int CONECTA_ARRIBA = 3;
    static int CONECTA_DERECHA = 4;
    static int CONECTA_ABAJO_DERECHA = 5;
    static int CONECTA_ARRIBA_DERECHA = 6;
    static int NO_CONECTA = 7;
    static int FILA_NO_ENCONTRADA = 8;

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
    static void jugar()
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

            columna = solicitarColumnaAJugador();
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
        jugar();
    }
}
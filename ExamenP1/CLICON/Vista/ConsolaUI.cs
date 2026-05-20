namespace CLICON.Vista;

public static class ConsolaUI
{
    private const ConsoleColor ColorTitulo = ConsoleColor.DarkCyan;
    private const ConsoleColor ColorError = ConsoleColor.Red;
    private const ConsoleColor ColorExito = ConsoleColor.DarkGreen;
    private const ConsoleColor ColorDestacado = ConsoleColor.Yellow;
    private const int Ancho = 60;

    public static void Encabezado()
    {
        Console.Clear();
        Console.ForegroundColor = ColorTitulo;
        Console.WriteLine(new string('═', Ancho));
        Console.WriteLine(Centrar("T I C K E T P R E M I U M"));
        Console.WriteLine(new string('═', Ancho));
        Console.ResetColor();
        Console.WriteLine();
    }

    public static void Titulo(string texto)
    {
        Console.ForegroundColor = ColorTitulo;
        Console.WriteLine(Centrar(texto));
        Console.ResetColor();
        Console.WriteLine(new string('─', Ancho));
        Console.WriteLine();
    }

    public static void Linea()
    {
        Console.WriteLine(new string('─', Ancho));
    }

    public static void Separador()
    {
        Console.ForegroundColor = ColorTitulo;
        Console.WriteLine(new string('═', Ancho));
        Console.ResetColor();
    }

    public static void MostrarError(string mensaje)
    {
        Console.ForegroundColor = ColorError;
        Console.WriteLine($"  {mensaje}");
        Console.ResetColor();
        Console.WriteLine();
    }

    public static void MostrarExito(string mensaje)
    {
        Console.ForegroundColor = ColorExito;
        Console.WriteLine($"  {mensaje}");
        Console.ResetColor();
    }

    public static void Info(string mensaje)
    {
        Console.ForegroundColor = ColorDestacado;
        Console.Write($"  {mensaje}");
        Console.ResetColor();
    }

    public static string LeerTexto(string prompt)
    {
        Console.Write($"  {prompt}: ");
        return Console.ReadLine()?.Trim() ?? string.Empty;
    }

    public static string LeerClave(string prompt)
    {
        Console.Write($"  {prompt}: ");
        var clave = string.Empty;
        ConsoleKeyInfo tecla;
        while ((tecla = Console.ReadKey(true)).Key != ConsoleKey.Enter)
        {
            if (tecla.Key == ConsoleKey.Backspace && clave.Length > 0)
            {
                clave = clave[..^1];
                Console.Write("\b \b");
            }
            else if (!char.IsControl(tecla.KeyChar))
            {
                clave += tecla.KeyChar;
                Console.Write("*");
            }
        }
        Console.WriteLine();
        return clave;
    }

    public static int LeerEntero(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write($"  {prompt}: ");
            var entrada = Console.ReadLine();
            if (int.TryParse(entrada, out var valor) && valor >= min && valor <= max)
            {
                return valor;
            }
            MostrarError($"Ingrese un número entre {min} y {max}.");
        }
    }

    public static bool LeerSiNo(string prompt)
    {
        while (true)
        {
            Console.Write($"  {prompt} (s/n): ");
            var entrada = Console.ReadLine()?.Trim().ToLower();
            if (entrada == "s") return true;
            if (entrada == "n") return false;
            MostrarError("Ingrese 's' o 'n'.");
        }
    }

    public static void EsperarTecla(string mensaje = "Presione cualquier tecla para continuar...")
    {
        Console.WriteLine();
        Info(mensaje);
        Console.ReadKey(true);
        Console.WriteLine();
        Console.ResetColor();
    }

    public static void Opcion(string numero, string texto)
    {
        Console.WriteLine($"  {numero,3}. {texto}");
    }

    public static void Campo(string etiqueta, string valor)
    {
        Console.WriteLine($"  {etiqueta,-18} {valor}");
    }

    private static string Centrar(string texto)
    {
        if (texto.Length >= Ancho) return texto;
        var espacios = (Ancho - texto.Length) / 2;
        return new string(' ', espacios) + texto;
    }

    public static void TablaEncabezado(params string[] columnas)
    {
        Linea();
        var anchoColumna = (Ancho - 4) / columnas.Length;
        foreach (var col in columnas)
            Console.Write($"  {col.PadRight(anchoColumna)}");
        Console.WriteLine();
        Linea();
    }

    public static void TablaFila(params string[] valores)
    {
        var anchoColumna = (Ancho - 4) / valores.Length;
        foreach (var val in valores)
            Console.Write($"  {val.PadRight(anchoColumna)}");
        Console.WriteLine();
    }

    public static void TablaFilaDestacada(params string[] valores)
    {
        Console.ForegroundColor = ColorTitulo;
        TablaFila(valores);
        Console.ResetColor();
    }
}

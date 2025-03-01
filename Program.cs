using System;       //Importate para el unso de funciones de consola int,string, exception
using System.IO;    //Servira para el manejo de archivos y derectorios

class Program
{  // En esta seccion se declaran las variables estaticas que se usaran para el manejo de archivos
    static string directorio = "LaboratorioAvengers";
    static string archivoInventos = Path.Combine(directorio, "inventos.txt");

    static void Main()    // aqui comienza la ejecucion principal sdel programa
    {
        Directory.CreateDirectory(directorio); // se crea un directorio si este no esta creado

        while (true)  // SE crea un buble para que el programa no temrmmine hasta que el usuario lo decida
        {
            // Limpiar pantalla antes de mostrar el encabezado y el menú
            Console.Clear();

                // Mostrar el encabezado junto con el menú
            MostrarEncabezado();

            // Mostrar el menú
            MostrarMenu();

            // Leer la opción seleccionada por el usuario
            if (int.TryParse(Console.ReadLine(), out int opcion))
            {
                switch (opcion)
                {
                    case 1:
                        CrearArchivo(); // se llama a la funcion para crear un archivo
                        break;
                    case 2:  // se llama a la funcion para agregar un invento
                        Console.Write("\nIngrese el nombre del invento: "); 
                        string invento = Console.ReadLine();
                        AgregarInvento(invento);
                        break;
                    case 3:    // se llama a la funcion para leer linea por linea
                        LeerLineaPorLinea();
                        break;
                    case 4: // se llama a la funcion para leer todo el texto
                        LeerTodoElTexto();
                        break;
                    case 5: // se llama a la funcion para copiar un archivo
                        CopiarArchivo();
                        break;
                    case 6:   // se llama a la funcion para mover un archivo
                        MoverArchivo();
                        break;
                    case 7:  // llama a la funcio para crear una carpeta
                        CrearCarpeta("ProyectosSecretos");
                        break;
                    case 8:    //este caso llama a la funcio para listar los archivos del directorio
                        ListarArchivos();
                        break;
                    case 9:  // en esta funcion se elimina un archivo
                        EliminarArchivo();
                        break;
                    case 10:   //servira para salir del programa
                        Console.WriteLine("Saliendo del sistema...");
                        return;
                    default:   // la opcion por defecto di no se selecciona una opcion valida
                        Console.WriteLine("Opción no válida.");
                        break;
                }

                // Pausa para que el usuario vea el resultado de su acción
                Console.WriteLine("\n Señor Stark ¿desea realizar otra acción? \n Presione una tecla para volver al menú...");
                Console.ReadKey();
            }
            else  // si la ocpion es invalida se mosrara un mensaje de error
            {
                Console.WriteLine("\nEntrada inválida. Intente de nuevo.");
                Console.WriteLine("\n Señor Stark ¿desea realizar otra acción? \n Presione una tecla para volver al menú...");
                Console.ReadKey();
            }
        }
    }

    // Función para mostrar el encabezado
    static void MostrarEncabezado()
    {
        Console.WriteLine("=======================================");
        Console.WriteLine("        ¡BIENVENIDO SEÑOR STARK!       ");
        Console.WriteLine("     ESTE ES UN SISTEMA DE GESTIÓN     ");
        Console.WriteLine("      PARA SU LABORATORIO SECRETO       ");
        Console.WriteLine("=======================================");
        Console.WriteLine();
    }

    // Función para mostrar el menú
    static void MostrarMenu()
    {
        Console.WriteLine("\n1. Crear archivo");
        Console.WriteLine("2. Agregar invento");
        Console.WriteLine("3. Leer archivo línea por línea");
        Console.WriteLine("4. Leer todo el archivo");
        Console.WriteLine("5. Copiar archivo a Backup");
        Console.WriteLine("6. Mover archivo a ArchivosClasificados");
        Console.WriteLine("7. Crear carpeta ProyectosSecretos");
        Console.WriteLine("8. Listar archivos del directorio");
        Console.WriteLine("9. Eliminar archivo");
        Console.WriteLine("10. Salir");
        Console.Write("Seleccione una opción: ");
    }

    static void CrearArchivo() // esta funcion crea un archivo llamado inventos.txt
    {
        try  // se usa para manejar los errores que puedan surgir
        {
            File.WriteAllText(archivoInventos, "");  //se crea un archivo cacio
            Console.WriteLine("Archivo 'inventos.txt' creado exitosamente."); // se muestra un mensaje se texto
        }
        catch (Exception ex) // se usa para capturar los errores que puedan surgir
        {
            Console.WriteLine($"Error: {ex.Message}"); //muestra un mensaje de error
        }
    }

    static void AgregarInvento(string invento) //agrega un invento al archivo inventos.txt
    {
        try 
        {
            int numero = 1; //se inicializa la variable numero en 1

            if (File.Exists(archivoInventos)) //se verigfica si el archivo existe
            {
                string[] lineas = File.ReadAllLines(archivoInventos); //se leen la s lineas del archivo
                if (lineas.Length > 0) //se verifica si hay lineas en el archivo
                {
                    string ultimaLinea = lineas[lineas.Length - 1]; //se obtiene la ultima linea del archivo
                    string[] partes = ultimaLinea.Split('.'); //esta linea nos indica que se separe la linea por el punto
                    if (partes.Length > 1 && int.TryParse(partes[0], out int ultimoNumero)) //se revisa si aun hay numeros en el archivo
                    {
                        numero = ultimoNumero + 1; //se incrementa el numero
                    }
                }
            }
            //se agrega el invento al archivo
            File.AppendAllText(archivoInventos, $"{numero}. {invento}{Environment.NewLine}");
            Console.WriteLine($"Invento agregado: {numero}. {invento}");
        }
        catch (Exception ex)//se captura error si hubiese
        {
            Console.WriteLine($"Error al agregar invento: {ex.Message}");
        }
    }
    //en este segmento se crean las funciones para leer el archivo
    static void LeerLineaPorLinea() // lee el archivo linea por linea
    {
        try
        {
            foreach (string linea in File.ReadLines(archivoInventos))
            {
                Console.WriteLine(linea);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void LeerTodoElTexto() //lee todo el texto del archivo
    {
        try
        {
            Console.WriteLine(File.ReadAllText(archivoInventos));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void CopiarArchivo()  //copia el archivo a la carpeta Backup
    {
        try
        {
            string destino = Path.Combine(directorio, "Backup", "inventos_backup.txt"); //se crea la ruta de destino
            Directory.CreateDirectory(Path.Combine(directorio, "Backup")); //se crea la carpeta Backup
            File.Copy(archivoInventos, destino, true);  //se copia el archivo
            Console.WriteLine("Archivo copiado a Backup.");  //se muesta un mensaje de texto
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void MoverArchivo()   //mueve el archivo a la carpeta ArchivosClasificados
    {
        try
        {//se crea la ruta de destino
            string destino = Path.Combine(directorio, "ArchivosClasificados", "inventos.txt"); 
            Directory.CreateDirectory(Path.Combine(directorio, "ArchivosClasificados"));   //se crea la carpeta ArchivosClasificados
            File.Move(archivoInventos, destino, true); //se mueve el archivo
            Console.WriteLine("Archivo movido a ArchivosClasificados."); //leyenda de texto
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    //en este segmento se crean las funciones para crear una carpeta, listar los archivos y eliminar un archivo
    static void CrearCarpeta(string nombreCarpeta) // crea una carpeta en el directorio
    {// de aqui en adelante se manejan los errores que puedan surgir
        try
        {
            Directory.CreateDirectory(Path.Combine(directorio, nombreCarpeta));
            Console.WriteLine($"Carpeta '{nombreCarpeta}' creada.");
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    //en esta funcion se listan los archivos del directorio
    static void ListarArchivos() 
    {
        try
        {
            string[] archivos = Directory.GetFiles(directorio);
            Console.WriteLine("Archivos en el directorio:");
            foreach (string archivo in archivos)
            {
                Console.WriteLine(Path.GetFileName(archivo));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    // esta seccion se encarga de eliminar un archivo
    static void EliminarArchivo() //elimina un archivo
    {
        try
        {
            if (File.Exists(archivoInventos))
            {
                File.Delete(archivoInventos);
                Console.WriteLine("Archivo eliminado.");
            }
            else
            {
                Console.WriteLine("El archivo no existe.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

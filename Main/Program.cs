using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EJ_1
{
    class Program
    {
        public static bool Inicio(Agenda agenda)
        {
            //Da inicio al programa y pregunta al usuario si desea partir de un fichero ya existente.
            string opcion;

            Console.WriteLine("Desea leer los contactos de un fichero ya existente? (La extensión del fichero debe ser \".csv\")");
            Console.WriteLine("1. Si");
            Console.WriteLine("2. No");
            
            opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1":
                    string ruta;
                    FileInfo ficherito;

                    Console.WriteLine("Introduzca la ruta del fichero (incluyendo el nombre y la extensión del mismo)");
                    ruta = Console.ReadLine();
                    ficherito = new FileInfo(ruta);

                    while (!ComprobarFichero(ficherito, ruta))
                    {
                        Console.WriteLine("Introduzca la ruta del fichero (incluyendo el nombre y la extensión del mismo)");
                        ruta = Console.ReadLine();
                        ficherito = new FileInfo(ruta);
                        
                        Console.WriteLine("Pruebe de nuevo");
                    }

                    agenda.CargarAgenda(ficherito);
                    
                    return true;
                case "2":
                    return true;
                default:
                    return false;
            }
        }

        public static bool ComprobarFichero(FileInfo ficherito, string ruta)
        {
            //Comprueba que el fichero exista y que tenga la extensión adecuada.
            if (!ficherito.Exists)
            {
                Console.WriteLine("El fichero introducido no existe");
                return false;
            } else
            {
                if (Path.GetExtension(ruta) != ".csv")
                {
                    Console.WriteLine("El fichero introducido no tiene la extensión \".csv\"");
                    return false;
                } else
                {
                    return true;
                }
            }
        }

        public static void Menu(Agenda agenda)
        {
            //Despliega el menú principal del programa.
            string opcion;
            string nombre, apellidos, telefono;
            string ruta;
            Contacto contacto;

            do
            {
                Console.WriteLine("1. Añadir un contacto");
                Console.WriteLine("2. Modificar el teléfono de un contacto");
                Console.WriteLine("3. Imprimir la agenda de contactos");
                Console.WriteLine("4. Guardar agenda y salir");
                Console.WriteLine("5. Salir del programa sin guardar");

                opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        //Recoge los datos de un contacto, comprueba que no exista previamente, y, si hay espacio en la agenda, lo añade.

                        Console.WriteLine("Introduzca los datos del contacto que desea añadir");
                        Console.WriteLine("Nombre:");
                        nombre = Console.ReadLine();
                        Console.WriteLine("Apellidos:");
                        apellidos = Console.ReadLine();
                        Console.WriteLine("Teléfono:");
                        telefono = Console.ReadLine();

                        contacto = new Contacto(nombre, apellidos, telefono);

                        if (agenda.ContactoRepe(contacto))
                        {
                            Console.WriteLine("El contacto introducido ya existe, no se ha añadido nada");
                        }
                        else
                        {
                            if (agenda.AddContacto(contacto))
                            {
                                Console.WriteLine("Se ha añadido un contacto");
                            } else
                            {
                                Console.WriteLine("La agenda está llena, no pueden añadirse más contactos");
                            }
                        }
                         
                        break;
                    case "2":
                        //Si la agenda no está vacía, se recogen los datos de un contacto ya existente y se cambia su numero de telefono previo por otro nuevo.

                        if (agenda.AgendaVacia())
                        {
                            Console.WriteLine("La agenda está vacía");
                        } else
                        {
                            Console.WriteLine("Introduzca los datos del contacto que desea modificar");
                            Console.WriteLine("Nombre:");
                            nombre = Console.ReadLine();
                            Console.WriteLine("Apellidos:");
                            apellidos = Console.ReadLine();

                            Console.WriteLine("Introduzca el nuevo numero de telefono");
                            telefono = Console.ReadLine();

                            contacto = new Contacto(nombre, apellidos, telefono);

                            if (agenda.ModificarContacto(contacto))
                            {
                                Console.WriteLine("El contacto se ha modificado correctamente");
                            } else
                            {
                                Console.WriteLine("El contacto introducido no existe, no ha habido modificaciones");
                            }
                        }

                        break;
                    case "3":
                        //Imprime la agenda. En caso de estar vacía se le informa al usuario.
                        if (agenda.AgendaVacia())
                        {
                            Console.WriteLine("La agenda está vacía");
                        }
                        else
                        {
                            Console.WriteLine(agenda);
                        }

                        break;
                    case "4":
                        //Crea un archivo con la información de la agenda y cierra el programa
                        Console.WriteLine("Introduzca la ruta completa del fichero que desea crear (incluyendo el nombre del fichero y la extensión \".csv\")");
                        ruta = Console.ReadLine();
                        agenda.GuardarAgenda(ruta);

                        opcion = "5";
                        break;
                    case "5":
                        Console.WriteLine("Cerrando el programa...");
                        break;
                    default:
                        Console.WriteLine("El valor introducido es incorrecto, pruebe de nuevo");
                        break;
                }
            } while (opcion != "5");



        }

        static void Main(string[] args)
        {
            /*
                Funcionamiento del programa: 
                1. Lee un fichero de contactos.
                2. Carga los contactos en un objeto Agenda.
                3. El usuario puede añadir un contacto, modificarlo o imprimir los contactos existentes.
                4. Al salir del programa se le da al usuario la opción de guardar la agenda en un nuevo fichero.
             */

            Agenda agenda;
            agenda = new Agenda();

            while (!Inicio(agenda))
            {
                Console.WriteLine("El valor introducido es incorrecto, pruebe de nuevo con \"1\" o \"2\"");
            }

            Menu(agenda);

        }
    }
}

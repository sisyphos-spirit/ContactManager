using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EJ_1
{
    class Agenda
    {
        const int TAM = 20;
        private Contacto[] contactos;

        public Agenda()
        {
            this.contactos = new Contacto[TAM];
        }

        public void CargarAgenda(FileInfo ficherito)
        {
            //Lee un fichero, comprueba que exista, y añade los contactos que tenga a una agenda.
            FileStream flujo;
            StreamReader leer;

            flujo = ficherito.OpenRead();
            leer = new StreamReader(flujo, Encoding.UTF8, true);

            Contacto contacto;

            for (int i = 0; i < 20; i++)
            {
                while (!leer.EndOfStream)
                {
                    string linea = leer.ReadLine().Trim();
                    string[] atributos = linea.Split(',');
                    if (atributos.Length == 3)
                    {
                        contacto = new Contacto(atributos[0], atributos[1], atributos[2]);
                        AddContacto(contacto);
                    }
                    
                }
            }

            leer.Dispose();
            flujo.Dispose();
        }

        public void GuardarAgenda(string ruta)
        {
            FileStream flujo = new FileStream(ruta, FileMode.Create);
            StreamWriter escribir = new StreamWriter(flujo, Encoding.UTF8);

            foreach (Contacto item in contactos)
            {
                escribir.WriteLine(item.GetContactoCSV());
            }

            escribir.Flush();
            escribir.Dispose();
            flujo.Dispose();
        }

        public bool AddContacto(Contacto contacto)
        {
            //Compruebo que haya espacio para añadir un contacto nuevo, en caso de no haberlo no se añade.
            for (int i = 0; i < TAM; i++)
            {
                if (contactos[i] == null)
                {
                    contactos[i] = contacto;
                    return true;
                }
            }

            return false;
        }

        public bool DeleteContacto(Contacto contacto)
        {
            //Recorro el array y compruebo si el contacto introducido coincide con alguno de los previamente guardados. En tal caso lo igualo a null.
            for (int i = 0; i < TAM; i++)
            {
                if (contacto.Equals(contactos[i]))
                {
                    contactos[i] = null;
                    return true;
                }
            }

            return false;
        }

        public bool ContactoRepe(Contacto contacto)
        {
            for (int i = 0; i < TAM; i++)
            {
                if (contacto.Equals(contactos[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public bool ModificarContacto(Contacto contacto)
        {
            for (int i = 0; i < TAM; i++)
            {
                if (contacto.Equals(contactos[i]))
                {
                    contactos[i] = contacto;
                    return true;
                }
            }

            return false;
        }

        public bool AgendaVacia()
        {
            for (int i = 0; i < TAM; i++)
            {
                if (contactos[i] != null)
                {
                    return false;
                }
            }

            return true;
        }
        public override string ToString()
        {
            //Primero declaro el StringBuilder.
            StringBuilder info = new StringBuilder("----- AGENDA DE CONTACTOS -----");
            foreach (Contacto item in contactos)
            {
                //Para añadir los contactos me aseguro de no tener en cuenta las posiciones nulas del array.
                if (item != null)
                {
                    info.Append("\n");
                    info.Append(item.ToString());
                }
            }

            return info.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EJ_1
{
    class Contacto
    {
        private string nombre;
        private string apellidos;
        private string telefono;

        public Contacto(string nombre, string apellidos, string telefono)
        {
            this.nombre = nombre;
            this.apellidos = apellidos;
            //Antes de asignar el telefono, compruebo que este sea un número válido.
            if (CompruebaTelefono(telefono))
            {
                this.telefono = telefono;
            }
            else
            {
                //Si el número no es valido asigno este por defecto.
                this.telefono = "123456789";
            }
        }

        private bool CompruebaTelefono(string telefono)
        {
            //Primero compruebo que el número de carcacteres del teléfono sea correcto.
            if (telefono.Length == 9)
            {
                //Despues me aseguro de que todos esos caracteres sean digitos.
                for (int i = 0; i < telefono.Length; i++)
                {
                    if (!(char.IsDigit(telefono[i])))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetContactoCSV()
        {
            return nombre + ", " + apellidos + ", " + telefono;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Contacto prueba = (Contacto)obj;
                return nombre == prueba.nombre && apellidos == prueba.apellidos;
            }
        }

        public override string ToString()
        {
            return "Nombre: " + nombre + " Apellidos: " + apellidos + " Teléfono: " + telefono;
        }
    }
}

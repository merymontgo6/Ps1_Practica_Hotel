using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace metodos_hotel
{
    internal class Aplicacion
    {
        //0 NIF, 1 nombre y 2 email
        const int NO_cliente = -1;
        const int NIF_cliente = 0;
        const int NOM_cliente = 1;
        const int EMAIL_cliente = 2;

        public void Inicio()
        {
            String[,] clientes = new String[15, 3];

            bool salir = false;
            string opcion;
            do
            {
                mostrarMenu();
                opcion = pedirOpcionMenu();
                salir = ejecutarOpcionesMenu(clientes, opcion);
            } while (!salir);
        }

        void mostrarMenu()
        {
            Console.WriteLine("Menú de opciones: ");
            Console.WriteLine("1. Mostrar clientes.");
            Console.WriteLine("2. Alta clientes.");
            Console.WriteLine("O. Salir.");
        }

        string pedirOpcionMenu()
        {
            string opcion;
            do
            {
                Console.Write("Opcion: ");
                opcion = Console.ReadLine();
            } while (!"012".Contains(opcion));

            return opcion;
        }

        bool ejecutarOpcionesMenu(String[,] clientes, string opcion)
        {
            bool salir = false;
            switch (opcion)
            {
                case "1":
                    mostrarClientes(clientes);
                    break;
                case "2":
                    altaClientes(clientes);
                    break;
                case "0":
                    salir = true;
                    break;
            }
            return salir;
        }

        void mostrarClientes(String[,] clientes)
        {
            //0 NIF, 1 nombre y 2 email
            Console.WriteLine("\nNIF\tNombre\tEmail");
            for (int i = 0; i < clientes.GetLength(0); i++)
            {
                for (int j = 0; j < clientes.GetLength(1); j++)
                {
                    Console.Write(clientes[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        void altaClientes(String[,] clientes)
        {
            String nif;
            String nombre;
            String email;
            int encontrar;

            nif = pedirnif();
            int fila;
            // Comprobar si el nif existe en el array usuarios
            fila = findNIFcliente(clientes, nif);
            if (fila == NO_cliente)
            {
                Console.WriteLine("El nif no existe.");
                //si no, pedir los datos del nombre y email y los añadirá en el array clientes
                Console.WriteLine("Ingrese el nombre: ");
                nombre = Console.ReadLine();
                Console.WriteLine("Ingrese el email: ");
                email = Console.ReadLine();

                encontrar = encontrar_fila(clientes);
                if (encontrar == NO_cliente)
                {
                    //en el caso de que no haya ninguna posición libre se mostrará un mensaje indicando este hecho
                    Console.WriteLine("No hay posiciones libres.");
                }
                else
                {
                    clientes[encontrar, NIF_cliente] = nif;
                    clientes[encontrar, NOM_cliente] = nombre;
                    clientes[encontrar, EMAIL_cliente] = email;
                }
            }
            else
            {
                //mostrar un mensaje indicando que el cliente ya existe
                Console.WriteLine("El cliente ya existe.");
                Console.WriteLine(clientes[fila, NIF_cliente]);
            }
        }

        string pedirnif()
        {
            // Pedir NIF
            string niff;
            Console.Write("NIF: ");
            niff = Console.ReadLine();
            return niff;
        }

        int findNIFcliente(String[,] clientes, string nif)
        {
            // comprobar si el nif ya existe en el array "clientes"
            bool encontrado = false;
            int fila = 0;
            while (fila < clientes.GetLength(0) & !encontrado)
            {
                if (nif.Equals(clientes[fila, NIF_cliente]))
                {
                    encontrado = true;
                }
                else
                {
                    fila++;
                }
            }
            if (encontrado)
            {
                return fila;
            }
            else
            {
                return NO_cliente;
            }
        }

        int encontrar_fila(String[,] clientes)
        {
            //comprobar si el array tienes posiciones libres para añadir los nuevos datos y añadirlos
            bool encontrado = false;
            int fila = 0;

            while (fila < clientes.GetLength(0) && !encontrado)
            {
                if (clientes[fila, NIF_cliente] == null)
                {
                    encontrado = true;
                }
                else
                {
                    fila++;
                }
            }
            if (encontrado)
            {
                return fila;
            }
            else
            {
                return NO_cliente;
            }
        }
    }
}
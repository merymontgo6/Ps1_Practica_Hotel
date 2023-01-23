using System;

namespace metodo_hotel_2
{
    internal class Aplicacion
    {
        //0 NIF, 1 nombre y 2 email
        const int NO_cliente = -1;
        const int NIF_cliente = 0;
        const int NOM_cliente = 1;
        const int EMAIL_cliente = 2;

        const int NUM_habitacion = 0;
        const int PISO_habitacion = 1;
        const int CAMAS_habitacion = 2;
        const int PRECIO_habitacion = 3;
        const int RESERVA_habitacion = 4;
        public void Inicio()
        {
            String[,] clientes = datosClientes();
            String[,] hotel = datosHotel();

            bool salir = false;
            string opcion;
            do
            {
                mostrarMenu();
                opcion = pedirOpcionMenu();
                salir = ejecutarOpcionesMenu(clientes, hotel, opcion);
            } while (!salir);
        }
        String[,] datosClientes()
        {
            String[,] clientes = new String[15, 3];
            return clientes;
        }
        String[,] datosHotel()
        {
            String[,] hotel = {{"101", "1", "2", "100", "11111111A"},
                {"102", "1", "2", "100", "11111111A"},
                {"103", "1", "2", "100", "44444444D"},
                {"104", "1", "3", "130", "22222222B"},
                {"105", "1", "3", "130", "22222222B"},
                {"106", "1", "1", "75", "33333333C"},
                {"201", "2", "2", "100", "44444444D"},
                {"202", "2", "2", "100", "44444444D"},
                {"203", "2", "2", "100", "55555555E"},
                {"204", "2", "3", "130", null},
                {"205", "2", "3", "130", null},
                {"206", "2", "1", "75", null},
                {"301", "3", "2", "100", "55555555E"},
                {"302", "3", "2", "100", null},
                {"303", "3", "2", "100", null},
                {"304", "3", "3", "130", null},
                {"305", "3", "3", "130", null},
                {"306", "3", "1", "75", null}};
            return hotel;
        }

        void mostrarMenu()
        {
            Console.WriteLine("Menú de opciones: ");
            Console.WriteLine("1. Mostrar clientes.");
            Console.WriteLine("2. Alta clientes.");
            Console.WriteLine("3. Mostrar habitaciones.");
            Console.WriteLine("4. Mostrar habitaciones libres.");
            Console.WriteLine("5. Mostrar habitaciones ocupadas con el nombre.");
            Console.WriteLine("O. Salir.");
        }

        string pedirOpcionMenu()
        {
            string opcion;
            do
            {
                Console.Write("Opcion: ");
                opcion = Console.ReadLine();
            } while (!"012345".Contains(opcion));

            return opcion;
        }

        bool ejecutarOpcionesMenu(String[,] clientes, String[,] hotel, string opcion)
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
                case "3":
                    mostrarHabitaciones(hotel);
                    break;
                case "4":
                    habitacionesLibres(hotel);
                    break;
                case "5":
                    habitacionesOcupadasNombre(hotel);
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
        void mostrarHabitaciones(String[,] hotel)
        {
            Console.WriteLine("\nHab\tPiso\tCamas\tPrecio\tLibre/NIF");
            for (int i = 0; i < hotel.GetLength(0); i++)
            {
                for (int j = 0; j < hotel.GetLength(1) - 1; j++)
                {
                    Console.Write(hotel[i, j] + "\t");
                }

                if (hotel[i, RESERVA_habitacion] == null)
                {
                    Console.WriteLine("libre");
                }
                else
                {
                    Console.WriteLine(hotel[i, RESERVA_habitacion]);
                }

            }
        }
        void habitacionesLibres(String[,] hotel)
        {
            for (int i = 0; i < hotel.GetLength(0); i++)
            {
                for (int j = 0; j < hotel.GetLength(1); j++)
                {
                    if (hotel[i, RESERVA_habitacion] == null)
                    {
                    Console.WriteLine(hotel[i, RESERVA_habitacion]);
                    }
                else
                    {
                    i++;
                    }
                }
                
            }
            //bool encontrado = false;
            //int fila = 0;
            //while (fila < hotel.GetLength(0) & !encontrado)
            //{
            //    if (hotel[fila, RESERVA_habitacion] == null)
            //    {
            //        encontrado = true;
            //    }
            //    else
            //    {
            //        fila++;
            //    }
            //}
            //if (encontrado)
            //{
            //    Console.WriteLine(hotel[fila, RESERVA_habitacion]);
            //}
        }
        void habitacionesOcupadasNombre(String[,] hotel)
        {

        }
    }
}

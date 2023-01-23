using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ps1_3_Práctica_Hotel
{
    internal class Aplicacion
    {
        //Ps1_1_Práctica Hotel
        //0 NIF, 1 nombre y 2 email
        const int NO_cliente = -1;
        const int NIF_cliente = 1;
        const int NOM_cliente = 0;
        const int EMAIL_cliente = 2;

        //Ps1_2_Práctica Hotel
        const int NUM_habitacion = 0;
        const int PISO_habitacion = 1;
        const int CAMAS_habitacion = 2;
        const int PRECIO_habitacion = 3;
        const int RESERVA_habitacion = 4;

        //Ps1_3_Práctica Hotel

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
            String[,] clientes = {{"Josep Busquets", "11111111A", ""},
                {"Maria Garcia", "22222222B", ""},
                {"Albert Gonzalez", "33333333C", ""},
                {"Hector Puig", "44444444D", ""},
                {"Isabel Casas", "55555555E", ""},
                {null, null, null},
                {null, null, null},
                {null, null, null},
                {null, null, null},
                {null, null, null},
                {null, null, null}};
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
            Console.WriteLine("6. Entrar reserva");
            Console.WriteLine("7.Lista reservas a partir de un Nif");
            Console.WriteLine("8.Factura de la reserva a partir de un Nif");
            Console.WriteLine("9.Anular reserva");
            Console.WriteLine("O. Salir.");
        }

        string pedirOpcionMenu()
        {
            string opcion;
            do
            {
                Console.Write("Opcion: ");
                opcion = Console.ReadLine();
            } while (!"0123456789".Contains(opcion));

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
                    habitacionesOcupadasNombre(hotel, clientes);
                    break;
                case "6":
                    entrar_reserva(clientes, hotel);
                    break;
                case "7":
                    listaReservaNif(hotel);
                    break;
                case "8":
                    facturaReservaNif(clientes, hotel);
                    break;
                case "9":
                    anularReserva(hotel, clientes);
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
            Console.WriteLine("\nNombre\tNIF\tEmail");
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
                Console.WriteLine(clientes[NOM_cliente, fila]);
            }
        }

        string pedirnif()
        {
            // Pedir NIF
            string niff;
            Console.Write("Introduce NIF: ");
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
            Console.WriteLine("\nHab\tPiso\tCamas");
            for (int i = 0; i < hotel.GetLength(0); i++)
            {
                if (hotel[i, 4] == null)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Console.Write(hotel[i, j] + " " + "\t");
                    }
                    Console.WriteLine();
                }
            }

        }

        void habitacionesOcupadasNombre(String[,] hotel, String[,] clientes)
        {
            string nombre;
            string nif;

            Console.WriteLine("\nHab\tPiso\tCamas\tPrecio\tNombre Cliente");
            for (int i = 0; i < hotel.GetLength(0); i++)
            {
                if (hotel[i, RESERVA_habitacion] != null)
                {
                    nif = hotel[i, RESERVA_habitacion];
                    nombre = nombreCliente(nif, clientes);
                    Console.WriteLine(hotel[i, NUM_habitacion] + "\t" + hotel[i, PISO_habitacion] + "\t" + hotel[i, CAMAS_habitacion]
                        + "\t" + hotel[0, PRECIO_habitacion] + "\t" + nombre);
                }
            }
        }

        string nombreCliente(string nif, String[,] clientes)
        {
            bool encontrado = false;
            int fila = 0;
            string nombre = "";

            while (fila < clientes.GetLength(0) & !encontrado)
            {
                if (clientes[fila, 1].Equals(nif))
                {
                    nombre = clientes[fila, 0];
                    encontrado = true;
                }
                else
                {
                    fila++;
                }
            }
            return nombre;
        }

        void entrar_reserva(String[,] clientes, String[,] hotel)
        {
            string tipo_hab;
            string nif;
            string nombre;
            int filaDispoHab;

            int fila;
            int filaNuevoCliente;

            Console.WriteLine("\n¿De qué tipo?\n1. Individual\t2. Doble\t3. Triple");
            tipo_hab = Console.ReadLine();

            filaDispoHab = disponibilidad_hab(hotel, tipo_hab);

            if (filaDispoHab == -1)
            {
                Console.WriteLine("No hay habitaciones disponibles");
            }
            else
            {
                nif = pedirnif();
                fila = findNIFcliente(clientes, nif);


                if (fila == NO_cliente)
                {
                    nombre = pedirnombre();
                    Console.WriteLine("¡Bienvenido, Sr/Sra. " + nombre + "!");

                    filaNuevoCliente = encontrar_fila(clientes);
                    if (filaNuevoCliente == NO_cliente)
                    {
                        Console.WriteLine("Hay un problema, no se puede registrar.");
                    }
                    else
                    {
                        clientes[filaNuevoCliente, NIF_cliente] = nif;
                        clientes[filaNuevoCliente, NOM_cliente] = nombre;
                    }


                }
                else
                {
                    Console.WriteLine("¡Bienvenido, Sr/Sra. " + clientes[fila, 0] + "!");
                }

            }
        }

        string pedirnombre()
        {
            // Pedir nombre
            string nombre;
            Console.Write("Introduce nombre: ");
            nombre = Console.ReadLine();

            return nombre;
        }

        int disponibilidad_hab(String[,] hotel, string dispoHab)
        {
            bool encontrado = false;
            int fila = 0;

            while (fila < hotel.GetLength(0) && !encontrado)
            {
                if (hotel[fila, CAMAS_habitacion] == dispoHab & hotel[fila, RESERVA_habitacion] == null)
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
                return -1;
            }

        }

        void listaReservaNif(String[,] hotel)
        {
            string nif;
            int count = 0;

            nif = pedirnif();

            Console.WriteLine("\nHab\tPiso\tCamas\tPrecio");
            for (int i = 0; i < hotel.GetLength(0); i++)
            {
                if (hotel[i, RESERVA_habitacion] == nif)
                {
                    Console.WriteLine(hotel[i, NUM_habitacion] + "\t" + hotel[i, PISO_habitacion] + "\t" + hotel[i, CAMAS_habitacion]
                        + "\t" + hotel[i, PRECIO_habitacion]);
                    count++;
                }
            }
            if (count == 0)
            {
                Console.WriteLine("No hay reservas con este NIF.");
            }
        }

        void facturaReservaNif(String[,] clientes, String[,] hotel)
        {
            string nif;
            string nombre;
            int fila;
            int suma = 0;

            nif = pedirnif();
            nombre = nombreCliente(nif, clientes);
            Console.WriteLine("Factura del cliente con NIF: " + nif + "\t" + nombre);
            fila = findNIFcliente(clientes, nif);

            if (fila == NO_cliente)
            {
                Console.WriteLine("El cliente no existe o no ha realizado una reserva.");
            }
            else
            {
                for (int i = 0; i < clientes.GetLength(0); i++)
                {
                    if (nif.Equals(hotel[i,RESERVA_habitacion]))
                    {
                        nif = hotel[i, RESERVA_habitacion];

                        Console.WriteLine(hotel[i, NUM_habitacion] + "\t" + hotel[i, PISO_habitacion] + "\t" + hotel[i, CAMAS_habitacion]
                            + "\t" + hotel[i, PRECIO_habitacion]);
                        suma += int.Parse(hotel[i, PRECIO_habitacion]);
                    }
                }
                Console.WriteLine("El importe de la factura de la reserva es: " + suma);
            }
        }

        void anularReserva(String[,] hotel, String[,] clientes)
        {
            string nif;
            string nombre;
            
            nif = pedirnif();
            nombre = nombreCliente(nif, clientes);
            Console.WriteLine("Anulacion de las reservas del cliete: " + nif + "\t" + nombre);

            

            for (int i = 0; i < hotel.GetLength(0); i++)
            {
                if (hotel[i, RESERVA_habitacion] == nif)
                {
                    hotel[i, RESERVA_habitacion] = null;
                }
            }

            mostrarHabitaciones(hotel);
            Console.WriteLine("Las reservas se han anulado correctamente.");

        }
    }
}
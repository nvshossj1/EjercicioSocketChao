using ServidorSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioSocketServidor
{
    class Program
    {
        static void GenerarComunicacion(ClienteCom clienteCom)
        {
            bool terminar = false;
            while (!terminar)
            {
                string mensaje = clienteCom.Leer();
                if(mensaje != null)
                {
                    Console.WriteLine("Cliente dice: {0}", mensaje);
                    if(mensaje.ToLower() == "chao")
                    {
                        terminar = true;
                    }
                    else
                    {
                        Console.WriteLine("Cliente Conectado: ");
                        mensaje = Console.ReadLine().Trim();
                        clienteCom.Escribir(mensaje);
                        if(mensaje.ToLower() == "chao")
                        {
                            terminar = true;
                        }
                    }
                }
                else
                {
                    terminar=false;
                }
            }
            clienteCom.Desconectar();
        }
        static void Main(string[] args)
        { 
           int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            Console.WriteLine("Levantando el servidor en el puerto {0}", puerto);
            ServerSocket serverSocket = new ServerSocket(puerto);

            if (serverSocket.Iniciar())
            {
                while (true)
                {
                    Console.WriteLine("Esperando Cliente: ");
                    Socket socket = serverSocket.ObtenerCliente();
                    Console.WriteLine("Cliente conectado");

                    ClienteCom clienteCom = new ClienteCom(socket);
                    GenerarComunicacion(clienteCom);
                }
            }
            else
            {
                Console.WriteLine("Error al conectar con el puerto {0}", puerto);
            }

        }
    }
}

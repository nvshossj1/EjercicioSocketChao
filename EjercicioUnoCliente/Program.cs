using ClienteSocketUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioUnoCliente
{
    class Program
    {
        static void GenerarComunicacion(ClienteSocket clienteSocket)
        {
            Console.Clear();
            bool terminar = false;
            while (!terminar)
            {
                Console.WriteLine("Ingrese lo que desee: ");
                string mensaje = Console.ReadLine().Trim();
                clienteSocket.Escribir(mensaje);
                if (mensaje.ToLower() == "chao")
                {
                    terminar = true;
                }
                else
                {
                    mensaje = clienteSocket.Leer();
                    if (mensaje != null)
                    {
                        Console.WriteLine("Servidor dice: {0}", mensaje);
                        if (mensaje.ToLower() == "chao")
                        {
                            terminar = true;
                        }
                    }
                    else
                    {
                        terminar = true;
                    }
                }
            }
            clienteSocket.Desconectar();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese Servidor: ");
            string servidor = Console.ReadLine().Trim();
            int puerto;
            do
            {
                Console.WriteLine("Ingrese Puerto: ");
            }while (!Int32.TryParse(Console.ReadLine().Trim(), out puerto));

            Console.WriteLine("Estableciendo Conexion.....");
            ClienteSocket clienteSocket = new ClienteSocket(servidor, puerto);

            if (clienteSocket.Conectar())
            {
                GenerarComunicacion(clienteSocket);
            }
            else
            {
                Console.WriteLine("Error de comunicacion");
            }
            Console.ReadKey();
        }
    }
}

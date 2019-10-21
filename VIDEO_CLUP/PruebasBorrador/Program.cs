using System;

namespace PruebasBorrador
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            
            Console.Clear();
            //bool exit = false;
            //do
            //{
            //    Console.WriteLine("1-INICIAR SESION");
            //    Console.WriteLine("2-REGISTRARSE");
            //    Console.WriteLine("3-SALIR");
            //    string menu1 = Console.ReadLine().ToLower();
            //    if (menu1.Contains("ini") || menu1.Contains("1"))
            //    {
            //        string iniciar = Iniciar();
            //        if (iniciar == null)
            //        {
            //            Console.Clear();
            //        }
            //        else
            //        {
            //            Console.Clear();
            //            MenuUsuario(CrearObjetoUsuario(iniciar));//el iniciar es el usuario luego crea el objeto y va al menu que solo tendra el usuario
            //        }

            //    }
            //    else if (menu1.Contains("regi") || menu1.Contains("2"))
            //    {
            //        if (Registrar())
            //        {
            //            bool salirRegistro = false;
            //            do
            //            {
            //                salirRegistro = false;
            //                Console.WriteLine($"¿1-Sea iniciar o 2-salir?");
            //                string input = Console.ReadLine().ToLower();
            //                if (input.Contains("inic") || input.Contains("1"))
            //                {
            //                    MenuUsuario(CrearObjetoUsuario(Iniciar()));
            //                    salirRegistro = true;
            //                }
            //                else if (input.Contains("sal") || input.Contains("2"))
            //                {
            //                    salirRegistro = true;
            //                }
            //                else
            //                {
            //                    Console.WriteLine("No te entindo");
            //                }
            //            } while (!salirRegistro);
            //        }
            //        else
            //        {
            //            Console.Clear();

            //        }

            //    }
            //    else if (menu1.Contains("sal") || menu1.Contains("3"))
            //    {
            //        exit = true;
            //    }
            //    else
            //    {
            //        Console.WriteLine("No te entiendo");
            //    }

            //******************************************************************************************
            //} while (!exit);

            //Console.WriteLine("Fecha de nacimiento");
            //string fecha = Console.ReadLine();
            //DateTime fechaNacimiento;
            //if (DateTime.TryParse(fecha, out fechaNacimiento))
            //{
            //    Console.WriteLine($"tu fecha de nacimiento es {fechaNacimiento.Year}");
            //}

            //int edad = DateTime.Today.Year - fechaNacimiento.Year;
            //if (DateTime.Today.Month > fechaNacimiento.Month)
            //{
            //    Console.WriteLine(edad);

            //}
            //else if (DateTime.Today.Month == fechaNacimiento.Month)
            //{
            //    if (DateTime.Today.Day >= fechaNacimiento.Day)
            //    {
            //        Console.WriteLine(edad);

            //    }
            //    else
            //    {
            //        Console.WriteLine(edad - 1);

            //    }
            //}
            //else
            //{
            //    Console.WriteLine(edad - 1);
            //}

        }
    }
}

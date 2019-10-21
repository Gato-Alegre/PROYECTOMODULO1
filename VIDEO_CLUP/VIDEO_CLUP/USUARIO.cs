using System;
using System.Collections.Generic;
using System.Text;

namespace VIDEO_CLUP
{
    class USUARIO
    {
        public int UsuarioID { get; set; }
        public string NombreID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Nacimiento { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public int Edad { get; set; }
        

        public USUARIO()
        {
        }

        public USUARIO(string nombreID, string nombre, string apellido, DateTime nacimiento, string email, string contrasena, int edad)// este es para crear el usuario y añadirlo a la base de datos
        {
            NombreID = nombreID;
            Nombre = nombre;
            Apellido = apellido;
            Nacimiento = nacimiento;
            Email = email;
            Contrasena = contrasena;
            Edad = edad;

        }

        public USUARIO(int usuarioId,string nombreID, string nombre, string apellido, DateTime nacimiento, string email, string contraseña, int edad)//este es para crear obejto de usuario 
        {
            UsuarioID = usuarioId;
            NombreID = nombreID;
            Nombre = nombre;
            Apellido = apellido;
            Nacimiento = nacimiento;
            Email = email;
            Contrasena = contraseña;
            Edad = edad;
        }

        public void MostrarDatosUsuario()
        {

            Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine($"UsuarioID: "); Console.ForegroundColor = ConsoleColor.White;Console.WriteLine($"{NombreID}");
            Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine($"Nombre: "); Console.ForegroundColor = ConsoleColor.White; Console.WriteLine($"{Nombre}");
            Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine($"Apellido: "); Console.ForegroundColor = ConsoleColor.White; Console.WriteLine($"{Apellido}");
            Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine($"Nacimiento: "); Console.ForegroundColor = ConsoleColor.White; Console.WriteLine($"{Nacimiento}");
            Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine($"Email: "); Console.ForegroundColor = ConsoleColor.White; Console.WriteLine($"{Email}");
            Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine($"Contraseña: "); Console.ForegroundColor = ConsoleColor.White; Console.WriteLine($"{Contrasena}");
            Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine($"Años: "); Console.ForegroundColor = ConsoleColor.White; Console.WriteLine($"{Edad}");
        }
    }
}

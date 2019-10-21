using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace VIDEO_CLUP
{
    class Program
    {
        static SqlConnection connection = new SqlConnection("Data Source=GATO_ALEGRE\\SQLEXPRESS;Initial Catalog=VIDEOCLUB;Integrated Security=True");

        static void Main(string[] args)
        {


            bool exit = false;
            do
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" xx    xx    xx  xx  ");
                Console.WriteLine("  xx  xx       xx    ");
                Console.WriteLine("    xx    o  xx  xx  ");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("1-INICIAR SESION");
                Console.WriteLine("2-REGISTRARSE");
                Console.WriteLine("3-SALIR");
                Console.ForegroundColor = ConsoleColor.White;
                string menu1 = Console.ReadLine().ToLower();
                if (menu1.Contains("ini") || menu1.Contains("1"))
                {
                    string iniciar = Iniciar();
                    if (iniciar == null)
                    {
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        MenuUsuario(CrearObjetoUsuario(iniciar));//"Iniciar" es el usuario luego crea el objeto y va al menu que solo tendra el usuario
                    }

                }
                else if (menu1.Contains("regi") || menu1.Contains("2"))
                {
                    if (Registrar())
                    {
                        bool salirRegistro = false;
                        do
                        {
                            salirRegistro = false;
                            Console.WriteLine($"¿1-Desea iniciar o 2-salir?");
                            string input = Console.ReadLine().ToLower();
                            if (input.Contains("inic") || input.Contains("1"))
                            {
                                Console.Clear();
                                string usuario = Iniciar();
                                if (usuario != null)
                                {
                                    Console.Clear();
                                    MenuUsuario(CrearObjetoUsuario(usuario));
                                }
                                salirRegistro = true;
                            }
                            else if (input.Contains("sal") || input.Contains("2"))
                            {
                                salirRegistro = true;
                            }
                            else
                            {
                                Console.WriteLine("No te entindo");
                            }
                        } while (!salirRegistro);
                    }
                    else
                    {
                        Console.Clear();

                    }

                }
                else if (menu1.Contains("sal") || menu1.Contains("3"))
                {
                    exit = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n*******NO TE ENTIENDO*******");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadKey();
                    Console.Clear();
                }


            } while (!exit);

        }

        /********************************************************************************************************************************/
        public static List<PELICULAS> ListaPeli(USUARIO x)//este metodo lo qu enos hace es creanos todos los objetos de las pelis y meterlo en la lista como s fura la tabla pelis en sql
        {
            string query = $"SELECT * FROM PELICULAS WHERE EdadRECOMENDADA <= {x.Edad}";
            List<PELICULAS> listaPeli = new List<PELICULAS>();

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int peliculasID = Convert.ToInt32(reader[0]);
                string titulo = reader[1].ToString();
                string sinopsis = reader[2].ToString();
                int edad = Convert.ToInt32(reader[3]);
                byte disponible = Convert.ToByte(reader[4]);
                string genero = reader[5].ToString();

                listaPeli.Add(new PELICULAS(peliculasID, titulo, sinopsis, edad, disponible, genero));

            }
            connection.Close();

            return listaPeli;
        }
        public static void MostrarPelis(List<PELICULAS> lista)//este me muestra la lista de todas las peliculas y si quiero mostrar con flitros tengo que meter las de listaPeliFiltro
        {

            Console.WriteLine("*******************************************************PELICULAS*******************************************************\n");
            foreach (var item in lista)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Id: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{item.PeliculasID} ---- ");

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("TITU: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{item.Titulo} ---- ");

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("EDAD: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{item.Edad} ---- ");

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("GENERO: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{item.Genero} ---- ");

                if (item.Disponible.Contains("DISPONIBLE"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{item.Disponible}\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (item.Disponible.Contains("ALQUILADO"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{item.Disponible}\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
        public static List<PELICULAS> ListaPeliFiltroOrden(string filtro, USUARIO x)//para llegar hasts aqui antes en el main he tenido que ir validadno que lo que mete el usuari esta bien
        {

            if (filtro.Contains("edad"))
            {
                filtro = "EdadRECOMENDADA";
            }
            else if (filtro.Contains("tit"))
            {
                filtro = "TITULO";
            }
            else if (filtro.Contains("gen"))
            {
                filtro = "GENERO";

            }

            string query = $" SELECT* FROM PELICULAS WHERE EdadRECOMENDADA <= {x.Edad} ORDER BY {filtro} ASC";

            List<PELICULAS> listaPeliFiltro = new List<PELICULAS>();

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int peliculasID = Convert.ToInt32(reader[0]);
                string titulo = reader[1].ToString();
                string sinopsis = reader[2].ToString();
                int edad = Convert.ToInt32(reader[3]);
                byte disponible = Convert.ToByte(reader[4]);
                string genero = reader[5].ToString();

                listaPeliFiltro.Add(new PELICULAS(peliculasID, titulo, sinopsis, edad, disponible, genero));

            }
            connection.Close();

            return listaPeliFiltro;
        }
        public static PELICULAS CreatObjetoPeli(int peliculaID)
        {
            string query = $"SELECT * FROM PELICULAS WHERE PeliculaID = {peliculaID}";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            PELICULAS obejrtoPelicula = new PELICULAS();
            if (reader.Read())
            {
                int peliculasID = Convert.ToInt32(reader[0]);
                string titulo = reader[1].ToString();
                string sinopsis = reader[2].ToString();
                int edad = Convert.ToInt32(reader[3]);
                byte disponible = Convert.ToByte(reader[4]);
                string genero = reader[5].ToString();

                obejrtoPelicula = new PELICULAS(peliculasID, titulo, sinopsis, edad, disponible, genero);
            }
            connection.Close();
            return obejrtoPelicula;


        }
        public static bool Registrar()
        {
            bool salir = false;
            do
            {
                string usuario;

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Introduzca nombre de usuario con el que va a iniciar sesion");
                Console.ForegroundColor = ConsoleColor.White;
                usuario = Console.ReadLine();
                if (!ComprobarUsuarioIDEnBBDD(usuario))
                {
                    salir = false;

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Nombre");
                    Console.ForegroundColor = ConsoleColor.White;
                    string nombre = Console.ReadLine();
                    if (nombre.Contains("sal"))
                    {
                        salir = true;
                    }
                    else
                    {

                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Apellido");
                        Console.ForegroundColor = ConsoleColor.White;
                        string apellido = Console.ReadLine();
                        if (apellido.Contains("sal"))
                        {
                            salir = true;
                        }
                        else
                        {
                            DateTime fechaNacimiento;
                            do
                            {
                                salir = false;
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("Fecha de nacimiento (dd/MM/yyyy)");
                                Console.ForegroundColor = ConsoleColor.White;
                                string input = Console.ReadLine();
                                if (DateTime.TryParse(input, out fechaNacimiento))
                                {
                                    salir = true;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\n*************La fecha se ha introducido mal*************\n");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                            } while (!salir);
                            int edad;
                            edad = CalcularEdad(fechaNacimiento);

                            string email;
                            do
                            {
                                salir = false;
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("Email");
                                Console.ForegroundColor = ConsoleColor.White;
                                email = Console.ReadLine().ToLower();
                                if (email.Contains("@gmail.com") || email.Contains("@hotmail.com"))
                                {
                                    string contrasena;
                                    do
                                    {
                                        salir = false;
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.WriteLine("Contraseña");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        contrasena = Console.ReadLine();

                                        if (contrasena.Length > 0)
                                        {
                                            if (ValidarContraseña(contrasena))
                                            {
                                                USUARIO @new = new USUARIO(usuario, nombre, apellido, fechaNacimiento, email, contrasena, edad);
                                                Console.Clear();
                                                bool salirComprobar = false;
                                                do
                                                {
                                                    salirComprobar = false;
                                                    @new.MostrarDatosUsuario();
                                                    Console.ForegroundColor = ConsoleColor.Blue;
                                                    Console.WriteLine("\nLos datos son correctos");
                                                    Console.ForegroundColor = ConsoleColor.White;
                                                    string confirmar = Console.ReadLine();
                                                    if (confirmar.Contains("si"))
                                                    {
                                                        MeterUsuarioBBDD(@new);
                                                        return true;
                                                    }
                                                    else if (confirmar.Contains("no"))
                                                    {
                                                        salir = true;
                                                        salirComprobar = true;
                                                    }
                                                    else
                                                    {

                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                        Console.WriteLine("No te entiendo");
                                                        Console.ForegroundColor = ConsoleColor.White;
                                                        Console.ReadKey();
                                                        Console.Clear();
                                                    }
                                                } while (!salirComprobar);
                                            }
                                        }
                                        if (contrasena.Contains("sal"))
                                        {
                                            salir = true;
                                        }


                                    } while (!salir);

                                }
                                else if (email.Contains("sal"))
                                {
                                    salir = true;
                                }
                                else
                                {
                                    Console.WriteLine("El EMAIL debe contener @gmail.com o @hotmail.com");
                                }

                            } while (!salir);

                        }
                    }
                }
                else
                {
                    Console.WriteLine("\nEste Usuario ya esta utilizado\n");
                    salir = false;
                }
            } while (!salir);

            return false;

        }
        public static void MeterUsuarioBBDD(USUARIO x)//Este metodo lo hago para no meter todo en el metodo registrar y asi dividir el metodo en dos
        {

            string query = $"INSERT INTO USUARIO (NOMBREID,NOMBRE,APELLIDO,FechaNACIMIENTO,EMAIL,CONTRASEÑA,ANIOS) VALUES ('{x.NombreID}','{x.Nombre}','{x.Apellido}','{x.Nacimiento}' ,'{x.Email}','{x.Contrasena}',{x.Edad})";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            if (command.ExecuteNonQuery() > 0) //para borrar clientes tengo que borrar primero los registrosd e la tabla reservas
            {                
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("¡¡FELICIDADES!!\n");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Su registro ha comcluido, le llegara un mensaje a su correo para confirmar");
                Console.ForegroundColor = ConsoleColor.White;
                connection.Close();
            }
            else
            {
                Console.WriteLine("Ha habido un error si continua llame a 112 es el fin del mundo");
                connection.Close();
            }
        }//Este metodo lo hago para no meter todo en el metodo registrar y asi dividir el metodo en dos
        public static bool ValidarContraseña(string contraseña)
        {
            bool largura = false;
            bool primeraEsLetra = false;
            bool mayus = false;
            bool digito = false;


            if (contraseña.Length >= 8)
            {
                largura = true;
            }
            else//si no tine 8 o mas caracteres
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n****La contraseña tiene que tener 8 o mas caracteres****");
                Console.ForegroundColor = ConsoleColor.White;
            }//si no tine 8 o mas caracteres
            char caracter1 = Convert.ToChar(contraseña.Substring(0, 1));
            if (!char.IsDigit(caracter1))
            {
                primeraEsLetra = true;
                if (char.IsUpper(caracter1))
                {
                    mayus = true;
                }
                else
                {
                    mayus = false;
                }

            }
            else
            {
                primeraEsLetra = false;
                //Console.WriteLine("La primera letra no pude ser numero");
            }
            for (int i = 1; i < contraseña.Length; i++)
            {
                char caracter = Convert.ToChar(contraseña.Substring(i, 1));
                if (char.IsDigit(caracter))
                {
                    digito = true;
                }
            }



            Console.ForegroundColor = ConsoleColor.Red;

            if (primeraEsLetra == false)
            {
                Console.WriteLine("\n*********El primer caracter tiene que ser letra*********");
            }
            if (mayus == false)
            {
                Console.WriteLine("\n********El primer caracter tine qye ser mayuscula*******");
            }
            if (digito == false)
            {
                Console.WriteLine("\n*******La contraseña tinee que tener algun digito*******\n");
            }
            Console.ForegroundColor = ConsoleColor.White;


            if (digito == true && mayus == true && largura == true && primeraEsLetra == true)//si la contraseña cumple todo es valida
            {
                return true;
            }//si la contraseña cumple todo es valida

            return false;
        }
        public static bool ComprobarUsuarioIDEnBBDD(string usuarioID)
        {
            string query = $"SELECT * FROM USUARIO WHERE NOMBREID ='{usuarioID}'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                connection.Close();
                return true;
            }
            connection.Close();
            return false;
        }
        public static string Iniciar()//devuleve la string del usuario tas haber hecho las comprobaciones de que el usurio es correcto
        {
            bool salir = false;
            bool continuarUsuario = false;
            bool continuarContra = false;
            string usuarioID;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Usuario ID: ");
                Console.ForegroundColor = ConsoleColor.White;
                usuarioID = Console.ReadLine();
                if (usuarioID.Contains("salir"))
                {
                    salir = true;
                }
                else if (ComprobarUsuarioIDEnBBDD(usuarioID))
                {
                    do
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("Contraseña: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        string contrasena = Console.ReadLine();
                        if (contrasena.Length > 0)
                        {

                            if (ComprobarContraseña(contrasena))
                            {
                                return usuarioID;
                            }
                            else if (contrasena.Contains("sal"))
                            {
                                salir = true;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nContraseña erroneo\n");
                                do
                                {
                                    continuarContra = false;
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("¿Has olvidado la Contraseña?");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("1-SI");
                                    Console.WriteLine("2-NO");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    string input = Console.ReadLine().ToLower();
                                    if (input.Contains("si") || input.Contains("1"))
                                    {
                                        string contra = "contra";
                                        string email;
                                        bool x = false;
                                        do
                                        {
                                            x = false;
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            Console.WriteLine("\nIntroduzca el Email para recuperrar La contraseña");
                                            Console.ForegroundColor = ConsoleColor.White;
                                            email = Console.ReadLine();
                                            if (email.Contains("sal"))
                                            {
                                                x = true;
                                                continuarUsuario = true;
                                                salir = true;
                                            }
                                            else if (ComprobarEmail(email))
                                            {
                                                x = true;
                                            }

                                        } while (!x);
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.Write($"La contraseña es: ");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.WriteLine($"{RecuperarInicio(contra, email)}");
                                        continuarContra = true;
                                    }
                                    else if (input.Contains("no") || input.Contains("2"))
                                    {
                                        continuarContra = true;
                                    }
                                    else if (input.Contains("sal"))
                                    {
                                        continuarContra = true;
                                        salir = true;
                                    }
                                    else
                                    {

                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("No te entiendo");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }

                                } while (!continuarContra);
                            }
                        }
                    } while (!salir);

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nUsuario erroneo\n");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    do
                    {
                        continuarUsuario = false;
                        Console.WriteLine("0-Registrarse");
                        Console.Write("\n-¿Has olvidad tu UsusarioID? responda ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("1-SI ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("o ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("2-NO");
                        Console.ForegroundColor = ConsoleColor.White;
                        string input = Console.ReadLine().ToLower();
                        if (input.Contains("regi") || input.Contains("0"))
                        {
                            Registrar();
                            continuarUsuario = true;
                        }
                        else if (input.Contains("si") || input.Contains("1"))
                        {
                            string usuario = "usuario";
                            string email;
                            bool x = false;
                            do
                            {
                                x = false;
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("Introduzca el Email para recuperrar UsuarioID");
                                Console.ForegroundColor = ConsoleColor.White;
                                email = Console.ReadLine();
                                if (email.Contains("sal"))
                                {
                                    x = true;
                                    continuarUsuario = true;
                                    salir = true;
                                }
                                else if (ComprobarEmail(email))
                                {
                                    x = true;
                                }

                            } while (!x);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write($"El nombre de UsuarioID es: ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($"{RecuperarInicio(usuario, email)}");
                            continuarUsuario = true;
                        }
                        else if (input.Contains("no") || input.Contains("2"))
                        {
                            continuarUsuario = true;
                        }
                        else if (input.Contains("sal"))
                        {
                            continuarUsuario = true;
                            salir = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nNo te entiendo\n");
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                    } while (!continuarUsuario);
                }

            } while (!salir);

            return null;

        }
        public static string RecuperarInicio(string x, string email)
        {
            if (x.Contains("usu"))
            {
                x = "NOMBREID";
            }
            else if (x.Contains("contr"))
            {
                x = "CONTRASEÑA";
            }
            string query = $"SELECT {x} FROM USUARIO WHERE EMAIL = '{email}' ";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                string usuCont = reader[0].ToString();
                connection.Close();
                return usuCont;
            }
            connection.Close();
            return "ERROR";

        }
        public static bool ComprobarEmail(string email)
        {
            string query = $"SELECT * FROM USUARIO WHERE EMAIL ='{email}'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                connection.Close();
                return true;
            }
            connection.Close();
            return false;
        }
        public static bool ComprobarContraseña(string contraseña)
        {
            char caracter1 = Convert.ToChar(contraseña.Substring(0, 1));
            if (char.IsUpper(caracter1))
            {
                string query = $"SELECT * FROM USUARIO WHERE CONTRASEÑA ='{contraseña}'";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    connection.Close();
                    return true;
                }
                connection.Close();
            }
            return false;
        }
        public static USUARIO CrearObjetoUsuario(string usuario)
        {
            //string usuario = Iniciar();

            string query = $"SELECT * FROM USUARIO WHERE NOMBREID = '{usuario}' ";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            USUARIO objetoUsuario = new USUARIO();

            if (reader.Read())
            {
                int usuarioID = Convert.ToInt32(reader[0]);
                string nombreID = reader[1].ToString();
                string nombre = reader[2].ToString();
                string apellido = reader[3].ToString();
                DateTime nacimineto = Convert.ToDateTime(reader[4]);
                string email = reader[5].ToString();
                string contraseña = reader[6].ToString();
                int años = Convert.ToInt32(reader[7]);

                objetoUsuario = new USUARIO(usuarioID, nombreID, nombre, apellido, nacimineto, email, contraseña, años);
            }
            connection.Close();
            return objetoUsuario;
        }
        public static int VerPeliSelecionada(string usuario)//esto muestra la pelicula selecionada con su titulo y su sinopsis y donde despues podremos alquilar o comprar
        {
            bool exit = false;
            bool fin = false;
            do
            {
                fin = false;
                exit = false;
                MostrarPelis(ListaPeli(CrearObjetoUsuario(usuario)));//esto solo mustra las peliculas que puede ver el usuario
                int peliculaID;
                do
                {
                    bool iDExite = true;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Seleccione pelicula o ¿desea ordenarlas?");
                    Console.ForegroundColor = ConsoleColor.White;
                    string input1 = Console.ReadLine().ToLower();
                    if (input1.Contains("orde"))
                    {
                        Console.Clear();
                        Console.WriteLine("¿Como las quieres ordenar?");
                        Console.WriteLine("1-TITULO");
                        Console.WriteLine("2-EDAD RECOMENDADA");
                        Console.WriteLine("3-GENERO");
                        string ordenar = Console.ReadLine().ToLower();
                        if (ordenar.Contains("titu") || ordenar.Contains("eda") || ordenar.Contains("gene") || ordenar.Contains("1") || ordenar.Contains("2") || ordenar.Contains("3"))
                        {

                            if (ordenar.Contains("titu") || ordenar.Contains("1"))
                            {
                                ordenar = "titulo";
                            }
                            else if (ordenar.Contains("eda") || ordenar.Contains("2"))
                            {
                                ordenar = "edad";
                            }
                            else if (ordenar.Contains("gene") || ordenar.Contains("3"))
                            {
                                ordenar = "genero";
                            }
                            //los tipos no las pongo masyusculas por que en el metodo listafiltro entra en minusculas y sales en mayusculas
                            Console.Clear();
                            MostrarPelis(ListaPeliFiltroOrden(ordenar, CrearObjetoUsuario(usuario)));


                        }
                        else if (ordenar.Contains("sal"))
                        {
                            Console.Clear();
                            exit = true;
                        }
                        else
                        {
                            Console.WriteLine("No te entiendo vuelva ha escribir");
                        }

                    }
                    else if (input1.Contains("sal"))
                    {
                        exit = true;
                        fin = true;
                    }
                    else if (int.TryParse(input1, out peliculaID))
                    {
                        bool decirNo = false;
                        foreach (var item in ListaPeli(CrearObjetoUsuario(usuario)))//recorre la lista y busca a ver si en esa lisa esta esa peli
                        {
                            if (item.PeliculasID == peliculaID)
                            {
                                bool salir = false;
                                do
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.Write($"Pelicula que has selecionado: ");
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine($"{item.Titulo}\n");
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.Write($"¿Es correcta? responda ");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write($"SI ");
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.Write($"o ");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"NO");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    string input = Console.ReadLine().ToLower();
                                    if (input.Contains("si"))
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write($"TITULO DE PELICULA: ");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.WriteLine($"{item.Titulo}");
                                        Console.WriteLine("\n" + MostrarSinopsisPeli(peliculaID, usuario));
                                        return item.PeliculasID;
                                    }
                                    else if (input.Contains("no"))
                                    {
                                        Console.Clear();
                                        salir = true;
                                        exit = true;
                                        decirNo = true;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\nNo te entiedno\n");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                } while (!salir);
                            }
                        }
                        if (!decirNo)
                        {
                            iDExite = false;
                        }

                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Introduzca solo el numero del ID de la pelicula");
                        Console.ForegroundColor = ConsoleColor.White;
                        exit = true;
                        Console.ReadKey();
                        Console.Clear();
                    }

                    if (iDExite == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Introduzca una pelicula existente");
                        Console.ForegroundColor = ConsoleColor.White;
                        exit = true;
                        Console.ReadKey();
                        Console.Clear();
                    }



                } while (!exit);

            } while (!fin);
            return 0;
        }
        public static string MostrarSinopsisPeli(int peliculaID, string usuario)
        {

            foreach (var item in ListaPeli(CrearObjetoUsuario(usuario)))
            {
                if (item.PeliculasID == peliculaID)
                {
                    return item.Sinopsis;
                }
            }
            return null;
        }
        public static void Alquilar(int usuario, int peliculaID)
        {
            string query = $"INSERT INTO ALQUILER (UsuarioID,PeliculaID,TITULO,FechaRESERVA,ESTADO) VALUES ( {usuario}, {peliculaID},'{CreatObjetoPeli(peliculaID).Titulo}' ,'{DateTime.Now}','PENDIENTE')";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            if (command.ExecuteNonQuery() > 0) //para borrar clientes tengo que borrar primero los registrosd e la tabla reservas
            {
                connection.Close();
                query = $"UPDATE PELICULAS SET DISPONIBLE = 0 WHERE PeliculaID = {peliculaID}";
                command = new SqlCommand(query, connection);
                connection.Open();
                if (command.ExecuteNonQuery() > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("¡¡FELICIDADES!!");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("La tiene que entregar en tres dias");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.ReadKey();
                    connection.Close();
                }
                connection.Close();
            }
            else
            {
                Console.WriteLine("Ha habido un error si continua llame a 112 es el fin del mundo");
                connection.Close();
            }
            connection.Close();

        }
        public static int CalcularEdad(DateTime fechaNacimiento)
        {

            int edad = DateTime.Today.Year - fechaNacimiento.Year;
            if (DateTime.Today.Month > fechaNacimiento.Month)
            {
                return edad;
            }
            else if (DateTime.Today.Month == fechaNacimiento.Month)
            {
                if (DateTime.Today.Day >= fechaNacimiento.Day)
                {
                    return edad;
                }
                else
                {
                    return edad - 1;

                }
            }
            else
            {
                return edad - 1;
            }

        }
        public static bool ComprobarEstadoPeli(int peliculaID)
        {
            string query = $"SELECT DISPONIBLE FROM PELICULAS WHERE PeliculaID = {peliculaID}";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            int estado;
            if (reader.Read())
            {
                estado = Convert.ToInt32(reader[0]);
                connection.Close();
                if (estado == 1)
                {
                    connection.Close();
                    return true;
                }
                else
                {
                    connection.Close();
                    return false;
                }
            }
            else
            {
                Console.WriteLine("ERROR");
            }
            connection.Close();
            return false;

        }
        public static List<ALQUILER> ListaAlquilerUsuarioHistorial(USUARIO x)
        {
            string query = $"SELECT * FROM ALQUILER WHERE UsuarioID ={x.UsuarioID} ORDER BY FechaRESERVA  DESC ";


            List<ALQUILER> listaAlquilerUsuario = new List<ALQUILER>();

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int alquilerID = Convert.ToInt32(reader[0]);
                int usuarioID = Convert.ToInt32(reader[1]);
                int peliculaID = Convert.ToInt32(reader[2]);
                string titulo = reader[3].ToString();
                string fechaReserva = reader[4].ToString();
                string fechaEntrega = reader[5].ToString();
                string estado = reader[6].ToString();

                listaAlquilerUsuario.Add(new ALQUILER(alquilerID, usuarioID, peliculaID, fechaReserva, fechaEntrega, estado, titulo));

            }
            connection.Close();

            return listaAlquilerUsuario;

        }
        public static void MostrarListaAlquilerUsuario(List<ALQUILER> lista)//en esta lista aprecera todas las peliculas alquiladas que ha hecho el usaurario
        {
            Console.WriteLine("**************************************************HISTORIAL DE ALQUILER*************************************************\n");
            foreach (var item in lista)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Id: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{item.PeliculaID} ---- ");

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("TITU: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{item.Titulo} ---- ");

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("FECHA DE RESERVA: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{item.FechaReserva} ---- ");

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("FECHA ENTREGA: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{item.FechaEntrga} ---- ");

                if (item.Estado.Contains("PENDIENTE"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{item.Estado}\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (item.Estado.Contains("ENTREGADO"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{item.Estado}\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (item.Estado.Contains("TARDE"))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{item.Estado}\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
        public static void HistorialAquiler(string usuario)
        {

            MostrarListaAlquilerUsuario(ListaAlquilerUsuarioHistorial(CrearObjetoUsuario(usuario)));
        }
        public static int PeliculaIDDevolver(string usuario)
        {

            bool exit = false;
            bool fin = false;
            do
            {
                fin = false;
                exit = false;
                MostrarListaAlquilerUsuarioDevolver(ListaAlquilerUsuarioDevolver(CrearObjetoUsuario(usuario)));//esto solo mustra las peliculas que puede ver el usuario
                int peliculaID;
                do
                {
                    string input1;
                    if (ListaAlquilerUsuarioDevolver(CrearObjetoUsuario(usuario)).Count > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\nSeleccione el ID de la peicula que quiere devolver");
                        Console.ForegroundColor = ConsoleColor.White;
                        input1 = Console.ReadLine().ToLower();

                        if (input1.Contains("sal"))
                        {
                            Console.Clear();
                            exit = true;
                            fin = true;
                        }
                        else if (int.TryParse(input1, out peliculaID))
                        {
                            foreach (var item in ListaAlquilerUsuarioDevolver(CrearObjetoUsuario(usuario)))//recorre la lista y busca a ver si en esa lisa esta esa peli
                            {
                                if (item.PeliculaID == peliculaID && item.Estado == "PENDIENTE")
                                {
                                    bool salir = false;
                                    do
                                    {
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.Write($"Pelicula que has selecionado: ");
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.WriteLine($"{item.Titulo}\n");
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.Write($"¿Es correcta? responda ");
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write($"SI ");
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.Write($"o ");
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine($"NO");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        string input = Console.ReadLine().ToLower();
                                        if (input.Contains("si"))
                                        {
                                            return item.AlquilerID;
                                        }
                                        else if (input.Contains("no"))
                                        {
                                            salir = true;

                                        }
                                        else
                                        {
                                            Console.WriteLine("No te entiedno");
                                        }
                                    } while (!salir);
                                }
                            }

                        }
                        else if (input1.Contains(""))
                        {
                            Console.Clear();
                            exit = true;
                        }
                        else
                        {
                            Console.WriteLine("Introduzca solo el numero del ID de la pelicula");
                        }


                    }
                    else
                    {
                        exit = false;
                        input1 = Console.ReadLine();
                        if (input1.Contains("sal"))
                        {
                            Console.Clear();
                            exit = true;
                            fin = true;
                        }
                        else
                        {
                            Console.Clear();
                            exit = true;
                        }

                    }

                } while (!exit);

            } while (!fin);
            return 0;

        }
        public static List<ALQUILER> ListaAlquilerUsuarioDevolver(USUARIO x)//en esta lista solo aparecen las peliculas que tine que devolver el usuario
        {
            string query = $"SELECT * FROM ALQUILER WHERE UsuarioID ={x.UsuarioID} AND ESTADO like 'PENDIENTE'";

            List<ALQUILER> listaAlquilerUsuarioDevolver = new List<ALQUILER>();

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int alquilerID = Convert.ToInt32(reader[0]);
                int usuarioID = Convert.ToInt32(reader[1]);
                int peliculaID = Convert.ToInt32(reader[2]);
                string titulo = reader[3].ToString();
                string fechaReserva = reader[4].ToString();
                string fechaEntrega = reader[5].ToString();
                string estado = reader[6].ToString();

                listaAlquilerUsuarioDevolver.Add(new ALQUILER(alquilerID, usuarioID, peliculaID, fechaReserva, fechaEntrega, estado, titulo));

            }
            connection.Close();

            return listaAlquilerUsuarioDevolver;

        }
        public static void MostrarListaAlquilerUsuarioDevolver(List<ALQUILER> lista)
        {
            Console.WriteLine("****************************************************LISTA DE DEVOLUCIONES***********************************************\n");
            foreach (var item in lista)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Id: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{item.PeliculaID} ---- ");

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("TITU: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{item.Titulo} ---- ");

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("FECHA DE RESERVA: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{item.FechaReserva} ---- ");

                DateTime tiempo = Convert.ToDateTime(item.FechaReserva);
                if (DateTime.Now > tiempo.AddDays(3))
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("Estado: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Fuera de plazo");
                    Console.ForegroundColor = ConsoleColor.White;

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("Estado: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Dentro de plazo");
                    Console.ForegroundColor = ConsoleColor.White;
                }



            }
        }
        public static void ValidarDevuelda(int alquierID, string nombreID)
        {
            foreach (var item in ListaAlquilerUsuarioDevolver(CrearObjetoUsuario(nombreID)))
            {
                if (alquierID == item.AlquilerID)
                {
                    string query = $"UPDATE PELICULAS SET DISPONIBLE = 1 WHERE PeliculaID = {item.PeliculaID}";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    if (command.ExecuteNonQuery() > 0) //para borrar clientes tengo que borrar primero los registrosd e la tabla reservas
                    {
                        connection.Close();
                        query = $"UPDATE ALQUILER SET FechaENTREGA = '{DateTime.Now}' WHERE AlquilerID = {alquierID}";
                        command = new SqlCommand(query, connection);
                        connection.Open();
                        if (command.ExecuteNonQuery() > 0)
                        {
                            connection.Close();

                            DateTime tiempo = Convert.ToDateTime(item.FechaReserva);
                            if (DateTime.Now < tiempo.AddDays(3))
                            {
                                query = $"UPDATE ALQUILER SET ESTADO = 'ENTREGADO' WHERE AlquilerID = {alquierID}";
                                command = new SqlCommand(query, connection);
                                connection.Open();
                                if (command.ExecuteNonQuery() > 0)
                                {
                                    connection.Close();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"\nGracias por entregarla en el tiempo establecido");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                connection.Close();

                            }
                            else if (DateTime.Now > tiempo.AddDays(3))
                            {
                                query = $"UPDATE ALQUILER SET ESTADO = 'TARDE' WHERE PeliculaID = {alquierID}";
                                command = new SqlCommand(query, connection);
                                connection.Open();
                                if (command.ExecuteNonQuery() > 0)
                                {
                                    connection.Close();
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine($"\nTiene que pagar 2 euros por entrgarla tarde");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                connection.Close();
                            }

                        }
                        connection.Close();

                    }
                    connection.Close();
                }
            }
        }
        public static void MenuUsuario(USUARIO x)
        {
            //x es un objeto usuario  
            bool salir = false;
            do
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"MENU USUARIO: {x.NombreID}\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("1-VER PELICULAS");
                Console.WriteLine("2-DEVOLVER PELICULAS");
                Console.WriteLine("3-HISTORIAL DE ALQUILERES");
                Console.WriteLine("4-DATO PERSONAL");
                Console.WriteLine("5-SALIR");
                Console.ForegroundColor = ConsoleColor.White;
                string menu1 = Console.ReadLine().ToLower();
                if (menu1.Contains("peli") || menu1.Contains("1"))
                {
                    Console.Clear();
                    bool salirVerPelis = false;
                    do
                    {
                        salirVerPelis = false;
                        int peliculaId = VerPeliSelecionada(x.NombreID);
                        if (peliculaId != 0)//verPeliculaSelcionada es un int ,es el el id de la pelicula 
                        {
                            bool salirAlquilar = false;
                            do
                            {
                                salirAlquilar = false;
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write("\n¿Quieres alquilarla? ");
                                Console.Write("Responda "); Console.ForegroundColor = ConsoleColor.Red; Console.Write("Si "); Console.ForegroundColor = ConsoleColor.Blue; Console.Write("o "); Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("No"); Console.ForegroundColor = ConsoleColor.White;
                                string input = Console.ReadLine();
                                if (input.Contains("si"))
                                {
                                    if (ComprobarEstadoPeli(peliculaId))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.WriteLine("\nEstas seguro que quieres alquilarla");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        string afirmar = Console.ReadLine().ToLower();
                                        if (afirmar.Contains("si"))
                                        {
                                            Alquilar(x.UsuarioID, peliculaId);
                                            Console.Clear();
                                            salirAlquilar = true;
                                        }
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("*******Esta peli no esta disponible*******");
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.WriteLine("\n*********Selecione otra pelicula**********");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.ReadKey();
                                        Console.Clear();
                                        salirAlquilar = true;
                                    }
                                }
                                else if (input.Contains("no"))
                                {
                                    Console.Clear();
                                    salirAlquilar = true;
                                }
                                else
                                {
                                    Console.WriteLine("No te entiendo");
                                }

                            } while (!salirAlquilar);
                        }
                        else
                        {
                            salirVerPelis = true;
                            Console.Clear();
                        }
                    } while (!salirVerPelis);
                }
                else if (menu1.Contains("devol") || menu1.Contains("2"))
                {
                    Console.Clear();
                    ValidarDevuelda(PeliculaIDDevolver(x.NombreID), x.NombreID);

                }
                else if (menu1.Contains("histo") || menu1.Contains("3"))
                {
                    bool salirHitorial = false;
                    do
                    {
                        Console.Clear();
                        MostrarListaAlquilerUsuario(ListaAlquilerUsuarioHistorial(CrearObjetoUsuario(x.NombreID)));
                        string salirHito = Console.ReadLine();
                        if (salirHito.Contains("sal"))
                        {
                            salirHitorial = true;
                            Console.Clear();
                        }

                    } while (!salirHitorial);
                }
                else if (menu1.Contains("dato") || menu1.Contains("4"))
                {
                    bool salirMostrarDatos = false;
                    do
                    {
                        Console.Clear();
                        x.MostrarDatosUsuario();
                        string salirHito = Console.ReadLine();
                        if (salirHito.Contains("sal"))
                        {
                            salirMostrarDatos = true;
                            Console.Clear();
                        }

                    } while (!salirMostrarDatos);
                }
                else if (menu1.Contains("sal") || menu1.Contains("5"))
                {
                    Console.Clear();
                    salir = true;
                }

            } while (!salir);
        }






    }
}

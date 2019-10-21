using System;
using System.Collections.Generic;
using System.Text;

namespace VIDEO_CLUP
{
    class PELICULAS
    {
        public int PeliculasID { get; set; }
        public string Titulo { get; set; }
        public string Sinopsis { get; set; }
        public int Edad { get; set; }
        public string Disponible { get; set; }
        
        public string Genero { get; set; }

        public PELICULAS()
        {
        }

        public PELICULAS(int peliculasID, string titulo, string sinopsis, int edad, byte disponible,string genero)
        {
            PeliculasID = peliculasID;
            Titulo = titulo;
            Sinopsis = sinopsis;
            Edad = edad;
            if (disponible == 1)
            {
                Disponible = "DISPONIBLE";
            }
            else if (disponible == 0)
            {
                Disponible = "ALQUILADO";
                
            }
            

            Genero = genero;
        }
    }
}

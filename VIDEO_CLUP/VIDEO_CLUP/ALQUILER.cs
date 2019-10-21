using System;
using System.Collections.Generic;
using System.Text;

namespace VIDEO_CLUP
{
    class ALQUILER
    {
        public int AlquilerID { get; set; }
        public int UsuarioId { get; set; }
        public int PeliculaID { get; set; }
        public string FechaReserva { get; set; }
        public string FechaEntrga { get; set; }
        public string Estado { get; set; } //El estado es como pendiente , entregado , tarde
        public string Titulo { get; set; }

        public ALQUILER(int alquilerID, int usuarioID, int peliculaID, string fechaReserva, string fechaEntrga, string estado,string titulo)
        {
            AlquilerID = alquilerID;
            UsuarioId = usuarioID;
            PeliculaID = peliculaID;
            FechaReserva = fechaReserva;
            FechaEntrga = fechaEntrga;
            Estado = estado;
            Titulo = titulo;
        }
    }
}

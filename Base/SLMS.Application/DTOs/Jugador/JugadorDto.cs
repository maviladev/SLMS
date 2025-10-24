using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.DTOs.Jugador
{
    public class JugadorDto
    {
        public int Id { get; set; }
        public int EquipoId { get; set; }
        public string EquipoNombre { get; set; }
        public int TorneoId { get; set; }
        public string TorneoNombre { get; set; }
        public int LigaId { get; set; }
        public string LigaNombre { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public DateTime Nacimiento { get; set; }
        public bool Estado { get; set; }
        public DateTime Creado { get; set; }
        public DateTime Modificado { get; set; }
        public int RolId { get; set; }
        public string RolNombre { get; set; }
    }
}

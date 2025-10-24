using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Application.DTOs.Liga
{
    public class UpdateLigaDto
    {
        [MaxLength(200)]
        public string Nombre { get; set; }

        [MaxLength(500)]
        public string Logo { get; set; }
        public bool? Estado { get; set; }
    }
}

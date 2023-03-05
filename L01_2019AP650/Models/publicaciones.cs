using System.ComponentModel.DataAnnotations;

namespace L01_2019AP650.Models
{
    public class publicaciones
    {
        [Key]

        public int publicacionId { get; set; }
        public string titulo { get; set; }
        public string descripcion {get; set; }
        public int usuarioid { get; set; }


       

    }
}

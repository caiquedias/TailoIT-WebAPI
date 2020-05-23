using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CRUD_TailorIT.Models
{
    public class Funcionario
    {
        public int ID { get; set; }
        [Required]
        public string NOME { get; set; }
        [Required]
        public DateTime DATANASC { get; set; }
        public string EMAIL{ get; set; }
        [Required]
        public string SEXO { get; set; }
        [Required]
        public string HABILIDADES { get; set; }
    }
}
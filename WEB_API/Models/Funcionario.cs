using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WEB_API.Models
{
    public class Funcionario
    {
        [Key]
        public int ID { get; set; }
        public string NOME { get; set; }
        public DateTime DATANASC { get; set; }
        public string EMAIL { get; set; }
        public string SEXO { get; set; }
        public string HABILIDADES { get; set; }
    }
}
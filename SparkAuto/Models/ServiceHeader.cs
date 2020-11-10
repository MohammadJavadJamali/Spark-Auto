using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace SparkAuto.Models
{
    public class ServiceHeader
    {


        public int Id { get; set; }
        public double Miles { get; set; }
        [Required]
        public double TotalOrice { get; set; }
        public string Details { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:MMM dd yyyy}")]
        public DateTime DataAdded { get; set; }

        public int CarId { get; set; }

        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }


    }
}

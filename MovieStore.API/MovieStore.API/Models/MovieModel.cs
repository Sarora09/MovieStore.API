using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.API.Models
{
    public class MovieModel
    {
        
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float Rating { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public float RentPrice { get; set; }
    }
}

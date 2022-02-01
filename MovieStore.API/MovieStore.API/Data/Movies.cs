using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.API.Data
{
    public class Movies
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Rating { get; set; }

        public string Genre { get; set; }

        public float RentPrice { get; set; }
    }
}

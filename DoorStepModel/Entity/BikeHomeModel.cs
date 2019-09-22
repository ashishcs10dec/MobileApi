using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorStepModel.Entity
{
    public class BikeHomeModel
    {
        public int BikeId { get; set; }
        public string ImageUrl { get; set; }
        public string BikeName { get; set; }
        public string ModelYear { get; set; }
        public string Driven { get; set; }
        public decimal Price { get; set; }
        public bool ? IsActive { get; set; }
    }
}

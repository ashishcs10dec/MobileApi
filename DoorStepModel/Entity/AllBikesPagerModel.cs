using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorStepModel.Entity
{
   public class AllBikesPagerModel
    {
        public List<BikeHomeModel> BikeHomeModel { get; set; }
        public Pager Pager { get; set; }
    }
}

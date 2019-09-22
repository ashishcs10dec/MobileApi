using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorStepModel.Entity
{
    public class Token
    {
        public string SecretToken { get; set; }
    }

    public class BikeInfoById
    {
        public int BikeId { get; set; }
        public string SecretToken { get; set; }
    }

    public class BikeInfoByIds
    {
        public string BikeId { get; set; }
    }

    public class TermsAndConditions
    {
        public string TermsConditions { get; set; }
    }
}

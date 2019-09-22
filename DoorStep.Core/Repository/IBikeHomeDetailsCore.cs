using DoorStepModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorStep.Core.Repository
{
    public interface IBikeHomeDetailsCore
    {
        AllBikesPagerModel BikeInfo(int PageNo, int PageSize);
    }
}

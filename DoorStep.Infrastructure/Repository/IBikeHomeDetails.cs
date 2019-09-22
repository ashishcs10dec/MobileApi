using DoorStep.Infrastructure.Database;
using DoorStepModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorStep.Infrastructure.Repository
{
    public interface IBikeHomeDetails
    {
        List<BikeInformation> BikeHomeInfo(int PageNo, int PageSize);
        Pager BikeListPager(int PageNo, int PageSize);
    }
}

using DoorStep.Infrastructure.Database;
using DoorStep.Infrastructure.Repository;
using DoorStepModel.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorStep.Infrastructure.Implementation
{
    public class BikeHomeDetails : IBikeHomeDetails
    {
        public List<BikeInformation> BikeHomeInfo(int PageNo, int PageSize)
        {
            var bikeInformation = new List<BikeInformation>();
            using (var db = new ObickeEntities())
            {
                var totalRecord = db.BikeInformations.Count();
                if (totalRecord <= PageSize)
                {
                    bikeInformation = db.BikeInformations.Where(a => a.IsActive == true && a.IsDelete == false).OrderBy(a => a.BikePrice).ToList();
                }
                else
                {
                    int recordCount = db.BikeInformations.Where(a => a.IsActive == true && a.IsDelete == false).OrderBy(a => a.BikePrice).Skip((PageNo - 1) * PageSize).Count();
                    if (recordCount < PageSize)
                        bikeInformation = db.BikeInformations.Where(a => a.IsActive == true && a.IsDelete == false).OrderBy(a => a.BikePrice).Skip((PageNo - 1) * PageSize).Take(recordCount).ToList();
                    else
                        bikeInformation = db.BikeInformations.Where(a => a.IsActive == true && a.IsDelete == false).OrderBy(a => a.BikePrice).Skip((PageNo - 1) * PageSize).Take(PageSize).ToList();
                }
                bikeInformation.ForEach(a => a.ImageDetails.Where(x => x.BikeId == a.BikeId));
            }
            return bikeInformation;
        }

        public Pager BikeListPager(int pageNo, int pageSize)
        {
            var pager = new Pager();
            var bikeInformation = new List<BikeInformation>();
            using (var db = new ObickeEntities())
            {
                var totalRecord = db.BikeInformations.Count();
                if (totalRecord > 0)
                {
                    // calculate total, start and end pages
                    var totalPages = (int)Math.Ceiling((decimal)totalRecord / (decimal)pageSize);
                    var currentPage = pageNo != null ? (int)pageNo : 1;
                    var startPage = currentPage - 5;
                    var endPage = currentPage + 4;
                    if (startPage <= 0)
                    {
                        endPage -= (startPage - 1);
                        startPage = 1;
                    }
                    if (endPage > totalPages)
                    {
                        endPage = totalPages;
                        if (endPage > 10)
                        {
                            startPage = endPage - 9;
                        }
                    }
                    pager.TotalItems = totalRecord;
                    pager.CurrentPage = currentPage;
                    pager.PageSize = pageSize;
                    pager.TotalPages = totalPages;
                    pager.StartPage = startPage;
                    pager.EndPage = endPage;
                }
                return pager;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABCostmeticWAD2.DAL.EntityModels;

namespace ABCostmeticWAD2.BAL.DTO
{
    public class StoreModel
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string Region { get; set; }
    }

    public class StoreStructureModel
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public Employee Employee { get; set; }

        public string Role { get; set; }
    }
}

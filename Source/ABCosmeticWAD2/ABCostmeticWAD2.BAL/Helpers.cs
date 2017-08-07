using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABCostmeticWAD2.BAL.DTO;
using ABCostmeticWAD2.DAL.EntityModels;
using AutoMapper;

namespace ABCostmeticWAD2.BAL
{
    public static class Helpers
    {
        public static MapperConfiguration MapperConfiguration { get; set; } = new MapperConfiguration(cfg =>
         {
             cfg.CreateMap<Category, CategoryModel>();
         });
    }
}

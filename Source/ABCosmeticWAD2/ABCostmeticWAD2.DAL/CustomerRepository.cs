﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABCostmeticWAD2.DAL.EntityModels;
using ABCostmeticWAD2.DAL.Interfaces;

namespace ABCostmeticWAD2.DAL
{
    public class CustomerRepository : RepositoryBase<ABCostmetic_WAD2Entities, Customer>, ICustomerRepository
    {

    }
}
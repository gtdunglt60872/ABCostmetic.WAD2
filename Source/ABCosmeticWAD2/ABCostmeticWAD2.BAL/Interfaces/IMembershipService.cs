using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABCostmeticWAD2.BAL.Interfaces
{
    using DTO;
    public interface IMembershipService : IServices<MembershipModel>
    {
        MembershipModel Login(string username, string password);
        string GetUserRole(int emplId);
    }
}

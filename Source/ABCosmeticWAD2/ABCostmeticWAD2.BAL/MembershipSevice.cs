using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime;
using AutoMapper;

namespace ABCostmeticWAD2.BAL
{
    using DTO;
    using DAL;
    using DAL.Interfaces;
    using DAL.EntityModels;
    using Interfaces;

    public class MembershipSevice : IMembershipService
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IStoreStructureRepository _storeStructureRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IStoreRepository _storeRepository;

        public MembershipSevice()
        {
            _storeRepository = _storeRepository ?? new StoreRepository();
            _employeeRepository = _employeeRepository ?? new EmployeeRepository();
            _storeStructureRepository = _storeStructureRepository ?? new StoreStructureRepository();
            _membershipRepository = _membershipRepository ?? new MembershipRepository();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Membership, MembershipModel>();
                cfg.CreateMap<MembershipModel, Membership>();
            });

        }

        public IList<MembershipModel> GetAll()
        {
            var list = _membershipRepository.GetAll();

            if (list == null || !list.Any())
            {
                return new List<MembershipModel>();
            }

            var res = Mapper.Map<IList<Membership>, IList<MembershipModel>>(list.ToList());

            return res.ToList();
        }

        public MembershipModel GetByKey(int id)
        {
            var model = _membershipRepository.FindBy(x => x.Id == id).FirstOrDefault();
            if (model == null)
            {
                return new MembershipModel();
            }

            var res = Mapper.Map<Membership, MembershipModel>(model);
            return res;
        }

        public void Update(MembershipModel obj)
        {
            if (obj == null) return;

            var model = Mapper.Map<MembershipModel, Membership>(obj);
            _membershipRepository.Edit(model);
            _membershipRepository.Save();
        }

        public void Delete(MembershipModel obj)
        {
            if (obj == null) return;

            var model = Mapper.Map<MembershipModel, Membership>(obj);

            _membershipRepository.Delete(model);
        }

        public void Add(MembershipModel obj)
        {
            if (obj == null) return;
            var model = Mapper.Map<MembershipModel, Membership>(obj);
            _membershipRepository.Add(model);
            _membershipRepository.Save();
        }

        public MembershipModel Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var obj = _membershipRepository.FindBy(x => x.UserName == username && x.Password == password).FirstOrDefault();
            var model = Mapper.Map<Membership, MembershipModel>(obj);
            return model;
        }

        /// <summary>
        /// Get user roles for authorize
        /// </summary>
        /// <param name="emplId">Employee Identity number</param>
        /// <returns>Employee's role</returns>
        public string GetUserRole(int emplId)
        {
            if (emplId == 0)
            {
                return string.Empty;
            }

            var obj = _storeStructureRepository.FindBy(x => x.Employee.EmployeeID == emplId).FirstOrDefault();
            return obj?.Role;
        }

        /// <summary>
        /// Get employee information with custom field
        /// </summary>
        /// <param name="emplId">Employee Identity number</param>
        /// <returns></returns>
        public EmployeeDto GetUserInfo(int emplId)
        {
            if (emplId == 0)
            {
                return null;
            }

            var obj = _employeeRepository.FindBy(x => x.EmployeeID == emplId).FirstOrDefault();
            if (obj == null)
            {
                return null;
            }

            var res = new EmployeeDto
            {
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                Address = obj.Address,
                BirthDate = obj.BirthDate,
                City = obj.City,
                HireDate = obj.HireDate,
                Title = obj.TitleLookup.Name,
                Role = GetUserRole(obj.EmployeeID)
            };

            var storeStructure = _storeStructureRepository.FindBy(x => x.EmployeeId == obj.EmployeeID).FirstOrDefault();
            if (storeStructure != null)
            {
                res.Store = storeStructure.Store;
            }

            return res;
        }
    }
}

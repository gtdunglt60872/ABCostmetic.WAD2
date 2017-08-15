using System;
using System.Collections.Generic;
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

        public MembershipSevice()
        {
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
    }
}

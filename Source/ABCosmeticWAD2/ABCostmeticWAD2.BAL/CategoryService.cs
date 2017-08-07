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
    using BAL.Interfaces;

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService()
        {
            _categoryRepository = _categoryRepository ?? new CategoryRepository();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Category, CategoryModel>();
                cfg.CreateMap<CategoryModel, Category>();
            });

        }

        public IList<CategoryModel> GetAll()
        {
            var list = _categoryRepository.GetAll();

            if (list == null || !list.Any())
            {
                return new List<CategoryModel>();
            }

            var res = Mapper.Map<IList<Category>, IList<CategoryModel>>(list.ToList());

            return res.ToList();
        }

        public CategoryModel GetByKey(int id)
        {
            var model = _categoryRepository.FindBy(x => x.CategoryID == id).FirstOrDefault();
            if (model == null)
            {
                return new CategoryModel();
            }

            var res = Mapper.Map<Category, CategoryModel>(model);
            return res;
        }

        public void Update(CategoryModel obj)
        {
            if (obj == null) return;

            var category = Mapper.Map<CategoryModel, Category>(obj);
            _categoryRepository.Edit(category);
            _categoryRepository.Save();
        }

        public void Delete(int id)
        {
            if (id == 0)
            {
                return;
            }
            var categoryModal = this.GetByKey(id);
            var category = Mapper.Map<CategoryModel, Category>(categoryModal);
            _categoryRepository.Delete(category);
            _categoryRepository.Save();
        }

        public void Add(CategoryModel obj)
        {
            if (obj == null) return;
            var category = Mapper.Map<CategoryModel, Category>(obj);
            _categoryRepository.Add(category);
            _categoryRepository.Save();
        }
    }
}

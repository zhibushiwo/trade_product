/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Domain.Entity.ProductManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.ProductManage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFine.Application.ProductManage
{
    public class CategoryApp
    {
        private ICategoryRepository service = new CategoryRepository();

        public List<CategoryEntity> GetList()
        {
            return service.IQueryable().ToList();
        }
        public CategoryEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            if (service.IQueryable().Count(t => t.F_ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                service.Delete(t => t.F_Id == keyValue);
            }
        }
        public void SubmitForm(CategoryEntity CategoryEntity, string keyValue)
        {
            if (GetMainLevel(CategoryEntity.F_ParentId) > 3)
            {
                throw new Exception("\""+CategoryEntity.Category_FullName +"\"不能存在下一级分类");
            }
            if (!string.IsNullOrEmpty(keyValue))
            {
                CategoryEntity.Modify(keyValue);
                service.Update(CategoryEntity);
            }
            else
            {
                CategoryEntity CategoryE_EnCode = service.FindEntity(t => t.Category_EnCode == CategoryEntity.Category_EnCode);
                if (CategoryE_EnCode != null)
                {
                    throw new Exception("已经存在分类编码");
                }
                CategoryEntity CategoryE_FullName = service.FindEntity(t => t.Category_FullName == CategoryEntity.Category_FullName);
                if (CategoryE_FullName != null)
                {
                    throw new Exception("已经存在分类名称");
                }
                CategoryEntity.Create();
                service.Insert(CategoryEntity);
            }
        }
        public int GetMainLevel(string f_id)
        {
            CategoryEntity CategoryE = new CategoryEntity();
            CategoryE= service.FindEntity(t => t.F_Id == f_id);
            if (CategoryE!=null)
            {
                CategoryE = service.FindEntity(t => t.F_Id == CategoryE.F_ParentId);
                if (CategoryE != null)
                {
                    CategoryE = service.FindEntity(t => t.F_Id == CategoryE.F_ParentId);
                    if (CategoryE != null)
                    {
                        CategoryE = service.FindEntity(t => t.F_Id == CategoryE.F_ParentId);
                        if (CategoryE != null)
                        {
                            CategoryE = service.FindEntity(t => t.F_Id == CategoryE.F_ParentId);
                            if (CategoryE != null)
                            {
                                return 5;
                            }
                            return 4;
                        }
                        return 3;
                    }
                    return 2;
                }
                return  1;
           }
            return 0;
        }
    }
}

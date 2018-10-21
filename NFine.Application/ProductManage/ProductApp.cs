/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Domain.Entity.ProductManage;
using NFine.Domain.IRepository.ProductManage;
using NFine.Domain.IRepository.ProductMangage;
using NFine.Repository.ProductManage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFine.Application.ProductManage
{
    public class ProductApp
    {
        private IProductRepository service = new ProductRepository();
        private IProductSubRepository serviceSub = new ProductSubRepository();
        public List<ProductEntity> GetList()
        {
            return service.IQueryable().OrderBy(t => t.F_CreatorTime).ToList();
        }
        public List<ProductEntity> GetList(Pagination pagination, string keyword = "")
        {
            var expression = ExtLinq.True<ProductEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Id.Contains(keyword));
                expression = expression.Or(t => t.F_Id.Contains(keyword));
            }
            return service.FindList(expression, pagination);
        }
        public ProductEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public List<ProductSubEntity> GetFormSub(string keyValue)
        {
            return serviceSub.IQueryable(t=>t.F_ParentId==keyValue).ToList();
        }
        public void DeleteForm(string keyValue)
        {

            serviceSub.Delete(t => t.F_ParentId == keyValue);
            service.Delete(t => t.F_Id == keyValue);
            
        }

        public void SubmitForm(ProductEntity productEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                productEntity.Modify(keyValue);
                service.Update(productEntity);
            }
            else
            {
                productEntity.Create();
                service.Insert(productEntity);
            }
        }

        //主子表保存
        public void SubmitForm(ProductEntity productEntity, string keyValue, List<ProductSubEntity> listProductSub)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                productEntity.Modify(keyValue);
            }
            else
            {
                productEntity.Create();
            }
            List<ProductSubEntity> listProductSubUpdate = new List<ProductSubEntity>();
            List<FileEntity> listFile = new List<FileEntity>();
            foreach (var itemId in listProductSub)
            {
                ProductSubEntity productSubEntity = new ProductSubEntity();
                productSubEntity.F_ParentId = productEntity.F_Id;
                productSubEntity.Attribute = itemId.Attribute;
                productSubEntity.PictureGuId = itemId.PictureGuId;
                productSubEntity.SKU = itemId.SKU;
                productSubEntity.Supplier = itemId.Supplier;
                productSubEntity.PurchaseAddress = itemId.PurchaseAddress;
                productSubEntity.HWeight = itemId.HWeight;
                productSubEntity.GWeight = itemId.GWeight;
                productSubEntity.SWeight = itemId.SWeight;
                productSubEntity.Long = itemId.Long;
                productSubEntity.Wide = itemId.Wide;
                productSubEntity.High = itemId.High;
                productSubEntity.PurchaseCost = itemId.PurchaseCost;
                productSubEntity.TransportCost = itemId.TransportCost;
                productSubEntity.OtherCost = itemId.OtherCost;
                productSubEntity.F_Description = itemId.F_Description;
                productSubEntity.Create();
                var guid = productSubEntity.F_Id;
                string[] files = itemId.PictureGuId.Split(',');
                foreach(string f in files)
                {
                    FileEntity file = new FileEntity();
                    file.F_ParentId = guid;
                    file.F_File = f;
                    file.Create();
                    listFile.Add(file);
                 }
                listProductSubUpdate.Add(productSubEntity);
            }
            service.SubmitForm(productEntity, listProductSubUpdate, keyValue);
        }
    }
}

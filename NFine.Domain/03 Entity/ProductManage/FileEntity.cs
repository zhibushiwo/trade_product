using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.ProductManage
{

    public class FileEntity : IEntity<FileEntity>, ICreationAudited, IModificationAudited
    {
        public string F_Id { get; set; }
        public string F_ParentId { get; set; }    //对应父级ID
        public string F_File { get; set; }    //文件路径
        public DateTime? F_CreatorTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public string F_Description { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.DataAccess;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface ICategoryDal : IEntityRepository<Category>
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CoreLayer.DataAccess.EntityFramework;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EfPostDal : EfEntityRepositoryBase<Post,EfContext>,IPostDal
    {
    }
}
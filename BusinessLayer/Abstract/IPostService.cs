using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IPostService
    {
        public int Add(Post post);
        public int Update(Post post);
        public int Delete(Post post);
        public Post GetById(int id);
        public List<Post> GetAll();
    }
}
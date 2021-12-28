using System.Collections.Generic;
using EntityLayer.Concrete;
using PagedList.Core;

namespace BusinessLayer.Abstract
{
    public interface IPostService
    {
        public int Add(Post post);
        public int Update(Post post);
        public int Delete(Post post);
        public Post GetById(int id);
        public List<Post> GetAll();
        public List<Post> SetDescriptionLimit(List<Post> posts, int limit);
        public List<Post> GetPosts(int page, int pageSize, int categoryId, bool ordering,
            string seacrhKeyword = "");

        public List<Post> SetTitleLimit(List<Post> posts, int limit);
    }
}
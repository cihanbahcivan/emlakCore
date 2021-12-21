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
        public PagedList<Post> SetDescriptionLimit(PagedList<Post> posts, int limit);
        public PagedList<Post> GetPosts(int page, int pageSize, int categoryId, bool ordering,
            string seacrhKeyword = "");

        public PagedList<Post> SetTitleLimit(PagedList<Post> posts, int limit);
    }
}
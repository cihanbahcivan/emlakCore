using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete;
using PagedList.Core;

namespace BusinessLayer.Concrete
{
    public class PostManager : IPostService
    {
        private readonly IPostDal _postDal;
        private readonly IImageDal _imageDal = new EfImageDal();

        public PostManager(IPostDal postDal)
        {
            _postDal = postDal;
        }

        public int Add(Post post)
        {
            try
            {
                _postDal.Add(post);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
            return 1;
        }

        public int Update(Post post)
        {
            try
            {
                _postDal.Update(post);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
            return 1;
        }

        public int Delete(Post post)
        {
            try
            {
                _postDal.Delete(post);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }

            return 1;
        }

        public Post GetById(int id)
        {
            var post = _postDal.Get(x => x.PostId == id);
            //post.Images = ImageOpr.GetImagesByPostId(post.PostId);
            return post;
        }

        public List<Post> GetAll()
        {
            return _postDal.GetAll();
        }

        public List<Post> GetBestSeller(int size = 3)
        {
            var list = _postDal.GetAll().OrderByDescending(p => p.Viewing).Take(size).ToList();

            foreach (var item in list)
            {
                item.Images = _imageDal.GetAll(x => x.PostId == item.PostId);
            }

            return list;
        }

        public void IncreaseViews(int postId)
        {
            try
            {

                var post = _postDal.Get(x => x.PostId == postId);
                post.Viewing = post.Viewing + 1;
                _postDal.Update(post);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public List<Post> GetMostExpensive(int size = 4)
        {
            List<Post> posts = null;
            try
            {
                posts = _postDal.GetAll().OrderByDescending(x => x.Price).Take(size).ToList();
                foreach (var post in posts)
                {
                    //post.Images = imageOpr.GetImagesByPostId(post.PostId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return posts;
        }

        public string ProduceCode(PagedList<Post> posts)
        {
            Random rnd = new Random();
            string chars = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyz";
            string produce = "";
            bool result = false;
            while (!result)
            {
                for (int i = 0; i < 6; i++)
                {
                    produce += chars[rnd.Next(chars.Length)];
                }
                if (!CheckCode(produce))
                {
                    result = false;
                    produce = "";
                }
                else
                {
                    result = true;
                }
            }
            return produce;
        }

        private bool CheckCode(string code)
        {
            bool result = false;
            Post post = null;
            try
            {
                post = _postDal.GetAll().FirstOrDefault(c => c.Code == code);
                if (post != null)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        private bool SetCollectionBoolen(string[] collection)
        {
            if (collection.Length > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public PagedList<Post> SetTitleLimit(PagedList<Post> posts, int limit)
        {
            StringBuilder title = null;
            try
            {
                foreach (var post in posts)
                {
                    if (post.Title.Length > limit)
                    {
                        title = new StringBuilder();
                        title.Append(post.Title.Substring(0, limit));
                        title.Append("...");
                        post.Title = title.ToString();
                    }
                }
            }
            catch (Exception)
            {

            }
            return posts;
        }

        public PagedList<Post> SetDescriptionLimit(PagedList<Post> posts, int limit)
        {
            StringBuilder description = null;
            try
            {
                foreach (var post in posts)
                {
                    if (post.Description.Length > limit)
                    {
                        description = new StringBuilder();
                        description.Append(post.Description.Substring(0, limit));
                        description.Append("...");
                        post.Description = description.ToString();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return posts;
        }

        public PagedList<Post> GetPosts(int page, int pageSize, int categoryId, bool ordering, string seacrhKeyword = "")
        {
            PagedList<Post> pagedPosts = null;
            try
            {

                if (String.IsNullOrEmpty(seacrhKeyword))
                {
                    if (ordering)
                    {
                        if (categoryId == 0)
                        {
                            pagedPosts = new PagedList<Post>(_postDal.GetAll().OrderByDescending(p => p.Viewing), page, pageSize);
                        }
                        else
                        {
                            pagedPosts = new PagedList<Post>(_postDal.GetAll().Where(p => p.CategoryId == categoryId).OrderByDescending(p => p.Viewing), page, pageSize);
                        }
                    }
                    else
                    {
                        if (categoryId == 0)
                        {
                            pagedPosts = new PagedList<Post>(_postDal.GetAll().OrderByDescending(p => p.PostDate), page, pageSize);
                        }
                        else
                        {
                            pagedPosts = new PagedList<Post>(_postDal.GetAll().Where(p => p.CategoryId == categoryId).OrderByDescending(p => p.PostDate), page, pageSize);
                        }
                    }
                }
                else
                {
                    if (ordering)
                    {
                        if (categoryId == 0)
                        {
                            pagedPosts = new PagedList<Post>(_postDal.GetAll().Where(p => p.Title.Contains(seacrhKeyword)).OrderByDescending(p => p.Viewing), page, pageSize);
                        }
                        else
                        {
                            pagedPosts = new PagedList<Post>(_postDal.GetAll().Where(p => p.CategoryId == categoryId && p.Title.Contains(seacrhKeyword)).OrderByDescending(p => p.Viewing), page, pageSize);
                        }
                    }
                    else
                    {
                        if (categoryId == 0)
                        {
                            pagedPosts = new PagedList<Post>(_postDal.GetAll().Where(p => p.Title.Contains(seacrhKeyword)).OrderByDescending(p => p.PostDate), page, pageSize);
                        }
                        else
                        {
                            pagedPosts = new PagedList<Post>(_postDal.GetAll().Where(p => p.CategoryId == categoryId && p.Title.Contains(seacrhKeyword)).OrderByDescending(p => p.PostDate), page, pageSize);
                        }
                    }
                }
                foreach (var post in pagedPosts)
                {
                    post.Images = _imageDal.GetAll(x => x.PostId == post.PostId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return pagedPosts;
        }

    }
}
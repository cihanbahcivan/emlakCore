using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete;

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
                    post.Images = _imageDal.GetAll().Where(x => x.PostId == post.PostId).ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return posts;
        }

        public string ProduceCode(List<Post> posts)
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
            bool result = true;
            Post post = null;
            try
            {
                post = _postDal.GetAll().FirstOrDefault(c => c.Code == code);
                if (post != null)
                {
                    result = false;
                }
            }
            catch (Exception)
            {
                result = true;
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

        public List<Post> SetTitleLimit(List<Post> posts, int limit)
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

        public List<Post> SetDescriptionLimit(List<Post> posts, int limit)
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

        public List<Post> GetPosts(int page, int pageSize, int categoryId, bool ordering, string seacrhKeyword = "")
        {
            List<Post> pagedPosts = null;
            try
            {

                if (String.IsNullOrEmpty(seacrhKeyword))
                {
                    if (ordering)
                    {
                        if (categoryId == 0)
                        {
                            pagedPosts = _postDal.GetAll().OrderByDescending(p => p.Viewing).ToList();
                        }
                        else
                        {
                            pagedPosts = _postDal.GetAll().Where(p => p.CategoryId == categoryId).OrderByDescending(p => p.Viewing).ToList();
                        }
                    }
                    else
                    {
                        if (categoryId == 0)
                        {
                            pagedPosts = _postDal.GetAll().OrderByDescending(p => p.PostDate).ToList();
                        }
                        else
                        {
                            pagedPosts = _postDal.GetAll().Where(p => p.CategoryId == categoryId).OrderByDescending(p => p.PostDate).ToList();
                        }
                    }
                }
                else
                {
                    if (ordering)
                    {
                        if (categoryId == 0)
                        {
                            pagedPosts = _postDal.GetAll().Where(p => p.Title.Contains(seacrhKeyword)).OrderByDescending(p => p.Viewing).ToList();
                        }
                        else
                        {
                            pagedPosts = _postDal.GetAll().Where(p => p.CategoryId == categoryId && p.Title.Contains(seacrhKeyword)).OrderByDescending(p => p.Viewing).ToList();
                        }
                    }
                    else
                    {
                        if (categoryId == 0)
                        {
                            pagedPosts = _postDal.GetAll().Where(p => p.Title.Contains(seacrhKeyword)).OrderByDescending(p => p.PostDate).ToList();
                        }
                        else
                        {
                            pagedPosts = _postDal.GetAll().Where(p => p.CategoryId == categoryId && p.Title.Contains(seacrhKeyword)).OrderByDescending(p => p.PostDate).ToList();
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
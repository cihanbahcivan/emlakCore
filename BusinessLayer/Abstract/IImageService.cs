using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IImageService
    {
        public int Add(Image image);
        public int Update(Image image);
        public int Delete(Image image);
        public Image GetById(int id);
        public List<Image> GetAll();
        public List<Image> GetImagesByPostId(int postId);
    }
}
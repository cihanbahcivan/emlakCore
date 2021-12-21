using System;
using System.Collections.Generic;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class ImageManager : IImageService
    {
        private readonly IImageDal _imageDal;

        public ImageManager(IImageDal imageDal)
        {
            _imageDal = imageDal;
        }
        public int Add(Image image)
        {
            try
            {
                _imageDal.Add(image);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }

            return 1;
        }

        public int Update(Image image)
        {
            try
            {
                _imageDal.Update(image);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }

            return 1;
        }

        public int Delete(Image image)
        {
            try
            {
                _imageDal.Delete(image);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
            return 1;
        }

        public Image GetById(int id)
        {
            return _imageDal.Get(x => x.ImageId == id);
        }

        public List<Image> GetAll()
        {
            return _imageDal.GetAll();
        }

        public List<Image> GetImagesByPostId(int postId)
        {
            return _imageDal.GetAll(x => x.PostId == postId);
        }
    }
}
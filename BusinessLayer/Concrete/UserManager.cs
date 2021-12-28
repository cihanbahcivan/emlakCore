using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public int Add(User user)
        {
            try
            {
                _userDal.Add(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }

            return 1;
        }
        public int Update(User user)
        {

            try
            {
                _userDal.Update(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }

            return 1;
        }

        public int Delete(User user)
        {
            try
            {
                _userDal.Delete(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }

            return 1;
        }

        public List<User> GetAll()
        {
            List<User> users = null;
            try
            {
                users = _userDal.GetAll();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return users;
        }

        public User GetById(int id)
        {
            User user = null;
            try
            {
                user = _userDal.Get(x => x.UserId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return user;
        }

        public bool IsUser(string userName, string password)
        {
            try
            {
                var user = _userDal.Get(x => x.Name == userName && x.Password == password);
                if (user != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

        }
        
    }
}
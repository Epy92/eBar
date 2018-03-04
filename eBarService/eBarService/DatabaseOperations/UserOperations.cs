using System;
using System.Linq;
using eBarService.Interfaces;
using eBarService.Messages;

namespace eBarService.DatabaseOperations
{
    public class UserOperations : IDisposable, IUserOperations
    {
        private eBarEntities _eBarEntities;
        public UserOperations()
        {
            _eBarEntities = new eBarEntities();
        }

        public bool IsUserValid(string usernameOrEmail, string password)
        {
            bool result = false;
            using (var _eBarEntities = new eBarEntities())
            {
                result = _eBarEntities.UserTbl.FirstOrDefault(x =>( x.Username == usernameOrEmail || x.Email == usernameOrEmail) && x.UserPassword == password) != null;
            }
            return result;
        }

        public string RegisterUser(UserTbl userToRegister)
        {
            string registerMessage = null;
            using (var _eBarEntities = new eBarEntities())
            {
                if (!UserAlreadyExist(userToRegister))
                {
                    _eBarEntities.UserTbl.Add(userToRegister);
                    _eBarEntities.SaveChanges();
                    registerMessage = UserMessages.UserRegisteredSuccesfully;
                }
                else
                {
                    registerMessage = UserMessages.DuplicateUser;
                }
            }
            return registerMessage;
        }

        private bool UserAlreadyExist(UserTbl userToRegister)
        {
            return _eBarEntities.UserTbl.FirstOrDefault(x => x.Username == userToRegister.Username || x.Email == userToRegister.Email) != null;
        }

        public void Dispose()
        {
            _eBarEntities.Dispose();
            _eBarEntities = null;
        }
    }
}
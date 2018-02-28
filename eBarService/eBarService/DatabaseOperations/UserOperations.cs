using System;
using System.Linq;
using eBarService.Messages;

namespace eBarService.DatabaseOperations
{
    public class UserOperations : IDisposable
    {
        private eBarEntities _eBarEntities;
        public UserOperations()
        {
            _eBarEntities = new eBarEntities();
        }
        public bool IsUserValid(UserTbl user)
        {
            bool result = false;
            using (var context = _eBarEntities)
            {
                result = context.UserTbl.FirstOrDefault(x =>( x.Username == user.Username || x.Email == user.Email) && x.UserPassword == user.UserPassword) != null;
            }
            return result;
        }

        public string RegisterUser(UserTbl userToRegister)
        {
            string registerMessage = null;
            using (var context = _eBarEntities)
            {
                if (!UserAlreadyExist(userToRegister))
                {
                    context.UserTbl.Add(userToRegister);
                    context.SaveChanges();
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
            bool result = false;
            using (var context = _eBarEntities)
            {
                result = context.UserTbl.FirstOrDefault(x => x.Username == userToRegister.Username || x.Email == userToRegister.Email) != null;
            }
            return result;
        }

        public void Dispose()
        {
            _eBarEntities.Dispose();
            _eBarEntities = null;
        }
    }
}
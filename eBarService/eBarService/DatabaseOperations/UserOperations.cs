﻿using System;
using System.Linq;
using eBarService.Interfaces;
using eBarService.Messages;

namespace eBarService.DatabaseOperations
{
    public class UserOperations : IUserOperations
    {
        public bool IsUserValid(string usernameOrEmail, string password)
        {
            bool result = false;
            using (var context = new eBarEntities())
            {
                result = context.UserTbl.FirstOrDefault(x =>( x.Username == usernameOrEmail || x.Email == usernameOrEmail) && x.UserPassword == password) != null;
            }
            return result;
        }

        public string RegisterUser(UserTbl userToRegister)
        {
            string registerMessage = null;
            using (var context = new eBarEntities())
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
            using (var context = new eBarEntities())
            {
                return context.UserTbl.FirstOrDefault(x => x.Username == userToRegister.Username || x.Email == userToRegister.Email) != null;
            }
            
        }
    }
}
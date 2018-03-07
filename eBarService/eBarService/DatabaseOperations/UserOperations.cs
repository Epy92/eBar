using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

        public void GenerateResetCode(string userOrEmail, ref string message)
        {
            using (var context = new eBarEntities())
            {
                var userDetails = context.UserTbl.FirstOrDefault(x => x.Username == userOrEmail || x.Email == userOrEmail);
                UserResetPasswordCodes resetPassword = new UserResetPasswordCodes();

                if (userDetails != null)
                {
                    var userResetCode = context.UserResetPasswordCodes.FirstOrDefault(x => x.UserID == userDetails.UserID);
                    if (userResetCode != null)
                    {
                        resetPassword.CreationDate = DateTime.Now;
                        resetPassword.UserID = userDetails.UserID;
                        resetPassword.ResetCode = Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
                        context.UserResetPasswordCodes.Add(resetPassword);
                        context.SaveChanges();
                        SendEmailResetCode(resetPassword.ResetCode, userDetails);
                    }
                    else
                    {
                        message = UserMessages.ResetCodeAlreadyGenerated;
                    }
                }
                else
                {
                    message = UserMessages.MissingUser;
                }
            }
        }

        public string ResetUserPassword(string username, string resetCode, string newPassword)
        {
            string message;
            using (var context = new eBarEntities())
            {
                var user = context.UserTbl.FirstOrDefault(x => x.Username == username || x.Email == username);
                if (user == null)
                {
                    message = UserMessages.MissingUser;
                    return message;
                }

                var userResetCode = context.UserResetPasswordCodes.FirstOrDefault(x => x.UserID == user.UserID);
                if ((userResetCode == null) || ((userResetCode.CreationDate - DateTime.Now).Minutes < 10))
                {
                    message = UserMessages.ResetCodeInvalid;
                }
                else
                {
                    if (userResetCode.ResetCode == resetCode)
                    {
                        user.UserPassword = newPassword;
                        context.Entry(user).State = EntityState.Modified;
                        context.SaveChanges();
                        message = UserMessages.PasswordChanged;
                    }
                    else
                    {
                        message = UserMessages.ResetCodeInvalid;
                    }
                }
            }
            return message;
        }

        private void SendEmailResetCode(string resetCode, UserTbl userDetails)
        {
            try
            {
                var mail = new MailMessage();

                // Set the to and from addresses.
                // The from address must be your GMail account
                mail.From = new MailAddress("ebartechnologies@gmail.com");
                mail.To.Add(new MailAddress(userDetails.Email));

                // Define the message
                mail.Subject = "Reset your eBar password";
                mail.IsBodyHtml = false;
                mail.Body = "Dear " + userDetails.Name + @", \n\n" + "Your reset code is " + resetCode + @". The reset code is available only 24 hours.\n\n";

                // Create a new Smpt Client using Google's servers
                var mailclient = new SmtpClient();
                mailclient.Host = "smtp.gmail.com";
                mailclient.Port = 587;

                // This is the critical part, you must enable SSL
                mailclient.EnableSsl = true;

                // Specify your authentication details
                mailclient.Credentials = new System.Net.NetworkCredential(
                                                 "ebartechnologies@gmail.com",
                                                 "ebarservices1!");
                mailclient.Send(mail);
            }
            catch (Exception ex)
            {

            }
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
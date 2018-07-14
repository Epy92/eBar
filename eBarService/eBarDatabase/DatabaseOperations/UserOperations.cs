using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Newtonsoft.Json;
using ViewModels;

namespace eBarDatabase
{
    public class UserOperations : IUserOperations
    {
        private readonly IDatabaseLogger _logger;
        public UserOperations()
        {
            _logger = new  DatabaseLogger();
        }
        public UserTbl GetUser(string usernameOrEmail, string password)
        {
            using (var context = new DBModels())
            {
                return context.UserTbl.FirstOrDefault(x => (x.Username == usernameOrEmail || x.Email == usernameOrEmail) && x.UserPassword == password);
            }
        }

        public string RegisterUser(UserTbl userToRegister)
        {
            string registerMessage;
            using (var context = new DBModels())
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

        public void GenerateResetCode(string userOrEmail, out string message, out bool responseFlag)
        {
            try
            {
                message = string.Empty;
                using (var context = new DBModels())
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
                            responseFlag = true;
                        }
                        else
                        {
                            message = UserMessages.ResetCodeAlreadyGenerated;
                            responseFlag = false;
                        }
                    }
                    else
                    {
                        message = UserMessages.MissingUser;
                        responseFlag = false;
                    }
                }
            }
            catch (Exception ex)
            {
                message = UserMessages.UnexpectedError;
                responseFlag = false;
                _logger.Log("GenerateResetCode_UserOperations Exception: ", ex.Message);
            }
        }

        public string ResetUserPassword(string username, string resetCode, string newPassword)
        {
            string message;
            try
            {
                using (var context = new DBModels())
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
            }
            catch (Exception ex)
            {
                _logger.Log("ResetUserPassword_UserOperations Exception: ", ex.Message);
                message = UserMessages.UnexpectedError;
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
                mailclient.Credentials = new NetworkCredential(
                                                 "ebartechnologies@gmail.com",
                                                 "ebarservices1!");
                mailclient.Send(mail);
            }
            catch (Exception ex)
            {
                _logger.Log("SendEmailResetCode_Exception: ", ex.Message);
            }
        }

        private bool UserAlreadyExist(UserTbl userToRegister)
        {
            using (var context = new DBModels())
            {
                return context.UserTbl.FirstOrDefault(x => x.Username == userToRegister.Username || x.Email == userToRegister.Email) != null;
            }
        }
        public string CheckAdminHasRestaurant(string username, string email)
        {
            bool adminHasRestaurants = false;

            if (username != string.Empty || email != string.Empty)
            {
                using (var context = new DBModels())
                {
                    adminHasRestaurants = (from user in context.UserTbl.Where(x => x.Username == username || x.Email == email)
                                           from usertype in context.UserTypes.Where(x => x.UserTypeId == user.UserID)
                                           from restadmin in context.RestaurantAdministrators.Where(x => x.UserID == user.UserID)
                                           where usertype.TypeName == "RestaurantAdministrator"
                                           select new
                                           {   user.Name,
                                               usertype.TypeName,
                                               restadmin.RestaurantId
                                           }) != null;
                }
            }
            string returnvalue = JsonConvert.SerializeObject(adminHasRestaurants);
            return returnvalue;
        }
    }
}
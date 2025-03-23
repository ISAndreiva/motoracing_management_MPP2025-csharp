using System;
using System.Security.Cryptography;
using System.Text;
using log4net;

namespace ConcursMotociclism.Utils
{
    public class PasswordHasher
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));

        public static string HashPassword(string password, string username)
        {
            Logger.Info("Hashing password for user: " + username);
            var salt = new byte[16];
            Array.Copy(Encoding.UTF8.GetBytes(username), salt, Math.Min(username.Length, 16));

            var hashedPassword = "";
            try
            {
                using (SHA512 sha512 = SHA512.Create())
                {
                    sha512.TransformBlock(salt, 0, salt.Length, salt, 0);
                    byte[] bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(password));
                    StringBuilder sb = new StringBuilder();
                    foreach (byte b in bytes)
                    {
                        sb.Append(b.ToString("x2"));
                    }
                    hashedPassword = sb.ToString();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            return hashedPassword;
        }
    }
}
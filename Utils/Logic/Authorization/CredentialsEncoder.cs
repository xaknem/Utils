using System;

namespace Utils.Logic.Authorization
{
    internal class CredentialsEncoder
    {
        /// <summary>
        /// Encode login/password in base64 for RequestHeader(base authorization)
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        private string Encode(Credentials credential)
        {
            if (credential == null || credential.Username == null || credential.Password == null)
            {
                throw new NullReferenceException("Не указаны данные для авторизации");
            }
            String encoded = Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1")
                .GetBytes(credential.Username + ":" + credential.Password));
            return encoded;
        }
    }
}
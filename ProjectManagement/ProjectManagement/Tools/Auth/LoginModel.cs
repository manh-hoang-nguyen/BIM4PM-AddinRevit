﻿namespace BIM4PM.UI.Tools.Auth
{
    using System.Net;

    /// <summary>
    /// Defines the <see cref="LoginModel" />
    /// </summary>
    public class LoginModel
    {
        private bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}

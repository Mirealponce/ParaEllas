using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingStore.Models
{
    public class PaypalConfiguration
    {

        public readonly static string clientId;
        public readonly static string clientSecret;
        static PaypalConfiguration()
        {
            var config = getconfig();
            clientId = "Abs_OG0BnB8sGBG7oQ5bU3_9EpNUh20Oq8vHKZfea08Iy6292KhP42ajvb3noVY3eaXDNQYrDv-22xzs";
            clientSecret = "ED3URbdx9R9T4NUy0lrsAE2YzuW_iO02Zveqas9har4ns67kIYG8_GQQ3B3MTkLv4pt8tA6LiO_LPMDj";
        }

        private static Dictionary<string,string> getconfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(clientId, clientSecret,getconfig()).GetAccessToken();
            return accessToken;
        }
         
        public static APIContext GetAPIContext()
        {
            APIContext apicontext = new APIContext(GetAccessToken());
            apicontext.Config = getconfig();
            return apicontext;
        }









    }
}
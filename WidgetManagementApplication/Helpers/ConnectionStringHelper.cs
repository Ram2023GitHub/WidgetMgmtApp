using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WidgetManagementApplication.Helpers
{
    public static class ConnectionStringHelper
    {
        private static string EncryptedConnectionString;
        private static string DecryptedConnectionString;

        public static void Initialize()
        {
            EncryptedConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            DecryptedConnectionString = CryptographyHelper.Decrypt(EncryptedConnectionString);
        }

        public static string GetConnectionString()
        {
            return DecryptedConnectionString;
        }
    }
}
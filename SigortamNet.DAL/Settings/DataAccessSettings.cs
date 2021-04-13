

using System.Collections.Generic;

namespace SigortamNet.DAL.Settings
{
    public class DataAccessSettings
    {
        public static string DefaultDbConnectionString { get; set; }
        public static string DefaultDBName { get; set; }
        public List<string> CompanyUrls { get; set; }
    }
}

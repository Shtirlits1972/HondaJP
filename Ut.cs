using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace HondaJP
{
    public class Ut
    {


        public static string GetMySQLConnect(string lang = "EN")
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfiguration Configuration = builder.Build();

            List<string> indexList = GetIndexOfConnectString();

            string strConnect = string.Empty;

            if (!String.IsNullOrEmpty(lang))
            {
                if(indexList.Contains(lang))
                {
                    strConnect = Configuration.GetConnectionString(lang);
                }
                else if (indexList.Count > 0)
                {
                    lang = indexList[0];
                }
            }
            else if (indexList.Count > 0)
            {
                lang = indexList[0];
            }

            return strConnect;
        }


        public static List<string>GetIndexOfConnectString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfiguration Configuration = builder.Build();

            List<string> indexList = new List<string>();

            var connections = Configuration.GetSection("ConnectionStrings").GetChildren().ToList();

            foreach(var conn in connections)
            {
                indexList.Add(conn.Key);
            }
            return indexList;
        }


        public static string GetImagePath()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfiguration Configuration = builder.Build();

            return Configuration.GetSection("MySettings").GetSection("imagePath").Value;
        }
    }
}

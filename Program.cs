using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SawaVideoPlayer
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string file = "";
            if (args.Length > 0) file = args[0];
            string token = getParamter(file, "token");
            if (token == "" || !ValidateToken(token))
            {
                Application.Run(new Form1(error: "Unauthorized"));
            }
            else Application.Run(new Form1(link: getParamter(file, "link")));
        }
        public static string getParamter(string source,string paramterName)
        {
            int Start, End;
            Start = source.IndexOf(paramterName, 0);
            if (Start == -1) return "";
            Start += paramterName.Length+1;
            End = source.IndexOf(";", Start);
            return source.Substring(Start, End - Start);
        }
        private static bool ValidateToken(string token)
        {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["Key"]));

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = ConfigurationManager.AppSettings["Issuer"],
                    ValidAudience = ConfigurationManager.AppSettings["Audience"],
                    IssuerSigningKey = mySecurityKey,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static string GenerateToken()
        {
            string key = ConfigurationManager.AppSettings["Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(ConfigurationManager.AppSettings["Issuer"],
                ConfigurationManager.AppSettings["Audience"],
                expires: DateTime.Now.AddMinutes(14*60 * 24),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

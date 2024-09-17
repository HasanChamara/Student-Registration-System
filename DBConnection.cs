using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationSystem
{
    internal class DBConnection
    {
        public string MyConnection()
        {
            string conn = @"Data Source=DESKTOP-REGH4GI\SQLEXPRESS;Initial Catalog=Student;Integrated Security=True;";
            return conn;
        }
    }
}

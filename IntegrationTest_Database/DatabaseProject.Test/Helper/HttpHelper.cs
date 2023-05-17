using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProject.Test.Helper
{
    internal class HttpHelper
    {
        public static StringContent GetJsonHttpContent(object items)
        {
            return new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json");
        }

        internal static class Urls
        {
            public readonly static string GetAllStudents = "/api/Student/GetAllAsync";
            public readonly static string GetStudent = "/api/Student/GetAsync";
            public readonly static string AddStudent = "/api/Student/AddAsync";
            public readonly static string EditStudent = "/api/Student/UpdateAsync";
            public readonly static string DeleteStudents = "/api/Student/DeleteAsync";
        }
    }
}

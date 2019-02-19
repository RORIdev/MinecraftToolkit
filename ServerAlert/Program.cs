using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServerAlert
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient cl = new HttpClient();
            cl.GetAsync("https://mcapi.us/server/status?ip=earthmc.net");

        }
    }
}

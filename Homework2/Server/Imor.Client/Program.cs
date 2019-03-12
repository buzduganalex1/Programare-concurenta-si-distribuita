using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Imor.Business;
using Imor.Client.Tests;
using Imor.Database;

namespace Imor.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();

            //new TagsRepositoryTests().Run();
            new ImageRepositoryTests().Run();

            sw.Stop();

            Console.WriteLine("Elapsed={0}", sw.Elapsed);
            //new ApiTests().Run();            
        }
    }
}

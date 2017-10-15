using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MskManager.Scrapper.Test.Helpers
{
    public class FileLoader
    {
        private static readonly Assembly _executingAssembly;
        static FileLoader()
        {
            _executingAssembly = Assembly.GetExecutingAssembly();
        }

        public static string Get(string filename)
        {
            var fs = _executingAssembly.GetManifestResourceStream($"MskManager.Scrapper.Test.{filename}");

            using (var str = new StreamReader(fs))
            {
                return str.ReadToEnd();
            }
        }
    }
}

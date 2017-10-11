using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace MskManager.Common.Test.Nancy
{
    public class TestingRootPathProvider<T> : IRootPathProvider where T : DefaultNancyBootstrapper
    {
        private static readonly string RootPath;

        static TestingRootPathProvider()
        {
            var assembly = typeof(T).Assembly;
            var directoryName = Path.GetDirectoryName(assembly.CodeBase);
            var assemblyName = assembly.GetName();

            if (directoryName != null)
            {
                var assemblyPath = directoryName.Replace(@"file:\", string.Empty);
                RootPath = Path.Combine(assemblyPath, "..", "..", "..", assemblyName.Name);
            }
        }

        public string GetRootPath()
        {
            return RootPath;
        }
    }
}

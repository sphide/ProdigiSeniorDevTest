using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ColourMatcher.Tests.ExtensionMethods
{
    //Enables support for embedded resources in test projects
    //https://www.codeproject.com/Tips/5256504/Using-Embedded-Resources-in-Unit-Tests-with-NET
    public static class AssemblyExtensions
    {
        public static Stream GetEmbeddedResourceStream(this Assembly assembly, string relativeResourcePath)
        {
            if (string.IsNullOrEmpty(relativeResourcePath))
                throw new ArgumentNullException("relativeResourcePath");

            var resourcePath = String.Format("{0}.{1}",
                Regex.Replace(assembly.ManifestModule.Name, @"\.(exe|dll)$",
                      string.Empty, RegexOptions.IgnoreCase), relativeResourcePath);

            var stream = assembly.GetManifestResourceStream(resourcePath);
            if (stream == null)
                throw new ArgumentException($"The specified embedded resource {relativeResourcePath} is not found.");
            return stream;
        }
    }
}

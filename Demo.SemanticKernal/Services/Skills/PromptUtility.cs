using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demo.SemanticKernal.Services.Skills
{
    public static class PromptUtility
    {
        public static string GetPromptByName(string prompt)
        {
            var resourceName = $"Demo.SemanticKernal.Services.Skills.{prompt}.txt";
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new ArgumentException($"The resource {resourceName} was not found.");
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}

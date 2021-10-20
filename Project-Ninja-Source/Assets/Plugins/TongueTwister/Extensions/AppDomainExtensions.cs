using System.Collections.Generic;

namespace TongueTwister.Extensions
{
    /// <summary>
    /// Provides utility for ease of use with <see cref="System.AppDomain"/>.
    /// </summary>
    public static class AppDomainExtensions
    {
        /// <summary>
        /// Gets all derived types from the given type.
        /// </summary>
        /// <param name="appDomain">This app domain.</param>
        /// <param name="type">The type to get all derived types of.</param>
        /// <returns>All derived <c>System.Type</c> references.</returns>
        public static System.Type[] GetAllDerivedTypes(this System.AppDomain appDomain, System.Type type)
        {
            var result = new List<System.Type>();
            var assemblies = appDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var allTypes = assembly.GetTypes();
                foreach (var givenType in allTypes)
                {
                    if (givenType.IsSubclassOf(type) && !givenType.IsAbstract)
                    {
                        result.Add(givenType);
                    }
                }
            }
            return result.ToArray();
        }
    }
}
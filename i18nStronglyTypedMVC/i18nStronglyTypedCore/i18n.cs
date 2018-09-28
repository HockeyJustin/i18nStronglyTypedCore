using i18nStronglyTypedCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;

namespace i18nStronglyTypedCore
{
    public abstract class i18n
    {
        #region "Standard content - do not change"
        private static IResourceProvider resourceProvider;

        private static IList<ResourceEntry> _resources;

        public static void InitResources(string pathToXml)
        {
            InitResources(new string[] { pathToXml });
        }

        public static void InitResources(string[] pathToXml)
        {
            resourceProvider =
             new XmlResourceProvider(pathToXml);
            _resources = ((XmlResourceProvider)resourceProvider).ReadResources();
        }

        public static void ReloadResources(string pathToXml)
        {
            InitResources(pathToXml);
        }

        public static void ReloadResources(string[] pathToXml)
        {
            InitResources(pathToXml);
        }

        public static string GetLocalisedStringValue<T>(Expression<Func<T>> propertyLambda, string cultureName)
        {
            var me = propertyLambda.Body as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }

            var propertyName = me.Member.Name;

            if (String.IsNullOrWhiteSpace(cultureName))
            {
                cultureName = "en-GB";
            }

            // Try to get a value in the locale requested.
            var returnValue = _resources.FirstOrDefault(i => i.Name == propertyName && i.Culture == cultureName)?.Value;

            //  No return value for the culture, fall back to english
            returnValue = returnValue ?? _resources.FirstOrDefault(i => i.Name == propertyName && i.Culture == "en-GB")?.Value;

            // Still no value, show a really obvious error.
            returnValue = returnValue ?? $"(RESOURCE NOT FOUND FOR CULTURE {CultureInfo.CurrentUICulture.Name})";

            return returnValue;
        }

        /// <summary>
        /// Get the value of a resource based on the current culture.
        /// </summary>
        /// <param name="propertyName">The calling property.</param>
        /// <returns>the value of a resource based on the current culture</returns>
        public static string GetStringValue([CallerMemberName] string propertyName = null)
        {
            // Try to get a value in the locale requested.
            var returnValue = _resources.FirstOrDefault(i => i.Name == propertyName && i.Culture == CultureInfo.CurrentUICulture.Name)?.Value;

            //  No return value for the culture, fall back to english
            returnValue = returnValue ?? _resources.FirstOrDefault(i => i.Name == propertyName && i.Culture == "en-GB")?.Value;

            // Still no value, show a really obvious error.
            returnValue = returnValue ?? $"(RESOURCE NOT FOUND FOR CULTURE {CultureInfo.CurrentUICulture.Name})";

            return returnValue;
        }

        public static List<string> GetAllResourceCultures()
        {
            return _resources.Select(_ => _.Culture).Distinct().OrderBy(_ => _).ToList();
        }




        #endregion




    }
}

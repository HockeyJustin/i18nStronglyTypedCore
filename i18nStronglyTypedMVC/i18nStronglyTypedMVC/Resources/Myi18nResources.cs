using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace i18nStronglyTypedMVC.Resources
{
    public class Myi18nResources : i18nStronglyTypedCore.i18n
    {
        public static string Site_Name { get { return GetStringValue(); } }
    }
}

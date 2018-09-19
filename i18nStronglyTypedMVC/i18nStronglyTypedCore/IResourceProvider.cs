using System;
using System.Collections.Generic;
using System.Text;

namespace i18nStronglyTypedCore
{
    public interface IResourceProvider
    {
        object GetResource(string name, string culture);
    }
}

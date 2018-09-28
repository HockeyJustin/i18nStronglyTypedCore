# i18nStronglyTypedCore

This is the source code for [the i18nStronglyTypedCore nuget package](https://www.nuget.org/packages/i18nStronglyTypedCore).

[Github source code](https://github.com/HockeyJustin/i18nStronglyTypedCore)

## About

This is a .net core port of [afana's](http://afana.me/archive/2013/11/01/aspnet-mvc-internationalization-store-strings-in-database-or-xml.aspx/) excellent i18n (internationalization) provider for MVC 5, using xml for the resource strings. 

There's also some extras, such as per request language responses and multiple resource file handling (see tutorial below).
 
Of course, dotnet core has its' own [i18n provider](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-2.1) 
but like many people, I'd rather keep things strongly typed as much as possible. It's simple to get up and running.

The upsides to this package are:
1. Strongly typed
2. Seems pretty solid
3. Only takes a couple of minutes to get up and running.
4. Can handle multiple resource files.
5. Can handle "per request" language responses (see tutorial)

The downsides to this package are:
1. *Doesn't work with Attributes (the standard .net core one does)
2. If you create the c# part, but forget to put anything in the xml, the error is a bit tricky to work out.

## Prerequisites

- Dotnet core 2.1+

## To get up and running (quick tutorial)

1. Create an asp.net core application. 
- In Visual Studio this is:
- File > New > Project > Web Tab > Asp.NET Core Web Application

- Give it a name (and location, not the one used in the pic) and hit ok

![alt tag](https://github.com//HockeyJustin/i18nStronglyTypedCore/blob/master/i18nStronglyTypedMVC/i18nStronglyTypedMVC/wwwroot/images/1.png?raw=true)

c. Choose the mvc option (which this tutorial uses) and hit ok.

![alt tag](https://github.com//HockeyJustin/i18nStronglyTypedCore/blob/master/i18nStronglyTypedMVC/i18nStronglyTypedMVC/wwwroot/images/2.png?raw=true)


2. Via the CLR or Package Manager (View > Other Windows > Package Manager Console) enter the following, then hit enter:

`Install-Package i18nStronglyTypedCore`

![alt tag](https://github.com//HockeyJustin/i18nStronglyTypedCore/blob/master/i18nStronglyTypedMVC/i18nStronglyTypedMVC/wwwroot/images/3.png?raw=true)

3. In wwwroot, add a folder called Resources and in that add an xml file called Resources.xml

![alt tag](https://github.com//HockeyJustin/i18nStronglyTypedCore/blob/master/i18nStronglyTypedMVC/i18nStronglyTypedMVC/wwwroot/images/4.png?raw=true)

4. In resources.xml, insert the following placeholder content:

> *WARNING*: The following example has the default culture to be British English (en-GB). You may need `en-US` or [another culture](https://msdn.microsoft.com/en-us/library/hh441729.aspx).

```
<?xml version="1.0" encoding="utf-8" ?>
<resources>
    <!-- ENGLISH -->
    <resource culture="en-GB" type="string" name="Site_Name" value="Site name (english)"></resource>
    
    <!-- FRENCH -->
    <resource culture="fr-FR" type="string" name="Site_Name" value="Site name (Francais)"></resource>

</resources>
```

![alt tag](https://github.com//HockeyJustin/i18nStronglyTypedCore/blob/master/i18nStronglyTypedMVC/i18nStronglyTypedMVC/wwwroot/images/4a.png?raw=true)

5. Add a top level folder called 'Resources' (not in wwwroot) and create a class called `i18n`.

6. Have that class inherit from `i18nStronglyTypedCore.i18n` and include the following property in the class.

`public static string Site_Name { get { return GetStringValue(); } }`


![alt tag](https://github.com//HockeyJustin/i18nStronglyTypedCore/blob/master/i18nStronglyTypedMVC/i18nStronglyTypedMVC/wwwroot/images/5.png?raw=true)

7. In startup.cs 'Configure' method, add the following 2 lines to initialise the resources.

```
var pathToResources = System.IO.Path.Combine(env.WebRootPath, @"Resources\Resources.xml");
Resources.i18n.InitResources(pathToResources);
```

> NOTE: If yo have more than one resource file, you can add multiple files as shown below (but don't copy for this tutorial, as we only have 1 resource file)  

```
var pathToResourcesEn = System.IO.Path.Combine(env.WebRootPath, @"Resources\Resources.xml");
var pathToResourcesFr = System.IO.Path.Combine(env.WebRootPath, @"Resources\Resources-fr.xml");
Resources.i18n.InitResources(new string[] { pathToResourcesEn, pathToResourcesFr });
```


![alt tag](https://github.com//HockeyJustin/i18nStronglyTypedCore/blob/master/i18nStronglyTypedMVC/i18nStronglyTypedMVC/wwwroot/images/6.png?raw=true)

8. In views/home/index, remove the standard content and add the following (remember to replace 'Testweb' with you own website's namespace).

```
@{
    ViewData["Title"] = "Home Page";
}
<h4>English Site Name</h4>
<p>@Testweb.Resources.i18n.Site_Name</p>

<h4>French Site Name</h4>
<p>@Testweb.Resources.i18n.GetLocalisedStringValue(() => Testweb.Resources.i18n.Site_Name, "fr-FR")</p>
```

![alt tag](https://github.com//HockeyJustin/i18nStronglyTypedCore/blob/master/i18nStronglyTypedMVC/i18nStronglyTypedMVC/wwwroot/images/7.png?raw=true)

9. Run the website. The end result should look like this.

NOTE: The provider will take from the current culture by default.

![alt tag](https://github.com//HockeyJustin/i18nStronglyTypedCore/blob/master/i18nStronglyTypedMVC/i18nStronglyTypedMVC/wwwroot/images/8.png?raw=true)

10. There are many ways to change the default culture. As a quick example, you could place the following in Startup.cs Configure method (before the resources code!)

```
var cultureInfo = new System.Globalization.CultureInfo("fr-FR");
System.Globalization.CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
```


![alt tag](https://github.com//HockeyJustin/i18nStronglyTypedCore/blob/master/i18nStronglyTypedMVC/i18nStronglyTypedMVC/wwwroot/images/9.png?raw=true)


11. Now run the site. The default culture will also be in french. Remove the work done in step 10 to get back to normal.

![alt tag](https://github.com//HockeyJustin/i18nStronglyTypedCore/blob/master/i18nStronglyTypedMVC/i18nStronglyTypedMVC/wwwroot/images/10.png?raw=true)


## To add more properties

1. Copy the `Site_Name` property in Resources/i18n.cs i.e. 

`public static string Site_Name { get { return GetStringValue(); } }` 

2. Paste it on a new line and change the proterty name e.g. 

```
namespace Testweb.Resources
{
    public class i18n : i18nStronglyTypedCore.i18n
    {
        public static string Site_Name { get { return GetStringValue(); } }

        public static string Hello_Text { get { return GetStringValue(); } }

        // Add more here...
    }
}
```

3. Go to the resources.xml file. Copy the `Site_Name` resource and paste below. Change the `name` attribute (`Site_Name`) 
to match the name of your new property (e.g. `Hello_Text`) and update the value e.g.

`<resource culture="en-GB" type="string" name="Hello_Text" value="Hello"></resource>`

4. For other languages, copy + paste your new row, remembering to change the `culture` attribute and value. e.g. 

`<resource culture="fr-FR" type="string" name="Hello_Text" value="Bonjour"></resource>`

5. So your final Resources.xml will look something like this:

```
<?xml version="1.0" encoding="utf-8" ?>
<resources>
    <!-- ENGLISH (this could be en-US) -->
    <resource culture="en-GB" type="string" name="Site_Name" value="Site name (english)"></resource>
    <resource culture="en-GB" type="string" name="Hello_Text" value="Hello"></resource>

    <!-- FRENCH -->
    <resource culture="fr-FR" type="string" name="Site_Name" value="Site name (Francais)"></resource>
    <resource culture="fr-FR" type="string" name="Hello_Text" value="Bonjour"></resource>

</resources>

```

6. And you would call it like this (Razor) (remember your namespace might be different to `Testweb`):

`@Testweb.Resources.i18n.Hello_Text`

7. Or like this (C#)

```
// Your default culture
var hello = Resources.i18n.Hello_Text;
// A request for the french resource
var bonjour = Resources.i18n.GetLocalisedStringValue(() => Testweb.Resources.i18n.Hello_Text, "fr-FR");
```

END


> Remember, you can split your resource files if needed (see section 7 of 'To get up and running' above). This tutorial has only used one file for simplicity.

Also, if you want to get a list of all the cultures (e.g. en-GB, fr-FR) available from the resources, use `Resources.i18n.GetAllResourceCultures();`






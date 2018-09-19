# i18nStronglyTypedCore

This is the source code for [the i18nStronglyTypedCore nuget package](https://www.nuget.org/packages/i18nStronglyTypedCore).

## About

It is a .net core port of [afana's](http://afana.me/archive/2013/11/01/aspnet-mvc-internationalization-store-strings-in-database-or-xml.aspx/) excellent
i18n provider for MVC 5. Of course, dotnet core has its' own [i18n provider](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-2.1) 
but like many people, I'd rather keep things strongly typed as much as possible. It's simple to get up and running.

## Prerequisites

- Dotnet core 2.1+

## To get up and running

1. Create an asp.net core application. 
- In Visual Studio this is:
a. File > New > Project > Web Tab > Asp.NET Core Web Application

b. Give it a name (and location, not the one used in the pic) and hit ok

![alt tag](https://github.com//HockeyJustin/i18nStronglyTypedCore/blob/master/i18nStronglyTypedMVC/i18nStronglyTypedMVC/wwwroot/images/1.png?raw=true)

c. Choose the mvc option (which this tutorial uses) and hit ok.

![alt tag](https://github.com//HockeyJustin/i18nStronglyTypedCore/blob/master/i18nStronglyTypedMVC/i18nStronglyTypedMVC/wwwroot/images/2.png?raw=true)


2. Via the CLR or Package Manager (View > Other Windows > Package Manager Console) enter the following, then hit enter:

`Install-Package i18nStronglyTypedCore`

![alt tag](https://github.com//HockeyJustin/i18nStronglyTypedCore/blob/master/i18nStronglyTypedMVC/i18nStronglyTypedMVC/wwwroot/images/3.png?raw=true)

3. In wwwroot, add a folder called Resources and in that add an xml file called Resources.xml

![alt tag](https://github.com//HockeyJustin/i18nStronglyTypedCore/blob/master/i18nStronglyTypedMVC/i18nStronglyTypedMVC/wwwroot/images/4.png?raw=true)

4. In resources.xml, insert the following placeholder content:

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

`
var pathToResources = System.IO.Path.Combine(env.WebRootPath, @"Resources\Resources.xml");
Resources.i18n.InitResources(pathToResources);

`
![alt tag](https://github.com//HockeyJustin/i18nStronglyTypedCore/blob/master/i18nStronglyTypedMVC/i18nStronglyTypedMVC/wwwroot/images/6.png?raw=true)

8. In views/home/index, remove the standard content and add the following (remember to replace 'Testweb' with you own website's namespace).


@{
    ViewData["Title"] = "Home Page";
}
<h4>English Site Name</h4>
<p>@Testweb.Resources.i18n.Site_Name</p>

<h4>French Site Name</h4>
<p>@Testweb.Resources.i18n.GetLocalisedStringValue(() => Testweb.Resources.i18n.Site_Name, "fr-FR")</p>


![alt tag](https://github.com//HockeyJustin/i18nStronglyTypedCore/blob/master/i18nStronglyTypedMVC/i18nStronglyTypedMVC/wwwroot/images/7.png?raw=true)

9. Run the website. The end result should look like this.

NOTE: The provider will take from the current culture by default.

![alt tag](https://github.com//HockeyJustin/i18nStronglyTypedCore/blob/master/i18nStronglyTypedMVC/i18nStronglyTypedMVC/wwwroot/images/8.png?raw=true)

10. There are many ways to change the default culture. As a quick example, you could place the following in Startup.cs Configure

```
var cultureInfo = new System.Globalization.CultureInfo("fr-FR");

System.Globalization.CultureInfo.DefaultThreadCurrentCulture = cultureInfo;

System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
```


![alt tag](https://github.com//HockeyJustin/i18nStronglyTypedCore/blob/master/i18nStronglyTypedMVC/i18nStronglyTypedMVC/wwwroot/images/9.png?raw=true)
`

11. Now run the site. Both resources will be in French.

![alt tag](https://github.com//HockeyJustin/i18nStronglyTypedCore/blob/master/i18nStronglyTypedMVC/i18nStronglyTypedMVC/wwwroot/images/10.png?raw=true)


`



# TruTxtConfig

This project builds on the core functionality of the `TruTxt` library to add the ability
to have a more functional and explicit approach to ASP.NET Core configuration.

## Getting started

1. Reference the library
2. Create a Configuration folder where all the config will be done
3. Add an extension class file to build and register the configuration

```c#
public static class ConfigExtentions
{
   public static void AddConfiguration(
       this IServiceCollection services, 
       IConfiguration configuration)
   {
       // TODO Build and register configuration
   }
}
```
4. Create a `class` or `record` to hold the configuration details in the same Configuration folder.
5. For example, an `EmailServer` class:

```c# 
public class EmailServer
{
   public string Address { get; set; }
   public ushort Port { get; set; }
   public string LoginId { get; set; }
   public string LoginPassword { get; set; }
}
```

5. Instantiate a `TruConfigReader` for the `EmailServer` in the extension method and pass the `configuration` instance through to it.

```c#
public static void AddTruConfiguration(this IServiceCollection services, IConfiguration configuration)
{
    var config = new TruConfigReader<EmailServer>(configuration);
    ... 
```

6. 
# TruTxtConfig

This project builds on the core functionality of the `TruTxt` library to add the ability
to have a more functional and explicit approach to ASP.NET Core configuration.

## Getting started

1. Reference the library
2. Create a Configuration folder where the config classes will reside.
3. Add an extension class file to build and register the configuration

```c#
public static class ConfigExtentions
{
   /// <summary>
   /// An extension class for reading and registering configurations
   /// </summary>
   /// <param name="builder">The <see cref="IHostApplicationBuilder"/> application instance</param>
   public static void AddTruConfiguration(this IHostApplicationBuilder builder)
   {
       // TODO Build and register configuration
   }
}
```

4. In the `Program.cs` file add this line after the `builder` is created.

```c#
// Extension method for adding TruConfig reader  
builder.AddTruConfiguration();
```

5. Create a `class` or `record` to hold the configuration details in the `Configuration` folder created in (2).
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

5. Instantiate a `TruConfigReader` for the `EmailServer` in the extension method and pass the `configuration` instance of the `builder` to it.

```c#
public static void AddTruConfiguration(this IHostApplicationBuilder builder)
   {
      var config = new TruConfigReader(builder.Configuration);
    ... 
```

6. If there are connection strings they can be read into a built-in record.

```c#
public static void AddTruConfiguration(this IHostApplicationBuilder builder)
   {
      var config = new TruConfigReader(builder.Configuration);
      var conStrings = config.ReadConnectionStrings();
      ...
```

7. Create a reader for the `EmailServer` type.

```c#
public static void AddTruConfiguration(this IHostApplicationBuilder builder)
   {
      var config = new TruConfigReader(builder.Configuration);
      var conStrings = config.ReadConnectionStrings();
      var emailReader = config.CreateReader<EmailServer>("EmailServer");
      ...
```

8. Read the config values from the `TruConfigReader` instance.

```c#
public static void AddTruConfiguration(this IHostApplicationBuilder builder)
   {
      var config = new TruConfigReader(builder.Configuration);
      var conStrings = config.ReadConnectionStrings();
      var emailReader = config.CreateReader<EmailServer>("EmailServer");
      
      var emailServerConfigResult =
         from port in emailReader.GetValue(p => p.Port)
         from loginId in emailReader.GetValue(p => p.LoginId)
         from address in emailReader.GetValue(p => p.Address)
         from loginPassword in emailReader.GetValue(p => p.LoginPassword)
         select new EmailServer(address, port, loginId, loginPassword);
      ...
```

9. Register the value in the `emailServerConfigResult` using the `Match` extension method on this instance.

```c#
emailServerConfigResult.Match(
         onPresent: p => builder.Services.AddSingleton(p.Value),
         onMissing: m => {
                var message = missing.ToHierarchicalMessage();
    
                var logger = LoggerFactory.Create(b => b.AddConsole()).CreateLogger("TruTxt Config Reader");
                logger.LogCritical(message);
    
                throw new Exception(message);
             }
      );
```

The final code looks like:

```C#
public static void AddTruConfiguration(this IHostApplicationBuilder builder)
   {
      var config = new TruConfigReader(builder.Configuration);
      var conStrings = config.ReadConnectionStrings();
      var emailReader = config.CreateReader<EmailServer>("EmailServer");
      
      var emailServerConfigResult =
         from port in emailReader.GetValue(p => p.Port)
         from loginId in emailReader.GetValue(p => p.LoginId)
         from address in emailReader.GetValue(p => p.Address)
         from loginPassword in emailReader.GetValue(p => p.LoginPassword)
         select new EmailServer(address, port, loginId, loginPassword);

      emailServerConfigResult.Match(
         onPresent: p => builder.Services.AddSingleton(p.Value),
         onMissing: m => {
                var message = missing.ToHierarchicalMessage();
                var logger = LoggerFactory.Create(b => b.AddConsole())
                    .CreateLogger("TruTxt Config Reader");
                logger.LogCritical(message);
    
                throw new Exception(message);
             }
      );
```

## Sub-Instances

If you want to create a hierarchy of configuration `records` then they can be added together using the `reader.GetModel(<modelResult>)` method. For instance:

```c#
var hostConfigResult =
     from port in hostReader.GetOptionalValue(p => p.Port, (ushort)8080)
     from email in hostReader.GetModel(
    
                throw new Exception(message);
             })
     select new Host(port, email, conStrings);

```

This ensures that all errors are read and passed through to the caller and not just the current one.

using TruAspNetCore.Config;

namespace TruTxtWebTests.Configuration;

using TruAspNetCore;

public static class ConfigExtensions
{
   /// <summary>
   /// An extension class for reading and registering configurations
   /// </summary>
   /// <param name="builder">The <see cref="IHostApplicationBuilder"/> application instance</param>
   public static void AddTruConfiguration(this IHostApplicationBuilder builder)
   {
      var config = new TruConfigReader(builder.Configuration);
      var conStrings = config.ReadConnectionStrings();
      var emailReader = config.CreateReader<EmailServer>("EmailServer");
      var hostReader = config.CreateReader<Host>("Host");

      var emailServer =
         from port in emailReader.GetValue(p => p.Port)
         from loginId in emailReader.GetValue(p => p.LoginId)
         from address in emailReader.GetValue(p => p.Address)
         from loginPassword in emailReader.GetValue(p => p.LoginPassword)
         select new EmailServer(address, port, loginId, loginPassword);

      var host =
         from port in hostReader.GetOptionalValue(p => p.Port, (ushort)8080)
         from email in hostReader.GetModel(emailServer)
         select new Host(port, email, conStrings);

      //* Find a type-safe way to get the ConnectionString 
      if (conStrings.IsEmpty() == false)
      {
         var _ = conStrings.Get("Default");
      }

      host.Match(
         onPresent: p => builder.Services.AddSingleton(p.Value),
         onMissing: builder.OnMissing
      );
   }

   private static IServiceCollection OnMissing<A>(this IHostApplicationBuilder builder, Missing<A> missing)
      where A : notnull
   {
      var message = missing.ToHierarchicalMessage();

      var logger = LoggerFactory.Create(b => b.AddConsole()).CreateLogger("TruTxt Config Reader");
      logger.LogCritical(message);

      throw new Exception(message);
   }
}
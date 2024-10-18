using TruConfig;

namespace TruTxtWebTests.Configuration;

public static class ConfigExtensions
{
   /// <summary>
   /// An extension class for reading and registering configurations
   /// </summary>
   /// <param name="builder">The <see cref="IHostApplicationBuilder"/> application instance</param>
   public static void AddTruConfiguration(this IHostApplicationBuilder builder)
   {
      var config = new TruConfigReader(builder.Configuration);
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
         select new Host(port, email);

      host.Match(
         onPresent: p => builder.Services.AddSingleton(p.Value),
         onMissing: m =>
         {
            var message = "Configuration Problems:\n" + m.Aggregate((s1, s2) => "  " + s1 + ", \n" + s2);  
            throw new Exception(message);
         }
         );
   }
}
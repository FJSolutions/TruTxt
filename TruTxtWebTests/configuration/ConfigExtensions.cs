using TruConfig;

namespace TruTxtWebTests.Configuration;

public static class ConfigExtensions
{
   public static void AddTruConfiguration(this IServiceCollection services, IConfiguration configuration)
   {
      var config = new TruConfigReader<EmailServer>(configuration, "EmailServer");
      var modelOptions =
         config.Read(s => s.Address)
         + config.Read(s => s.Port)
         + config.Read(s => s.LoginId)
         + config.Read(s => s.LoginPassword);

      var emailServer = 
         from port in modelOptions.GetValue(p => p.Port)
         from loginId in modelOptions.GetValue(p => p.LoginId)
         from address in modelOptions.GetValue(p => p.Address)
         from loginPassword in modelOptions.GetValue(p => p.LoginPassword)
         select new EmailServer { Address = address, Port = port, LoginId = loginId, LoginPassword = loginPassword };
   }
}
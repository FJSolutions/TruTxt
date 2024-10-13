using TruConfig;

namespace TruTxtWebTests.Configuration;

public static class ConfigExtensions
{
   public static void AddTruConfiguration(this IServiceCollection services, IConfiguration configuration)
   {
      var emailConfig = new TruConfigReader<EmailServer>(configuration, "EmailServer");
      var emailResults =
         emailConfig.Read(s => s.Address)
         + emailConfig.Read(s => s.Port)
         + emailConfig.Read(s => s.LoginId)
         + emailConfig.Read(s => s.LoginPassword)
         + emailConfig.ReadOptional(s => s.Retries, 0);

      var emailServer =
         from port in emailResults.GetValue(p => p.Port)
         from loginId in emailResults.GetValue(p => p.LoginId)
         from address in emailResults.GetValue(p => p.Address)
         from loginPassword in emailResults.GetValue(p => p.LoginPassword)
         from retries in emailResults.GetValue(p => p.Retries)
         select new EmailServer(address, port, loginId, loginPassword, retries);
      
      var hostConfig = new TruConfigReader<Host>(configuration, "Host");
      var hostResults = hostConfig.Read(h => h.Port)
         + hostConfig.AddModel(h => h.EmailServer, emailServer);
      
      var host = 
         from port in hostResults.GetValue(p => p.Port)
         from email in hostResults.GetValue(p => p.EmailServer)
         select new Host(port, email);
   }
}
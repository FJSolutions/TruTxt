namespace TruTxtWebTests.Configuration;

public class EmailServer
{
   public string Address { get; set; }
   public ushort Port { get; set; }
   public string LoginId { get; set; }
   public string LoginPassword { get; set; }
}
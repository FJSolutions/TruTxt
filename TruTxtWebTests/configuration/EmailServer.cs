namespace TruTxtWebTests.Configuration;

public record EmailServer(string Address, ushort Port, string LoginId, string LoginPassword);

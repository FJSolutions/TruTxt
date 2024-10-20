using System.Collections.Immutable;

using TruAspNetCore;
using TruAspNetCore.Config;

namespace TruTxtWebTests.Configuration;

public record Host(ushort Port, EmailServer EmailServer, ConnectionStringsCollector ConnectionStrings);

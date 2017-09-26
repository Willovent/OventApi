using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KodiRpc
{
    public class SocketFactory : ISocketFactory
    {
        public ISocket GetSocket()
        {
            return new SocketWrapper();
        }

        public async Task<string[]> ResolveHostname(string hostname)
        {
            return null;
        }
    }
}

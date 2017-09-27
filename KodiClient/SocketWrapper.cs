using System;
using System.IO;
using System.Threading.Tasks;

namespace KodiRpc
{
    public class SocketWrapper : ISocket
    {

        public SocketWrapper()
        {
        }

        public void Dispose()
        {
        }

        public async Task ConnectAsync(string hostName, int port)
        {
        }

        public Stream GetInputStream()
        {
            return null;
        }
    }
}

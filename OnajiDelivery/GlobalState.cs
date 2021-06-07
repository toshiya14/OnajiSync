using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace OnajiDelivery
{
    internal static class GlobalState
    {
        private const string ListenAddress = "0.0.0.0";
        private const int DefaultListenPort = 23366;

        internal static EndPoint ListenEndPoint;

        static GlobalState()
        {
            ListenEndPoint = new IPEndPoint(IPAddress.Parse(ListenAddress), DefaultListenPort);
        }
    }
}

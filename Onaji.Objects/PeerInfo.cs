using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Onaji.Objects
{
    public class PeerInfo
    {
        [JsonProperty(PropertyName = "prefName")]
        public string PreferName { get; set; }

        [JsonProperty(PropertyName = "peerid")]
        public string ID { get; private set; }

        private EndPoint _clientEndPoint;

        [JsonProperty(PropertyName = "ip")]
        public string IP { get; private set; }

        [JsonProperty(PropertyName = "port")]
        public int Port { get; private set; }

        public PeerInfo(string ip, int port, string prefName = null)
        {
            var id = Guid.NewGuid();
            var idbytes = id.ToByteArray();
            var base32id = SimpleBase.Base32.ZBase32.Encode(idbytes);
            this.ID = base32id;
            this.PreferName = prefName;
            this.IP = ip;
            this.Port = port;
        }
    }
}

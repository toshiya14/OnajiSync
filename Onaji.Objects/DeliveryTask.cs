using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onaji.Objects
{
    public enum UnlistFileActions {
        Ignore,
        Delete
    }
    public enum FileUpdateActions
    {
        /// <summary>
        /// Check Exists,
        /// Check Size Equality,
        /// Check Last Modified Equality,
        /// Check Sha-256,
        /// If all passed, ignore. Otherwise, replace.
        /// </summary>
        Update,
        /// <summary>
        /// Check Exists,
        /// Check Sha-256,
        /// If all passed, ignore. Otherwise, replace.
        /// </summary>
        Repair,
        /// <summary>
        /// Force replace all files.
        /// </summary>
        Replace,
        /// <summary>
        /// Check Exists,
        /// If exists, ignore. Otherwise, replace.
        /// </summary>
        FixMissing
    }
    public class DeliveryTask
    {
        [JsonProperty(PropertyName = "basePath")]
        public string BasePath { get; set; }

        [JsonProperty(PropertyName = "skiprules")]
        public string[] SkipRules { get; set; }

        [JsonProperty(PropertyName = "port")]
        public int ListenPort { get; set; }

        public UnlistFileActions UnlistFileAction { get; set; }

        [JsonProperty(PropertyName = "ignoreHide")]
        public bool IsHideFileIgnored { get; set; }

        [JsonProperty(PropertyName = "ignoreSysHide")]
        public bool IsSystemHideFileIgnored { get; set; }

        [JsonProperty(PropertyName = "keepLastModified")]
        public bool IsLastModifiedKeeped { get; set; }

        [JsonProperty(PropertyName = "isEmptyFolderIgnored")]
        public bool IsEmptyFolderIgnored { get; set; }

        public DeliveryTask() {
            this.IsHideFileIgnored = false;
            this.IsSystemHideFileIgnored = true;
            this.IsLastModifiedKeeped = true;
            this.IsEmptyFolderIgnored = true;
            this.UnlistFileAction = UnlistFileActions.Ignore;
        }

        public DeliveryTask(string basepath, int port)
            :this()
        {
            this.BasePath = basepath;
            this.ListenPort = port;
        }

        public string ToClientJson() {
            var jserializer = new JsonSerializer {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                Formatting = Formatting.None
            };
            var jtoken = JToken.FromObject(this, jserializer);
            jtoken["basename"]?.Remove();
            return jtoken.ToString();
        }

    }
}

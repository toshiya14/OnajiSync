using Onaji.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace OnajiDelivery
{
    internal class FileDeliveryService
    {
        public string Status { get; private set; }

        private Dictionary<string, string> msgDict;

        public void UpdateStatus(string msg)
        {
            this.Status = msg;
        }

        public void StartHost(DeliveryTask task)
        {
            var baseFI = new DirectoryInfo(task.BasePath);
            var baseUri = new Uri(baseFI.FullName);

            // Check base folder is exists.
            UpdateStatus("Scanning the files...");
            if (!baseFI.Exists)
            {
                UpdateStatus($"Task Failed: task base path is not exists. BasePath:{baseFI.FullName}");
                return;
            }

            try
            {
                // Enumerate all files.
                foreach(var file in baseFI.EnumerateFiles("*", SearchOption.AllDirectories))
                {
                    var todo = new ToDoFile();
                    todo.Name = file.Name;
                    var uri = new Uri(file.FullName);
                    todo.Location = uri.MakeRelativeUri(baseUri).ToString();
                    todo.LastModified = file.LastWriteTimeUtc;
                    todo.Size = file.Length;
                    using(var stream = new BufferedStream(File.OpenRead(file.FullName), 1200000))
                    {
                        var sha256 = new SHA256Managed();
                        var checksum = sha256.ComputeHash(stream);
                        todo.checksum = SimpleBase.Base85.Ascii85.Encode(checksum);
                    }
                    // TODO: HERE
                }

                // Enumerate all folder and add the empty folders.
                if (!task.IsEmptyFolderIgnored)
                {

                }
            }
            catch (Exception ex)
            {
                UpdateStatus($"Task Failed during scanning the folder. {ex.Message}");
                return;
            }

            // Prepare for networking.
            var gEndPoint = GlobalState.ListenEndPoint as IPEndPoint;
            var endpoint = new IPEndPoint(gEndPoint.Address, task.ListenPort);

        }
    }
}

/// <summary>
        /// download using http and install
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="path">path</param>
        /// <returns></returns>
        public bool HttpDownload(string url, string path)
        {
            string tempPath = System.IO.Path.GetDirectoryName(path) + @"\temp";
            System.IO.Directory.CreateDirectory(tempPath); 
            string tempFile = tempPath + @"\" + System.IO.Path.GetFileName(path) + ".temp";
            if (System.IO.File.Exists(tempFile))
            {
                System.IO.File.Delete(tempFile);
            try
            {
                FileStream fs = new FileStream(tempFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
               
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
              
                Stream responseStream = response.GetResponseStream();
              
                //Stream stream = new FileStream(tempFile, FileMode.Create);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    //stream.Write(bArr, 0, size);
                    fs.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                //stream.Close();
                fs.Close();
                responseStream.Close();
                System.IO.File.Move(tempFile, path);
                string strInput = @"msiexec /i" + param + "/qb";
                Process p = new Process();
                p.StartInfo.FileName = tempFile.FileName;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.StandardInput.WriteLine(strInput + "&exit");
                p.StandardInput.AutoFlush = true;
                string strOuput = p.StandardOutput.ReadToEnd();

                p.WaitForExit();
                p.Close();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
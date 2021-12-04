/ <summary>
        /// RDP Connection
        /// </summary>
        /// <param name="connectionType"></param>
        /// <param name="ipAddress"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>


        private void Connect(string connectionType, string ipAddress,string userName,string password)
        {
    
            if (connectionType == "ip")
            {
    
                if (string.IsNullOrEmpty(str_computer.Text.Trim()))
                {
    
                    throw new Exception("Invalid ip Address");
                    return;
                }
                try
                {
    
                    string IP = ipAddress.Trim();
                    Ping pingsender = new Ping();
                    PingReply reply = pingsender.Send(IP);
                    if (reply.Status == IPStatus.Success)
                    {
    
                        t.rdp.Server = IP;
                        t.rdp.UserName = userName.Trim();
                        t.rdp.AdvancedSettings2.RDPPort = Convert.ToInt16(RDPPort.Text.Trim());
                        t.rdp.AdvancedSettings2.SmartSizing = true;
                        t.rdp.AdvancedSettings9.NegotiateSecurityLayer = true;
                        IMsTscNonScriptable securd = (IMsTscNonScriptable)t.rdp.GetOcx();
                        securd.ClearTextPassword = password.Trim();
                        t.rdp.AdvancedSettings5.ClearTextPassword = password.Trim();
                        t.rdp.ColorDepth = 24;
                        t.rdp.Connect();
                        t.Show();

                    }
                    else
                    {
                        throw new Exception("Current IP address not working");

                    }
                }
                catch
                {
    
                    ;
                }
            }
            else
            {
    
                t.Hide();
                try
                {
    
                    t.rdp.Disconnect();

                }
                catch
                {
    
                    ;
                }
                t.rdp.Refresh();
            }
        }
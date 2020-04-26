using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace OSConsole.Server.Models
{
    public class Device
    {
        public string Name { get; set; }
        public string Port { get; set; }
        public int BaudRate { get; set; }
        public int OutletNumber { get; set; }
    }
}

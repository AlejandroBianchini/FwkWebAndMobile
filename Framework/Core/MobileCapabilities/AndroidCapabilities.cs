using System;
using System.Collections.Generic;
using System.Text;

namespace Core.MobileCapabilities
{
    public class AndroidCapabilities : GeneralCapabilities
    {
        public string AppPackage { get; set; }
        public string AppActivity { get; set; }
        public string AppWaitActivity { get; set; }
        public string AppWaitPackage { get; set; }
    }
}

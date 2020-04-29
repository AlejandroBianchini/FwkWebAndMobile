using System;
using System.Collections.Generic;
using System.Text;

namespace Core.MobileCapabilities
{
    public class GeneralCapabilities
    {
        public string DeviceName { get; set; }
        public string PlatformVersion { get; set; }
        public string PlatformName { get; set; }
        public string BrowserName { get; set; }
        public string Udid { get; set; }
        public string Language { get; set; }
        public string Orientation { get; set; }
        public string NoReset { get; set; }
        public string FullReset { get; set; }
        public string App { get; set; }
    }
}

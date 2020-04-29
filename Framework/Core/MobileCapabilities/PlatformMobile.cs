using System;
using System.Collections.Generic;
using System.Text;

namespace Core.MobileCapabilities
{
    public class PlatformMobile
    {
        public AndroidCapabilities Android { get; set; }
        public iOSCapabilities iOS { get; set; }
    }
}

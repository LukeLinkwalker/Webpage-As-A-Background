using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Webpage_As_A_Background.Utils
{
    public static class Extension
    {
        /// <summary>
        /// Returns the alphanumeric part of the DeviceName.
        /// </summary>
        public static string GetDeviceNameSanitized(this Screen screen)
        {
            return screen.DeviceName.Substring(4);
        }
    }
}

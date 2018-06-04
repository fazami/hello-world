using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Farzin.Infrastructure.CrossCuttings.Helpers
{
    public class DeviceHelper
    {
        public static Dictionary<char, string> GetAllDriveSerials()
        {
            var drives = System.IO.DriveInfo.GetDrives();
            
            //var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk WHERE DriveType=3");

            int i = 0;
            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                //// get the hard drive from collection
                //// using index
                //HardDrive hd = (HardDrive)hdCollection[i];

                //// get the hardware serial no.
                //if (wmi_HD["SerialNumber"] == null)
                //    hd.SerialNo = "None";
                //else
                //    hd.SerialNo = wmi_HD["SerialNumber"].ToString();

                ++i;
            }
            return null;
        }
    }
}

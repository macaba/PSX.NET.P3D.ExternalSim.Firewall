using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PSX.NET.P3D.ExternalSim.Firewall
{
    class Program
    {
        static void Main(string[] args)
        {
            if (XMLSettings.Exist("PSX.NET.P3D.ExternalSim"))
            {
                Console.WriteLine("PSX.NET.P3D.ExternalSim.xml found");
                var externalSimSettings = XMLSettings.Load<Settings>("PSX.NET.P3D.ExternalSim");
                var split = externalSimSettings.RouterSubAddress.Split(':');
                if (split.Count() == 3)
                {
                    var port = split[2];
                    if (Regex.IsMatch(port, @"^\d+$"))      //Basic validation to check that we've got a port number and not anything else
                    {
                        Console.WriteLine("Adding firewall rules for port " + port);
                        Console.WriteLine(AdministratorCmd.Run("netsh advfirewall firewall delete rule name=\"PSX.NET.P3D.ExternalSim RouterSub\""));
                        Console.WriteLine(AdministratorCmd.Run("netsh advfirewall firewall add rule name=\"PSX.NET.P3D.ExternalSim RouterSub\" dir=out action=allow protocol=tcp remoteport=" + port));
                    }
                    else
                        Console.WriteLine("Error: Could not create firewall rule for RouterSubAddress (regex didn't match)");
                }
                else
                    Console.WriteLine("Error: Could not create firewall rule for RouterSubAddress (port string not found)");

                split = externalSimSettings.FeedbackPubAddress.Split(':');
                if (split.Count() == 3)
                {
                    var port = split[2];
                    if (Regex.IsMatch(port, @"^\d+$"))      //Basic validation to check that we've got a port number and not anything else
                    {
                        Console.WriteLine("Adding firewall rules for port " + port);
                        Console.WriteLine(AdministratorCmd.Run("netsh advfirewall firewall delete rule name=\"PSX.NET.P3D.ExternalSim FeedbackPub\""));
                        Console.WriteLine(AdministratorCmd.Run("netsh advfirewall firewall add rule name=\"PSX.NET.P3D.ExternalSim FeedbackPub\" dir=out action=allow protocol=tcp remoteport=" + port));
                    }
                    else
                        Console.WriteLine("Error: Could not create firewall rule for FeedbackPubAddress (regex didn't match)");
                }
                else
                    Console.WriteLine("Error: Could not create firewall rule for FeedbackPubAddress (port string not found)");
            }


            if (XMLSettings.Exist("PSX.NET.P3D.ExternalSim.Router"))
            {
                Console.WriteLine("PSX.NET.P3D.ExternalSim.Router.xml found");
                var externalSimRouterSettings = XMLSettings.Load<Router.Settings>("PSX.NET.P3D.ExternalSim.Router");
                var split = externalSimRouterSettings.RouterPubBind.Split(':');
                if (split.Count() == 3)
                {
                    var port = split[2];
                    if (Regex.IsMatch(port, @"^\d+$"))      //Basic validation to check that we've got a port number and not anything else
                    {
                        Console.WriteLine("Adding firewall rules for port " + port);
                        Console.WriteLine(AdministratorCmd.Run("netsh advfirewall firewall delete rule name=\"PSX.NET.P3D.ExternalSim.Router RouterPub\""));
                        Console.WriteLine(AdministratorCmd.Run("netsh advfirewall firewall add rule name=\"PSX.NET.P3D.ExternalSim.Router RouterPub\" dir=in action=allow protocol=tcp remoteport=" + port));
                    }
                    else
                        Console.WriteLine("Error: Could not create firewall rule for RouterPubBind (regex didn't match)");
                }
                else
                    Console.WriteLine("Error: Could not create firewall rule for RouterPubBind (port string not found)");

                split = externalSimRouterSettings.FeedbackSubBind.Split(':');
                if (split.Count() == 3)
                {
                    var port = split[2];
                    if (Regex.IsMatch(port, @"^\d+$"))      //Basic validation to check that we've got a port number and not anything else
                    {
                        Console.WriteLine("Adding firewall rules for port " + port);
                        Console.WriteLine(AdministratorCmd.Run("netsh advfirewall firewall delete rule name=\"PSX.NET.P3D.ExternalSim.Router FeedbackSub\""));
                        Console.WriteLine(AdministratorCmd.Run("netsh advfirewall firewall add rule name=\"PSX.NET.P3D.ExternalSim.Router FeedbackSub\" dir=in action=allow protocol=tcp remoteport=" + port));
                    }
                    else
                        Console.WriteLine("Error: Could not create firewall rule for FeedbackSubBind (regex didn't match)");
                }
                else
                    Console.WriteLine("Error: Could not create firewall rule for FeedbackSubBind (port string not found)");
            }

            Console.WriteLine("Firewall is configured. Press any key to exit.");
            Console.Read();
        }
    }
}

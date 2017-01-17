using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSX.NET.P3D.ExternalSim.Firewall
{
    public static class AdministratorCmd
    {
        public static string Run(string arguments)
        {
            var output = new StringBuilder();
            var error = new StringBuilder();

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C " + arguments;
            startInfo.Verb = "runas";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            process.StartInfo = startInfo;
            process.OutputDataReceived += (o, e) => output.Append(e.Data + " ");
            process.ErrorDataReceived += (o, e) => error.Append(e.Data + " ");
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            if (!string.IsNullOrEmpty(output.ToString().Trim()) && !string.IsNullOrEmpty(error.ToString().Trim()))
                return "Command output: " + output.ToString().Trim() + " Command error: " + error.ToString().Trim();
            else if (!string.IsNullOrEmpty(output.ToString().Trim()))
                return "Command output: " + output.ToString().Trim();
            else
                return "Command error: " + error.ToString().Trim();
        }       
    }
}

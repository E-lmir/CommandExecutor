using System.Diagnostics;
using System.Text;

namespace Executor
{
    public class CommandLineExecutor
    {
        private ProcessStartInfo processInfo;
        public CommandLineExecutor() => processInfo = new() { FileName = "cmd.exe", UseShellExecute = true };

        public void Execute(params string[] args)
        {        
            var arguments = new StringBuilder();
            arguments.Append("/C");
            arguments.AppendJoin("&&", args);

            processInfo.Arguments = arguments.ToString();
            using var process = Process.Start(processInfo);
        }
    }
}

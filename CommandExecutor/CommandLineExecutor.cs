using System.Diagnostics;
using System.Text;

namespace CommandExecutor
{
    /// <summary>
    /// Provides an easier way to execute comands.
    /// </summary>
    public class CommandLineExecutor
    {
        private ProcessStartInfo processInfo;

        /// <summary>
        /// Initializes a new instance of the  <see cref="CommandLineExecutor"/> class.
        /// </summary>
        public CommandLineExecutor() => processInfo = new() { FileName = "cmd.exe", 
            UseShellExecute = false, 
            RedirectStandardInput = true, 
            RedirectStandardOutput = false,
            WindowStyle = ProcessWindowStyle.Hidden,
            CreateNoWindow = true
        };


        /// <summary>
        /// Execute commands.<br/>
        /// <example>
        /// This shows how to execute command.
        /// <code>
        /// var executor = new CommandLineExecutor();
        /// executor.Execute("cd C:/", "rmdir windows");
        /// </code>
        /// </example>
        /// </summary>
        public Process Execute(params string[] args)
        {        
            var arguments = new StringBuilder();
            arguments.Append("/C");
            arguments.AppendJoin("&&", args);

            processInfo.Arguments = arguments.ToString();
            using var process = Process.Start(processInfo);
            return process;
        }
    }
}

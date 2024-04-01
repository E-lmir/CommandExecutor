using System.Diagnostics;
using System.Text;

namespace CommandExecutor
{
    public class CommandLineExecutor
    {
        private ProcessStartInfo processInfo;
        public CommandLineExecutor() => processInfo = new() { FileName = "cmd.exe", UseShellExecute = true };


        /// <summary>
        /// Execute commands
        /// </summary>
        /// <example>
        /// This shows how to execute command.
        /// <code>
        ///     var executor = new CommandLineExecutor();
        ///     executor.Execute("cd C:/", "rmdir Windows");
        /// </code>
        /// </example>
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

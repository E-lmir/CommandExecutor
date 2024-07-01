using System.Diagnostics;
using System.Text;

namespace CommandExecutor
{
    /// <summary>
    /// Provides an easier way to execute comands.
    /// </summary>
    public class CommandLineExecutor
    {
        private ProcessStartInfo _processInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLineExecutor"/> class.
        /// </summary>
        public CommandLineExecutor() => _processInfo = new() { 
            FileName = "cmd.exe", 
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
        /// <remarks>Wait proccess for exit</remarks>
        /// </summary>
        public void Execute(params string[] args) => this.Execute(true, args);

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
        public void Execute(bool waitForExit, params string[] args)
        {        
            var arguments = new StringBuilder();
            arguments.Append("/C");
            arguments.AppendJoin("&&", args);

            _processInfo.Arguments = arguments.ToString();
            using (var process = Process.Start(_processInfo))
            {
                if (waitForExit)
                    process.WaitForExit();
            }
        }
    }
}

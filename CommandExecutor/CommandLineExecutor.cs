using System.Diagnostics;
using System.Text;

namespace CommandExecutor
{
    /// <summary>
    /// Provides an easier way to execute commands.
    /// </summary>
    public class CommandLineExecutor
    {
        private ProcessStartInfo _processInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLineExecutor"/> class.
        /// </summary>
        public CommandLineExecutor() => _processInfo = new() { 
            FileName = "cmd.exe",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            RedirectStandardInput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
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
        /// <remarks>Wait process for exit.</remarks>
        /// <return>Shell output.</return>
        /// </summary>
        public string Execute(params string[] args) => Execute(true, args);

        /// <summary>
        /// Execute commands.<br/>
        /// <example>
        /// This shows how to execute command.
        /// <code>
        /// var executor = new CommandLineExecutor();
        /// executor.Execute("cd C:/", "rmdir windows");
        /// </code>
        /// </example>
        /// <return>Shell output.</return>
        /// </summary>
        public string Execute(bool waitForExit, params string[] args)
        {
            Process cmdProcess = new Process();
            cmdProcess.StartInfo = _processInfo;
            cmdProcess.EnableRaisingEvents = true;

            cmdProcess.Start();

            foreach (var arg in args)
              cmdProcess.StandardInput.WriteLine(arg);

            cmdProcess.StandardInput.WriteLine("exit");

            if (waitForExit)
              cmdProcess.WaitForExit();

            return cmdProcess.StandardOutput.ReadToEnd();
        }
    }
}

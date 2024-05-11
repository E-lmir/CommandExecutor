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
        public CommandLineExecutor() => processInfo = new() { FileName = "cmd.exe", UseShellExecute = false };


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
        public void Execute(params string[] args)
        {        
            var arguments = new StringBuilder();
            arguments.Append("/C");
            arguments.AppendJoin("&&", args);

            processInfo.Arguments = arguments.ToString();
            var process = Process.Start(processInfo);
            if (process != null)
            {
                process.WaitForExit();
                var errorOutput = process.StandardError.ReadToEnd();
                var standardOutput = process.StandardOutput.ReadToEnd();
                if (process.ExitCode != 0)
                    throw new Exception("Exit code: " + process.ExitCode.ToString() + " " + (!string.IsNullOrEmpty(errorOutput) ? " " + errorOutput : "") + " " + (!string.IsNullOrEmpty(standardOutput) ? " " + standardOutput : ""));
            }
        }
    }
}

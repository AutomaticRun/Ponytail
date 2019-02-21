using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IWshRuntimeLibrary;

namespace Ponytail.Core.FileSystem
{
    /// <summary>
    /// File helper
    /// </summary>
    public class File
    {
        /// <summary>
        /// Create Windows Shorcut
        /// </summary>
        /// <param name="sourceFile">A file you want to make shortcut to</param>
        /// <param name="shortcutFile">Path and shorcut file name including file extension (.lnk)</param>
        /// <param name="description">Shortcut description</param>
        /// <param name="arguments">Command line arguments</param>
        /// <param name="hotKey">Shortcut hot key as a string, for example "Ctrl+F"</param>
        /// <param name="icon">shortcut icon file path</param>
        /// <param name="workingDirectory">"Start in" shorcut parameter</param>
        public static void CreateShortcut(string sourceFile, string shortcutFile, string description,
           string arguments, string hotKey,string icon, string workingDirectory)
        {
            // Check necessary parameters first:
            if (string.IsNullOrEmpty(sourceFile))
                throw new ArgumentNullException("sourceFile");
            if (string.IsNullOrEmpty(shortcutFile))
                throw new ArgumentNullException("shortcutFile");

            // Create WshShellClass instance:
            var wshShell = new WshShell();

            // Create shortcut object:
            IWshShortcut shorcut = (IWshShortcut)wshShell.CreateShortcut(shortcutFile);

            // Assign shortcut properties:
            shorcut.TargetPath = sourceFile;
            shorcut.Description = description;
            if (!string.IsNullOrEmpty(arguments))
                shorcut.Arguments = arguments;
            if (!string.IsNullOrEmpty(hotKey))
                shorcut.Hotkey = hotKey;
            if (!string.IsNullOrEmpty(workingDirectory))
                shorcut.WorkingDirectory = workingDirectory;

            // Save the shortcut:
            shorcut.Save();
        }
    }
}

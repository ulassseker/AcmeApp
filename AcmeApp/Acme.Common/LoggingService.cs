using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Common
{
    /// <summary>
    /// Provides logging.
    /// </summary>
    public static class LoggingService // static classes can only contain static members
    {
        /// <summary>
        /// Logs actions.
        /// </summary>
        /// <param name="action">Action to log.</param>
        public static string LogAction(string action) // bir class için static keyword kulanıyorsak her alt sınıfı da static olmalıdır.
        {
            var logText = "Action: " + action;
            Console.WriteLine(logText);

            return logText;
        }
    }
}

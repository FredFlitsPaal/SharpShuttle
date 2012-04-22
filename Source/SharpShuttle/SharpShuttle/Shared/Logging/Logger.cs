using System;
using System.IO;

namespace Shared.Logging
{
    /// <summary>
    /// Logger class
    /// Gebruik door Shared.Logging.Logger.Write(name, message) aan te roepen
    /// </summary>
    public class Logger
    {
		/// <summary>
		/// singleton instance van Logger
		/// </summary>
        private static Logger logger = new Logger();

		/// <summary>
		/// StreamWriter waarmee naar het log-bestand geschreven wordt
		/// </summary>
        private StreamWriter writer;


		/// <summary>
		/// Opent een nieuw logfile met de huidige datum/tijd als naam
		/// </summary>
        private Logger()
        {
            DateTime now = DateTime.Now;
            //Er mag helaas geen : in een filename voor de timestamp
            string logName = now.ToString("dd-MM-yyyy HH-mm-ss") + ".log";
            FileStream file = new FileStream(logName, FileMode.OpenOrCreate, FileAccess.Write);
            writer = new StreamWriter(file);
        }

        /// <summary>
        /// Schrijft de message in het volgende formaat: [dd-mm-yyyy] (hh:mm:ss) {name} message
        /// Thread-safe
        /// </summary>
        /// <param name="name"> Naam van de message, om makkelijk naar bepaalde typen messages te zoeken </param>
        /// <param name="message"> De message </param>
        public static void Write(string name, string message)
        {
            lock (logger)
            {
                DateTime now = DateTime.Now;
                logger.writer.WriteLine("[" + now.ToString("dd-MM-yyyy") + "] (" + now.ToString("HH:mm:ss") +
                                        ") {" + name + "} " + message);
                //Zorgt dat de data daadwerkelijk vanuit een buffer naar de file geschreven wordt
                logger.writer.Flush();
            }
        }
    }
}

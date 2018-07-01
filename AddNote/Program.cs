using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLParser;

/*****
 * Add note to database.
 * Parameters: [-N[ote]] <message> [-E[vent] <event>] [[-T[ag] <tag>] ...]
 * 
 * remark: Tool name is not called Add-Note because it could confused in PowerShell.
 */

namespace AddNote
{
	class Program
	{
		static void Main(string[] args)
		{
			#region Debug
#if DEBUG
			Log($"Start {nameof(Main)}");
#endif
			#endregion
			CLI cli = new CLI();
			cli.Add("Message")
				.AddAlias("M")
				.AddAlias("Msg");

			cli.Add("Event")
				.AddAlias("E");

			cli.Add("Tag")
				.NotRequiredValue()
				.MaxListValue()
				.AddAlias("T");

			#region Debug
#if DEBUG
			//Log("GetAliassen");
#endif
			//Console.WriteLine();
			//Console.WriteLine("Alias: Argument");
			//Console.WriteLine("---------------");
			//foreach (var item in cli.GetAliasses())
			//{
			//	Console.WriteLine("{0}: {1}", item.Key, item.Value);
			//}

			//Console.WriteLine();
			//Console.WriteLine($"Alias N is agument: {cli.GetName("n")}");

			//Console.WriteLine();
			#endregion
			cli.Parse(args);

			Console.WriteLine();
			Console.Write("Press any key...");
			Console.ReadKey();
		}

		#region Debug
#if DEBUG
		public static void Log(string logMessage)
		{
			Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} {logMessage}");
		}
#endif
		#endregion
	}
}

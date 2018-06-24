using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLParser;

/*****
 * Add note to database.
 * Parameters: [-N[ote]] <message> [[-T[ag] <tag>] ...] [-E[vent] <event>]
 * 
 * remark: Tool name is not called Add-Note because it could confused in PowerShell.
 */

namespace AddNote
{
	class Program
	{
		static void Main(string[] args)
		{
			CLI cli = new CLI();
			//CLI cli;
			cli.Add("Name");
			cli.AddAlias("Name", "N");

			Console.WriteLine();
			Console.WriteLine("Alias: Argument");
			Console.WriteLine("---------------");
			foreach (var item in cli.GetAliasses())
			{
				Console.WriteLine("{0}: {1}", item.Key, item.Value);
			}

			Console.WriteLine();
			Console.WriteLine($"Alias N is agument: {cli.GetName("n")}");

			Console.WriteLine();
			Console.Write("Press any key...");
			Console.ReadKey();
		}
	}
}

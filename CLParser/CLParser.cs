using System;
using System.Collections.Generic;
using System.Text;

namespace CLParser
{
	public class CLI
	{
		public static IDictionary<string, Argument> Arguments = new Dictionary<string, Argument>();
		private static IDictionary<string, string> Aliasses = new Dictionary<string, string>();

		public CLI()
		{
		}

		#region Add >> Add Argument(<Name>)
		public void Add(string argumentName)
		{
			AddAlias(argumentName, argumentName);
			try
			{
				Argument argument = new Argument
				{
					Name = argumentName
				};
				Arguments.Add(argumentName, argument);
			}
			catch (Exception)
			{

			}
		}
		#endregion

		#region AddAlias >> Add Alias for ParserItem:Name
		public bool AddAlias(string argumentName, string aliasName)
		{
			bool Result = true;
			try
			{
				Aliasses.Add(aliasName.ToLower(), argumentName);
			}
			catch (Exception e)
			{
				Result = false;
				Console.WriteLine($"Error: Argument {argumentName} does already exists.");
			}

			return Result;
		}
		#endregion

		#region Parse >> Parse CLI arguments
		public bool Parse(string[] args)
		{
			bool Result = false;



			return Result;
		}
		#endregion

		#region GetAliasses >> List all aliasses
		public IDictionary<string, string> GetAliasses()
		{
			return Aliasses;
		}
		#endregion

		#region GetName >> Search for the ParserItem:Name on the Alias
		public string GetName(string alias)
		{
			string Result = String.Empty;
			alias = alias.ToLower();

			if (Aliasses.ContainsKey(alias))
			{
				Result = Aliasses[alias];
			}

			return Result;
		}
		#endregion
	}

	public class Argument
	{
		public string Name { get; private set; }
		public string Value { get; private set; }
		public bool Exists { get; private set; } = false;
		public int Count { get; private set; }

		public Argument()
		{
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CLParser
{
	public class CLI
	{
		public string ArgumentNamePrefixes { get; set; } = "-/";
		public string ArgumentNameValueSeparators { get; set; } = ":";
		public string ValueListSeparators { get; set; } = ",|";

		public static IDictionary<string, Argument> Arguments = new Dictionary<string, Argument>();
		internal static IDictionary<string, string> Aliasses = new Dictionary<string, string>();

		public CLI()
		{
		}

		public CLI SetArgumentNamePrefixes(string value)
		{
			ArgumentNamePrefixes = value;
			return this;
		}

		public CLI SetArgumentNameValueSeparators(string value)
		{
			ArgumentNameValueSeparators = value;
			return this;
		}

		public CLI SetValueListSeparators(string value)
		{
			ValueListSeparators = value;
			return this;
		}

		#region Add >> Add Argument(<Name>)
		public Argument Add(string argumentName)
		{
			Argument argument = new Argument();

			AddAlias(argumentName, argumentName);
			try
			{
				argument.Name = argumentName;
				Arguments.Add(argumentName, argument);
			}
			catch (Exception)
			{

			}

			return argument;
		}
		#endregion

		#region AddAlias >> Add Alias for this object
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
			bool Result = true;

			if (Result = ParseArgs(args))
			{
				Console.WriteLine("Info: Parsed arguments");
				if (Result = ValidateArguments())
				{
					Console.WriteLine("Info: Validated arguments");
				}
				else
				{
					Console.WriteLine("Error: Failed validation arguments");
				}
			}
			else
			{
				Console.WriteLine("Error: Failed parse arguments");
			}

			return Result;
		}
		#endregion

		#region Parse arguments
		private bool ParseArgs(string[] args)
		{
			bool Result = true;

			foreach (var item in args)
			{

			}

			return Result;
		}
		#endregion

		#region Process argument
		private void ProcessArgument(string argumentName, string argumentValue)
		{

		}
		#endregion

		#region Validate arguments
		private bool ValidateArguments()
		{
			bool Result = true;

			foreach (var item in Arguments)
			{
				//ToDo: Validation
			}

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
		//Local counter for Id
		private static byte _id;
		public byte Id { get; set; }

		public string Name { get; set; }
		public string Value { get; private set; }
		public int Count { get; private set; }

		public byte MinCount { get; set; } = 1;
		public byte MaxCount { get; set; } = 1;

		public Argument()
		{
			_id++;
			Id = _id;
		}

		public Argument NotRequiredValue()
		{
			MinCount = 0;
			return this;
		}

		public Argument MaxListValue(byte maxCount = 255)
		{
			MaxCount = maxCount;
			return this;
		}

		#region AddAlias >> Add Alias
		public Argument AddAlias(string aliasName)
		{
			try
			{
				CLI.Aliasses.Add(aliasName.ToLower(), this.Name);
			}
			catch (Exception)
			{
				Console.WriteLine($"Error: Argument {this.Name} does already exists.");
			}

			return this;
		}
		#endregion

		public bool IsRequeredArgument()
		{
			return MinCount > 0;
		}

		public bool IsListableArgument()
		{
			return MaxCount > MinCount;
		}

		public bool HasArgumentValue()
		{
			return Value != null;
		}

		public bool IsListValue()
		{
			if (Value == null)
			{
				return false;
			}
			return Value.Length > 1;
		}
	}
}

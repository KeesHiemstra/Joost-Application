using System;
using System.Collections.Generic;
using System.Text;

namespace CLParser
{
	public class CLI
	{
		public string ArgumentNamePrefixes { get; set; } = "-/";
		public string ArgumentNameValueSeparators { get; set; } = ":";
		public char[] ValueListSeparator { get; set; } = { '|' };

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

		public CLI SetValueListSeparators(char[] value)
		{
			ValueListSeparator = value;
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
#if DEBUG
			Log($"Start Parse");
#endif
			bool Result = true;

			if (Result = ParseArgs(args))
			{
#if DEBUG
				Log("Parse arguments completed");
#endif
				if (Result = ValidateArguments())
				{
#if DEBUG
					Log("Validate arguments completed");
#endif
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

#if DEBUG
			Log($"Parse result: {Result}");
#endif
			return Result;
		}
		#endregion

		#region Parse arguments
		private bool ParseArgs(string[] args)
		{
#if DEBUG
			Log("Start ParseArgs");
#endif
			bool Result = true;
			byte argumentCount = 0;
			string argumentName = string.Empty;
			string argumentValue = string.Empty;

			for (int currentArgument = 0; currentArgument < args.Length; currentArgument++)
			{
				argumentValue = string.Empty;
				if (IsArgumentName(args[currentArgument]))
				{
					//Remove prefix and translate alias to argument name
					argumentName = CompleteArgumentName(args[currentArgument]);

					if (!string.IsNullOrEmpty(argumentName))
					{
						int nextArgument = currentArgument + 1;
						while (nextArgument < args.Length && !IsArgumentName(args[nextArgument]))
						{
							nextArgument++;
						}

						if (nextArgument < args.Length)
						{
							nextArgument--;
							if (nextArgument > currentArgument)
							{
								argumentValue = args[nextArgument];
								currentArgument = nextArgument;
							}
							else
							{
								//No argument values
								argumentValue = string.Empty;
							}
						}//Get argument value
					}
					else
					{
						//No argument name provided
						foreach (var item in Arguments)
						{
							if (item.Value.Id == argumentCount)
							{
								argumentName = item.Value.Name;
#if DEBUG
								Log($"Assumed argument name: {argumentName}");
#endif
								break;
							}
						}
						argumentValue = args[currentArgument];
					}

					argumentCount++;

				}//if IsArgumentName

				ProcessArgument(argumentName, argumentValue);
			}//Loop

#if DEBUG
			Log($"ParseArgs result: {Result}");
#endif
			return Result;
		}
		#endregion

		#region CompleteArgumentName
		private string CompleteArgumentName(string aliasName)
		{
			if (aliasName.Length == 0) { return string.Empty; }
			//Remove prefix
			aliasName = aliasName.Substring(1, aliasName.Length - 1);

			//Return the name based on the alias
			aliasName = GetName(aliasName);
			return aliasName;
		}
		#endregion

		#region IsArgumentName
		private bool IsArgumentName(string argument)
		{
			string argumentName = string.Empty;
			argumentName = argument.Substring(0, 1);
			if (ArgumentNamePrefixes.Contains(argumentName))
			{
				return true;
			}
			return false;
		}
		#endregion

		#region Process argument
		private void ProcessArgument(string argumentName, string argumentValue)
		{
			if (argumentName == string.Empty && argumentValue == string.Empty) { return; }

			#region Debug
#if DEBUG
			Log($"Argument: {argumentName} => {argumentValue}");
#endif
			#endregion
			if (argumentName != string.Empty)
			{
				Log(argumentValue);
				//ToDo: Find argumentName
			}

			if (!string.IsNullOrEmpty(argumentName))
			{
				Arguments[argumentName].Count++;
				if (!string.IsNullOrEmpty(argumentValue))
				{
					if (Arguments[argumentName].Value == null) { Arguments[argumentName].Value = new List<string>(); }

					string[] values = argumentValue.Split(ValueListSeparator);
					foreach (var item in values)
					{
						Arguments[argumentName].Value.Add(item);
					}
				}
			}

		}
		#endregion

		#region Validate arguments
		private bool ValidateArguments()
		{
			bool Result = true;
			bool result = true;

			foreach (var argument in Arguments)
			{
				result = argument.Value.Count >= argument.Value.MinCount;
				result = result && argument.Value.Count <= argument.Value.MaxCount;

				if (argument.Value.IsRequeredArgument()) { Result = Result && result; }

				#region Debug
#if DEBUG
				string option = "required";
				if (!argument.Value.IsRequeredArgument()) { option = "optional"; }
				string msg = $"{argument.Value.Name} {option} ({argument.Value.Count}) = {result}";
				if (argument.Value.Value != null)
				{
					if (argument.Value.Value.Count == 1)
					{
						msg += ": " + argument.Value.Value[0];
					}
				}
				Log(msg);
				if (argument.Value.Value != null && argument.Value.Value.Count > 1)
				{
					foreach (var item in argument.Value.Value)
					{
						Log($" - {item}");
					}
				}
#endif
				#endregion
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

		#region Debug
#if DEBUG
		public static void Log(string logMessage)
		{
			Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} {logMessage}");
		}
#endif
		#endregion
	}

	public class Argument
	{
		//Local counter for Id
		private static byte _id;
		public byte Id { get; set; }

		public string Name { get; set; }
		public IList<string> Value { get; set; }
		public int Count { get; set; }

		public byte MinCount { get; set; } = 1;
		public byte MaxCount { get; set; } = 1;

		public Argument()
		{
			Id = _id;
			_id++;
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
			return Value.Count > 1;
		}
	}
}

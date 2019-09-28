using System;

namespace Bau.Libraries.LibLogger.Core.EventArguments
{
	/// <summary>
	///		Argumentos de los eventos de log
	/// </summary>
	public class LogEventArgs : EventArgs
	{
		public LogEventArgs(int indent, Models.Log.LogModel item)
		{
			Indent = indent;
			Item = item;
		}

		/// <summary>
		///		Indentación
		/// </summary>
		public int Indent { get; }

		/// <summary>
		///		Elemento de log
		/// </summary>
		public Models.Log.LogModel Item { get; }
	}
}

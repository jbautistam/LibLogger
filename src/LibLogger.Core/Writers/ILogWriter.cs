using System;
using System.Collections.Generic;

namespace Bau.Libraries.LibLogger.Core.Writers
{
	/// <summary>
	///		Interface para los elementos de log
	/// </summary>
	public interface ILogWriter
	{
		/// <summary>
		///		Escribe una serie de elementos de log
		/// </summary>
		void Flush(List<Models.Log.LogModel> items);
	}
}

using System;
using System.Collections.Generic;

using Bau.Libraries.LibLogger.Core.Models.Log;

namespace Bau.Libraries.LibLogger.Core.Writers
{
	/// <summary>
	///		Base para las clases de escritura de log
	/// </summary>
	public abstract class LogWriterBase : ILogWriter
	{
		public LogWriterBase(List<LogModel.LogType> typesToLog = null)
		{
			if (typesToLog != null)
				TypesToLog.AddRange(typesToLog);
		}

		/// <summary>
		///		Escribe una serie de elementos de log
		/// </summary>
		public abstract void Flush(List<LogModel> items);

		/// <summary>
		///		Selecciona los elementos adecuados para escribir
		/// </summary>
		protected IEnumerable<LogModel> Select(List<LogModel> items)
		{
			foreach (LogModel item in items)
				if (MustWrite(item.Type))
					yield return item;
		}

		/// <summary>
		///		Comprueba si se debe escribir un tipo de log
		/// </summary>
		private bool MustWrite(LogModel.LogType type)
		{
			// Comprueba si se debe escribir
			if (TypesToLog.Count == 0)
				return true;
			else
				foreach (LogModel.LogType typeToLog in TypesToLog)
					if (typeToLog == type)
						return true;
			// Si ha llegado hasta aquí es porque no ha encontrado nada
			return false;
		}

		/// <summary>
		///		Tipos de log a escribir
		/// </summary>
		public List<LogModel.LogType> TypesToLog { get; } = new List<LogModel.LogType>();
	}
}

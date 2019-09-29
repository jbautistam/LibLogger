using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Bau.Libraries.LibLogger.Core.Models.Log
{
	/// <summary>
	///		Clase con los datos de un elemento de log
	/// </summary>
	public class LogModel
	{
		/// <summary>
		///		Tipo de log
		/// </summary>
		public enum LogType
		{
			/// <summary>Depuración</summary>
			Debug,
			/// <summary>Información</summary>
			Info,
			/// <summary>Información para consola</summary>
			Console,
			/// <summary>Advertencia</summary>
			Warning,
			/// <summary>Error</summary>
			Error,
			/// <summary>Traza</summary>
			Trace,
			/// <summary>Progreso de tarea</summary>
			Progress,
			/// <summary>Error en suposición</summary>
			AssertError,
			/// <summary>Suposición correcta</summary>
			AssertCorrect
		}

		public LogModel(LogModel parent, LogType type, string message, [CallerFilePath] string fileName = null, 
						[CallerMemberName] string methodName = null, [CallerLineNumber] int lineNumber = 0)
		{
			// Propiedades del log
			Parent = parent;
			Type = type;
			Message = message;
			// Asigna los datos del método de llamada
			FileName = fileName;
			MethodName = methodName;
			LineNumber = lineNumber;
		}

		/// <summary>
		///		Añade un parámetro al log
		/// </summary>
		public void AddParameter(string name, object value)
		{
			Parameters.Add(name, value);
		}

		/// <summary>
		///		Elemento padre
		/// </summary>
		public LogModel Parent { get; }

		/// <summary>
		///		Nivel del elemento
		/// </summary>
		public int Level
		{
			get 
			{ 
				if (Parent == null)
					return 0;
				else
					return Parent.Level + 1;
			}
		}

		/// <summary>
		///		Tipo de log
		/// </summary>
		public LogType Type { get; }

		/// <summary>
		///		Mensaje de log
		/// </summary>
		public string Message { get; }

		/// <summary>
		///		Nombre del archivo desde el que se ha creado el elemento de log
		/// </summary>
		public string FileName { get; }

		/// <summary>
		///		Nombre del método desde el que se ha creado el elemento de log
		/// </summary>
		public string MethodName { get; }

		/// <summary>
		///		Número de línea del archivo desde el que se ha creado el elemento de log
		/// </summary>
		public int LineNumber { get; }

		/// <summary>
		///		Progreso actual
		/// </summary>
		public long ActualProgress { get; set; }

		/// <summary>
		///		Final del progreso
		/// </summary>
		public long TotalProgress { get; set; }

		/// <summary>
		///		Fecha de creación del elemento
		/// </summary>
		public DateTime CreatedAt { get; } = DateTime.UtcNow;

		/// <summary>
		///		Excepción
		/// </summary>
		public Exception Exception { get; set; }

		/// <summary>
		///		Parámetros del elemento de log
		/// </summary>
		public Dictionary<string, object> Parameters { get; } = new Dictionary<string, object>();
	}
}

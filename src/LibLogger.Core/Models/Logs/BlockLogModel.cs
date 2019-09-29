using System;

namespace Bau.Libraries.LibLogger.Core.Models.Log
{
	/// <summary>
	///		Bloque de elementos de log
	/// </summary>
	public class BlockLogModel : Base.SetBaseModel
	{
		internal BlockLogModel(LogModelCollection logs, LogModel item) : base(Guid.NewGuid().ToString())
		{
			Logs = logs;
			Item = item;
		}

		/// <summary>
		///		Añade un mensaje de depuración
		/// </summary>
		public LogModel Debug(string message)
		{
			return Logs.Debug(message);
		}

		/// <summary>
		///		Añade un mensaje informativo
		/// </summary>
		public LogModel Info(string message)
		{
			return Logs.Info(message);
		}

		/// <summary>
		///		Añade un mensaje de advertencia
		/// </summary>
		public LogModel Warning(string message)
		{
			return Logs.Warning(message);
		}

		/// <summary>
		///		Añade un mensaje de error
		/// </summary>
		public LogModel Error(string message, Exception exception = null)
		{
			return Logs.Error(message, exception);
		}

		/// <summary>
		///		Añade un mensaje de traza
		/// </summary>
		public LogModel Trace(string message)
		{
			return Logs.Trace(message);
		}

		/// <summary>
		///		Añade una aserción
		/// </summary>
		public LogModel Assert(bool withError, string message)
		{
			return Logs.Assert(withError, message);
		}

		/// <summary>
		///		Añade un mensaje de error en suposición
		/// </summary>
		public LogModel AssertError(string message)
		{
			return Logs.AssertError(message);
		}

		/// <summary>
		///		Añade un mensaje de suposición correcta
		/// </summary>
		public LogModel AssertCorrect(string message)
		{
			return Logs.AssertCorrect(message);
		}

		/// <summary>
		///		Añade un elemento de log
		/// </summary>
		public LogModel Log(LogModel.LogType type, string message)
		{
			return Logs.Log(type, message);
		}

		/// <summary>
		///		Añade un elemento de log de progreso
		/// </summary>
		public LogModel Progress(string message, long actual, long total)
		{
			return Logs.Progress(message, actual, total);
		}

		/// <summary>
		///		Añade un parámetro al log
		/// </summary>
		public void AddParameter(string name, object value)
		{
			Item.AddParameter(name, value);
		}

		/// <summary>
		///		Elementos de log
		/// </summary>
		private LogModelCollection Logs { get; }

		/// <summary>
		///		Elemento de log
		/// </summary>
		public LogModel Item { get; }
	}
}

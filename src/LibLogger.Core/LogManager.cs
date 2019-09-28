using System;
using System.Collections.Generic;

using Bau.Libraries.LibLogger.Core.Models;

namespace Bau.Libraries.LibLogger.Core
{
	/// <summary>
	///		Manager de log
	/// </summary>
	public class LogManager : IDisposable
	{
		// Eventos
		public event EventHandler<EventArguments.LogEventArgs> Logged;

		public LogManager()
		{
			Contexts = new ContextModelCollection(this);
			Default = CreateContext("Default", "LogManager");
		}

		/// <summary>
		///		Crea un contexto
		/// </summary>
		public ContextModel CreateContext(string key, string app)
		{
			return Contexts.Add(key, new ContextModel(this, key, app));
		}

		/// <summary>
		///		Obtiene un contexto
		/// </summary>
		public ContextModel GetContext(string key)
		{
			return Contexts.Get(key);
		}

		/// <summary>
		///		Añade un objeto de escritura del log
		/// </summary>
		public void AddWriter(Writers.ILogWriter writer)
		{
			Writers.Add(writer);
		}

		/// <summary>
		///		Lanza el evento de log
		/// </summary>
		internal void RaiseLog(Models.Log.LogModel log)
		{
			Logged?.Invoke(this, new EventArguments.LogEventArgs(log.Level, log));
		}

		/// <summary>
		///		Envía a los objetos de escritura los elementos de log
		/// </summary>
		public void Flush()
		{
			Contexts.Flush();
		}

		/// <summary>
		///		Libera la memoria
		/// </summary>
		protected virtual void Dispose(bool disposing)
		{
			if (!IsDisposed)
			{
				// Libera la memoria
				if (disposing)
					Flush();
				// Indica que se ha liberado
				IsDisposed = true;
			}
		}

		/// <summary>
		///		Libera la memoria
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
		}

		/// <summary>
		///		Objetos de persistencia de los elementos de log
		/// </summary>
		public List<Writers.ILogWriter> Writers { get; } = new List<Writers.ILogWriter>();

		/// <summary>
		///		Contextos
		/// </summary>
		private ContextModelCollection Contexts { get; }

		/// <summary>
		///		Contexto predeterminado
		/// </summary>
		public ContextModel Default { get; }

		/// <summary>
		///		Indica si se ha liberado el objeto
		/// </summary>
		public bool IsDisposed { get; private set; }
	}
}

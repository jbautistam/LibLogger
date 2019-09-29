using System;
using System.Collections.Generic;

namespace Bau.Libraries.LibLogger.Core.Models.Log
{
	/// <summary>
	///		Colección de <see cref="LogModel"/>
	/// </summary>
	public class LogModelCollection : List<LogModel>
	{
		// Variables privadas
		private object _lock = new object();

		public LogModelCollection(ContextModel context)
		{
			Context = context;
			Blocks = new List<BlockLogModel>();
		}

		/// <summary>
		///		Añade un bloque
		/// </summary>
		public BlockLogModel CreateBlock(LogModel.LogType type, string message)
		{
			BlockLogModel block = new BlockLogModel(this, Log(type, message));

				// Asigna el manejador de eventos
				block.Disposed += (sender, args) =>
										{
											// Añade un elemento al log indicando que ha finalizado el bloque
											if (sender is BlockLogModel blockDisposed)
												Progress($"End block {blockDisposed.Item.Message}. Elapsed: {(DateTime.UtcNow - blockDisposed.Item.CreatedAt).ToString()}", 0, 0);
											// Elimina el bloque de la lista
											lock (_lock)
											{
												Blocks.RemoveAt(Blocks.Count - 1);
											}
										};
				// Añade el bloque
				lock (_lock)
				{
					Blocks.Add(block);
				}
				// Devuelve el bloque generado
				return block;
		}

		/// <summary>
		///		Añade un mensaje de depuración
		/// </summary>
		public LogModel Debug(string message)
		{
			return Log(LogModel.LogType.Debug, message);
		}

		/// <summary>
		///		Añade un mensaje informativo
		/// </summary>
		public LogModel Info(string message)
		{
			return Log(LogModel.LogType.Info, message);
		}

		/// <summary>
		///		Añade un mensaje de advertencia
		/// </summary>
		public LogModel Warning(string message)
		{
			return Log(LogModel.LogType.Warning, message);
		}

		/// <summary>
		///		Añade un mensaje de error
		/// </summary>
		public LogModel Error(string message, Exception exception = null)
		{
			LogModel item = CreateLog(LogModel.LogType.Error, message);

				// Asigna la excepción
				item.Exception = exception;
				// Lanza el evento (una vez creado el elemento de log completo)
				RaiseLog(item);
				// Devuelve el log creado
				return item;
		}

		/// <summary>
		///		Añade un mensaje de progreso
		/// </summary>
		internal LogModel Progress(string message, long actual, long total)
		{
			LogModel item = CreateLog(LogModel.LogType.Progress, message);

				// Asigna los datos de progreso
				item.ActualProgress = actual;
				item.TotalProgress = total;
				// Lanza el evento (una vez creado el elemento de log completo)
				RaiseLog(item);
				// Devuelve el elemento de log creado
				return item;
		}

		/// <summary>
		///		Añade un mensaje de traza
		/// </summary>
		public LogModel Trace(string message)
		{
			return Log(LogModel.LogType.Trace, message);
		}

		/// <summary>
		///		Añade una aserción
		/// </summary>
		public LogModel Assert(bool withError, string message)
		{
			if (withError)
				return AssertError(message);
			else
				return AssertCorrect(message);
		}

		/// <summary>
		///		Añade un mensaje de error en suposición
		/// </summary>
		public LogModel AssertError(string message)
		{
			return Log(LogModel.LogType.AssertError, message);
		}

		/// <summary>
		///		Añade un mensaje de suposición correcta
		/// </summary>
		public LogModel AssertCorrect(string message)
		{
			return Log(LogModel.LogType.AssertCorrect, message);
		}

		/// <summary>
		///		Añade información destinada a consola
		/// </summary>
		public LogModel Console(string message)
		{
			return Log(LogModel.LogType.Console, message);
		}

		/// <summary>
		///		Añade un elemento de log
		/// </summary>
		public LogModel Log(LogModel.LogType type, string message)
		{
			LogModel log = CreateLog(type, message);

				// Envía el evento de log
				RaiseLog(log);
				// Devuelve el elemento de log
				return log;
		}

		/// <summary>
		///		Añade un elemento de log
		/// </summary>
		private LogModel CreateLog(LogModel.LogType type, string message)
		{
			LogModel log;

				// Añade el elemento del log al último bloque o a la lista general
				if (LastBlock != null)
					log = new LogModel(LastBlock.Item, type, message);
				else
					log = new LogModel(null, type, message);
				// Añade el elemento de log
				lock (_lock)
				{
					Items.Add(log);
				}
				// Guarda el log
				LastLog = log;
				// Devuelve el elemento de log
				return log;
		}

		/// <summary>
		///		Añade un parámetro al log
		/// </summary>
		public void AddParameter(string name, object value)
		{
			if (LastLog != null)
				LastLog.AddParameter(name, value);
		}

		/// <summary>
		///		Lanza los datos de log
		/// </summary>
		public void Flush()
		{
			lock (_lock)
			{
				// Llama a los procesos de almacenamiento
				foreach (Writers.ILogWriter writer in Context.Manager.Writers)
					writer.Flush(Items);
				// Limpia los elementos de log que teníamos hasta ahora
				Clear();
			}
		}

		/// <summary>
		///		Lanza el evento de log
		/// </summary>
		private void RaiseLog(LogModel log)
		{
			Context.Manager.RaiseLog(log);
		}

		/// <summary>
		///		Contexto de los elementos de log
		/// </summary>
		public ContextModel Context { get; }

		/// <summary>
		///		Pila de bloques de log
		/// </summary>
		private List<BlockLogModel> Blocks { get; } = new List<BlockLogModel>();

		/// <summary>
		///		Pila de bloques de log
		/// </summary>
		public BlockLogModel LastBlock 
		{ 
			get
			{
				if (Blocks.Count == 0)
					return null;
				else
					return Blocks[Blocks.Count - 1];
			}
		}

		/// <summary>
		///		Elementos de log
		/// </summary>
		public List<LogModel> Items { get; } = new List<LogModel>();

		/// <summary>
		///		Ultimo elemento de log añadido
		/// </summary>
		public LogModel LastLog { get; private set; }
	}
}

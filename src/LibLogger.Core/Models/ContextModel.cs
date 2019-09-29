using System;
using System.Collections.Generic;

using Bau.Libraries.LibLogger.Core.Models.Metrics;

namespace Bau.Libraries.LibLogger.Core.Models
{
	/// <summary>
	///		Modelo con el contexto de log
	/// </summary>
	public class ContextModel : Base.SetBaseModel
	{
		public ContextModel(LogManager manager, string key, string app) : base(key)
		{
			Manager = manager;
			App = app;
			LogRecords = new Log.LogModelCollection(this);
			//Metrics = new MetricModelCollection(this);
			Metrics = new MetricGroupModelCollection(this);
		}

		/// <summary>
		///		Añade un bloque
		/// </summary>
		public Log.BlockLogModel CreateBlock(Log.LogModel.LogType type, string message)
		{
			return LogRecords.CreateBlock(type, message);
		}

		/// <summary>
		///		Envía los elementos para que se traten
		/// </summary>
		public void Flush()
		{
			LogRecords.Flush();
		}

		/// <summary>
		///		Manager de log
		/// </summary>
		public LogManager Manager { get; }

		/// <summary>
		///		Nombre de aplicación a la que se asocia el contexto
		/// </summary>
		public string App { get; }

		/// <summary>
		///		Parámetros globales del contexto
		/// </summary>
		public Dictionary<string, object> Parameters { get; } = new Dictionary<string, object>();

		/// <summary>
		///		Elementos de log
		/// </summary>
		public Log.LogModelCollection LogRecords { get; }

		/// <summary>
		///		Grupos de métricas
		/// </summary>
		public MetricGroupModelCollection Metrics { get; }
	}
}

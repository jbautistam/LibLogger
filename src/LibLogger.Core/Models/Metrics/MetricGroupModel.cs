using System;

namespace Bau.Libraries.LibLogger.Core.Models.Metrics
{
	/// <summary>
	///		Grupo de métricas
	/// </summary>
	public abstract class MetricGroupModel
	{
		/// <summary>
		///		Inicializa las métricas
		/// </summary>
		public abstract void Reset();

		/// <summary>
		///		Clave del grupo
		/// </summary>
		public string Key { get; internal set; }

		/// <summary>
		///		Unidades
		/// </summary>
		public MetricUnitModel Unit { get; internal set; }
	}
}

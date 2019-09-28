using System;

namespace Bau.Libraries.LibLogger.Core.Models.Metrics
{
	/// <summary>
	///		Clase base con los datos de una métrica
	/// </summary>
	public abstract class MetricModel
	{
		/// <summary>
		///		Inicializa el valor
		/// </summary>
		public abstract void Reset();

		/// <summary>
		///		Clave de la métrica
		/// </summary>
		public string Key { get; internal set; }

		/// <summary>
		///		Grupo al que pertenece la métrica
		/// </summary>
		public string Group { get; internal set; }

		/// <summary>
		///		Unidades de la métrica
		/// </summary>
		public MetricUnitModel Unit { get; internal set; }
	}
}

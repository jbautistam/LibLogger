using System;

namespace Bau.Libraries.LibLogger.Core.Models.Metrics
{
	/// <summary>
	///		Valor de una métrica
	/// </summary>
	public class MetricValueModel<TypeData>
	{
		public MetricValueModel(string key, TypeData value)
		{
			Key = key;
			Value = value;
		}

		/// <summary>
		///		Clave del valor
		/// </summary>
		public string Key { get; }

		/// <summary>
		///		Valor
		/// </summary>
		public TypeData Value { get; internal set; }
	}
}

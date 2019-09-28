using System;

namespace Bau.Libraries.LibLogger.Core.Models.Metrics
{
	/// <summary>
	///		Colección de <see cref="MetricModel"/>
	/// </summary>
	public class MetricModelCollection : Base.NormalizedModelDictionary<MetricModel>
	{
		public MetricModelCollection(ContextModel context)
		{
			Context = context;
		}

		/// <summary>
		///		Crea una métrica para un valor
		/// </summary>
		public TypeData Create<TypeData>(string key, string group, MetricUnitModel unit) where TypeData : MetricModel, new()
		{
			TypeData metric = new TypeData();
			
				// Asigna las propiedades básicas de la métrica
				metric.Key = key;
				metric.Group = group;
				metric.Unit = unit;
				// Añade el valor
				Add(key, metric);
				// Devuelve el valor
				return metric;
		}

		/// <summary>
		///		Obtiene una métrica
		/// </summary>
		public TypeData GetMetric<TypeData>(string key) where TypeData : MetricModel
		{
			return Get(key) as TypeData;
		}

		/// <summary>
		///		Inicializa todas las métricas
		/// </summary>
		public void Reset()
		{
			foreach ((string _, MetricModel metric) in Enumerate())
				metric.Reset();
		}

		/// <summary>
		///		Contexto de la métrica
		/// </summary>
		public ContextModel Context { get; }
	}
}

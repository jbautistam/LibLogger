using System;

namespace Bau.Libraries.LibLogger.Core.Models.Metrics
{
	/// <summary>
	///		Métrica con un histograma
	/// </summary>
	public class MetricHistogramModel : MetricModel
	{
		/// <summary>
		///		Modifica el valor de una serie
		/// </summary>
		public void Update(string serie, double value)
		{
			MetricHistogramSerieModel serieValues;

				// Obtiene la serie
				if (Series.Contains(serie))
					serieValues = Series.Get(serie);
				else
				{
					// Crea una nueva serie
					serieValues = new MetricHistogramSerieModel();
					// y la añade al diccionario de series
					Series.Add(serie, serieValues);
				}
				// Añade el valor a la serie
				serieValues.Values.Add(value);
		}

		/// <summary>
		///		Inicializa los valores
		/// </summary>
		public override void Reset()
		{
			Series.Clear();
		}

		/// <summary>
		///		Series
		/// </summary>
		public Base.NormalizedModelDictionary<MetricHistogramSerieModel> Series { get; } = new Base.NormalizedModelDictionary<MetricHistogramSerieModel>();
	}
}

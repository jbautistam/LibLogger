using System;

using Bau.Libraries.LibLogger.Core.Models.Base;

namespace Bau.Libraries.LibLogger.Core.Models.Metrics
{
	/// <summary>
	///		Definición de una métrica para medir tiempos y ratios de sucesos
	/// </summary>
	public class MetricTimerModel : MetricModel
	{
		/// <summary>
		///		Marca una métrica
		/// </summary>
		public void Mark(string serie, double value)
		{
			TimeSerieModel<MetricHistogramSerieModel> timeSerie;

				// Obtiene la serie
				if (Meters.Contains(serie))
					timeSerie = Meters.Get(serie);
				else
				{
					// Crea la serie
					timeSerie = new TimeSerieModel<MetricHistogramSerieModel>();
					// Añade la serie
					Meters.Add(serie, timeSerie);
				}
				// Marca el elemento
				timeSerie.Add(CreateNewHistogram(value),
							  item => {
										item.Values.Add(value);
										return item;
									  }
							 );
		}

		/// <summary>
		///		Crea un nuevo histograma
		/// </summary>
		private MetricHistogramSerieModel CreateNewHistogram(double value)
		{
			MetricHistogramSerieModel histogram = new MetricHistogramSerieModel();

				// Añade el valor al histograma
				histogram.Values.Add(value);
				// Devuelve el histograma creado
				return histogram;
		}

		/// <summary>
		///		Obtiene las marcas de los últimos n minutos
		/// </summary>
		private NormalizedModelDictionary<MetricHistogramSerieModel> GetMarksLast(int minutes)
		{
			NormalizedModelDictionary<MetricHistogramSerieModel> series = new NormalizedModelDictionary<MetricHistogramSerieModel>();

				// Acumula las métricas
				foreach ((string serie, TimeSerieModel<MetricHistogramSerieModel> timeSerie) in Meters.Enumerate())
					series.Add(serie, timeSerie.GetMarksLast(minutes));
				// Devuelve las series
				return series;
		}

		/// <summary>
		///		Inicializa la métrica
		/// </summary>
		public override void Reset()
		{
			Meters.Clear();
		}

		/// <summary>
		///		Métricas
		/// </summary>
		private NormalizedModelDictionary<TimeSerieModel<MetricHistogramSerieModel>> Meters { get; } = new NormalizedModelDictionary<TimeSerieModel<MetricHistogramSerieModel>>();

		/// <summary>
		///		Obtiene las métricas del último minuto
		/// </summary>
		public NormalizedModelDictionary<MetricHistogramSerieModel> MeterLastMinute 
		{ 
			get { return GetMarksLast(1); }
		}

		/// <summary>
		///		Obtiene las métricas de los últimos cinco minutos
		/// </summary>
		public NormalizedModelDictionary<MetricHistogramSerieModel> MeterLastFiveMinutes 
		{ 
			get { return GetMarksLast(5); }
		}

		/// <summary>
		///		Obtiene las métricas de los últimos quince minutos
		/// </summary>
		public NormalizedModelDictionary<MetricHistogramSerieModel> MeterLastFifteenMinutes 
		{ 
			get { return GetMarksLast(15); }
		}
	}
}

using System;

namespace Bau.Libraries.LibLogger.Core.Models.Metrics.Histograms
{
	/// <summary>
	///		Grupo de <see cref="HistogramModel"/>
	/// </summary>
	public class HistogramGroupModel : MetricGroupModel
	{
		/// <summary>
		///		Inicializa los valores
		/// </summary>
		public override void Reset()
		{
			Histograms.Reset();
		}

		/// <summary>
		///		Elementos
		/// </summary>
		public MetricGroupItemsModel<HistogramModel> Histograms { get; } = new MetricGroupItemsModel<HistogramModel>();
	}
}

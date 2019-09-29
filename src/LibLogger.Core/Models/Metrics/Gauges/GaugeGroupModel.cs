using System;

namespace Bau.Libraries.LibLogger.Core.Models.Metrics.Gauges
{
	/// <summary>
	///		Grupo de <see cref="GaugeModel"/>
	/// </summary>
	public class GaugeGroupModel : MetricGroupModel
	{
		/// <summary>
		///		Inicializa los valores
		/// </summary>
		public override void Reset()
		{
			Gauges.Reset();
		}
		/// <summary>
		///		Elementos
		/// </summary>
		public MetricGroupItemsModel<GaugeModel> Gauges { get; } = new MetricGroupItemsModel<GaugeModel>();
	}
}

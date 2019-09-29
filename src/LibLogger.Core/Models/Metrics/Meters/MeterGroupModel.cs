using System;

namespace Bau.Libraries.LibLogger.Core.Models.Metrics.Meters
{
	/// <summary>
	///		Grupo de <see cref="MeterModel"/>
	/// </summary>
	public class MeterGroupModel : MetricGroupModel
	{
		/// <summary>
		///		Inicializa los valores
		/// </summary>
		public override void Reset()
		{
			Meters.Reset();
		}

		/// <summary>
		///		Elementos
		/// </summary>
		public MetricGroupItemsModel<MeterModel> Meters { get; } = new MetricGroupItemsModel<MeterModel>();
	}
}

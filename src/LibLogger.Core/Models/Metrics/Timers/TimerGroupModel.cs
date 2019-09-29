using System;

namespace Bau.Libraries.LibLogger.Core.Models.Metrics.Timers
{
	/// <summary>
	///		Grupo de <see cref="TimerModel"/>
	/// </summary>
	public class TimerGroupModel : MetricGroupModel
	{
		/// <summary>
		///		Inicializa los valores
		/// </summary>
		public override void Reset()
		{
			Timers.Reset();
		}

		/// <summary>
		///		Elementos
		/// </summary>
		public MetricGroupItemsModel<TimerModel> Timers { get; } = new MetricGroupItemsModel<TimerModel>();
	}
}

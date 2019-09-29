using System;

namespace Bau.Libraries.LibLogger.Core.Models.Metrics.Counters
{
	/// <summary>
	///		Grupo de <see cref="CounterModel"/>
	/// </summary>
	public class CounterGroupModel : MetricGroupModel
	{
		/// <summary>
		///		Inicializa los contadores
		/// </summary>
		public override void Reset()
		{
			Counters.Reset();
		}

		/// <summary>
		///		Contadores
		/// </summary>
		public MetricGroupItemsModel<CounterModel> Counters { get; } = new MetricGroupItemsModel<CounterModel>();
	}
}

using System;
using Xunit;
using FluentAssertions;

using Bau.Libraries.LibLogger.Core;
using Bau.Libraries.LibLogger.Models.Metrics;
using Bau.Libraries.LibLogger.Models.Metrics.Counters;
using Bau.Libraries.LibLogger.Models.Metrics.Gauges;
using Bau.Libraries.LibLogger.Models.Metrics.Histograms;
using Bau.Libraries.LibLogger.Models.Metrics.Meters;
using Bau.Libraries.LibLogger.Models.Metrics.Timers;

namespace LibLogger.Test
{
	/// <summary>
	///		Clase de pruebas para métricas
	/// </summary>
	public class Metrics_should
	{
		/// <summary>
		///		Crea un grupo de contadores
		/// </summary>
		[Fact]
		public void create_counters_group()
		{
			LogManager manager = new LogManager();
			CounterGroupModel groups = manager.Default.Metrics.Create<CounterGroupModel>("group", MetricUnitModel.Bytes);

				// Añade los contadores
				groups.Counters["first"].Increment();
				groups.Counters["second"].Increment();
				groups.Counters["second"].Increment(3);
				groups.Counters["second"].Decrement();
				// Obtiene los valores
				groups.Counters.Count.Should().Be(2);
				manager.Default.Metrics.GetGroup<CounterGroupModel>("group").Counters.Count.Should().Be(2);
				manager.Default.Metrics.GetGroup<CounterGroupModel>("group").Counters["first"].Value.Should().Be(1);
				manager.Default.Metrics.GetGroup<CounterGroupModel>("group").Counters["second"].Value.Should().Be(3);
		}

		/// <summary>
		///		Crea un grupo de gauge
		/// </summary>
		[Fact]
		public void create_gauges_group()
		{
			LogManager manager = new LogManager();
			GaugeGroupModel groups = manager.Default.Metrics.Create<GaugeGroupModel>("group", MetricUnitModel.Bytes);

				// Añade los contadores
				groups.Gauges["first"].Assign(3);
				groups.Gauges["second"].Assign(1);
				groups.Gauges["second"].Assign(7);
				groups.Gauges["second"].Assign(2);
				// Obtiene los valores
				groups.Gauges.Count.Should().Be(2);
				manager.Default.Metrics.GetGroup<GaugeGroupModel>("group").Gauges.Count.Should().Be(2);
				manager.Default.Metrics.GetGroup<GaugeGroupModel>("group").Gauges["first"].Value.Should().Be(3);
				manager.Default.Metrics.GetGroup<GaugeGroupModel>("group").Gauges["second"].Value.Should().Be(2);
				manager.Default.Metrics.GetGroup<GaugeGroupModel>("group").Gauges["second"].Minimum.Should().Be(1);
				manager.Default.Metrics.GetGroup<GaugeGroupModel>("group").Gauges["second"].Maximum.Should().Be(7);
		}

		/// <summary>
		///		Comprueba que se puede tratar un valor de métrica
		/// </summary>
		[Fact]
		public void create_meter_group()
		{
			LogManager manager = new LogManager();
			MeterGroupModel groups = manager.Default.Metrics.Create<MeterGroupModel>("group", MetricUnitModel.Bytes);

				// Espera al siguiente minuto
				WaitNextMinute();
				// Añade los contadores
				groups.Meters["first"].Mark();
				groups.Meters["first"].Mark();
				groups.Meters["second"].Mark();
				groups.Meters["second"].Mark();
				groups.Meters["second"].Mark();
				// Obtiene los valores
				groups.Meters.Count.Should().Be(2);
				groups.Meters["first"].LastMinute.Should().Be(2);
				groups.Meters["first"].LastFiveMinutes.Should().Be(2);
				groups.Meters["first"].LastFifteenMinutes.Should().Be(2);
				groups.Meters["second"].LastMinute.Should().Be(3);
		}

		/// <summary>
		///		Comprueba que se puede tratar un valor de histograma
		/// </summary>
		[Fact]
		public void create_histogram_group()
		{
			LogManager manager = new LogManager();
			HistogramGroupModel group = manager.Default.Metrics.Create<HistogramGroupModel>("Group", MetricUnitModel.Bytes);

				// Incrementa, decrementa diferentes datos
				group.Histograms["first"].Add(1);
				group.Histograms["second"].Add(2);
				group.Histograms["third"].Add(3);
				group.Histograms["first"].Add(4);
				group.Histograms["second"].Add(5);
				group.Histograms["third"].Add(6);
				group.Histograms["third"].Add(7);
				// Comprueba el resultado de first
				group.Histograms["first"].Count.Should().Be(2);
				group.Histograms["first"].Maximum.Should().Be(4);
				group.Histograms["first"].Minimum.Should().Be(1);
				group.Histograms["first"].Total.Should().Be(5);
				group.Histograms["first"].Average.Should().Be((1.0 + 4.0) / 2.0);
				// Comprueba el resultado de second
				group.Histograms["second"].Count.Should().Be(2);
				group.Histograms["second"].Maximum.Should().Be(5);
				group.Histograms["second"].Minimum.Should().Be(2);
				group.Histograms["second"].Total.Should().Be(7);
				group.Histograms["second"].Average.Should().Be((2.0 + 5.0) / 2.0);
				// Comprueba el resultado de third
				group.Histograms["third"].Count.Should().Be(3);
				group.Histograms["third"].Maximum.Should().Be(7);
				group.Histograms["third"].Minimum.Should().Be(3);
				group.Histograms["third"].Total.Should().Be(3 + 6 + 7);
				group.Histograms["third"].Average.Should().Be((3.0 + 6.0 + 7.0) / 3.0);
		}

		/// <summary>
		///		Comprueba que se puede tratar un valor de temporizador
		/// </summary>
		[Fact]
		public void create_timer_group()
		{
			LogManager manager = new LogManager();
			TimerGroupModel group;

				// Espera al siguiente minuto
				WaitNextMinute();
				// Crea la métrica
				group = manager.Default.Metrics.Create<TimerGroupModel>("Group", MetricUnitModel.Bytes);
				// Incrementa, decrementa diferentes datos
				group.Timers["first"].Mark(30);
				group.Timers["first"].Mark(10);
				group.Timers["first"].Mark(20);
				group.Timers["second"].Mark(40);
				// Comprueba el resultado de la primera serie
				group.Timers["first"].LastMinute.Count.Should().Be(3);
				group.Timers["first"].LastMinute.Total.Should().Be(10 + 20 + 30);
				group.Timers["first"].LastMinute.Minimum.Should().Be(10);
				group.Timers["first"].LastMinute.Maximum.Should().Be(30);
				group.Timers["first"].LastMinute.Average.Should().Be((10.0 + 20.0 + 30.0) / 3.0);
				// Comprueba el resultado de la segunda serie
				group.Timers["second"].LastMinute.Count.Should().Be(1);
		}

		/// <summary>
		///		Espera al siguiente minuto
		/// </summary>
		private void WaitNextMinute()
		{
			DateTime now = DateTime.Now;

				while (now.Second > 50)
				{
					System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(1));
					now = DateTime.Now;
				}
		}
	}
}

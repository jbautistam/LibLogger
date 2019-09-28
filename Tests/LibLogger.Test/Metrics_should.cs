using System;
using Xunit;
using FluentAssertions;

using Bau.Libraries.LibLogger.Core;
using Bau.Libraries.LibLogger.Core.Models.Metrics;

namespace LibLogger.Test
{
	/// <summary>
	///		Clase de pruebas para métricas
	/// </summary>
	public class Metrics_should
	{
		/// <summary>
		///		Comprueba que se puede crear un contador
		/// </summary>
		[Fact]
		public void CreateCounter()
		{
			LogManager manager = new LogManager();

				// Crea un contador
				manager.Default.Metrics.Create<MetricCounterModel>("Main", "Group", "number");
				// Obtiene el contador
				manager.Default.Metrics.Get("Main").Should().NotBeNull();
		}

		/// <summary>
		///		Comprueba que se puede tratar un contador
		/// </summary>
		[Fact]
		public void TreatCounter()
		{
			LogManager manager = new LogManager();
			MetricCounterModel counter;

				// Crea un contador
				counter = manager.Default.Metrics.Create<MetricCounterModel>("Main", "Group", "number");
				// Incrementa, decrementa diferentes datos
				counter.Increment(5);
				counter.Decrement(2);
				// Comprueba el resultado
				counter.Value.Should().Be(3);
		}

		/// <summary>
		///		Comprueba que se pueden tratar varios contador
		/// </summary>
		[Fact]
		public void TreatCounters()
		{
			LogManager manager = new LogManager();
			MetricCounterModel counter, counter2, counter3;

				// Crea varios contadores
				counter = manager.Default.Metrics.Create<MetricCounterModel>("first", "Group", "number");
				counter2 = manager.Default.Metrics.Create<MetricCounterModel>("second", "Group", "number");
				counter3 = manager.Default.Metrics.Create<MetricCounterModel>("third", "Group", "number");
				// Incrementa, decrementa diferentes datos
				counter.Increment(5);
				counter2.Increment(7);
				counter3.Increment(12);
				// Comprueba los resultados
				manager.Default.Metrics.GetMetric<MetricCounterModel>("first").Value.Should().Be(5);
				manager.Default.Metrics.GetMetric<MetricCounterModel>("second").Value.Should().Be(7);
				manager.Default.Metrics.GetMetric<MetricCounterModel>("third").Value.Should().Be(12);
		}

		/// <summary>
		///		Comprueba que se puede crear un valor Gauge
		/// </summary>
		[Fact]
		public void CreateGauge()
		{
			LogManager manager = new LogManager();

				// Crea un contador
				manager.Default.Metrics.Create<MetricGaugeModel>("Main", "Group", "number");
				// Obtiene el contador
				manager.Default.Metrics.Get("Main").Should().NotBeNull();
		}

		/// <summary>
		///		Comprueba que se puede tratar un valor gauge
		/// </summary>
		[Fact]
		public void TreatGauge()
		{
			LogManager manager = new LogManager();
			MetricGaugeModel gauge;

				// Crea un contador
				gauge = manager.Default.Metrics.Create<MetricGaugeModel>("Main", "Group", "number");
				// Incrementa, decrementa diferentes datos
				gauge.Assign(5);
				gauge.Assign(3);
				gauge.Assign(2);
				gauge.Assign(1);
				// Comprueba el resultado
				gauge.Value.Should().Be(1);
		}

		/// <summary>
		///		Comprueba que se puede tratar los valores máximos y mínimos del gauge
		/// </summary>
		[Fact]
		public void TreatGaugeMaximum()
		{
			LogManager manager = new LogManager();
			MetricGaugeModel gauge;

				// Crea un contador
				gauge = manager.Default.Metrics.Create<MetricGaugeModel>("Main", "Group", "number");
				// Incrementa, decrementa diferentes datos
				gauge.Assign(5);
				gauge.Assign(3);
				gauge.Assign(2);
				gauge.Assign(1);
				gauge.Assign(4);
				// Comprueba los resultados
				gauge.Value.Should().Be(4);
				gauge.Maximum.Should().Be(5);
				gauge.Minimum.Should().Be(1);
		}

		/// <summary>
		///		Comprueba que se puede tratar un valor de métrica
		/// </summary>
		[Fact]
		public void TreatMeter()
		{
			LogManager manager = new LogManager();
			MetricMeterModel meter;

				// Crea un contador
				meter = manager.Default.Metrics.Create<MetricMeterModel>("Main", "Group", "number");
				// Incrementa, decrementa diferentes datos
				meter.Mark();
				// Comprueba el resultado
				meter.MeterLastMinute.Should().Be(1);
				meter.MeterLastFiveMinutes.Should().Be(1);
				meter.MeterLastFifteenMinutes.Should().Be(1);
		}

		/// <summary>
		///		Comprueba que se puede tratar un valor de histograma
		/// </summary>
		[Fact]
		public void TreatHistogram()
		{
			LogManager manager = new LogManager();
			MetricHistogramModel meter;

				// Crea un contador
				meter = manager.Default.Metrics.Create<MetricHistogramModel>("Main", "Group", MetricUnitModel.Bytes);
				// Incrementa, decrementa diferentes datos
				meter.Update("first", 1);
				meter.Update("second", 2);
				meter.Update("third", 3);
				meter.Update("first", 4);
				meter.Update("second", 5);
				meter.Update("third", 6);
				meter.Update("third", 7);
				// Comprueba el resultado de first
				meter.Series.Get("first").Values.Count.Should().Be(2);
				meter.Series.Get("first").Maximum.Should().Be(4);
				meter.Series.Get("first").Minimum.Should().Be(1);
				meter.Series.Get("first").Total.Should().Be(5);
				meter.Series.Get("first").Average.Should().Be((1.0 + 4.0) / 2.0);
				// Comprueba el resultado de second
				meter.Series.Get("second").Values.Count.Should().Be(2);
				meter.Series.Get("second").Maximum.Should().Be(5);
				meter.Series.Get("second").Minimum.Should().Be(2);
				meter.Series.Get("second").Total.Should().Be(7);
				meter.Series.Get("second").Average.Should().Be((2.0 + 5.0) / 2.0);
				// Comprueba el resultado de third
				meter.Series.Get("third").Values.Count.Should().Be(3);
				meter.Series.Get("third").Maximum.Should().Be(7);
				meter.Series.Get("third").Minimum.Should().Be(3);
				meter.Series.Get("third").Total.Should().Be(3 + 6 + 7);
				meter.Series.Get("third").Average.Should().Be((3.0 + 6.0 + 7.0) / 3.0);
		}

		/// <summary>
		///		Comprueba que se puede tratar un valor de temporizador
		/// </summary>
		[Fact]
		public void TreatTimer()
		{
			LogManager manager = new LogManager();
			MetricTimerModel meter;

				// Crea la métrica
				meter = manager.Default.Metrics.Create<MetricTimerModel>("Main", "Group", MetricUnitModel.Bytes);
				// Incrementa, decrementa diferentes datos
				meter.Mark("first", 30);
				// Comprueba el resultado
				meter.MeterLastMinute.Get("first").Series.Get("third").Values.Count.Should().Be(3);
				meter.Series.Get("third").Maximum.Should().Be(7);
				meter.Series.Get("third").Minimum.Should().Be(3);
				meter.Series.Get("third").Total.Should().Be(3 + 6 + 7);
				meter.Series.Get("third").Average.Should().Be((3.0 + 6.0 + 7.0) / 3.0);
		}
	}
}

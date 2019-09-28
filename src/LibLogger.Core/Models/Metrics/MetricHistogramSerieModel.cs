using System;
using System.Collections.Generic;

namespace Bau.Libraries.LibLogger.Core.Models.Metrics
{
	/// <summary>
	///		Elementos de una serie de <see cref="MetricHistogramModel"/>
	/// </summary>
	public class MetricHistogramSerieModel
	{
		/// <summary>
		///		Valores
		/// </summary>
		public List<double> Values { get; } = new List<double>();

		/// <summary>
		///		Suma
		/// </summary>
		public double Total
		{
			get
			{
				double sum = 0;

					// Acumula los valores
					foreach (double value in Values)
						sum += value;
					// Devuelve la suma
					return sum;
			}
		}

		/// <summary>
		///		Media
		/// </summary>
		public double Average
		{
			get
			{
				if (Values.Count == 0)
					return 0;
				else
					return Total / Values.Count;
			}
		}

		/// <summary>
		///		Valor máximo
		/// </summary>
		public double Maximum
		{
			get
			{
				if (Values.Count == 0)
					return 0;
				else
				{
					double maximum = Values[0];

						// Obtiene el valor máximo
						foreach (double value in Values)
							if (value > maximum)
								maximum = value;
						// Devuelve el valor máximo
						return maximum;
				}
			}
		}

		/// <summary>
		///		Valor mínimo
		/// </summary>
		public double Minimum
		{
			get
			{
				if (Values.Count == 0)
					return 0;
				else
				{
					double minimum = Values[0];

						// Obtiene el valor mínimo
						foreach (double value in Values)
							if (value < minimum)
								minimum = value;
						// Devuelve el valor mínimo
						return minimum;
				}
			}
		}
	}
}

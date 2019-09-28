using System;
using System.Collections.Generic;

namespace Bau.Libraries.LibLogger.Core.Models
{
	/// <summary>
	///		Colección de <see cref="ContextModel"/>
	/// </summary>
	public class ContextModelCollection : Base.SetBaseModelDictionary<ContextModel>
	{
		public ContextModelCollection(LogManager manager)
		{
			Manager = manager;
		}

		/// <summary>
		///		Libera el contenido de los elementos
		/// </summary>
		public void Flush()
		{
			foreach (KeyValuePair<string, ContextModel> item in Dictionary)
				item.Value.Flush();
		}

		/// <summary>
		///		Manager de log
		/// </summary>
		public LogManager Manager { get; }
	}
}

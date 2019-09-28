using System;

namespace Bau.Libraries.LibLogger.Core.Models.Base
{
	/// <summary>
	///		Clase con los datos básicos para un grupo
	/// </summary>
	public abstract class SetBaseModel : IDisposable
	{
		// Eventos públicos
		internal event EventHandler Disposed;

		protected SetBaseModel(string key)
		{
			Key = key;
		}

		/// <summary>
		///		Libera la memoria
		/// </summary>
		protected virtual void Dispose(bool disposing)
		{
			if (!IsDisposed)
			{
				// Lanza el evento para indicar al manager que puede destruir este objeto
				if (disposing)
					Disposed?.Invoke(this, EventArgs.Empty);
				// Indica que se ha liberado
				IsDisposed = true;
			}
		}

		/// <summary>
		///		Libera la memoria
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
		}

		/// <summary>
		///		Clave del elemento
		/// </summary>
		public string Key { get; }

		/// <summary>
		///		Indica si se ha liberado la memoria del contexto
		/// </summary>
		public bool IsDisposed { get; private set; }
	}
}

using System;
using System.Collections.Generic;

namespace Bau.Libraries.LibLogger.Core.Models.Base
{
	/// <summary>
	///		Diccionario de <see cref="SetBaseModel"/>
	/// </summary>
	public abstract class SetBaseModelDictionary<TypeData> where TypeData : SetBaseModel
	{
		/// <summary>
		///		Añade un elemento
		/// </summary>
		public TypeData Add(TypeData item)
		{
			return Add(item.Key, item);
		}

		/// <summary>
		///		Añade un elemento
		/// </summary>
		public TypeData Add(string key, TypeData item)
		{
			// Añade el elemento a la colección
			Dictionary.Add(Normalize(key), item);
			// Asigna el manejador de eventos para quitar el elemento del diccionario cuando sea necesario
			item.Disposed += (sender, args) => Dictionary.Remove(item.Key);
			// Devuelve el elemento añadido
			return item;
		}

		/// <summary>
		///		Comprueba si contiene una clave
		/// </summary>
		public bool Contains(string key)
		{
			return Dictionary.ContainsKey(Normalize(key));
		}

		/// <summary>
		///		Obtiene un elemento
		/// </summary>
		public TypeData Get(string key)
		{
			return Dictionary[Normalize(key)];
		}

		/// <summary>
		///		Borra un elemento
		/// </summary>
		public void Remove(string key)
		{
			Dictionary.Remove(Normalize(key));
		}

		/// <summary>
		///		Normaliza la clave
		/// </summary>
		private string Normalize(string key)
		{
			return key.Trim().ToUpper();
		}

		/// <summary>
		///		Enumera el contenido del diccionario
		/// </summary>
		public IEnumerable<(string key, TypeData value)> Enumerate()
		{
			foreach (KeyValuePair<string, TypeData> item in Dictionary)
				yield return (item.Key, item.Value);
		}

		/// <summary>
		///		Diccionario
		/// </summary>
		protected Dictionary<string, TypeData> Dictionary { get; } = new Dictionary<string, TypeData>();
	}
}

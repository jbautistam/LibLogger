using System;
using Xunit;
using FluentAssertions;

using Bau.Libraries.LibLogger.Core;
using Bau.Libraries.LibLogger.Models;

namespace LibLogger.Test
{
	/// <summary>
	///		Clase de pruebas para <see cref="LogManager"/>
	/// </summary>
	public class LogManager_should
	{
		/// <summary>
		///		Al crearse el manager, se crea un contexto predeterminado con la clave "Default"
		/// </summary>
		[Fact]
		public void CreateDefault()
		{
			LogManager manager = new LogManager();

				// Comprueba los datos del contexto predeterminado
				manager.Default.Should().NotBeNull();
				manager.Default.Key.Should().Be("Default");
				manager.Contexts.Count.Should().Be(1);
		}

		/// <summary>
		///		Se pueden crear contextos y devuelve el contexto seleccionado
		/// </summary>
		[Fact]
		public void CreateContext()
		{
			LogManager manager = new LogManager();
			ContextModel context;

				// Crea y obtiene el contexto
				context = manager.CreateContext("First", "TestApp");
				context.Should().NotBeNull();
				context.Key.Should().Be("First");
				context.App.Should().Be("TestApp");
				manager.Contexts.Count.Should().Be(2);
		}

		/// <summary>
		///		Se puede obtener un contexto
		/// </summary>
		[Fact]
		public void GetContext()
		{
			LogManager manager = new LogManager();
			ContextModel context;

				// Crea un contexto
				manager.CreateContext("First", "TestApp");
				// Obtiene el contexto
				context = manager.GetContext("FIRST");
				// Comprueba los datos del contexto
				context.Should().NotBeNull();
				context.Key.Should().Be("First");
				context.App.Should().Be("TestApp");
		}
	}
}

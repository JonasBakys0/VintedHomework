using DiscountCalculator.Models;
using DiscountCalculator.Services;
using DiscountCalculator.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DiscountCalculator
{
	class Program
	{
		private static IServiceProvider _serviceProvider;

		static void Main()
		{
			RegisterServices();
			IServiceScope scope = _serviceProvider.CreateScope();
			scope.ServiceProvider.GetRequiredService<ConsoleApplication>().Run();
			DisposeServices();
		}

		public static void RegisterServices()
		{
			var services = new ServiceCollection();
			services.AddSingleton<IRulesService, RulesService>();
			services.AddSingleton<ICountingService, CountingService>();
			services.AddSingleton<ConsoleApplication>();
			_serviceProvider = services.BuildServiceProvider(true);
		}

		private static void DisposeServices()
		{
			if (_serviceProvider == null)
			{
				return;
			}
			if (_serviceProvider is IDisposable)
			{
				((IDisposable)_serviceProvider).Dispose();
			}
		}
	}
}

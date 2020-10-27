using DiscountCalculator.Services;
using DiscountCalculator.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DiscountCalculator
{
	class Program
	{
		private static IServiceProvider _serviceProvider;

		static void Main()
		{
			RegisterServices();
			IServiceScope scope = _serviceProvider.CreateScope();
			scope.ServiceProvider.GetRequiredService<DiscountCalculator>().Run();
			DisposeServices();
		}

		public static void RegisterServices()
		{
			var services = new ServiceCollection();
			services.AddSingleton<IRulesService, RulesService>();
			services.AddSingleton<IInputOutputService, InputOutputService>();
			services.AddSingleton<IValidateService, ValidateService>();
			services.AddSingleton<ICountingService, CountingService>();
			services.AddSingleton<DiscountCalculator>();
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

using AutomationFramework.Core.Dependencies;

namespace AutomationExerciseUIBDD.Tests.Dependencies
{
	public class DIContainerSpecflow
	{
		private static IServiceProvider serviceProvider;

		public static void ConfigureService()
		{
			serviceProvider = DIContainer.ConfigureServices();
		}

		public static IServiceProvider GetServiceProvider()
		{
			return serviceProvider;
		}
	}
}

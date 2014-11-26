using System;
using Autofac;

namespace VersionedServiceLocator
{
	class Program
	{
		static void Main(string[] args)
		{
			ContainerBuilder container = new ContainerBuilder();

			// Register the version 1 implementation
			container.RegisterType<Implementation1>()
				.Keyed<IDependentInterface>(VersionRange.Version1);

			// Register the version 2 implementation
			container.RegisterType<Implementation2>()
				.Keyed<IDependentInterface>(VersionRange.Version2);

			// My example consumer.
			container.RegisterType<ExampleClass>().As<IExampleInterface>().WithVersionFilter();

			using (ILifetimeScope scope = container.Build())
			{
				// Find out which version got resolved.
				Console.WriteLine(scope.Resolve<IExampleInterface>().GetMessage());

				Console.Read();
			}
		}
	}
}

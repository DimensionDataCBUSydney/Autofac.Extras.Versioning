using System;
using Autofac;

namespace VersionedServiceLocator
{
	class Program
	{
		static void Main(string[] args)
		{
			ContainerBuilder container = new ContainerBuilder();

			container.RegisterType<Implementation1>()
				.Keyed<IDependentInterface>(VersionRange.Version1);

			container.RegisterType<Implementation2>()
				.Keyed<IDependentInterface>(VersionRange.Version2);

			// My example consumer.
			container.RegisterType<ExampleClass>().As<IExampleInterface>().WithVersionFilter();

			using (ILifetimeScope scope = container.Build())
			{
				Console.WriteLine(scope.Resolve<IExampleInterface>().GetMessage());

				Console.Read();
			}
		}
	}
}

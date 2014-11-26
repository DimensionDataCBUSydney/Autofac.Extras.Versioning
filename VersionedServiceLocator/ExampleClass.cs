namespace VersionedServiceLocator
{
	public class ExampleClass
		: IExampleInterface
	{
		private readonly IDependentInterface myImplementation;

		public ExampleClass
			(
				[VersionedDependency(VersionRange.Version2)]
				IDependentInterface myInterface
			)
		{
			myImplementation = myInterface;
		}

		public string GetMessage()
		{
			return myImplementation.GetMessage();
		}
	}
}

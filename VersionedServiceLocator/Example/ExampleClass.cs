namespace VersionedServiceLocator
{
	/// <summary>
	/// An example class that has a versioned dependency.
	/// </summary>
	public class ExampleClass
		: IExampleInterface
	{
		private readonly IDependentInterface myImplementation;

		/// <summary>
		/// Initializes a new instance of the <see cref="ExampleClass"/> class.
		/// </summary>
		/// <param name="myInterface">My interface (has to be version 2).</param>
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

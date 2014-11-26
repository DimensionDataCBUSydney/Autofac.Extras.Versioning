namespace VersionedServiceLocator
{
	[VersionedComponent(RequiredVersion = VersionRange.Version1)]
	public class Implementation2
		:IDependentInterface
	{
		public string GetMessage()
		{
			return "Number 2!";
		}
	}
}

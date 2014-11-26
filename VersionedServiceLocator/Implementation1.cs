namespace VersionedServiceLocator
{
	[VersionedComponent(RequiredVersion = VersionRange.Version1)]
	public class Implementation1
		: IDependentInterface
	{
		public string GetMessage()
		{
			return "Version 1!";
		}
	}
}

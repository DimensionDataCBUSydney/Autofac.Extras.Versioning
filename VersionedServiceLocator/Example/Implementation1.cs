namespace VersionedServiceLocator
{
	/// <summary>
	/// Implementation of <see cref="IDependentInterface"/> Version 1.
	/// </summary>
	[VersionedComponent(VersionRange.Version1)]
	public class Implementation1
		: IDependentInterface
	{
		public string GetMessage()
		{
			return "Version 1!";
		}
	}
}

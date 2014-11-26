namespace VersionedServiceLocator
{
	/// <summary>
	/// Implementation of <see cref="IDependentInterface"/> Version 2.
	/// </summary>
	[VersionedComponent(VersionRange.Version1)]
	public class Implementation2
		:IDependentInterface
	{
		public string GetMessage()
		{
			return "Number 2!";
		}
	}
}

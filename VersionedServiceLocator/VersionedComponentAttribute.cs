using System;
using System.ComponentModel.Composition;

namespace VersionedServiceLocator
{
	[MetadataAttribute]
	public class VersionedComponentAttribute
		: Attribute
	{
		public VersionRange RequiredVersion { get; set; }
	}
}

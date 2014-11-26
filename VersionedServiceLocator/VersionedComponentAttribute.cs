using System;
using System.ComponentModel.Composition;

namespace VersionedServiceLocator
{
	/// <summary>
	/// A versioned Autofac component
	/// </summary>
	[MetadataAttribute]
	public class VersionedComponentAttribute
		: Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="VersionedComponentAttribute"/> class.
		/// </summary>
		/// <param name="version">The version.</param>
		public VersionedComponentAttribute(VersionRange version)
		{
			Version = version;
		}

		/// <summary>
		/// The target version of the component
		/// </summary>
		public VersionRange Version { get; set; }
	}
}

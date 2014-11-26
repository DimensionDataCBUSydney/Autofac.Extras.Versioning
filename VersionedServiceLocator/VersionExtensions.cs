using System;
using System.Linq;
using Autofac;
using Autofac.Builder;
using Autofac.Extras.Attributed;

namespace VersionedServiceLocator
{
	/// <summary>
	/// Extension methods for specifying a component has a version filter.
	/// TODO: Find a way for this not to be required.
	/// </summary>
	public static class VersionExtensions
	{
		/// <summary>
		/// Ensure that the registered component has a version filter in it's parameter list.
		/// </summary>
		/// <typeparam name="TLimit">The type of the limit.</typeparam>
		/// <typeparam name="TReflectionActivatorData">The type of the reflection activator data.</typeparam>
		/// <typeparam name="TRegistrationStyle">The type of the registration style.</typeparam>
		/// <param name="builder">The builder.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">builder</exception>
		public static IRegistrationBuilder<TLimit, TReflectionActivatorData, TRegistrationStyle>
			WithVersionFilter<TLimit, TReflectionActivatorData, TRegistrationStyle>(
				this IRegistrationBuilder<TLimit, TReflectionActivatorData, TRegistrationStyle> builder)
			where TReflectionActivatorData : ReflectionActivatorData
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			return builder.WithParameter(
				(p, c) => p.GetCustomAttributes(true).OfType<ParameterFilterAttribute>().Any(),
				(p, c) =>
				{
					var filter = p.GetCustomAttributes(true).OfType<ParameterFilterAttribute>().First();
					return filter.ResolveParameter(p, c);
				});
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Extras.Attributed;
using Autofac.Features.Metadata;
using Autofac.Features.Scanning;

namespace VersionedServiceLocator
{
	public class VersionedDependencyAttribute
		: ParameterFilterAttribute
	{
		/// <summary>
        /// Reference to the <see cref="Autofac.Extras.Attributed.WithMetadataAttribute.FilterOne{T}"/>
        /// method used in creating a closed generic reference during registration.
        /// </summary>
        private static readonly MethodInfo filterOne = typeof(VersionedDependencyAttribute).GetMethod("FilterOne", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.InvokeMethod);
 
        /// <summary>
        /// Reference to the <see cref="Autofac.Extras.Attributed.WithMetadataAttribute.FilterAll{T}"/>
        /// method used in creating a closed generic reference during registration.
        /// </summary>
        private static readonly MethodInfo filterAll = typeof(VersionedDependencyAttribute).GetMethod("FilterAll", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.InvokeMethod);
 
        public VersionedDependencyAttribute(VersionRange range)
        {
	        this.Range = range;
        }

		public VersionRange Range { get; set; }
 
        /// <summary>
        /// Resolves a constructor parameter based on metadata requirements.
        /// </summary>
        /// <param name="parameter">The specific parameter being resolved that is marked with this attribute.</param>
        /// <param name="context">The component context under which the parameter is being resolved.</param>
        /// <returns>
        /// The instance of the object that should be used for the parameter value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if <paramref name="parameter" /> or <paramref name="context" /> is <see langword="null" />.
        /// </exception>
        public override object ResolveParameter(ParameterInfo parameter, IComponentContext context)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException("parameter");
            }
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
 
            // GetElementType currently is the effective equivalent of "Determine if the type
            // is in IEnumerable and if it is, get the type being enumerated." This doesn't support
            // the other relationship types like Lazy<T>, Func<T>, etc. If we need to add that,
            // this is the place to do it.
            var elementType = GetElementType(parameter.ParameterType);
            var hasMany = elementType != parameter.ParameterType;
 
            if (hasMany)
            {
                return filterAll.MakeGenericMethod(elementType).Invoke(null, new object[] { context, this.Range });
            }
 
            return filterOne.MakeGenericMethod(elementType).Invoke(null, new object[] { context, this.Range });
        }
 
        private static Type GetElementType(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                return type.GetGenericArguments()[0];
 
            return type;
        }
 
        private static T FilterOne<T>(IComponentContext context, VersionRange range)
        {
            // Using Lazy<T> to ensure components that aren't actually used won't get activated.
	        return context.ResolveKeyed<T>(range);
        }
 
        private static IEnumerable<T> FilterAll<T>(IComponentContext context, VersionRange range)
        {
            // Using Lazy<T> to ensure components that aren't actually used won't get activated.
	        throw new NotSupportedException("Multiple versioned dependencies not supported.");
        }
    }

	public static class VersionExtensions
	{
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

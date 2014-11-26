Autofac.Extras.Versioning
=========================

Example of a versioned service locator using Autofac.

You can add an attribute to your constructors to specify the version of a dependency to be resolved from the container.

When you register components. 

What, Why?
==============

Lets say you want to run 2 implementations of a common service, e.g. 

IMyRestApiClient

Let's say the implementations of IMyRestApiClient change, the vendor changes the API on you (not again!).

But, you have some existing code running on the old API and you want to take advantage of the new service methods.

You can register a MyRestApiClientv1 and MyRestApiClientv2 and specify the version for each component on registration.

Then, when resolving using the constructor resolver you can specify the version for each required Interface. e.g.

```
		public ExampleClass
			(
				[VersionedDependency(VersionRange.Version2)]
				IDependentInterface myInterface
			)
		{
			myImplementation = myInterface;
		}
```
This will make sure you get a component that was registered as being the implementation for Version1.

TODO
===========

Add an extension method.
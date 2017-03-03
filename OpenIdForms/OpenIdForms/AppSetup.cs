using System;
using Autofac;

namespace OpenIdForms
{
	public class AppSetup
	{
		public void CreateContainer()
		{
			var builder = new ContainerBuilder();
			RegisterDependencies(builder);
			DependencyResolver.Container = builder.Build();
		}

		protected virtual void RegisterDependencies(ContainerBuilder builder)
		{

		}
	}

	public static class DependencyResolver
	{
		public static IContainer Container;

		public static T Get<T>()
		{
			return Container.Resolve<T>();
		}
	}
}

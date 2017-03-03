using System;
using Autofac;

namespace OpenIdForms.Droid
{
	public class AndroidAppSetup : AppSetup
	{
		protected override void RegisterDependencies(Autofac.ContainerBuilder builder)
		{
			base.RegisterDependencies(builder);

			builder.RegisterType<OpenIdService>().As<IOpenIdService>().SingleInstance();
		}
	}
}

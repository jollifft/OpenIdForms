using System;
using Autofac;

namespace OpenIdForms.iOS
{
	public class IOSAppSetup : AppSetup
	{
		protected override void RegisterDependencies(Autofac.ContainerBuilder builder)
		{
			base.RegisterDependencies(builder);

			builder.RegisterType<OpenIdService>().As<IOpenIdService>().SingleInstance();
		}
	}
}

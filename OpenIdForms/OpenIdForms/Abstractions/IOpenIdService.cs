using System;
using System.Threading.Tasks;

namespace OpenIdForms
{
	public interface IOpenIdService
	{
		Task GetAuthorizationCode();
	}
}

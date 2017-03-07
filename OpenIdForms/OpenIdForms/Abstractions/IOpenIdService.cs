using System;
using System.Threading.Tasks;

namespace OpenIdForms
{
	public interface IOpenIdService
	{
		Task GetAuthorizationCode();
		bool DoTaskWithFreshTokens(Func<string, bool> work);
		void PerformTokenRequest(string authStateJson);
	}
}

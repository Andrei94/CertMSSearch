using System.Configuration;

namespace CertMSSearch
{
	internal static class AppProperties
	{
		internal static string Search => ConfigurationManager.AppSettings["searchCommand"];
		internal static string FailureMsg => ConfigurationManager.AppSettings["failureMsg"];
	}
}

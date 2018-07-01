using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CertMSSearch
{
	public static class Parser
	{
		public static string ConvertFromBase64(string str)
		{
			try
			{
				return Encoding.ASCII.GetString(System.Convert.FromBase64String(str));
			}
			catch (FormatException)
			{
				return string.Empty;
			}
		}

		public static string Convert(IEnumerable<Certificate> certs)
		{
			return string.Join(";", certs.Select(cert => System.Convert.ToBase64String(Encoding.ASCII.GetBytes(cert.ToString()))));
		}
	}
}

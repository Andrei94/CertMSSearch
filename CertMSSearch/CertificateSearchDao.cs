using System.Collections.Generic;

namespace CertMSSearch
{
	public interface CertificateSearchDao
	{
		List<Certificate> Search(string criteria);
	}
}

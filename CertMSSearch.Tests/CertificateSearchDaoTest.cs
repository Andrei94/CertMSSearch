using System.Collections.Generic;
using Xunit;
using static CertMSSearch.Tests.Utils.TestUtils;

namespace CertMSSearch.Tests
{
	public class CertificateSearchDaoTest
	{
		private readonly CertificateSearchDao dao;

		public CertificateSearchDaoTest() => dao = new InMemoryCertificateSearchDao();

		// ReSharper disable once MemberCanBePrivate.Global MemberData works ONLY with public providers
		public static IEnumerable<object[]> GetSearchCriteria()
		{
			yield return new object[] {0, "name = test"};
			yield return new object[] {2, "Subject = test"};
			yield return new object[] {3, "Issuer = me"};
			yield return new object[] {2, "Subject = test & Issuer = me"};
			yield return new object[] {3, "Subject = t | Issuer = me"};
			yield return new object[] {1, "Subject = test & Issuer = me & Username = qwerty"};
			yield return new object[] {3, "Subject = test | Issuer = him | Username = qwerty"};
			yield return new object[] {0, "Subject=test&Issuer=me|Username=qwerty"};
			yield return new object[] {2, "Subject = test & Issuer = me | das"};
			yield return new object[] {1, "Subject = test2 & Issuer = me & Password = 1234"};
			yield return new object[] {1, "ValidFrom = 11/13/2017"};
			yield return new object[] {1, "ValidUntil = 11/13/2017"};
		}

		[Theory]
		[MemberData(nameof(GetSearchCriteria))]
		public void SearchDaoInMemory(int expectedCount, string criteria)
		{
			var results = dao.Search(criteria);
			AreEqual(expectedCount, results.Count);
		}
	}
}
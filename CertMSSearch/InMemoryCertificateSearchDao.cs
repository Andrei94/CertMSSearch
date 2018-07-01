using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CertMSSearch
{
	public class InMemoryCertificateSearchDao : CertificateSearchDao
	{
		private readonly List<Certificate> certificates = new List<Certificate>
		{
			new Certificate {Subject = "test", Issuer = "me", Username = "poiuytr"},
			new Certificate {Subject = "test", Issuer = "me", Username = "qwerty"},
			new Certificate {Subject = "test2", Issuer = "me", Username = "qwerty", Password = "1234"},
			new Certificate {ValidFrom = "11/13/2017"},
			new Certificate {ValidUntil = "11/13/2017"}
		};

		public List<Certificate> Search(string criteria)
		{
			var tree = new SearchTree();
			tree.Parse(criteria);

			return new List<Certificate>(certificates.AsQueryable().Provider.CreateQuery<Certificate>(WhereCallExpression(certificates.AsQueryable(), tree)));
		}

		private static MethodCallExpression WhereCallExpression(IQueryable queryableData, SearchTree tree)
		{
			return Expression.Call(
				typeof(Queryable),
				"Where",
				new[] {queryableData.ElementType},
				queryableData.Expression,
				Expression.Lambda<Func<Certificate, bool>>(tree.CreatePredicateExpression(new QueryBuilder(tree)
					.BuildSubject()
					.BuildIssuer()
					.BuildUsername()
					.BuildPassword()
					.BuildValidFrom()
					.BuildValidUntil()
					.Build()), tree.Parameter));
		}
	}
}
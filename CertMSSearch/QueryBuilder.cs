using System.Collections.Generic;
using System.Linq.Expressions;

namespace CertMSSearch
{
	internal class QueryBuilder
	{
		private readonly List<Expression> searchQuery = new List<Expression>();
		private readonly SearchTree tree;

		internal QueryBuilder(SearchTree tree) => this.tree = tree;
		internal QueryBuilder BuildSubject() => BuildFilter(nameof(Certificate.Subject));
		internal QueryBuilder BuildIssuer() => BuildFilter(nameof(Certificate.Issuer));
		internal QueryBuilder BuildUsername() => BuildFilter(nameof(Certificate.Username));
		internal QueryBuilder BuildPassword() => BuildFilter(nameof(Certificate.Password));
		internal QueryBuilder BuildValidFrom() => BuildFilter(nameof(Certificate.ValidFrom));
		internal QueryBuilder BuildValidUntil() => BuildFilter(nameof(Certificate.ValidUntil));

		private QueryBuilder BuildFilter(string filterName)
		{
			var value = tree.GetValueForKey(filterName);
			if(!string.IsNullOrWhiteSpace(value))
				searchQuery.Add(tree.GetEqualsExpression(filterName, value));
			return this;
		}

		internal List<Expression> Build() => searchQuery;
	}
}
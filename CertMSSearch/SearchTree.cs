using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CertMSSearch
{
	internal class SearchTree
	{
		internal readonly ParameterExpression Parameter = Expression.Parameter(typeof(Certificate), "cert");
		private readonly List<KeyValuePair<string, string>> matchers = new List<KeyValuePair<string, string>>();
		private readonly Queue<string> connectors = new Queue<string>();

		internal Expression GetEqualsExpression(string property, string value)
		{
			var issuerProperty = Expression.PropertyOrField(Parameter, property);
			Expression issuerValue = Expression.Constant(value);
			return Expression.Equal(issuerProperty, issuerValue);
		}

		internal void Parse(string criteria)
		{
			var tokens = GetTokens(criteria);
			for(var index = 0; index < tokens.Length - 1; index++)
				if(tokens[index + 1].Equals("="))
					matchers.Add(new KeyValuePair<string, string>(tokens[index], tokens[index + 2]));
				else if(tokens[index].Equals("&") || tokens[index].Equals("|"))
					connectors.Enqueue(tokens[index]);
		}

		private static string[] GetTokens(string criteria) => criteria.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

		internal Expression CreatePredicateExpression(List<Expression> searchQuery)
		{
			var sn = Expression.PropertyOrField(Parameter, nameof(Certificate.SerialNumber));
			Expression predicateBody = Expression.Equal(sn, Expression.Constant(string.Empty));
			var nextConnector = connectors.Count > 0 ? connectors.Dequeue() : string.Empty;
			if (searchQuery.Count == 1)
				predicateBody = searchQuery[0];
			for(var index = 0; index < searchQuery.Count - 1; index++)
			{
				if(nextConnector.Equals("&"))
					predicateBody = Expression.And(searchQuery[index], searchQuery[index + 1]);
				else if(nextConnector.Equals("|"))
					predicateBody = Expression.OrElse(searchQuery[index], searchQuery[index + 1]);
				searchQuery[index + 1] = predicateBody;
			}
			return predicateBody;
		}

		internal string GetValueForKey(string key) => matchers.Find(pair => pair.Key.Equals(key)).Value;
	}
}
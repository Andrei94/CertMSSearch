using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;

namespace CertMSSearch
{
	public class MongoCertificateSearchDao : CertificateSearchDao
	{
		private readonly IMongoCollection<CertificateMongo> certificates;

		public MongoCertificateSearchDao() : this(ConfigurationManager.AppSettings["DBName"])
		{
		}

		private MongoCertificateSearchDao(string databaseName)
		{
			var client = new MongoClient("mongodb://localhost:27017");
			var db = client.GetDatabase(databaseName);
			certificates = db.GetCollection<CertificateMongo>("Certificates");
		}

		public List<Certificate> Search(string criteria)
		{
			var tree = new SearchTree();
			tree.Parse(criteria);

			var all = GetAll().ToList();
			var methodCallExpression = WhereCallExpression(all.AsQueryable(), tree);
			var collection = all.AsQueryable().Provider.CreateQuery<Certificate>(methodCallExpression);
			return new List<Certificate>(collection);
		}

		private IEnumerable<Certificate> GetAll()
		{
			var cursor = certificates.FindSync(x => true);
			var certs = new List<CertificateMongo>();
			while (cursor.MoveNext())
				certs.AddRange(cursor.Current);
			return certs.Select(cert => cert.CreateCertificate());
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

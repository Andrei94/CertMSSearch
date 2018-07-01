using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace CertMSSearch
{
	[BsonIgnoreExtraElements]
	public class CertificateMongo
	{
		public CertificateMongo(Certificate certificate)
		{
			SerialNumber = certificate.SerialNumber;
			Subject = certificate.Subject;
			Issuer = certificate.Issuer;
			ValidFrom = Convert.ToDateTime(certificate.ValidFrom);
			ValidUntil = Convert.ToDateTime(certificate.ValidUntil);
			ExtraProperties = new Dictionary<string, string>
			{
				{nameof(certificate.Username), certificate.Username},
				{nameof(certificate.Password), certificate.Password}
			};
		}

		public string SerialNumber { get; set; }
		public string Subject { get; set; }
		public string Issuer { get; set; }
		public DateTime ValidFrom { get; set; }
		public DateTime ValidUntil { get; set; }
		public IDictionary<string, string> ExtraProperties { get; set; }

		public Certificate CreateCertificate()
		{
			return new Certificate
			{
				SerialNumber = SerialNumber,
				Subject = Subject,
				Issuer = Issuer,
				ValidFrom = ValidFrom.ToString(),
				ValidUntil = ValidUntil.ToString(),
				Username = ExtraProperties[nameof(Certificate.Username)],
				Password = ExtraProperties[nameof(Certificate.Password)]
			};
		}
	}
}
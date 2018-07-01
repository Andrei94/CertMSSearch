using System;

namespace CertMSSearch
{
	public class Certificate
	{
		public string SerialNumber { get; set; }
		public string Subject { get; set; }
		public string Issuer { get; set; }
		public string ValidFrom { get; set; }
		public string ValidUntil { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }

		public override string ToString()
		{
			return FormatedSerialNumber + Environment.NewLine +
			       FormatedSubject + Environment.NewLine +
			       FormatedIssuer + Environment.NewLine +
			       FormatedStartDate + Environment.NewLine +
			       FormatedUsername + Environment.NewLine +
			       FormatedPassword + Environment.NewLine +
				   FormatedExpirationDate + Environment.NewLine;
		}

		private string FormatedSerialNumber => string.IsNullOrWhiteSpace(SerialNumber) ? string.Empty : $"SerialNumber: {SerialNumber}";
		private string FormatedSubject => string.IsNullOrWhiteSpace(Subject) ? string.Empty : $"Subject: {Subject}";
		private string FormatedUsername => string.IsNullOrWhiteSpace(Username) ? string.Empty : $"Username: {Username}";
		private string FormatedPassword => string.IsNullOrWhiteSpace(Password) ? string.Empty : $"Password: {Password}";
		private string FormatedIssuer => string.IsNullOrWhiteSpace(Issuer) ? string.Empty : $"Issuer: {Issuer}";
		private string FormatedStartDate => ValidFrom == null ? string.Empty : $"Valid From: {ValidFrom}";
		private string FormatedExpirationDate => ValidFrom == null ? string.Empty : $"Valid Until: {ValidUntil}";
	}
}

using Xunit;

namespace CertMSSearch.Tests.Utils
{
	internal static class TestUtils
	{
		internal static void True(bool condition)
		{
			Assert.True(condition);
		}

		internal static void AreEqual<T>(T expected, T actual)
		{
			Assert.Equal(expected, actual);
		}
	}
}

using WPFCommonUI;

namespace CertMSSearch
{
	public class SearchViewModel : ViewModelBase<IMainView>
	{
		private readonly CertificateSearchDao dao = new MongoCertificateSearchDao();

		public SearchViewModel(IMainView view) : base(view)
		{
		}

		public string PerformSearch(string data)
		{
			View.Close();
			return Parser.Convert(dao.Search(Parser.ConvertFromBase64(data)));
		}
	}
}

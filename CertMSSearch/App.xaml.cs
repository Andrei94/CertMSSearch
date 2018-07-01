using System;
using System.Windows;
using WPFCommonUI;

namespace CertMSSearch
{
	public partial class App
	{
		private ViewModelBase<IMainView> viewModel;

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			var response = AppProperties.FailureMsg;
			if (e.Args[0].Equals(AppProperties.Search))
			{
				viewModel = new SearchViewModel(new MainWindow());
				response = ((SearchViewModel)viewModel).PerformSearch(e.Args[1]);
			}
			Console.WriteLine(response);
		}
	}
}

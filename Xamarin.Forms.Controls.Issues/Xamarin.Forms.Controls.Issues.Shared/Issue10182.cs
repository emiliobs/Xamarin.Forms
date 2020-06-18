using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms.CustomAttributes;
using Xamarin.Forms.Internals;
using System.Threading.Tasks;
using System.Threading;


#if UITEST
using Xamarin.UITest;
using NUnit.Framework;
using Xamarin.Forms.Core.UITests;
#endif

namespace Xamarin.Forms.Controls.Issues
{
	[Preserve(AllMembers = true)]
	[Issue(IssueTracker.Github, 10182, "[Bug] Exception Ancestor must be provided for all pushes except first", PlatformAffected.Android, NavigationBehavior.SetApplicationRoot)]
#if UITEST
	[NUnit.Framework.Category(Core.UITests.UITestCategories.Github5000)]
	[NUnit.Framework.Category(UITestCategories.LifeCycle)]
#endif
	public class Issue10182 : TestContentPage
	{
		protected override void Init()
		{
			Content = new StackLayout()
			{
				Children =
				{
					new Label()
					{
						Text = "Click the Back Button",
						AutomationId = "Loaded"
					}
				}
			};
		}

		public async void OnSleep()
		{
			await Task.Delay(1);
			Application.Current.MainPage = new Issue10182();
		}


#if UITEST
		[Test]
		public void UpdatingSourceOfDisposedListViewDoesNotCrash()
		{
			RunningApp.WaitForElement("Loaded");
			RunningApp.Invoke("BackgroundApp");
			Thread.Sleep(1000);
			RunningApp.Invoke("ForegroundApp");
			RunningApp.TapCoordinates(100, 100);


			RunningApp.WaitForElement("Loaded");
			RunningApp.Invoke("BackgroundApp");
			Thread.Sleep(1000);
			RunningApp.Invoke("ForegroundApp");
			RunningApp.TapCoordinates(100, 100);


#if __ANDROID__
			//AppSetup.InitializeAndroidApp();
#endif
		}
#endif
	}
}

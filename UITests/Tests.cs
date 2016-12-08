using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void FirstTest()
        {
            app.WaitForElement(x => x.Class("UINavigationItemView").Marked("Scott Hanselman"));
            app.Screenshot("On home screen");

            app.Tap("slideout.png");
            app.WaitForNoElement("slideout.png");
            app.Screenshot("Side menu opens via tap");

            var tableView = app.Query(x => x.Class("UITableView"));
            var rect = tableView[0].Rect;

            var width = rect.Width;
            var cy = rect.CenterY;

            app.TapCoordinates(width + 1, cy);
            app.WaitForElement(x => x.Class("UINavigationItemView").Marked("Scott Hanselman"));
            app.Screenshot("Side menu closes via tap");

            app.SwipeLeftToRight();
            app.WaitForNoElement(x => x.Class("UINavigationItemView").Marked("Scott Hanselman"));
            app.Screenshot("Side menu opens via swipe");

            app.SwipeRightToLeft();
            app.WaitForElement(x => x.Class("UINavigationItemView").Marked("Scott Hanselman"));
            app.Screenshot("Side menu closes via swipe");
        }
    }
}

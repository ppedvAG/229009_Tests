using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BooksManager.Tests
{
    public class MainWindowTests : IDisposable
    {
        FlaUI.Core.Application app;

        public MainWindowTests()
        {
            var appPath = typeof(MainWindow).Assembly.Location.Replace(".dll", ".exe");

            app = FlaUI.Core.Application.Launch(appPath);
        }

        [Fact]
        public void Click_on_Load_button_should_show_at_least_one_row_in_grid()
        {
            using (var auto = new UIA3Automation())
            {
                var win = app.GetMainWindow(auto);

                var btn = win.FindFirstDescendant(x => x.ByText("Laden")).AsButton();
                btn.Click();

                var grid = win.FindFirstDescendant(x => x.ByControlType(FlaUI.Core.Definitions.ControlType.DataGrid)).AsDataGridView();

                grid.Rows.Count().Should().BeGreaterThan(1);
            }
        }

        public void Dispose()
        {
            app?.Close();
        }
    }
}

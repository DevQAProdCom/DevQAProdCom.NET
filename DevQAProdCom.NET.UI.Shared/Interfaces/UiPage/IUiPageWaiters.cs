using System.Text.RegularExpressions;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiPage
{
    internal interface IUiPageWaiters
    {
        public IUiPage WaitTitleContains(string substring, TimeSpan timespan);
        public IUiPage WaitTitleContains(string substring, uint seconds = 30);

        public IUiPage WaitTitleIs(string title, TimeSpan timespan);
        public IUiPage WaitTitleIs(string title, uint seconds = 30);

        public IUiPage WaitUrlContains(string fragment, TimeSpan timespan);
        public IUiPage WaitUrlContains(string fragment, uint seconds = 30);

        public IUiPage WaitUrlIs(string url, TimeSpan timespan);
        public IUiPage WaitUrlIs(string url, uint seconds = 30);

        public IUiPage WaitUrlMatches(string regex, TimeSpan timespan);
        public IUiPage WaitUrlMatches(string regex, uint seconds = 30);
        public IUiPage WaitUrlMatches(Regex regex, TimeSpan timespan);
        public IUiPage WaitUrlMatches(Regex regex, uint seconds = 30);
    }
}

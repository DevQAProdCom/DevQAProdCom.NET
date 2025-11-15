using System.Collections.ObjectModel;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiElements
{
    public interface IUiElementsList<TUiElement> : IEnumerable<TUiElement> where TUiElement : IUiElement
    {
        IUiPage UiPage { get; }
        ReadOnlyCollection<TUiElement> AsReadOnly(bool reFindItems = true);

        int BinarySearch(int index, int count, TUiElement item, IComparer<TUiElement>? comparer, bool reFindItems = true);
        int BinarySearch(TUiElement item, bool reFindItems = true);
        int BinarySearch(TUiElement item, IComparer<TUiElement>? comparer, bool reFindItems = true);
        bool Contains(TUiElement item, bool reFindItems = true);

        int Count(bool reFindItems = true);
        bool Exists(Predicate<TUiElement> match, bool reFindItems = true);
        TUiElement? Find(Predicate<TUiElement> match, bool reFindItems = true);
        List<TUiElement> FindAll(Predicate<TUiElement> match, bool reFindItems = true);
        int FindIndex(Predicate<TUiElement> match, bool reFindItems = true);
        int FindIndex(int startIndex, Predicate<TUiElement> match, bool reFindItems = true);
        int FindIndex(int startIndex, int count, Predicate<TUiElement> match, bool reFindItems = true);
        TUiElement? FindLast(Predicate<TUiElement> match, bool reFindItems = true);
        int FindLastIndex(Predicate<TUiElement> match, bool reFindItems = true);
        int FindLastIndex(int startIndex, Predicate<TUiElement> match, bool reFindItems = true);
        int FindLastIndex(int startIndex, int count, Predicate<TUiElement> match, bool reFindItems = true);
        void ForEach(Action<TUiElement> action, bool reFindItems = true);

        List<TUiElement> GetRange(int index, int count, bool reFindItems = true);
        List<TUiElement> GetUiElementItems(bool reFindItems = true);
        TUiElement ElementAt(int index, bool reFindItems = true);

        int IndexOf(TUiElement item, bool reFindItems = true);
        int IndexOf(TUiElement item, int index, bool reFindItems = true);
        int IndexOf(TUiElement item, int index, int count, bool reFindItems = true);
        int LastIndexOf(TUiElement item, bool reFindItems = true);
        int LastIndexOf(TUiElement item, int index, bool reFindItems = true);
        int LastIndexOf(TUiElement item, int index, int count, bool reFindItems = true);
        void Reverse(bool reFindItems = true);
        void Reverse(int index, int count, bool reFindItems = true);

        public IEnumerable<TResult> Select<TResult>(Func<TUiElement, TResult> selector, bool reFindItems = true);
        public IEnumerable<TResult> SelectMany<TResult>(Func<TUiElement, IEnumerable<TResult>> selector, bool reFindItems = true);

        void Sort(bool reFindItems = true);
        void Sort(IComparer<TUiElement>? comparer, bool reFindItems = true);
        void Sort(int index, int count, IComparer<TUiElement>? comparer, bool reFindItems = true);
        void Sort(Comparison<TUiElement> comparison, bool reFindItems = true);
        bool TrueForAll(Predicate<TUiElement> match, bool reFindItems = true);
        TUiElement this[int index] { get; }
    }
}

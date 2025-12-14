using System.Collections;
using System.Collections.ObjectModel;
using DevQAProdCom.NET.Global.Extensions;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;
using MySqlX.XDevAPI.Common;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements
{
    public abstract class BaseUiElementsList<TUiElement> : IUiElementsList<TUiElement> where TUiElement : IUiElement
    {
        public IUiPage UiPage { get; internal set; }
        public IUiElementInfo Info { get; internal set; }
        protected List<TUiElement>? UiElementItems { get; set; }
        public abstract List<TUiElement> GetUiElementItems(bool reFindItems = true);

        protected INativeElementsSearcher NativeElementsSearcher;

        protected IUiElementsInstantiator UiElementsInstantiator;

        protected IExecuteJavaScript JavaScriptExecutor;

        protected Dictionary<string, object> NativeObjects { get; set; } = new();

        protected ILogger _log;

        internal BaseUiElementsList(ILogger log, IUiPage uiPage, IUiElementInfo info, INativeElementsSearcher nativeElementsSearcher, IExecuteJavaScript javaScriptExecutor,
            IUiElementsInstantiator uiElementsInstantiator, params KeyValuePair<string, object>[] nativeObjects)

        {
            _log = log;
            UiPage = uiPage;
            Info = info;
            NativeElementsSearcher = nativeElementsSearcher;
            JavaScriptExecutor = javaScriptExecutor;
            UiElementsInstantiator = uiElementsInstantiator;
            NativeObjects.Upsert(nativeObjects);
        }

        internal BaseUiElementsList(ILogger log, IUiPage uiPage, List<TUiElement> uiElementItems, IUiElementInfo info, INativeElementsSearcher nativeElementsSearcher, IExecuteJavaScript javaScriptExecutor,
            IUiElementsInstantiator uiElementsInstantiator, params KeyValuePair<string, object>[] nativeObjects)
            : this(log, uiPage, info, nativeElementsSearcher, javaScriptExecutor, uiElementsInstantiator, nativeObjects)
        {
            UiElementItems = uiElementItems;
        }

        internal void Add(TUiElement item)
        {
            UiElementItems ??= new List<TUiElement>();
            UiElementItems.Add(item);
        }

        internal void AddRange(IEnumerable<TUiElement> collection)
        {
            UiElementItems ??= new List<TUiElement>();
            UiElementItems.AddRange(collection);
        }

        public ReadOnlyCollection<TUiElement> AsReadOnly(bool reFindItems = true) => GetUiElementItems(reFindItems).AsReadOnly();
        public int BinarySearch(int index, int count, TUiElement item, IComparer<TUiElement>? comparer, bool reFindItems = true)
            => GetUiElementItems(reFindItems).BinarySearch(index, count, item, comparer);
        public int BinarySearch(TUiElement item, bool reFindItems = true) => GetUiElementItems(reFindItems).BinarySearch(item);
        public int BinarySearch(TUiElement item, IComparer<TUiElement>? comparer, bool reFindItems = true) => GetUiElementItems(reFindItems).BinarySearch(item, comparer);

        internal void Clear()
        {
            UiElementItems?.Clear();
        }
        public bool Contains(TUiElement item, bool reFindItems = true) => GetUiElementItems(reFindItems).Contains(item);

        public int Count(bool reFindItems = true) => GetUiElementItems(reFindItems).Count;

        internal List<TOutput> ConvertAll<TOutput>(Converter<TUiElement, TOutput> converter, bool reFindItems = true) =>
            GetUiElementItems(reFindItems).ConvertAll(converter);

        internal void CopyTo(TUiElement[] array, bool reFindItems = true) => GetUiElementItems(reFindItems).CopyTo(array);

        internal void CopyTo(int index, TUiElement[] array, int arrayIndex, int count, bool reFindItems = true) => GetUiElementItems(reFindItems).CopyTo(index, array, arrayIndex, count);
        internal void CopyTo(TUiElement[] array, int arrayIndex, bool reFindItems = true) => GetUiElementItems(reFindItems).CopyTo(array, arrayIndex);
        internal int EnsureCapacity(int capacity, bool reFindItems = true) => GetUiElementItems(reFindItems).EnsureCapacity(capacity);

        public bool Exists(Predicate<TUiElement> match, bool reFindItems = true) => GetUiElementItems(reFindItems).Exists(match);
        public TUiElement? Find(Predicate<TUiElement> match, bool reFindItems = true) => GetUiElementItems(reFindItems).Find(match);
        public List<TUiElement> FindAll(Predicate<TUiElement> match, bool reFindItems = true) => GetUiElementItems(reFindItems).FindAll(match);
        public int FindIndex(Predicate<TUiElement> match, bool reFindItems = true) => GetUiElementItems(reFindItems).FindIndex(match);
        public int FindIndex(int startIndex, Predicate<TUiElement> match, bool reFindItems = true) => GetUiElementItems(reFindItems).FindIndex(startIndex, match);
        public int FindIndex(int startIndex, int count, Predicate<TUiElement> match, bool reFindItems = true) => GetUiElementItems(reFindItems).FindIndex(startIndex, count, match);
        public TUiElement? FindLast(Predicate<TUiElement> match, bool reFindItems = true) => GetUiElementItems(reFindItems).FindLast(match);
        public int FindLastIndex(Predicate<TUiElement> match, bool reFindItems = true) => GetUiElementItems(reFindItems).FindLastIndex(match);
        public int FindLastIndex(int startIndex, Predicate<TUiElement> match, bool reFindItems = true) => GetUiElementItems(reFindItems).FindLastIndex(startIndex, match);
        public int FindLastIndex(int startIndex, int count, Predicate<TUiElement> match, bool reFindItems = true) => GetUiElementItems(reFindItems).FindLastIndex(startIndex, count, match);
        public void ForEach(Action<TUiElement> action, bool reFindItems = true) => GetUiElementItems(reFindItems).ForEach(action);

        public IEnumerator<TUiElement> GetEnumerator() => GetUiElementItems(reFindItems: true).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetUiElementItems(reFindItems: true).GetEnumerator();
        public List<TUiElement> GetRange(int index, int count, bool reFindItems = true) => GetUiElementItems(reFindItems).GetRange(index, count);
        public TUiElement ElementAt(int index, bool reFindItems = true)
        {
            TUiElement item;

            try
            {
                var items = GetUiElementItems(reFindItems);

                if (items.Count != 0)
                    item = items[index];
                else
                    throw new ArgumentOutOfRangeException($"Unable to get element '{Info.InstantiationStage.FullName}[{index}]' from the list, because the list contains 0 elements.");
            }
            catch (Exception)
            {
                Console.Write($"Unable to get element '{Info.InstantiationStage.FullName}[{index}]' from the list.");
                throw;
            }

            return item;
        }

        public int IndexOf(TUiElement item, bool reFindItems = true) => GetUiElementItems(reFindItems).IndexOf(item);
        public int IndexOf(TUiElement item, int index, bool reFindItems = true) => GetUiElementItems(reFindItems).IndexOf(item, index);
        public int IndexOf(TUiElement item, int index, int count, bool reFindItems = true) => GetUiElementItems(reFindItems).IndexOf(item, index, count);

        internal void Insert(int index, TUiElement item)
        {
            UiElementItems ??= new List<TUiElement>();
            UiElementItems.Insert(index, item);
        }

        internal void InsertRange(int index, IEnumerable<TUiElement> collection)
        {
            UiElementItems ??= new List<TUiElement>();
            UiElementItems.InsertRange(index, collection);
        }

        public int LastIndexOf(TUiElement item, bool reFindItems = true) => GetUiElementItems(reFindItems).LastIndexOf(item);
        public int LastIndexOf(TUiElement item, int index, bool reFindItems = true) => GetUiElementItems(reFindItems).LastIndexOf(item, index);
        public int LastIndexOf(TUiElement item, int index, int count, bool reFindItems = true) => GetUiElementItems(reFindItems).LastIndexOf(item, index, count);

        internal bool Remove(TUiElement item)
        {
            if (UiElementItems != null)
                return UiElementItems.Remove(item);

            return false;
        }

        internal int RemoveAll(Predicate<TUiElement> match)
        {
            if (UiElementItems.ToArray() != null)
                return UiElementItems.RemoveAll(match);

            return 0;
        }

        internal void RemoveAt(int index)
        {
            if (UiElementItems != null)
                UiElementItems.RemoveAt(index);
        }

        internal void RemoveRange(int index, int count)
        {
            if (UiElementItems != null)
                UiElementItems.RemoveRange(index, count);
        }

        public abstract IEnumerable<TResult> Select<TResult>(Func<TUiElement, TResult> selector, bool reFindItems = true);
        public abstract IEnumerable<TResult> SelectMany<TResult>(Func<TUiElement, IEnumerable<TResult>> selector, bool reFindItems = true);

        public void Reverse(bool reFindItems = true) => GetUiElementItems(reFindItems).Reverse();
        public void Reverse(int index, int count, bool reFindItems = true) => GetUiElementItems(reFindItems).Reverse(index, count);
        public void Sort(bool reFindItems = true) => GetUiElementItems(reFindItems).Sort();
        public void Sort(IComparer<TUiElement>? comparer, bool reFindItems = true) => GetUiElementItems(reFindItems).Sort(comparer);
        public void Sort(int index, int count, IComparer<TUiElement>? comparer, bool reFindItems = true) => GetUiElementItems(reFindItems).Sort(index, count, comparer);
        public void Sort(Comparison<TUiElement> comparison, bool reFindItems = true) => GetUiElementItems(reFindItems).Sort(comparison);

        internal TUiElement[] ToArray()
        {
            if (UiElementItems == null || UiElementItems.Count == 0)
                return Array.Empty<TUiElement>();

            return UiElementItems.ToArray();
        }

        internal void TrimExcess() => UiElementItems?.TrimExcess();

        public bool TrueForAll(Predicate<TUiElement> match, bool reFindItems = true) => GetUiElementItems(reFindItems).TrueForAll(match);

        public TUiElement this[int index]
        {
            get => ElementAt(index, reFindItems: true);
            protected internal set
            {
                UiElementItems ??= new();
                UiElementItems[index] = value;
            }
        }
    }
}

using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Shared.Extensions;
using DevQAProdCom.NET.UI.Shared.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage
{
    public class UiPageFactory : BaseUiPageFactory
    {
        private readonly IUiPageBehaviorFactory _uiPageBehaviorFactory;
        private readonly IUiElementBehaviorFactory _uiElementBehaviorFactory;
        private IUiPage _uiPage;
        private readonly INativeElementsSearcher _nativeElementsSearcher;
        private readonly IUiInteractorTab _uiInteractorTab;
        private readonly IExecuteJavaScript _javaScriptExecutor;
        private readonly IUiElementsInterfaceImplementationRegister _uiElementsInterfaceImplementationRegister;

        public UiPageFactory(ILogger log,
            IUiInteractorTab uiInteractorTab,
            IUiPageBehaviorFactory uiPageBehaviorFactory,

            IUiElementBehaviorFactory uiElementBehaviorFactory,
            INativeElementsSearcher nativeElementsSearcher,
            IExecuteJavaScript javaScriptExecutor,

            IUiElementsInterfaceImplementationRegister uiElementsInterfaceImplementationRegister,

            string? applicationName = null, string? pageName = null, string? baseUri = null, string? relativeUri = null,
            Dictionary<string, object>? nativeObjects = null) : base(log: log, nativeObjects: nativeObjects)
        {
            _uiPageBehaviorFactory = uiPageBehaviorFactory;
            _uiElementBehaviorFactory = uiElementBehaviorFactory;
            _uiInteractorTab = uiInteractorTab;
            _javaScriptExecutor = javaScriptExecutor;
            _uiElementsInterfaceImplementationRegister = uiElementsInterfaceImplementationRegister;
            _nativeElementsSearcher = nativeElementsSearcher;
        }

        public override IUiPage CreatePage<TUiPage>()
        {
            if (!typeof(TUiPage).IsSubclassOf(typeof(UiPage)))
                throw new NotSupportedException();

            object? pageInstance = null;

            try
            {
                pageInstance = Activator.CreateInstance(typeof(TUiPage));
            }
            catch
            {
                throw;
            }

            if (pageInstance != null)
            {
                var page = pageInstance as UiPage;
                _uiPage = page;

                if (page != null)
                {
                    page.UiTab = _uiInteractorTab;
                    page.PageName = nameof(TUiPage);
                    page.UiElementsInstantiator = this;
                    page.NativeElementsSearcher = _nativeElementsSearcher;
                    page.JavaScriptExecutor = _javaScriptExecutor;
                    page.PageBehaviorFactory = _uiPageBehaviorFactory;
                    page.NativeObjects = NativeObjects;

                    InstantiateUiElements(pageInstance);

                    return (IUiPage)pageInstance;
                }

                throw new InvalidCastException();
            }
            throw new Exception();
        }

        public override object CreateUiElement(Type @type, IUiElementInfo uiElementInfo, params KeyValuePair<string, object>[] nativeObjects)
        {
            Type typeOfUiElementImplementation = _uiElementsInterfaceImplementationRegister.GetTypeOfIUiElementImplementation();

            if (@type.IsSimpleUiElement())
            {
                var iUiElement = Activator.CreateInstance(typeOfUiElementImplementation,
                    Log, _uiPage, uiElementInfo, _nativeElementsSearcher, _javaScriptExecutor, _uiElementBehaviorFactory, this, nativeObjects);

                return iUiElement;
            }
            else if (@type.IsComplexUiElementAsClass())
            {
                Type implementationType = _uiElementsInterfaceImplementationRegister.GetUiElementImplementationType(@type);
                var uiElementAsClassOfTypeT = Activator.CreateInstance(implementationType);
                var uiElement = uiElementAsClassOfTypeT as UiElement;

                var iUiElement = Activator.CreateInstance(typeOfUiElementImplementation, Log, _uiPage, uiElementInfo, _nativeElementsSearcher, _javaScriptExecutor, _uiElementBehaviorFactory, this, nativeObjects);

                uiElement.UiPage = _uiPage;
                uiElement.InternalUiElement = iUiElement as IUiElement;
                uiElement.NativeElementsSearcher = _nativeElementsSearcher;
                uiElement.UiElementsInstantiator = this;
                uiElement.JavaScriptExecutor = _javaScriptExecutor;

                return uiElementAsClassOfTypeT;
            }

            else
                throw new ArgumentException();
        }

        public override object CreateListOfUiElements(Type @type, IUiElementInfo uiElementInfo)
        {
            if (@type.IsUiElementsList())
            {
                var concreteType = _uiElementsInterfaceImplementationRegister.GetTypeOfIUiElementsListImplementation().MakeGenericType(@type.GenericTypeArguments);
                var subAsset = Activator.CreateInstance(concreteType, Log, _uiPage, uiElementInfo, _nativeElementsSearcher, _javaScriptExecutor, this, NativeObjects?.ToArray());

                return subAsset;
            }

            throw new ArgumentException();
        }
    }
}

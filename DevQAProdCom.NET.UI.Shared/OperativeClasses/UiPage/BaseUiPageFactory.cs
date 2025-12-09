using System.Reflection;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Extensions;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Search;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage
{
    public abstract class BaseUiPageFactory : IUiPageFactory
    {
        protected readonly ILogger Log;
        protected readonly Dictionary<string, object> NativeObjects = new();

        public BaseUiPageFactory(ILogger log, Dictionary<string, object>? nativeObjects = null)
        {
            Log = log;

            if (nativeObjects != null)
                NativeObjects = nativeObjects;
        }

        public abstract IUiPage CreatePage<TUiPage>() where TUiPage : IUiPage;
        public abstract object CreateUiElement(Type @type, IUiElementInfo uiElementInfo, params KeyValuePair<string, object>[] nativeObjects);
        public abstract object CreateUiElementsList(Type @type, IUiElementInfo uiElementInfo);

        #region IUiElementsInstantiator

        public void InstantiateUiElements(object asset)
        {
            if (asset == null)
                throw new ArgumentNullException(nameof(asset), "Page Object for initialization cannot be null.");

            InstantiateFields(asset);
            InstantiateProperties(asset);

            //TODO Add check that if no fields with attributes inside class throw exception
        }

        private void InstantiateFields(object asset)
        {
            var assetType = asset.GetType();

            var fields = assetType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);

            if (fields.Length > 0)
                foreach (var field in fields)
                {
                    var findOptions = GetFindOptions(assetType, field);

                    if (findOptions.Any())
                    {
                        var parentContainer = asset as IUiElement;

                        IUiElementInstantiationStageInfo instantiationStageInfo = new UiElementInstantiationStageInfo(field.Name, findOptions, parentContainer: parentContainer, isList: field.FieldType.IsGenericType);
                        IUiElementInfo uiElementInfo = new UiElementInfo(instantiationStageInfo);

                        if ((field.FieldType.IsInterface || (field.FieldType.IsClass && !field.FieldType.IsAbstract)) && !field.FieldType.IsGenericType)
                            InstantiateFieldOfUiElementType(asset, field, uiElementInfo);

                        else if (field.FieldType.IsGenericType)
                            InstantiateFieldOfUiElementsListType(asset, field, uiElementInfo);

                        else
                            throw new Exception($"Field '{field.Name}' has '{nameof(FindAttribute)}', but field with type '{field.FieldType.FullName}' is not supported.");
                    }
                    else
                    {
                        //TODO log field name is private/public and doesn't have find attribute to avoid "System.NullReferenceException : Object reference not set to an instance of an object." message without logs
                    }
                }
        }

        protected void InstantiateFieldOfUiElementType(object asset, FieldInfo field, IUiElementInfo uiElementInfo)
        {
            if (field.FieldType.IsUiElement())
            {
                var uiElement = CreateUiElement(field.FieldType, uiElementInfo, nativeObjects: NativeObjects.ToArray());

                if (field.FieldType.IsComplexUiElementAsClass())
                    InstantiateUiElements(uiElement);

                field.SetValue(asset, uiElement);
            }
            else
                throw new ArgumentException(
                    $"Unable to initialize field '{field.Name}' of field type '{field.FieldType.FullName}, but field type is not subclass of, but should be inherited from type: '{typeof(IUiElement).Name}({typeof(IUiElement).FullName})'.");
        }

        private void InstantiateFieldOfUiElementsListType(object asset, FieldInfo field, IUiElementInfo uiElementInfo)
        {
            if (field.FieldType.IsUiElementsList())
            {
                var subAsset = CreateUiElementsList(field.FieldType, uiElementInfo);
                field.SetValue(asset, subAsset);
            }
            else
                throw new ArgumentException($"Unable to initialize field '{field.Name}' of field type '{field.FieldType.FullName}', which should either implement or be assignable to '{typeof(IUiElementsList<IUiElement>).Name}({typeof(IUiElementsList<IUiElement>).FullName})'.");
        }

        private void InstantiateProperties(object asset)
        {
            var assetType = asset.GetType();

            var properties = assetType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
            //TODO Add check that property has setter

            if (properties.Length > 0)
                foreach (var property in properties)
                {
                    var findOptions = GetFindOptions(assetType, property);

                    if (findOptions.Any())
                    {
                        var parentContainer = asset as IUiElement;

                        IUiElementInstantiationStageInfo instantiationStageInfo = new UiElementInstantiationStageInfo(property.Name, findOptions, parentContainer: parentContainer, isList: property.PropertyType.IsGenericType);
                        IUiElementInfo uiElementInfo = new UiElementInfo(instantiationStageInfo);

                        if ((property.PropertyType.IsInterface || (property.PropertyType.IsClass && !property.PropertyType.IsAbstract)) && !property.PropertyType.IsGenericType)
                            InstantiatePropertyOfUiElementType(asset, property, uiElementInfo);

                        else if (property.PropertyType.IsGenericType)
                            InstantiatePropertyOfUiElementsListType(asset, property, uiElementInfo);

                        else
                            throw new Exception($"Field '{property.Name}' has '{nameof(FindAttribute)}', but field with type '{property.PropertyType.FullName}' is not supported.");
                    }
                    else
                    {
                        //TODO log field name is private/public and doesn't have find attribute to avoid "System.NullReferenceException : Object reference not set to an instance of an object." message without logs
                    }
                }
        }

        protected void InstantiatePropertyOfUiElementType(object asset, PropertyInfo property, IUiElementInfo uiElementInfo)
        {
            if (property.PropertyType.IsUiElement())
            {
                var uiElement = CreateUiElement(property.PropertyType, uiElementInfo, nativeObjects: NativeObjects.ToArray());

                if (property.PropertyType.IsComplexUiElementAsClass())
                    InstantiateUiElements(uiElement);

                property.SetValue(asset, uiElement);
            }
            else
                throw new ArgumentException(
                    $"Unable to initialize field '{property.Name}' of field type '{property.PropertyType.FullName}, but field type is not subclass of, but should be inherited from type: '{typeof(IUiElement).Name}({typeof(IUiElement).FullName})'.");
        }

        private void InstantiatePropertyOfUiElementsListType(object asset, PropertyInfo property, IUiElementInfo uiElementInfo)
        {
            if (property.PropertyType.IsUiElementsList())
            {
                var subAsset = CreateUiElementsList(property.PropertyType, uiElementInfo);
                property.SetValue(asset, subAsset);
            }
            else
                throw new ArgumentException($"Unable to initialize field '{property.Name}' of field type '{property.PropertyType.FullName}', which should either implement or be assignable to '{typeof(IUiElementsList<IUiElement>).Name}({typeof(IUiElementsList<IUiElement>).FullName})'.");
        }

        private List<IUiElementsFindInfo> GetFindOptions(Type assetType, MemberInfo member)
        {
            List<IUiElementsFindInfo> findInfo = new();
            List<FindAttribute> findAttributes = member.GetCustomAttributes<FindAttribute>().ToList();
            List<FrameAttribute> frameAttributes = member.GetCustomAttributes<FrameAttribute>().ToList();
            List<ShadowRootHostAttribute> shadowRootHostAttributes = member.GetCustomAttributes<ShadowRootHostAttribute>().ToList();

            foreach (var findAttribute in findAttributes)
                findInfo.Add(new UiElementsFindInfo(elementsFindMethod: findAttribute.ElementsFindMethod, elementsFindCriteria: findAttribute.ElementsFindCriteria,
                    framesFindMethod: findAttribute.FramesFindMethod, framesFindCriteria: findAttribute.FramesFindCriteria,
                    shadowRootHostsFindMethod: findAttribute.ShadowRootHostsFindMethod, shadowRootHostsFindCriteria: findAttribute.ShadowRootHostsFindCriteria,
                    findOrderType: findAttribute.FindOrderType));

            foreach (var frameAttribute in frameAttributes)
                findInfo.Add(new UiElementsFindInfo(framesFindMethod: frameAttribute.FramesFindMethod, framesFindCriteria: frameAttribute.FramesFindCriteria,
                   shadowRootHostsFindMethod: frameAttribute.ShadowRootHostsFindMethod, shadowRootHostsFindCriteria: frameAttribute.ShadowRootHostsFindCriteria,
                   findOrderType: frameAttribute.FindOrderType));


            foreach (var shadowRootHostAttribute in shadowRootHostAttributes)
                findInfo.Add(new UiElementsFindInfo(shadowRootHostsFindMethod: shadowRootHostAttribute.ShadowRootHostsFindMethod, shadowRootHostsFindCriteria: shadowRootHostAttribute.ShadowRootHostsFindCriteria,
                    framesFindMethod: shadowRootHostAttribute.FramesFindMethod, framesFindCriteria: shadowRootHostAttribute.FramesFindCriteria,
                    findOrderType: shadowRootHostAttribute.FindOrderType));

            return findInfo;
        }

        public List<TUiElement> InitializeUiElementsList<TUiElement, TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement>
            (IUiElementInfo uiElementInfo, IList<IFindResult<TNativeElement, TNativeFrameElement, TNativeShadowRootHostElement>> nativeElements) //TODO Move back or leave without ,Dictionary<string, object>? NativeObjects
            where TUiElement : IUiElement
            where TNativeElement : class
            where TNativeFrameElement : class
            where TNativeShadowRootHostElement : class
        {
            var uiElementsList = new List<TUiElement>();

            for (var i = 0; i < nativeElements.Count(); i++)
            {
                IFindParametersWithSearchResult elementFindParameters = nativeElements.ElementAt(i).FindParametersWithSearchResult;

                IUiElementInstantiationStageInfo instantiationStageInfo = new UiElementInstantiationStageInfo(uiElementInfo.InstantiationStage.Name, new List<IUiElementsFindInfo>() { elementFindParameters.FindInfo }, parentContainer: uiElementInfo.InstantiationStage.ParentContainer, isList: false, uiIndex: i + 1);
                IUiElementFindStageInfo findStageInfo = new UiElementFindStageInfo(elementFindParameters);

                var info = new UiElementInfo(instantiationStageInfo, findStageInfo);
                IUiElement? iUiElement = default;

                try
                {
                    var type = typeof(TUiElement);
                    object? nativeElement = null;

                    if (elementFindParameters.FindInfo.UiElementType == UiElementType.Standard)
                        nativeElement = elementFindParameters.NativeElement;
                    else if (elementFindParameters.FindInfo.UiElementType == UiElementType.Frame)
                        nativeElement = elementFindParameters.NativeFrameElement;
                    else if (elementFindParameters.FindInfo.UiElementType == UiElementType.ShadowRootHost)
                        nativeElement = elementFindParameters.NativeShadowRootHostElement;
                    else
                        throw new NotSupportedException($"Search of element of 'UiElementType.{elementFindParameters.FindInfo.UiElementType}' is not supported.");

                    if (nativeElement == null)
                        throw new Exception($"Found element should not be null");  //TODO


                    var nativeElementObject = new KeyValuePair<string, object>(SharedUiConstants.NativeElement, nativeElement);

                    List<KeyValuePair<string, object>> nativeObjectsAggregated = new() { nativeElementObject };

                    if (NativeObjects?.Count() > 0)
                        nativeObjectsAggregated.AddRange(NativeObjects);

                    iUiElement = InstantiateUiElement<TUiElement>(info, nativeObjectsAggregated.ToArray());
                }
                catch
                {
                    Log.Info($"Unable to create instance of type '{typeof(TUiElement).FullName}' for UI Element '{info.InstantiationStage.FullName}'.");
                    throw;
                }

                uiElementsList.Add((TUiElement)iUiElement);
            }

            return uiElementsList;
        }

        public TUiElement InstantiateUiElement<TUiElement>(IUiElementInfo uiElementInfo, params KeyValuePair<string, object>[] nativeObjects) where TUiElement : IUiElement
        {
            var type = typeof(TUiElement);

            if (type.IsUiElement())
            {
                var uiElement = CreateUiElement(type, uiElementInfo, nativeObjects);

                if (type.IsComplexUiElementAsClass())
                    InstantiateUiElements(uiElement);

                var iUiElement = (IUiElement)uiElement;
                var tUiElement = (TUiElement)iUiElement;

                return tUiElement;
            }
            else
                throw new NotSupportedException();
        }

        public IUiElementsList<TUiElement> InstantiateUiElementsList<TUiElement>(IUiElementInfo uiElementInfo) where TUiElement : IUiElement
        {
            var type = typeof(IUiElementsList<TUiElement>);

            try
            {
                var list = CreateUiElementsList(type, uiElementInfo);
                return (IUiElementsList<TUiElement>)list;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Unable to initialize field '{uiElementInfo.InstantiationStage.Name}' of field type '{type.FullName}, which should either implement or be assignable to '{typeof(IUiElementsList<IUiElement>).Name}({typeof(IUiElementsList<IUiElement>).FullName})'." +
                    $" Exception: {ex}");
            }
        }
        public TUiElement InstantiateUiElement<TUiElement>(string method, string criteria, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement
        {
            name ??= GetUiElementName<TUiElement>();
            var findOptions = new List<IUiElementsFindInfo>() { new UiElementsFindInfo(method, criteria) };
            IUiElementInstantiationStageInfo instantiationStageInfo = new UiElementInstantiationStageInfo(name, findOptions, parentUiElement);
            IUiElementInfo uiElementInfo = new UiElementInfo(instantiationStageInfo);
            var uiElement = InstantiateUiElement<TUiElement>(uiElementInfo, nativeObjects: NativeObjects?.ToArray());

            return uiElement;
        }

        public TUiElement InstantiateUiElement<TUiElement>(Use method, string criteria, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement
        {
            return InstantiateUiElement<TUiElement>(method.ToString(), criteria, parentUiElement: parentUiElement, name: name);
        }

        public TUiElement InstantiateUiElement<TUiElement>(List<IUiElementsFindInfo> findOptions, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement
        {
            name ??= GetUiElementName<TUiElement>();
            IUiElementInstantiationStageInfo instantiationStageInfo = new UiElementInstantiationStageInfo(name, findOptions, parentUiElement);
            IUiElementInfo uiElementInfo = new UiElementInfo(instantiationStageInfo);
            var uiElement = InstantiateUiElement<TUiElement>(uiElementInfo);

            return uiElement;
        }

        public IUiElementsList<TUiElement> InstantiateUiElementsList<TUiElement>(string method, string criteria, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement
        {
            name ??= GetUiElementsListName<TUiElement>();
            var findOptions = new List<IUiElementsFindInfo>() { new UiElementsFindInfo(method, criteria) };
            IUiElementInstantiationStageInfo instantiationStageInfo = new UiElementInstantiationStageInfo(name, findOptions, parentUiElement, isList: true);
            IUiElementInfo uiElementInfo = new UiElementInfo(instantiationStageInfo);
            var uiElementsList = InstantiateUiElementsList<TUiElement>(uiElementInfo);

            return uiElementsList;
        }

        public IUiElementsList<TUiElement> InstantiateUiElementsList<TUiElement>(Use method, string criteria, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement
        {
            return InstantiateUiElementsList<TUiElement>(method.ToString(), criteria, parentUiElement: parentUiElement, name: name);
        }

        public IUiElementsList<TUiElement> InstantiateUiElementsList<TUiElement>(List<IUiElementsFindInfo> findOptions, IUiElement? parentUiElement = null, string? name = null) where TUiElement : IUiElement
        {
            name ??= GetUiElementsListName<TUiElement>();
            IUiElementInstantiationStageInfo instantiationStageInfo = new UiElementInstantiationStageInfo(name, findOptions, parentUiElement, isList: true);
            IUiElementInfo uiElementInfo = new UiElementInfo(instantiationStageInfo);
            var uiElementsList = InstantiateUiElementsList<TUiElement>(uiElementInfo);

            return uiElementsList;
        }

        private string GetUiElementName<TUiElement>()
        {
            return typeof(TUiElement).Name;
        }

        private string GetUiElementsListName<TUiElement>()
        {
            return $"List of {typeof(TUiElement).Name}";
        }

        #endregion IUiElementsInstantiator
    }
}

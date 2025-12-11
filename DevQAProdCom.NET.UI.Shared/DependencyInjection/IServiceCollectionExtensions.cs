using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.Global.OperativeClasses;
using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Keyboard;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Mouse;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Others;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Scroll;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Behaviors.Text;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorTab;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Keyboard;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiPage.Behaviors.Mouse;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Keyboard;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Mouse;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Others;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Scroll;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiElements.Behaviors.Text;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor.Behaviors;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractorsManager;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractorTab.Behaviors;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage.Behaviors;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage.Behaviors.Keyboard;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiPage.Behaviors.Mouse;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DevQAProdCom.NET.UI.Shared.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddUiInteractionBehavior<TInterface, TImplementation>(this IServiceCollection serviceCollection)
            where TImplementation : TInterface
            where TInterface : IBehavior
        {
            serviceCollection.TryAddTransient<Func<IBehaviorParameters, TInterface>>((provider) =>
            {
                return new Func<IBehaviorParameters, TInterface>(behaviorParameters => ActivatorUtilities.CreateInstance<TImplementation>(provider, behaviorParameters));
            });

            return serviceCollection;
        }

        public static IServiceCollection AddUiInteractorsManagersProvider(this IServiceCollection serviceCollection, Func<string>? getCurrentTestIdentifierFunc = null, Func<string>? getCurrentFeatureIdentifierFunc = null)
        {
            serviceCollection.AddSingleton<IUiInteractorsManagersProvider, UiInteractorsManagersProvider>(provider =>
               {
                   ILogger log = provider.GetRequiredService<ILogger>();
                   Func<IUiInteractorsManager> getUiInteractorManagerFunc = () => provider.GetRequiredService<IUiInteractorsManager>();
                   return new UiInteractorsManagersProvider(getUiInteractorManagerFunc: getUiInteractorManagerFunc, log: log,
                       getCurrentTestIdentifierFunc: getCurrentTestIdentifierFunc, getCurrentFeatureIdentifierFunc: getCurrentFeatureIdentifierFunc);
               });

            return serviceCollection;
        }

        public static IServiceCollection AddBaseSetOfUiInteractionBehaviors(this IServiceCollection serviceCollection)
        {
            serviceCollection
                //Mouse Behaviors
                .AddUiInteractionBehavior<IUiElementBehaviorClickJs, UiElementBehaviorClickJs>()
                .AddUiInteractionBehavior<IUiElementBehaviorContextClickJs, UiElementBehaviorContextClickJs>()
                .AddUiInteractionBehavior<IUiElementBehaviorMouseDownJs, UiElementBehaviorMouseDownJs>()
                .AddUiInteractionBehavior<IUiElementBehaviorMouseUpJs, UiElementBehaviorMouseUpJs>()

                .AddUiInteractionBehavior<IUiPageBehaviorMouseMoveJs, UiPageBehaviorMouseMoveJs>()
                .AddUiInteractionBehavior<IUiPageBehaviorMouseDownJs, UiPageBehaviorMouseDownJs>()
                .AddUiInteractionBehavior<IUiPageBehaviorMouseUpJs, UiPageBehaviorMouseUpJs>()

                .AddUiInteractionBehavior<IUiPageBehaviorMouseScrollHorizontally, UiPageBehaviorMouseScrollHorizontally>()
                .AddUiInteractionBehavior<IUiPageBehaviorMouseScrollVertically, UiPageBehaviorMouseScrollVertically>()

                //Keyboard Behaviors
                .AddUiInteractionBehavior<IUiElementBehaviorKeysDown, UiElementBehaviorKeysDown>()
                .AddUiInteractionBehavior<IUiElementBehaviorKeysUp, UiElementBehaviorKeysUp>()
                .AddUiInteractionBehavior<IUiElementBehaviorPressKey, UiElementBehaviorPressKey>()
                .AddUiInteractionBehavior<IUiElementBehaviorPressKeysSequentially, UiElementBehaviorPressKeysSequentially>()
                .AddUiInteractionBehavior<IUiElementBehaviorPressKeysSimultaneously, UiElementBehaviorPressKeysSimultaneously>()

                .AddUiInteractionBehavior<IUiPageBehaviorPressKey, UiPageBehaviorPressKey>()
                .AddUiInteractionBehavior<IUiPageBehaviorPressKeysSimultaneously, UiPageBehaviorPressKeysSimultaneously>()

                //Others
                .AddUiInteractionBehavior<IUiElementBehaviorFocusJs, UiElementBehaviorFocusJs>()
                .AddUiInteractionBehavior<IUiElementBehaviorGetCheckedAttribute, UiElementBehaviorGetCheckedAttribute>()
                .AddUiInteractionBehavior<IUiElementBehaviorGetHrefAttribute, UiElementBehaviorGetHrefAttribute>()
                .AddUiInteractionBehavior<IUiElementBehaviorGetSrcAttribute, UiElementBehaviorGetSrcAttribute>()
                .AddUiInteractionBehavior<IUiElementBehaviorGetValueAttribute, UiElementBehaviorGetValueAttribute>()

                //Scroll Behaviors
                .AddUiInteractionBehavior<IUiElementBehaviorScrollIntoViewInstantlyJs, UiElementBehaviorScrollIntoViewInstantlyJs>()
                .AddUiInteractionBehavior<IUiElementBehaviorScrollIntoViewSmoothlyJs, UiElementBehaviorScrollIntoViewSmoothlyJs>()

                //Text Behaviors
                .AddUiInteractionBehavior<IUiElementBehaviorAppendText, UiElementBehaviorAppendText>()
                .AddUiInteractionBehavior<IUiElementBehaviorCopyPasteTextJs, UiElementBehaviorCopyPasteTextJs>()
                .AddUiInteractionBehavior<IUiElementBehaviorSetTextCopyPasteJs, UiElementBehaviorSetTextCopyPasteJs>()
                .AddUiInteractionBehavior<IUiElementBehaviorSetInnerHtmlJs, UiElementBehaviorSetInnerHtmlJs>()
                .AddUiInteractionBehavior<IUiElementBehaviorSetTextContentJs, UiElementBehaviorSetTextContentJs>()
                .AddUiInteractionBehavior<IUiElementBehaviorClearTextByDeleteKey, UiElementBehaviorClearTextByDeleteKey>()
                .AddUiInteractionBehavior<IUiElementBehaviorClearTextByBackspaceKey, UiElementBehaviorClearTextByBackspaceKey>()
                .AddUiInteractionBehavior<IUiElementBehaviorGetTextFromValueAttribute, UiElementBehaviorGetTextFromValueAttribute>()
                .AddUiInteractionBehavior<IUiElementBehaviorIsInputTextEmpty, UiElementBehaviorIsInputTextEmpty>();

            return serviceCollection;
        }

        public static IServiceCollection AddBaseUiInteractionServices(this IServiceCollection serviceCollection, Func<string>? getCurrentTestIdentifierFunc = null, Func<string>? getCurrentFeatureIdentifierFunc = null)
        {
            serviceCollection.AddUiInteractorsManagersProvider(getCurrentTestIdentifierFunc: getCurrentTestIdentifierFunc, getCurrentFeatureIdentifierFunc: getCurrentFeatureIdentifierFunc)
                .AddSingleton<IUiPageFactoryProvider, UiPageFactoryProvider>()
                .AddSingleton<IBehaviorProvider>(_ => new BehaviorProvider(serviceCollection))
                .AddSingleton<IUiInteractorBehaviorFactory, UiInteractorBehaviorFactory>()
                .AddSingleton<IUiInteractorTabBehaviorFactory, UiInteractorTabBehaviorFactory>()
                .AddSingleton<IUiPageBehaviorFactory, UiPageBehaviorFactory>()
                .AddSingleton<IUiElementBehaviorFactory, UiElementBehaviorFactory>()
                .AddBaseSetOfUiInteractionBehaviors();

            return serviceCollection;
        }
    }
}

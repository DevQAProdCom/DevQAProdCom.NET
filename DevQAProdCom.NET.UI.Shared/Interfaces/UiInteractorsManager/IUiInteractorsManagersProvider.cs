using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager
{
    public interface IUiInteractorsManagersProvider : IHaveIdentifiers
    {
        //No DisposeUiInteractorsManager/DisposeUiInteractor by Thread methods are implemented as safeguard, because, if Feature Scope is used, it is possible that, if invoked in OneTimeTearDown,
        //OneTimeTearDown may be running in a different thread than OneTimeSeup, where it could be created for the whole Feature.

        #region UiInteractorsManagers

        public IUiInteractorsManager GetUiInteractorsManager(string uiInteractorsManagerName = SharedUiConstants.DefaultUiInteractorsManagerInstance, int? threadId = null);
        public IUiInteractorsManager GetUiInteractorsManagerOfCurrentThread();

        /// <summary>
        /// Can be used in 'TearDown' for disposing UiInteractorManager with 'UiInteractorsManagerScope.Test' when ThreadId is the same with what was in 'SetUp' hook during creation.
        /// </summary>
        /// <param name="threadId"></param>
        /// <param name="uiInteractorsManagerName"></param>
        public void DisposeUiInteractorsManager(string uiInteractorsManagerName = SharedUiConstants.DefaultUiInteractorsManagerInstance, int? threadId = null);

        /// <summary>
        /// Can be used in 'OmeTimeTearDown' for disposing UiInteractorManager with 'UiInteractorsManagerScope.Feature' when ThreadId may differ from what was in 'OneTimeSetUp' hook during creation.
        /// </summary>
        /// <param name="uiInteractorsManagerName"></param>
        public void DisposeUiInteractorsManagers(string uiInteractorsManagerName = SharedUiConstants.DefaultUiInteractorsManagerInstance);
        public void DisposeUiInteractorsManagerOfCurrentThread();
        public void DisposeAllUiInteractorsManagers();

        #endregion UiInteractorsManagers

        #region UiInteractors

        public IUiInteractor GetUiInteractor(string uiInteractorsManagerName = SharedUiConstants.DefaultUiInteractorsManagerInstance, string uiInteractorName = SharedUiConstants.DefaultUiInteractorInstance, int? threadId = null);
        public IUiInteractor GetUiInteractorOfCurrentThread(string uiInteractorName = SharedUiConstants.DefaultUiInteractorInstance);
        public void DisposeUiInteractor(string uiInteractorsManagerName = SharedUiConstants.DefaultUiInteractorsManagerInstance, string uiInteractorName = SharedUiConstants.DefaultUiInteractorInstance, int? threadId = null);
        public void DisposeAllUiInteractors(string uiInteractorsManagerName = SharedUiConstants.DefaultUiInteractorsManagerInstance);

        #endregion UiInteractors
    }
}

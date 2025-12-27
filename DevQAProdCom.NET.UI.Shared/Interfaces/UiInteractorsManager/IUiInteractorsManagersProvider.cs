using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager
{
    public interface IUiInteractorsManagersProvider : IHaveIdentifiers
    {
        //No DisposeUiInteractorsManager/DisposeUiInteractor by Thread methods are implemented as safeguard, because, if Feature Scope is used, it is possible that, if invoked in OneTimeTearDown,
        //OneTimeTearDown may be running in a different thread than OneTimeSeup, where it could be created for the whole Feature.

        #region UiInteractorsManagers

        public IUiInteractorsManager GetUiInteractorsManager(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null, int? threadId = null);
        public IUiInteractorsManager GetUiInteractorsManagerOfCurrentThread();

        /// <summary>
        /// Can be used in 'TearDown' for disposing UiInteractorManager with 'UiInteractorsManagerScope.Test' when ThreadId is the same with what was in 'SetUp' hook during creation.
        /// </summary>
        /// <param name="uiInteractorsManagerScope"></param>
        /// <param name="threadId"></param>
        /// <param name="uiInteractorsManagerName"></param>
        public void DisposeUiInteractorsManager(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null, int? threadId = null);

        /// <summary>
        /// Can be used in 'OmeTimeTearDown' for disposing UiInteractorManager with 'UiInteractorsManagerScope.Feature' when ThreadId may differ from what was in 'OneTimeSetUp' hook during creation.
        /// </summary>
        /// <param name="uiInteractorsManagerScope"></param>
        /// <param name="uiInteractorsManagerName"></param>
        public void DisposeUiInteractorsManagers(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null);
        public void DisposeAllUiInteractorsManagers();

        #endregion UiInteractorsManagers

        #region UiInteractors

        public IUiInteractor GetUiInteractor(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null, string uiInteractorName = SharedUiConstants.DefaultUiInteractorInstance, int? threadId = null);
        public IUiInteractor GetUiInteractorOfCurrentThread(string uiInteractorName = SharedUiConstants.DefaultUiInteractorInstance);
        public void DisposeUiInteractor(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null, string uiInteractorName = SharedUiConstants.DefaultUiInteractorInstance, int? threadId = null);
        public void DisposeAllUiInteractors(UiInteractorsManagerScope uiInteractorsManagerScope, string? uiInteractorsManagerName = null);

        #endregion UiInteractors
    }
}

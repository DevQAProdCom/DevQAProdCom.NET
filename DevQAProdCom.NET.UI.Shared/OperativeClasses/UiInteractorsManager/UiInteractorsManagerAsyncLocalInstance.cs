using DevQAProdCom.NET.Logging.Shared.InterfacesAndEnumerations.Interfaces;
using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractorsManager;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractorsManager
{
    public class UiInteractorsManagerAsyncLocalInstance(Func<IUiInteractorsManager> getUiInteractorManagerFunc, ILogger log) : IUiInteractorsManagerAsyncLocalInstance //IUiInteractorsManagerThreadSpecificInstance
    {
        public Guid Id { get; } = Guid.NewGuid();

        private string? _name;
        public string? Name
        {
            get
            {
                if (_name == null)
                    _name = GetType().FullName;

                return _name;
            }
            set { _name = value; }
        }

        private readonly AsyncLocal<IUiInteractorsManager?> _uiInteractorsManager = new();

        internal IUiInteractorsManager UiInteractorsManager
        {
            get
            {
                if (_uiInteractorsManager.Value == null)
                    _uiInteractorsManager.Value = getUiInteractorManagerFunc();

                return _uiInteractorsManager.Value;
            }
        }

        //[ThreadStatic]
        //private IUiInteractorsManager? _uiInteractorsManager;

        //internal IUiInteractorsManager UiInteractorsManager
        //{
        //    get
        //    {
        //        if (_uiInteractorsManager == null)
        //            _uiInteractorsManager = getUiInteractorManagerFunc();

        //        return _uiInteractorsManager;
        //    }
        //}

        public IUiInteractor GetUiInteractor(string name = SharedUiConstants.DefaultUiInteractorInstance)
        {
            return UiInteractorsManager.GetUiInteractor(name);
        }

        public void DisposeUiInteractor(string name = SharedUiConstants.DefaultUiInteractorInstance)
        {
            UiInteractorsManager.DisposeUiInteractor(name);

        }

        public void DisposeUiInteractors()
        {
            UiInteractorsManager.DisposeUiInteractors();

        }

        public void DisposeUiInteractorsManager()
        {
            _uiInteractorsManager.Value?.DisposeUiInteractors();
            _uiInteractorsManager.Value = null;
        }

        //public void DisposeUiInteractorsManager()
        //{
        //    _uiInteractorsManager.DisposeUiInteractors();
        //    _uiInteractorsManager = null;
        //}
    }
}

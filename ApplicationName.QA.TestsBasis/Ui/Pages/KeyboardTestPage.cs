using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.UiElements.Interfaces;

namespace ApplicationName.QA.TestsBasis.Ui.Pages
{
    public class KeyboardTestPage : BaseAppUiPage
    {
        public override string RelativeUri => @"/KeyboardTestPage";

        [Find(Use.IdEquals, "Key Down Event Interceptor")]
        public IUiElement KeyDownEventInterceptorSection;

        [Find(Use.IdEquals, "Key Down Code")]
        public IInputText KeyDownCodeInfo;

        [Find(Use.IdEquals, "Key Down Value")]
        public IInputText KeyDownValueInfo;

        [Find(Use.IdEquals, "Key Up Event Interceptor")]
        public IUiElement KeyUpEventInterceptorSection;

        [Find(Use.IdEquals, "Key Up Code")]
        public IInputText KeyUpCodeInfoTextBox;

        [Find(Use.IdEquals, "Key Up Value")]
        public IInputText KeyUpValueInfoTextBox;

        [Find(Use.IdEquals, "Key Press Event Interceptor")]
        public IUiElement KeyPressEventInterceptorSection;

        [Find(Use.IdEquals, "Key Press Event Interceptor Key Down Code")]
        public IInputText KeyPressEventInterceptorKeyDownCodeInfo;

        [Find(Use.IdEquals, "Key Press Event Interceptor Key Down Value")]
        public IInputText KeyPressEventInterceptorKeyDownValueInfo;

        [Find(Use.IdEquals, "Key Press Event Interceptor Key Up Code")]
        public IInputText KeyPressEventInterceptorKeyUpCodeInfo;

        [Find(Use.IdEquals, "Key Press Event Interceptor Key Up Value")]
        public IInputText KeyPressEventInterceptorKeyUpValueInfo;

        [Find(Use.IdEquals, "Copy TextBox")]
        public IInputText CopyTextBox;

        [Find(Use.IdEquals, "Paste TextBox")]
        public IInputText PasteTextBox;

        [Find(Use.IdEquals, "Input TextBox")]
        public IInputText InputTextBox;

        [Find(Use.IdEquals, "Key DownUp Event Interceptor")]
        public IUiElement KeyDownUpEventInterceptorSection;

        [Find(Use.IdEquals, "Key DownUp Code")]
        public IInputText KeyDownUpCodeInfo;

        [Find(Use.IdEquals, "Key DownUp Value")]
        public IInputText KeyDownUpValueInfo;
    }
}

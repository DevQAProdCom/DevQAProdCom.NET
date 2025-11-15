using DevQAProdCom.NET.UI.Shared.Attributes;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.UI.UiElements.OperativeClasses;

namespace ApplicationName.QA.TestsBasis.Ui.Pages
{
    public class KeyboardTestPage : BaseAppUiPage
    {
        public override string RelativeUri => @"/KeyboardTestPage";

        [Find(Use.IdEquals, "Key Down Event Interceptor")]
        public IUiElement KeyDownEventInterceptorSection;

        [Find(Use.IdEquals, "Key Down Code")]
        public InputText KeyDownCodeInfo;

        [Find(Use.IdEquals, "Key Down Value")]
        public InputText KeyDownValueInfo;

        [Find(Use.IdEquals, "Key Up Event Interceptor")]
        public IUiElement KeyUpEventInterceptorSection;

        [Find(Use.IdEquals, "Key Up Code")]
        public InputText KeyUpCodeInfoTextBox;

        [Find(Use.IdEquals, "Key Up Value")]
        public InputText KeyUpValueInfoTextBox;

        [Find(Use.IdEquals, "Key Press Event Interceptor")]
        public IUiElement KeyPressEventInterceptorSection;

        [Find(Use.IdEquals, "Key Press Event Interceptor Key Down Code")]
        public InputText KeyPressEventInterceptorKeyDownCodeInfo;

        [Find(Use.IdEquals, "Key Press Event Interceptor Key Down Value")]
        public InputText KeyPressEventInterceptorKeyDownValueInfo;

        [Find(Use.IdEquals, "Key Press Event Interceptor Key Up Code")]
        public InputText KeyPressEventInterceptorKeyUpCodeInfo;

        [Find(Use.IdEquals, "Key Press Event Interceptor Key Up Value")]
        public InputText KeyPressEventInterceptorKeyUpValueInfo;

        [Find(Use.IdEquals, "Copy TextBox")]
        public InputText CopyTextBox;

        [Find(Use.IdEquals, "Paste TextBox")]
        public InputText PasteTextBox;

        [Find(Use.IdEquals, "Input TextBox")]
        public InputText InputTextBox;

        [Find(Use.IdEquals, "Key DownUp Event Interceptor")]
        public IUiElement KeyDownUpEventInterceptorSection;

        [Find(Use.IdEquals, "Key DownUp Code")]
        public InputText KeyDownUpCodeInfo;

        [Find(Use.IdEquals, "Key DownUp Value")]
        public InputText KeyDownUpValueInfo;
    }
}

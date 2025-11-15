using DevQAProdCom.NET.UI.Shared.Constants;
using DevQAProdCom.NET.UI.Shared.Enumerations;
using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Keyboard;
using DevQAProdCom.NET.UI.Shared.Interfaces.Behaviors.Text;
using DevQAProdCom.NET.UI.Shared.Interfaces.UiElements;
using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;
using DevQAProdCom.NET.Global.OperativeClasses;

namespace DevQAProdCom.NET.UI.Shared.OperativeClasses.Behaviors
{
    public abstract class BaseUiElementFulfillTextBehavior<T> : BaseBehavior, IFulfillTextBehavior
         where T : IUiElement
    {
        protected T UiElement;

        public BaseUiElementFulfillTextBehavior(IBehaviorParameters parameters) : base(parameters)
        {
            UiElement = (T)parameters.Get<IUiElement>(SharedUiConstants.IUiElement);
        }

        public abstract void TypeText(string text);
        public abstract void SetText(string text);

        public void SetInnerHtmlJs(string text)
        {
            ExecuteScript(SharedUiConstants.Files.SetInnerHtmlJavaScriptFilePath, text);
        }

        public void SetTextContentJs(string text)
        {
            ExecuteScript(SharedUiConstants.Files.CopyPasteTextJavaScriptFilePath, text);
        }

        public void AppendText(string text)
        {
            UiElement.FocusJs();
            string currentText = UiElement.AddBehavior<IGetTextBehavior>().GetText();
            string textToAppend = currentText + text;
            TypeText(textToAppend);
        }

        public void CopyPasteText(string text)
        {
            ExecuteScript(SharedUiConstants.Files.CopyPasteTextJavaScriptFilePath, text);
            UiElement.AddBehavior<IKeyboardBehavior>().PressKeysSimultaneously($"{Key.Control}+v");
        }

        private string GetScriptPath(string path)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string? parentDirectory = Directory.GetParent(currentDirectory)?.FullName;

            if (!string.IsNullOrEmpty(parentDirectory))
                return Path.Combine(parentDirectory, path);
            return path;
        }

        private void ExecuteScript(string path, string text)
        {
            UiElement.FocusJs();
            var scriptPath = GetScriptPath(path);
            var filePath = new FileInfo(scriptPath);
            UiElement.ExecuteJavaScript(filePath, new KeyValuePair<string, object>("textParameter", text));
        }
    }
}

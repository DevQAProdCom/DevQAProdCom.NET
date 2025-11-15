using DevQAProdCom.NET.Global.ModelsAndInterfaces.Interfaces;

namespace DevQAProdCom.NET.UI.Shared.Interfaces.UiElements.Search
{
    public interface IFindParameters : IHaveId
    {
        public uint NestingLevel { get; }
        public IUiElementsFindInfo FindInfo { get; }
        public IUiElement Parent { get; }
        public int? UiIndex { get; }
        public bool IsList { get; }
        public bool IsElementOfList { get; }
        public bool IsElementInsideFrame { get; }

        public object? NativeElement { get; }
        public object? NativeFrameElement { get; }
        public object? NativeShadowRootHostElement { get; }

        //Auxiliary
        public string Name { get; }
    }
}

namespace DevQAProdCom.NET.UI.Shared.Enumerations
{
    public enum Use
    {
        NotSet, //was introduced for find attribute because it doesn't allow nullable enum  Use? framesFindMethod = null

        //taken from Selenium
        IdEquals,
        IdContains,

        NameEquals,
        NameContains,

        ClassNameEquals,
        ClassNameContains,

        LinkTextEquals,
        LinkTextContains,

        TagName,
        CssSelector,
        XPath,

        //taken from Playwright
        Role, //NotSupported

        TextEquals,
        TextContains,

        LabelEquals,
        LabelContains,

        PlaceholderEquals,
        PlaceholderContains,

        AltTextEquals,
        AltTextContains,

        TitleEquals,
        TitleContains,

        DataTestIdEquals,
        DataTestIdContains,
    }
}

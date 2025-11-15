using DevQAProdCom.NET.UI.Shared.Interfaces.UiInteractor;
using DevQAProdCom.NET.UI.Shared.OperativeClasses.UiInteractor;
using Tests.DevQAProdCom.NET.UI.DependencyInjection;
using Tests.DevQAProdCom.NET.UI.Models;

namespace Tests.DevQAProdCom.NET.UI.Constants
{
    internal class Const
    {
        public static class Folders
        {
            public const string TestData = "TestData";
        }

        public static class DragAndDrop
        {
            public const string ElementToDrag = "Fall asleep";
            public const string ElementToDropTo = "Brush teeth";

            public static readonly List<string> ToDoListInitialState = new List<string>()
            {
                "Get to work",
                "Pick up groceries",
                "Go home",
                "Fall asleep"
            };

            public static readonly List<string> DoneListInitialState = new List<string>()
            {
                "Get up",
                "Brush teeth",
                "Take a shower",
                "Check e-mail",
                "Walk dog"
            };
        }

        public static class Ui
        {
            public const string AttributeForCustomFindOptionSearchMethodRegisteredFromDi = "attribute-for-custom-find-option-search-method-registered-from-di";
            public const string CustomFindOptionSearchMethodRegisteredFromDiUsingCustomAttribute = "CustomFindOptionSearchMethodRegisteredFromDiUsingCustomAttribute";

            public const string AttributeForCustomFindOptionSearchMethodRegisteredFromActivatorCreateInstanceT = "attribute-for-custom-find-option-search-method-registered-from-activator-create-instance-t";
            public const string CustomFindOptionSearchMethodRegisteredFromActivatorCreateInstanceTUsingCustomAttribute = "CustomFindOptionSearchMethodRegisteredFromActivatorCreateInstanceTUsingCustomAttribute";
        }

        public const string ButtonNotInFrame = "Button Not In Frame";
        public const string TextContentOfButtonInsideTopLevel0FrameA = "Button Inside Top Level 0 Frame A";
        public const string TextContentOfButtonInsideTopLevel0FrameB = "Button Inside Top Level 0 Frame B";
        public const string ButtonFrameInsideFrame = "Frame Inside Frame Level 1 Button";

        public const string Interactor1 = "Interactor1";
        public const string Interactor2 = "Interactor2";

        public const string Interactor1_1 = "Interactor1_1";
        public const string Interactor1_2 = "Interactor1_2";

        public const string Interactor2_1 = "Interactor2_1";
        public const string Interactor2_2 = "Interactor2_2";

        public const string Tab1 = "Tab1";
        public const string Tab1Button1 = "Tab1 Button1";

        public const string Tab1ClickToCheckCookie = "Tab1 Click to check Cookie";
        public static string Tab1CookieAFound => $"Tab1: {CookieAMessage} Found!";
        public static string Tab1CookieANotFound => $"Tab1: {CookieAMessage} Not Found!";

        public const string Tab2 = "Tab2";
        public const string Tab2Button1 = "Tab2 Button1";
        public const string Tab2ClickToCheckCookie = "Tab2 Click to check Cookie";
        public static string Tab2CookieAFound => $"Tab2: {CookieAMessage} Found!";
        public static string Tab2CookieANotFound => $"Tab2: {CookieAMessage} Not Found!";

        public const string Tab3 = "Tab3";
        public const string Tab3Button1 = "Tab3 Button1";
        public const string Tab3ClickToCheckCookie = "Tab3 Click to check Cookie";
        public static string Tab3CookieBFound => $"Tab3: {CookieBMessage} Found!";
        public static string Tab3CookieBNotFound => $"Tab3: {CookieBMessage} Not Found!";

        public const string ButtonMultipleFindAttributes1 = "Button Multiple Find Attributes 1";
        public const string ButtonMultipleFindAttributes2 = "Button Multiple Find Attributes 2";

        public const string id = "id";
        public const string dataText = "data-text";

        public const string SomePath = "SomePath";
        public const string SomePath2 = "SomePath2";
        public const string Screenshot_Directory = "Screenshot_Directory";

        public static string GetCookieMessage(string name, string value)
        {
            return $"Cookie with name \"{name}\" and value \"{value}\" -";
        }

        #region Cookie A

        public const string CookieAName = "cookieAName";
        public const string CookieAValue = "cookieAValue";

        public static string CookieAPath = DiContainer.Instance.AppSettings.BaseUrl + SomePath;
        public static Uri CookieAUri = new Uri(CookieAPath);

        public static DateTime CookieAExpiresFullDateTime = DateTime.UtcNow.AddDays(1).AddMinutes(1);
        public static DateTime CookieAExpires = new DateTime(CookieAExpiresFullDateTime.Year, CookieAExpiresFullDateTime.Month, CookieAExpiresFullDateTime.Day, CookieAExpiresFullDateTime.Hour, CookieAExpiresFullDateTime.Minute, CookieAExpiresFullDateTime.Second, DateTimeKind.Utc);

        public static string CookieAMessage = GetCookieMessage(CookieAName, CookieAValue);

        public static readonly IUiInteractorCookie CookieAWithDomainPathConstructor = new UiInteractorCookie(CookieAName, CookieAValue, CookieAUri.Host, CookieAUri.AbsolutePath)
        {
            Expires = CookieAExpires,
            HttpOnly = false,
            Secure = false,
            SameSite = "Strict"
        };

        public static readonly IUiInteractorCookie CookieAWithUriConstructor = new UiInteractorCookie(CookieAName, CookieAValue, CookieAUri)
        {
            Expires = CookieAWithDomainPathConstructor.Expires,
            HttpOnly = CookieAWithDomainPathConstructor.HttpOnly,
            Secure = CookieAWithDomainPathConstructor.Secure,
            SameSite = CookieAWithDomainPathConstructor.SameSite
        };

        #endregion Cookie A

        #region Cookie B

        public const string CookieBName = "cookieBName";
        public const string CookieBValue = "cookieBValue";

        public static string CookieBPath = DiContainer.Instance.AppSettings.BaseUrl + SomePath2;
        public static Uri CookieBUri = new Uri(CookieBPath);


        public static DateTime CookieBExpiresFullDateTime = DateTime.UtcNow.AddHours(1).AddMinutes(1).AddSeconds(1);
        public static DateTime CookieBExpires = new DateTime(CookieBExpiresFullDateTime.Year, CookieBExpiresFullDateTime.Month, CookieBExpiresFullDateTime.Day, CookieBExpiresFullDateTime.Hour, CookieBExpiresFullDateTime.Minute, CookieBExpiresFullDateTime.Second, DateTimeKind.Utc);

        public static string CookieBMessage = GetCookieMessage(CookieBName, CookieBValue);

        public static readonly IUiInteractorCookie CookieBWithDomainPathConstructor = new UiInteractorCookie(CookieBName, CookieBValue, CookieBUri.Host, CookieBUri.AbsolutePath)
        {
            Expires = CookieBExpires,
            HttpOnly = false,
            Secure = true,
            SameSite = "Lax"
        };

        #endregion Cookie B

        #region Cookie X

        public const string CookieXName = "cookieXName";
        public const string CookieXValue = "cookieXValue";

        public static string CookieXPath = DiContainer.Instance.AppSettings.BaseUrl + SomePath;
        public static Uri CookieXUri = new Uri(CookieXPath);

        public static DateTime CookieXExpiresFullDateTime = DateTime.UtcNow.AddHours(1).AddMinutes(1);
        public static DateTime CookieXExpires = new DateTime(CookieXExpiresFullDateTime.Year, CookieXExpiresFullDateTime.Month, CookieXExpiresFullDateTime.Day, CookieXExpiresFullDateTime.Hour, CookieXExpiresFullDateTime.Minute, CookieXExpiresFullDateTime.Second, DateTimeKind.Utc);


        public static readonly IUiInteractorCookie CookieXWithDomainPathConstructor = new UiInteractorCookie(CookieXName, CookieXValue, CookieXUri.Host, CookieXUri.AbsolutePath)
        {
            Expires = CookieXExpires,
            HttpOnly = true,
            Secure = true,
            SameSite = "Lax"
        };


        #endregion Cookie X

        public static List<Row> Table2Rows = new List<Row>()
        {
            new Row()
            {
                Cells = new List<Cell>()
                {
                    new Cell("Table2 | Row_1 Cell_1 Header_1"),
                    new Cell("Table2 | Row_1 Cell_2 Header_2"),
                }
            },
            new Row()
            {
                Cells = new List<Cell>()
                {
                    new Cell("Table2 | Row_2 Cell_1 SubHeader_1"),
                    new Cell("Table2 | Row_2 Cell_2 SubHeader_2"),
                }
            }
        };

        public static List<Row> Table2BRows = new List<Row>()
        {
            new Row()
            {
                Cells = new List<Cell>()
                {
                    new Cell("Table2 B| Row_1 Cell_1 Header_1"),
                    new Cell("Table2 B| Row_1 Cell_2 Header_2"),
                }
            },
            new Row()
            {
                Cells = new List<Cell>()
                {
                    new Cell("Table2 B| Row_2 Cell_1 SubHeader_1"),
                    new Cell("Table2 B| Row_2 Cell_2 SubHeader_2"),
                }
            }
        };

        public static List<Row> Table3Rows = new List<Row>()
        {
            new Row()
            {
                Cells = new List<Cell>()
                {
                    new Cell("Table3 | Row_1 Cell_1 Header_1"),
                    new Cell("Table3 | Row_1 Cell_2 Header_2"),
                }
            },
            new Row()
            {
                Cells = new List<Cell>()
                {
                    new Cell("Table3 | Row_2 Cell_1 SubHeader_1"),
                    new Cell("Table3 | Row_2 Cell_2 SubHeader_2"),
                }
            }
        };

        public static List<string> TestListItems1DivButtonsTextContent = new List<string>()
            {
                "TestListItem[0].Div.Button",
                "TestListItem[1].Div.Button",
            };

        public const string KeyDown = "KeyDown";
        public const string KeyUp = "KeyUp";
    }
}

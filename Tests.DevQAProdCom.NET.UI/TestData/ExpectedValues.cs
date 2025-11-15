namespace Tests.DevQAProdCom.NET.UI.TestData
{
    public static class ExpectedValues
    {
        public const string HyphenArrowRight = "->";
        public const string Underscore = "_";

        public const string Page = "page";
        public const string PageHyphenArrowRight = $"{Page}{HyphenArrowRight}";
        public const string ShadowRoot = "shadow-root";
        public const string SimpleUiElementAsInterface = "simpleUiElementAsInterface";
        public const string FrameSimpleUiElementAsInterface = "frameSimpleUiElementAsInterface";
        public const string FrameComplexUiElementAsClass = "frameComplexUiElementAsClass";
        public const string ShadowRootHostSimpleUiElementAsInterface = "shadowRootHostSimpleUiElementAsInterface";
        public const string ShadowRootHostComplexUiElementAsClass = "shadowRootHostComplexUiElementAsClass";

        public const string ComplexUiElementAsClass = "complexUiElementAsClass";
        public const string UiElementsList = "uiElementsList";

        public static readonly string Page_TopLevelSimpleUiElementAsInterface = $"{PageHyphenArrowRight}topLevelSimpleUiElementAsInterface";
        public static readonly string Page_ComplexUiElement = $"{PageHyphenArrowRight}{ComplexUiElementAsClass}";

        public static readonly string Page_SimpleUiElementAsInterface_e424_TextContent = $"{PageHyphenArrowRight}{GetSimpleUiElementAsInterfaceWithId("e424")}";
        public static readonly string Page_ComplexUiElementAsClass_4fe1_TextContent = $"{PageHyphenArrowRight}{GetComplexUiElementAsClassWithId("4fe1")}";


        private const string Guid_36d9 = "36d9";
        public static readonly string Page_UiElementsList_36d9_SimpleUiElementAsInterface_36d9_Indx1_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfSimpleUiElementsAsInterfaceWithId(Guid_36d9, 1)}";
        public static readonly string Page_UiElementsList_36d9_SimpleUiElementAsInterface_36d9_Indx2_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfSimpleUiElementsAsInterfaceWithId(Guid_36d9, 2)}";

        private const string Guid_e125 = "e125";
        public static readonly string Page_UiElementsList_e125_ComplexUiElementsAsClass_e125_Indx1_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_e125, 1)}";
        public static readonly string Page_UiElementsList_e125_ComplexUiElementsAsClass_e125_Indx2_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_e125, 2)}";

        public static readonly string Page_FrameSimpleUiElementAsInterface_c7bd_IdAttribute = $"{PageHyphenArrowRight}{GetFrameSimpleUiElementAsInterfaceWithId("c7bd")}";

        private const string Guid_07e0 = "07e0";
        public static readonly string Page_FrameComplexUiElementAsClass_07e0_FrameSimpleUiElementAsInterface_07e0_IdAttribute
            = $"{PageHyphenArrowRight}{GetFrameComplexUiElementAsClassWithId(Guid_07e0)}{HyphenArrowRight}{GetFrameSimpleUiElementAsInterfaceWithId(Guid_07e0)}";

        private const string Guid_1fc6 = "1fc6";
        public static readonly string Page_UiElementsList_1fc6_FrameSimpleUiElementAsInterface_1fc6_Indx1_IdAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameSimpleUiElementsAsInterfaceWithId(Guid_1fc6, 1)}";
        public static readonly string Page_UiElementsList_1fc6_FrameSimpleUiElementAsInterface_1fc6_Indx2_IdAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameSimpleUiElementsAsInterfaceWithId(Guid_1fc6, 2)}";

        private const string Guid_bbd6 = "bbd6";
        public static readonly string Page_UiElementsList_bbd6_ShadowRootHostSimpleUiElementAsInterface_bbd6_Indx1_IdAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostSimpleUiElementsAsInterfaceWithId(Guid_bbd6, 1)}";
        public static readonly string Page_UiElementsList_bbd6_ShadowRootHostSimpleUiElementAsInterface_bbd6_Indx2_IdAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostSimpleUiElementsAsInterfaceWithId(Guid_bbd6, 2)}";

        private const string Guid_740c = "740c";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_740c_ShadowRootHostSimpleUiElementAsInterface_740c_IdAttribute
            = $"{PageHyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId(Guid_740c)}{HyphenArrowRight}{GetShadowRootHostSimpleUiElementAsInterfaceWithId(Guid_740c)}";


        public static readonly string Page_ShadowRootHostSimpleUiElementAsInterface_4d55_IdAttribute = $"{PageHyphenArrowRight}{GetShadowRootHostSimpleUiElementAsInterfaceWithId("4d55")}";

        private const string Guid54dd = "54dd";
        public static readonly string Page_ShadowRoot54dd_SimpleUiElement54dd_TextContent = $"{PageHyphenArrowRight}{GetShadowRootWithId(Guid54dd)}{HyphenArrowRight}{GetSimpleUiElementAsInterfaceWithId(Guid54dd)}";


        #region Page -> Complex_UiElement_AsClass

        #region Page -> Complex_UiElement_AsClass -> Simple_UiElement_AsInterface
        private const string Guid_86ea = "86ea";
        public static readonly string Page_ComplexUiElementAsClass_86ea_SimpleUiElementAsInterface_86ea = $"{PageHyphenArrowRight}{GetComplexUiElementAsClassWithId(Guid_86ea)}{HyphenArrowRight}{GetSimpleUiElementAsInterfaceWithId(Guid_86ea)}";
        #endregion Page -> Complex_UiElement_AsClass -> Simple_UiElement_AsInterface

        #region Page -> Complex_UiElement_AsClass -> Complex_UiElement_AsClass
        public static readonly string Page_ComplexUiElementAsClass_0d51_ComplexUiElementAsClass_fd65 = $"{PageHyphenArrowRight}{GetComplexUiElementAsClassWithId("0d51")}{HyphenArrowRight}{GetComplexUiElementAsClassWithId("fd65")}";
        #endregion Page -> Complex_UiElement_AsClass -> Complex_UiElement_AsClass

        #region Page -> Complex_UiElement_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface
        public static readonly string Page_ComplexUiElementAsClass_576e = $"{PageHyphenArrowRight}{GetComplexUiElementAsClassWithId("576e")}";
        private const string Guid_e141 = "e141";
        public static readonly string Page_ComplexUiElementAsClass_576e_UiElementsList_e141_SimpleUiElementAsInterface_e141_Indx1_TextContent = $"{Page_ComplexUiElementAsClass_576e}{HyphenArrowRight}{GetUiElementsListOfSimpleUiElementsAsInterfaceWithId(Guid_e141, 1)}";
        public static readonly string Page_ComplexUiElementAsClass_576e_UiElementsList_e141_SimpleUiElementAsInterface_e141_Indx2_TextContent = $"{Page_ComplexUiElementAsClass_576e}{HyphenArrowRight}{GetUiElementsListOfSimpleUiElementsAsInterfaceWithId(Guid_e141, 2)}";
        #endregion Page -> Complex_UiElement_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface

        #region Page -> Complex_UiElement_AsClass -> UiElementsList_Of_Complex_UiElements_AsClass
        public static readonly string Page_ComplexUiElementAsClass_44a5 = $"{PageHyphenArrowRight}{GetComplexUiElementAsClassWithId("44a5")}";
        private const string Guid_b0d2 = "b0d2";
        public static readonly string Page_ComplexUiElementAsClass_44a5_UiElementsList_b0d2_ComplexUiElementAsClass_b0d2_Indx1_TextContent = $"{Page_ComplexUiElementAsClass_44a5}{HyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_b0d2, 1)}";
        public static readonly string Page_ComplexUiElementAsClass_44a5_UiElementsList_b0d2_ComplexUiElementAsClass_b0d2_Indx2_TextContent = $"{Page_ComplexUiElementAsClass_44a5}{HyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_b0d2, 2)}";
        #endregion Page -> Complex_UiElement_AsClass -> UiElementsList_Of_Complex_UiElements_AsClass

        #region Page -> Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface
        private const string Guid_cd75 = "cd75";
        public static readonly string Page_ComplexUiElementAsClass_cd75_FrameSimpleUiElementAsInterface_cd75_IdAttribute
            = $"{PageHyphenArrowRight}{GetComplexUiElementAsClassWithId(Guid_cd75)}{HyphenArrowRight}{GetFrameSimpleUiElementAsInterfaceWithId(Guid_cd75)}";
        #endregion Page -> Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface

        #region Page -> Complex_UiElement_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface
        private const string Guid_030a = "030a";
        public static readonly string Page_ComplexUiElementAsClass_030a = $"{PageHyphenArrowRight}{GetComplexUiElementAsClassWithId(Guid_030a)}";
        public static readonly string Page_ComplexUiElementAsClass_030a_UiElementsList_030a_FrameSimpleUiElementAsInterface_030a_Indx1_IdAttribute = $"{Page_ComplexUiElementAsClass_030a}{HyphenArrowRight}{GetUiElementsListOfFrameSimpleUiElementsAsInterfaceWithId(Guid_030a, 1)}";
        public static readonly string Page_ComplexUiElementAsClass_030a_UiElementsList_030a_FrameSimpleUiElementAsInterface_030a_Indx2_IdAttribute = $"{Page_ComplexUiElementAsClass_030a}{HyphenArrowRight}{GetUiElementsListOfFrameSimpleUiElementsAsInterfaceWithId(Guid_030a, 2)}";
        #endregion Page -> Complex_UiElement_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface

        #region Page -> Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface
        private const string Guid_5905 = "5905";
        public static readonly string Page_ComplexUiElementAsClass_5905_FrameComplexUiElementAsClass_5905_FrameSimpleUiElementAsInterface_5905_IdAttribute
            = $"{PageHyphenArrowRight}{GetComplexUiElementAsClassWithId(Guid_5905)}{HyphenArrowRight}{GetFrameComplexUiElementAsClassWithId(Guid_5905)}{HyphenArrowRight}{GetFrameSimpleUiElementAsInterfaceWithId(Guid_5905)}";
        #endregion Page -> Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface

        #region Page -> Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface
        private const string Guid_7275 = "7275";
        public static readonly string Page_ComplexUiElementAsClass_7275_ShadowRootHostSimpleUiElementAsInterface_7275_IdAttribute
            = $"{PageHyphenArrowRight}{GetComplexUiElementAsClassWithId(Guid_7275)}{HyphenArrowRight}{GetShadowRootHostSimpleUiElementAsInterfaceWithId(Guid_7275)}";
        #endregion Page -> Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        #region Page -> Complex_UiElement_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface
        private const string Guid_8939 = "8939";
        public static readonly string Page_ComplexUiElementAsClass_8939 = $"{PageHyphenArrowRight}{GetComplexUiElementAsClassWithId(Guid_8939)}";
        public static readonly string Page_ComplexUiElementAsClass_8939_UiElementsList_8939_ShadowRootHostSimpleUiElementAsInterface_8939_Indx1_IdAttribute = $"{Page_ComplexUiElementAsClass_8939}{HyphenArrowRight}{GetUiElementsListOfShadowRootHostSimpleUiElementsAsInterfaceWithId(Guid_8939, 1)}";
        public static readonly string Page_ComplexUiElementAsClass_8939_UiElementsList_8939_ShadowRootHostSimpleUiElementAsInterface_8939_Indx2_IdAttribute = $"{Page_ComplexUiElementAsClass_8939}{HyphenArrowRight}{GetUiElementsListOfShadowRootHostSimpleUiElementsAsInterfaceWithId(Guid_8939, 2)}";
        #endregion Page -> Complex_UiElement_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface

        #region Page -> Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface
        private const string Guid_4f49 = "4f49";
        public static readonly string Page_ComplexUiElementAsClass_4f49_ShadowRootHostComplexUiElementAsClass_4f49_ShadowRootHostSimpleUiElementAsInterface_4f49_IdAttribute
            = $"{PageHyphenArrowRight}{GetComplexUiElementAsClassWithId(Guid_4f49)}{HyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId(Guid_4f49)}{HyphenArrowRight}{GetShadowRootHostSimpleUiElementAsInterfaceWithId(Guid_4f49)}";
        #endregion Page -> Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        #endregion Page -> Complex_UiElement_AsClass

        #region Page -> UiElementsList_Of_Complex_UiElements_AsClass

        #region Page -> UiElementsList_Of_Complex_UiElements_AsClass -> Simple_UiElement_AsInterface

        private const string Guid_8921 = "8921";
        public static readonly string Page_UiElementsList_8921_ComplexUiElementAsClass_8921_Indx1_SimpleUiElementAsInterface_8921_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_8921, 1)}{HyphenArrowRight}{GetSimpleUiElementAsInterfaceWithId(Guid_8921)}";
        public static readonly string Page_UiElementsList_8921_ComplexUiElementAsClass_8921_Indx2_SimpleUiElementAsInterface_8921_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_8921, 2)}{HyphenArrowRight}{GetSimpleUiElementAsInterfaceWithId(Guid_8921)}";

        #endregion Page -> UiElementsList_Of_Complex_UiElements_AsClass -> Simple_UiElement_AsInterface

        #region Page -> UiElementsList_Of_Complex_UiElements_AsClass -> Complex_UiElement_AsClass

        private const string Guid_8e8c = "8e8c";
        private const string Guid_d543 = "d543";
        public static readonly string Page_UiElementsList_8e8c_ComplexUiElementAsClass_8e8c_Indx1_ComplexUiElementAsClass_d543_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_8e8c, 1)}{HyphenArrowRight}{GetComplexUiElementAsClassWithId(Guid_d543)}";
        public static readonly string Page_UiElementsList_8e8c_ComplexUiElementAsClass_8e8c_Indx2_ComplexUiElementAsClass_d543_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_8e8c, 2)}{HyphenArrowRight}{GetComplexUiElementAsClassWithId(Guid_d543)}";

        #endregion Page -> UiElementsList_Of_Complex_UiElements_AsClass -> Complex_UiElement_AsClass

        #region Page -> UiElementsList_Of_Complex_UiElements_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface

        private const string Guid_79c4 = "79c4";
        private const string Guid_5bea = "5bea";
        public static readonly string Page_UiElementsList_79c4_ComplexUiElementAsClass_79c4_Indx1_UiElementsList_5bea_SimpleUiElementAsInterface_5bea_Indx1_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_79c4, 1)}{HyphenArrowRight}{GetUiElementsListOfSimpleUiElementsAsInterfaceWithId(Guid_5bea, 1)}";
        public static readonly string Page_UiElementsList_79c4_ComplexUiElementAsClass_79c4_Indx1_UiElementsList_5bea_SimpleUiElementAsInterface_5bea_Indx2_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_79c4, 1)}{HyphenArrowRight}{GetUiElementsListOfSimpleUiElementsAsInterfaceWithId(Guid_5bea, 2)}";
        public static readonly string Page_UiElementsList_79c4_ComplexUiElementAsClass_79c4_Indx2_UiElementsList_5bea_SimpleUiElementAsInterface_5bea_Indx1_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_79c4, 2)}{HyphenArrowRight}{GetUiElementsListOfSimpleUiElementsAsInterfaceWithId(Guid_5bea, 1)}";
        public static readonly string Page_UiElementsList_79c4_ComplexUiElementAsClass_79c4_Indx2_UiElementsList_5bea_SimpleUiElementAsInterface_5bea_Indx2_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_79c4, 2)}{HyphenArrowRight}{GetUiElementsListOfSimpleUiElementsAsInterfaceWithId(Guid_5bea, 2)}";

        #endregion Page -> UiElementsList_Of_Complex_UiElements_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface

        #region Page -> UiElementsList_Of_Complex_UiElements_AsClass -> UiElementsList_Of_Complex_UiElements_AsClass

        private const string Guid_c0d6 = "c0d6";
        private const string Guid_9400 = "9400";
        public static readonly string Page_UiElementsList_c0d6_ComplexUiElementAsClass_c0d6_Indx1_UiElementsList_9400_ComplexUiElementAsClass_9400_Indx1_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_c0d6, 1)}{HyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_9400, 1)}";
        public static readonly string Page_UiElementsList_c0d6_ComplexUiElementAsClass_c0d6_Indx1_UiElementsList_9400_ComplexUiElementAsClass_9400_Indx2_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_c0d6, 1)}{HyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_9400, 2)}";
        public static readonly string Page_UiElementsList_c0d6_ComplexUiElementAsClass_c0d6_Indx2_UiElementsList_9400_ComplexUiElementAsClass_9400_Indx1_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_c0d6, 2)}{HyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_9400, 1)}";
        public static readonly string Page_UiElementsList_c0d6_ComplexUiElementAsClass_c0d6_Indx2_UiElementsList_9400_ComplexUiElementAsClass_9400_Indx2_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_c0d6, 2)}{HyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_9400, 2)}";

        #endregion Page -> UiElementsList_Of_Complex_UiElements_AsClass -> UiElementsList_Of_Complex_UiElements_AsClass

        #region Page -> UiElementsList_Of_Complex_UiElements_AsClass -> Frame_Simple_UiElement_AsInterface

        private const string Guid_4596 = "4596";
        public static readonly string Page_UiElementsList_4596_ComplexUiElementAsClass_4596_Indx1_FrameSimpleUiElementAsInterface_4596_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_4596, 1)}{HyphenArrowRight}{GetFrameSimpleUiElementAsInterfaceWithId(Guid_4596)}";
        public static readonly string Page_UiElementsList_4596_ComplexUiElementAsClass_4596_Indx2_FrameSimpleUiElementAsInterface_4596_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_4596, 2)}{HyphenArrowRight}{GetFrameSimpleUiElementAsInterfaceWithId(Guid_4596)}";

        #endregion Page -> UiElementsList_Of_Complex_UiElements_AsClass -> Frame_Simple_UiElement_AsInterface


        #region Page -> UiElementsList_Of_Complex_UiElements_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface

        private const string Guid_9240 = "9240";
        private const string Guid_07b9 = "07b9";
        public static readonly string Page_UiElementsList_9240_ComplexUiElementAsClass_9240_Indx1_UiElementsList_07b9_FrameSimpleUiElementAsInterface_07b9_Indx1_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_9240, 1)}{HyphenArrowRight}{GetUiElementsListOfFrameSimpleUiElementsAsInterfaceWithId(Guid_07b9, 1)}";
        public static readonly string Page_UiElementsList_9240_ComplexUiElementAsClass_9240_Indx1_UiElementsList_07b9_FrameSimpleUiElementAsInterface_07b9_Indx2_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_9240, 1)}{HyphenArrowRight}{GetUiElementsListOfFrameSimpleUiElementsAsInterfaceWithId(Guid_07b9, 2)}";
        public static readonly string Page_UiElementsList_9240_ComplexUiElementAsClass_9240_Indx2_UiElementsList_07b9_FrameSimpleUiElementAsInterface_07b9_Indx1_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_9240, 2)}{HyphenArrowRight}{GetUiElementsListOfFrameSimpleUiElementsAsInterfaceWithId(Guid_07b9, 1)}";
        public static readonly string Page_UiElementsList_9240_ComplexUiElementAsClass_9240_Indx2_UiElementsList_07b9_FrameSimpleUiElementAsInterface_07b9_Indx2_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_9240, 2)}{HyphenArrowRight}{GetUiElementsListOfFrameSimpleUiElementsAsInterfaceWithId(Guid_07b9, 2)}";

        #endregion Page -> UiElementsList_Of_Complex_UiElements_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface

        #region Page -> UiElementsList_Of_Complex_UiElements_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface

        private const string Guid_68b1 = "68b1";
        public static readonly string Page_UiElementsList_68b1_ComplexUiElementAsClass_68b1_Indx1_FrameComplexUiElementAsClass_68b1_FrameSimpleUiElementAsInterface_68b1_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_68b1, 1)}{HyphenArrowRight}{GetFrameComplexUiElementAsClassWithId(Guid_68b1)}{HyphenArrowRight}{GetFrameSimpleUiElementAsInterfaceWithId(Guid_68b1)}";
        public static readonly string Page_UiElementsList_68b1_ComplexUiElementAsClass_68b1_Indx2_FrameComplexUiElementAsClass_68b1_FrameSimpleUiElementAsInterface_68b1_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_68b1, 2)}{HyphenArrowRight}{GetFrameComplexUiElementAsClassWithId(Guid_68b1)}{HyphenArrowRight}{GetFrameSimpleUiElementAsInterfaceWithId(Guid_68b1)}";

        #endregion Page -> UiElementsList_Of_Complex_UiElements_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface


        #region Page -> UiElementsList_Of_Complex_UiElements_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        private const string Guid_52fa = "52fa";
        public static readonly string Page_UiElementsList_52fa_ComplexUiElementAsClass_52fa_Indx1_ShadowRootHostSimpleUiElementAsInterface_52fa_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_52fa, 1)}{HyphenArrowRight}{GetShadowRootHostSimpleUiElementAsInterfaceWithId(Guid_52fa)}";
        public static readonly string Page_UiElementsList_52fa_ComplexUiElementAsClass_52fa_Indx2_ShadowRootHostSimpleUiElementAsInterface_52fa_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_52fa, 2)}{HyphenArrowRight}{GetShadowRootHostSimpleUiElementAsInterfaceWithId(Guid_52fa)}";

        #endregion Page -> UiElementsList_Of_Complex_UiElements_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        #region Page -> UiElementsList_Of_Complex_UiElements_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface

        private const string Guid_d413 = "d413";
        private const string Guid_a018 = "a018";
        public static readonly string Page_UiElementsList_d413_ComplexUiElementAsClass_d413_Indx1_UiElementsList_a018_ShadowRootHostSimpleUiElementAsInterface_a018_Indx1_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_d413, 1)}{HyphenArrowRight}{GetUiElementsListOfShadowRootHostSimpleUiElementsAsInterfaceWithId(Guid_a018, 1)}";
        public static readonly string Page_UiElementsList_d413_ComplexUiElementAsClass_d413_Indx1_UiElementsList_a018_ShadowRootHostSimpleUiElementAsInterface_a018_Indx2_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_d413, 1)}{HyphenArrowRight}{GetUiElementsListOfShadowRootHostSimpleUiElementsAsInterfaceWithId(Guid_a018, 2)}";
        public static readonly string Page_UiElementsList_d413_ComplexUiElementAsClass_d413_Indx2_UiElementsList_a018_ShadowRootHostSimpleUiElementAsInterface_a018_Indx1_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_d413, 2)}{HyphenArrowRight}{GetUiElementsListOfShadowRootHostSimpleUiElementsAsInterfaceWithId(Guid_a018, 1)}";
        public static readonly string Page_UiElementsList_d413_ComplexUiElementAsClass_d413_Indx2_UiElementsList_a018_ShadowRootHostSimpleUiElementAsInterface_a018_Indx2_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_d413, 2)}{HyphenArrowRight}{GetUiElementsListOfShadowRootHostSimpleUiElementsAsInterfaceWithId(Guid_a018, 2)}";

        #endregion Page -> UiElementsList_Of_Complex_UiElements_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface


        #region Page -> UiElementsList_Of_Complex_UiElements_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        private const string Guid_49e5 = "49e5";
        public static readonly string Page_UiElementsList_49e5_ComplexUiElementAsClass_49e5_Indx1_ShadowRootHostComplexUiElementAsClass_49e5_ShadowRootHostSimpleUiElementAsInterface_49e5_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_49e5, 1)}{HyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId(Guid_49e5)}{HyphenArrowRight}{GetShadowRootHostSimpleUiElementAsInterfaceWithId(Guid_49e5)}";
        public static readonly string Page_UiElementsList_49e5_ComplexUiElementAsClass_49e5_Indx2_ShadowRootHostComplexUiElementAsClass_49e5_ShadowRootHostSimpleUiElementAsInterface_49e5_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_49e5, 2)}{HyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId(Guid_49e5)}{HyphenArrowRight}{GetShadowRootHostSimpleUiElementAsInterfaceWithId(Guid_49e5)}";

        #endregion Page -> UiElementsList_Of_Complex_UiElements_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface



        #endregion Page -> UiElementsList_Of_Complex_UiElements_AsClass

        #region Page -> Frame_Complex_UiElement_AsClass

        #region Page -> Frame_Complex_UiElement_AsClass -> Simple_UiElement_AsInterface
        private const string Guid_965d = "965d";
        public static readonly string Page_FrameComplexUiElementAsClass_965d_SimpleUiElementAsInterface_965d = $"{PageHyphenArrowRight}{GetFrameComplexUiElementAsClassWithId(Guid_965d)}{HyphenArrowRight}{GetSimpleUiElementAsInterfaceWithId(Guid_965d)}";
        #endregion Page -> Frame_Complex_UiElement_AsClass -> Simple_UiElement_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> Complex_UiElement_AsClass
        public static readonly string Page_FrameComplexUiElementAsClass_c39d_ComplexUiElementAsClass_c39d = $"{PageHyphenArrowRight}{GetFrameComplexUiElementAsClassWithId("c39d")}{HyphenArrowRight}{GetComplexUiElementAsClassWithId("c39d")}";
        #endregion Page -> Frame_Complex_UiElement_AsClass -> Complex_UiElement_AsClass

        #region Page -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface
        public static readonly string Page_FrameComplexUiElementAsClass_7c15 = $"{PageHyphenArrowRight}{GetFrameComplexUiElementAsClassWithId("7c15")}";
        private const string Guid_6399 = "6399";
        public static readonly string Page_FrameComplexUiElementAsClass_7c15_UiElementsList_6399_SimpleUiElementAsInterface_6399_Indx1_TextContent = $"{Page_FrameComplexUiElementAsClass_7c15}{HyphenArrowRight}{GetUiElementsListOfSimpleUiElementsAsInterfaceWithId(Guid_6399, 1)}";
        public static readonly string Page_FrameComplexUiElementAsClass_7c15_UiElementsList_6399_SimpleUiElementAsInterface_6399_Indx2_TextContent = $"{Page_FrameComplexUiElementAsClass_7c15}{HyphenArrowRight}{GetUiElementsListOfSimpleUiElementsAsInterfaceWithId(Guid_6399, 2)}";
        #endregion Page -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Complex_UiElements_AsClass
        public static readonly string Page_FrameComplexUiElementAsClass_b0fc = $"{PageHyphenArrowRight}{GetFrameComplexUiElementAsClassWithId("b0fc")}";
        private const string Guid_47fa = "47fa";
        public static readonly string Page_FrameComplexUiElementAsClass_b0fc_UiElementsList_47fa_ComplexUiElementAsClass_47fa_Indx1_TextContent = $"{Page_FrameComplexUiElementAsClass_b0fc}{HyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_47fa, 1)}";
        public static readonly string Page_FrameComplexUiElementAsClass_b0fc_UiElementsList_47fa_ComplexUiElementAsClass_47fa_Indx2_TextContent = $"{Page_FrameComplexUiElementAsClass_b0fc}{HyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_47fa, 2)}";
        #endregion Page -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Complex_UiElements_AsClass

        #region Page -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface
        private const string Guid_b152 = "b152";
        public static readonly string Page_FrameComplexUiElementAsClass_b152_FrameSimpleUiElementAsInterface_b152_IdAttribute
            = $"{PageHyphenArrowRight}{GetFrameComplexUiElementAsClassWithId(Guid_b152)}{HyphenArrowRight}{GetFrameSimpleUiElementAsInterfaceWithId(Guid_b152)}";
        #endregion Page -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface
        private const string Guid_50c2 = "50c2";
        public static readonly string Page_FrameComplexUiElementAsClass_50c2 = $"{PageHyphenArrowRight}{GetFrameComplexUiElementAsClassWithId(Guid_50c2)}";
        public static readonly string Page_FrameComplexUiElementAsClass_50c2_UiElementsList_50c2_FrameSimpleUiElementAsInterface_50c2_Indx1_IdAttribute = $"{Page_FrameComplexUiElementAsClass_50c2}{HyphenArrowRight}{GetUiElementsListOfFrameSimpleUiElementsAsInterfaceWithId(Guid_50c2, 1)}";
        public static readonly string Page_FrameComplexUiElementAsClass_50c2_UiElementsList_50c2_FrameSimpleUiElementAsInterface_50c2_Indx2_IdAttribute = $"{Page_FrameComplexUiElementAsClass_50c2}{HyphenArrowRight}{GetUiElementsListOfFrameSimpleUiElementsAsInterfaceWithId(Guid_50c2, 2)}";
        #endregion Page -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface
        private const string Guid_e77c = "e77c";
        private const string Guid_7116 = "7116";

        public static readonly string Page_FrameComplexUiElementAsClass_e77c_FrameComplexUiElementAsClass_7116_FrameSimpleUiElementAsInterface_7116_IdAttribute
            = $"{PageHyphenArrowRight}{GetFrameComplexUiElementAsClassWithId(Guid_e77c)}{HyphenArrowRight}{GetFrameComplexUiElementAsClassWithId(Guid_7116)}{HyphenArrowRight}{GetFrameSimpleUiElementAsInterfaceWithId(Guid_7116)}";
        #endregion Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface
        private const string Guid_d481 = "d481";
        public static readonly string Page_FrameComplexUiElementAsClass_d481_ShadowRootHostSimpleUiElementAsInterface_d481_IdAttribute
            = $"{PageHyphenArrowRight}{GetFrameComplexUiElementAsClassWithId(Guid_d481)}{HyphenArrowRight}{GetShadowRootHostSimpleUiElementAsInterfaceWithId(Guid_d481)}";
        #endregion Page -> Frame_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface
        private const string Guid_b37b = "b37b";
        public static readonly string Page_FrameComplexUiElementAsClass_b37b = $"{PageHyphenArrowRight}{GetFrameComplexUiElementAsClassWithId(Guid_b37b)}";
        public static readonly string Page_FrameComplexUiElementAsClass_b37b_UiElementsList_b37b_ShadowRootHostSimpleUiElementAsInterface_b37b_Indx1_IdAttribute = $"{Page_FrameComplexUiElementAsClass_b37b}{HyphenArrowRight}{GetUiElementsListOfShadowRootHostSimpleUiElementsAsInterfaceWithId(Guid_b37b, 1)}";
        public static readonly string Page_FrameComplexUiElementAsClass_b37b_UiElementsList_b37b_ShadowRootHostSimpleUiElementAsInterface_b37b_Indx2_IdAttribute = $"{Page_FrameComplexUiElementAsClass_b37b}{HyphenArrowRight}{GetUiElementsListOfShadowRootHostSimpleUiElementsAsInterfaceWithId(Guid_b37b, 2)}";
        #endregion Page -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface
        private const string Guid_8acd = "8acd";
        public static readonly string Page_FrameComplexUiElementAsClass_8acd_ShadowRootHostComplexUiElementAsClass_8acd_ShadowRootHostSimpleUiElementAsInterface_8acd_IdAttribute
            = $"{PageHyphenArrowRight}{GetFrameComplexUiElementAsClassWithId(Guid_8acd)}{HyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId(Guid_8acd)}{HyphenArrowRight}{GetShadowRootHostSimpleUiElementAsInterfaceWithId(Guid_8acd)}";
        #endregion Page -> Frame_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        #endregion Page -> Frame_Complex_UiElement_AsClass

        #region Page -> ShadowRootHost_Complex_UiElement_AsClass

        #region Page -> ShadowRootHost_Complex_UiElement_AsClass -> Simple_UiElement_AsInterface
        private const string Guid_5282 = "5282";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_5282_SimpleUiElementAsInterface_5282 = $"{PageHyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId(Guid_5282)}{HyphenArrowRight}{GetSimpleUiElementAsInterfaceWithId(Guid_5282)}";
        #endregion Page -> ShadowRootHost_Complex_UiElement_AsClass -> Simple_UiElement_AsInterface

        #region Page -> ShadowRootHost_Complex_UiElement_AsClass -> Complex_UiElement_AsClass
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_8a39_ComplexUiElementAsClass_8a39 = $"{PageHyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId("8a39")}{HyphenArrowRight}{GetComplexUiElementAsClassWithId("8a39")}";
        #endregion Page -> ShadowRootHost_Complex_UiElement_AsClass -> Complex_UiElement_AsClass

        #region Page -> ShadowRootHost_Complex_UiElement_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_3134 = $"{PageHyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId("3134")}";
        private const string Guid_4d30 = "4d30";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_3134_UiElementsList_4d30_SimpleUiElementAsInterface_4d30_Indx1_TextContent = $"{Page_ShadowRootHostComplexUiElementAsClass_3134}{HyphenArrowRight}{GetUiElementsListOfSimpleUiElementsAsInterfaceWithId(Guid_4d30, 1)}";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_3134_UiElementsList_4d30_SimpleUiElementAsInterface_4d30_Indx2_TextContent = $"{Page_ShadowRootHostComplexUiElementAsClass_3134}{HyphenArrowRight}{GetUiElementsListOfSimpleUiElementsAsInterfaceWithId(Guid_4d30, 2)}";
        #endregion Page -> ShadowRootHost_Complex_UiElement_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface

        #region Page -> ShadowRootHost_Complex_UiElement_AsClass -> UiElementsList_Of_Complex_UiElements_AsClass
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_8c40 = $"{PageHyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId("8c40")}";
        private const string Guid_43de = "43de";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_8c40_UiElementsList_43de_ComplexUiElementAsClass_43de_Indx1_TextContent = $"{Page_ShadowRootHostComplexUiElementAsClass_8c40}{HyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_43de, 1)}";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_8c40_UiElementsList_43de_ComplexUiElementAsClass_43de_Indx2_TextContent = $"{Page_ShadowRootHostComplexUiElementAsClass_8c40}{HyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_43de, 2)}";
        #endregion Page -> ShadowRootHost_Complex_UiElement_AsClass -> UiElementsList_Of_Complex_UiElements_AsClass

        #region Page -> ShadowRootHost_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface
        private const string Guid_66c4 = "66c4";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_66c4_FrameSimpleUiElementAsInterface_66c4_IdAttribute
            = $"{PageHyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId(Guid_66c4)}{HyphenArrowRight}{GetFrameSimpleUiElementAsInterfaceWithId(Guid_66c4)}";
        #endregion Page -> ShadowRootHost_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface

        #region Page -> ShadowRootHost_Complex_UiElement_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface
        private const string Guid_f789 = "f789";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_f789 = $"{PageHyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId(Guid_f789)}";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_f789_UiElementsList_f789_FrameSimpleUiElementAsInterface_f789_Indx1_IdAttribute = $"{Page_ShadowRootHostComplexUiElementAsClass_f789}{HyphenArrowRight}{GetUiElementsListOfFrameSimpleUiElementsAsInterfaceWithId(Guid_f789, 1)}";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_f789_UiElementsList_f789_FrameSimpleUiElementAsInterface_f789_Indx2_IdAttribute = $"{Page_ShadowRootHostComplexUiElementAsClass_f789}{HyphenArrowRight}{GetUiElementsListOfFrameSimpleUiElementsAsInterfaceWithId(Guid_f789, 2)}";
        #endregion Page -> ShadowRootHost_Complex_UiElement_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface

        #region Page -> ShadowRootHost_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface
        private const string Guid_8d68 = "8d68";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_8d68_FrameComplexUiElementAsClass_8d68_FrameSimpleUiElementAsInterface_8d68_IdAttribute
            = $"{PageHyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId(Guid_8d68)}{HyphenArrowRight}{GetFrameComplexUiElementAsClassWithId(Guid_8d68)}{HyphenArrowRight}{GetFrameSimpleUiElementAsInterfaceWithId(Guid_8d68)}";
        #endregion Page -> ShadowRootHost_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface

        #region Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface
        private const string Guid_7fbe = "7fbe";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_7fbe_ShadowRootHostSimpleUiElementAsInterface_7fbe_IdAttribute
            = $"{PageHyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId(Guid_7fbe)}{HyphenArrowRight}{GetShadowRootHostSimpleUiElementAsInterfaceWithId(Guid_7fbe)}";
        #endregion Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        #region Page -> ShadowRootHost_Complex_UiElement_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface
        private const string Guid_ba6b = "ba6b";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_ba6b = $"{PageHyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId(Guid_ba6b)}";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_ba6b_UiElementsList_ba6b_ShadowRootHostSimpleUiElementAsInterface_ba6b_Indx1_IdAttribute = $"{Page_ShadowRootHostComplexUiElementAsClass_ba6b}{HyphenArrowRight}{GetUiElementsListOfShadowRootHostSimpleUiElementsAsInterfaceWithId(Guid_ba6b, 1)}";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_ba6b_UiElementsList_ba6b_ShadowRootHostSimpleUiElementAsInterface_ba6b_Indx2_IdAttribute = $"{Page_ShadowRootHostComplexUiElementAsClass_ba6b}{HyphenArrowRight}{GetUiElementsListOfShadowRootHostSimpleUiElementsAsInterfaceWithId(Guid_ba6b, 2)}";
        #endregion Page -> ShadowRootHost_Complex_UiElement_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface

        #region Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface
        private const string Guid_4e79 = "4e79";
        private const string Guid_bb3d = "bb3d";

        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_4e79_ShadowRootHostComplexUiElementAsClass_bb3d_ShadowRootHostSimpleUiElementAsInterface_bb3d_IdAttribute
            = $"{PageHyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId(Guid_4e79)}{HyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId(Guid_bb3d)}{HyphenArrowRight}{GetShadowRootHostSimpleUiElementAsInterfaceWithId(Guid_bb3d)}";
        #endregion Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        #endregion Page -> ShadowRootHost_Complex_UiElement_AsClass

        #region Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass

        #region Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> Simple_UiElement_AsInterface

        private const string Guid_5d7b = "5d7b";
        public static readonly string Page_UiElementsList_5d7b_FrameComplexUiElementAsClass_5d7b_Indx1_SimpleUiElementAsInterface_5d7b_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_5d7b, 1)}{HyphenArrowRight}{GetSimpleUiElementAsInterfaceWithId(Guid_5d7b)}";
        public static readonly string Page_UiElementsList_5d7b_FrameComplexUiElementAsClass_5d7b_Indx2_SimpleUiElementAsInterface_5d7b_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_5d7b, 2)}{HyphenArrowRight}{GetSimpleUiElementAsInterfaceWithId(Guid_5d7b)}";

        #endregion Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> Simple_UiElement_AsInterface

        #region Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> Complex_UiElement_AsClass

        private const string Guid_5694 = "5694";
        private const string Guid_246f = "246f";
        public static readonly string Page_UiElementsList_5694_FrameComplexUiElementAsClass_5694_Indx1_ComplexUiElementAsClass_246f_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_5694, 1)}{HyphenArrowRight}{GetComplexUiElementAsClassWithId(Guid_246f)}";
        public static readonly string Page_UiElementsList_5694_FrameComplexUiElementAsClass_5694_Indx2_ComplexUiElementAsClass_246f_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_5694, 2)}{HyphenArrowRight}{GetComplexUiElementAsClassWithId(Guid_246f)}";

        #endregion Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> Complex_UiElement_AsClass

        #region Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface

        private const string Guid_473a = "473a";
        private const string Guid_b48f = "b48f";
        public static readonly string Page_UiElementsList_473a_FrameComplexUiElementAsClass_473a_Indx1_UiElementsList_b48f_SimpleUiElementAsInterface_b48f_Indx1_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_473a, 1)}{HyphenArrowRight}{GetUiElementsListOfSimpleUiElementsAsInterfaceWithId(Guid_b48f, 1)}";
        public static readonly string Page_UiElementsList_473a_FrameComplexUiElementAsClass_473a_Indx1_UiElementsList_b48f_SimpleUiElementAsInterface_b48f_Indx2_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_473a, 1)}{HyphenArrowRight}{GetUiElementsListOfSimpleUiElementsAsInterfaceWithId(Guid_b48f, 2)}";
        public static readonly string Page_UiElementsList_473a_FrameComplexUiElementAsClass_473a_Indx2_UiElementsList_b48f_SimpleUiElementAsInterface_b48f_Indx1_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_473a, 2)}{HyphenArrowRight}{GetUiElementsListOfSimpleUiElementsAsInterfaceWithId(Guid_b48f, 1)}";
        public static readonly string Page_UiElementsList_473a_FrameComplexUiElementAsClass_473a_Indx2_UiElementsList_b48f_SimpleUiElementAsInterface_b48f_Indx2_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_473a, 2)}{HyphenArrowRight}{GetUiElementsListOfSimpleUiElementsAsInterfaceWithId(Guid_b48f, 2)}";

        #endregion Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface

        #region Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> UiElementsList_Of_Frame_Complex_UiElements_AsClass

        private const string Guid_5c57 = "5c57";
        private const string Guid_0f36 = "0f36";
        public static readonly string Page_UiElementsList_5c57_FrameComplexUiElementAsClass_5c57_Indx1_UiElementsList_0f36_ComplexUiElementAsClass_0f36_Indx1_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_5c57, 1)}{HyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_0f36, 1)}";
        public static readonly string Page_UiElementsList_5c57_FrameComplexUiElementAsClass_5c57_Indx1_UiElementsList_0f36_ComplexUiElementAsClass_0f36_Indx2_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_5c57, 1)}{HyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_0f36, 2)}";
        public static readonly string Page_UiElementsList_5c57_FrameComplexUiElementAsClass_5c57_Indx2_UiElementsList_0f36_ComplexUiElementAsClass_0f36_Indx1_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_5c57, 2)}{HyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_0f36, 1)}";
        public static readonly string Page_UiElementsList_5c57_FrameComplexUiElementAsClass_5c57_Indx2_UiElementsList_0f36_ComplexUiElementAsClass_0f36_Indx2_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_5c57, 2)}{HyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_0f36, 2)}";

        #endregion Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> UiElementsList_Of_Frame_Complex_UiElements_AsClass

        #region Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> Frame_Simple_UiElement_AsInterface

        private const string Guid_5d5a = "5d5a";
        public static readonly string Page_UiElementsList_5d5a_FrameComplexUiElementAsClass_5d5a_Indx1_FrameSimpleUiElementAsInterface_5d5a_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_5d5a, 1)}{HyphenArrowRight}{GetFrameSimpleUiElementAsInterfaceWithId(Guid_5d5a)}";
        public static readonly string Page_UiElementsList_5d5a_FrameComplexUiElementAsClass_5d5a_Indx2_FrameSimpleUiElementAsInterface_5d5a_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_5d5a, 2)}{HyphenArrowRight}{GetFrameSimpleUiElementAsInterfaceWithId(Guid_5d5a)}";

        #endregion Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> Frame_Simple_UiElement_AsInterface

        #region Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface

        private const string Guid_dd66 = "dd66";
        private const string Guid_ac68 = "ac68";
        public static readonly string Page_UiElementsList_dd66_FrameComplexUiElementAsClass_dd66_Indx1_UiElementsList_ac68_FrameSimpleUiElementAsInterface_ac68_Indx1_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_dd66, 1)}{HyphenArrowRight}{GetUiElementsListOfFrameSimpleUiElementsAsInterfaceWithId(Guid_ac68, 1)}";
        public static readonly string Page_UiElementsList_dd66_FrameComplexUiElementAsClass_dd66_Indx1_UiElementsList_ac68_FrameSimpleUiElementAsInterface_ac68_Indx2_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_dd66, 1)}{HyphenArrowRight}{GetUiElementsListOfFrameSimpleUiElementsAsInterfaceWithId(Guid_ac68, 2)}";
        public static readonly string Page_UiElementsList_dd66_FrameComplexUiElementAsClass_dd66_Indx2_UiElementsList_ac68_FrameSimpleUiElementAsInterface_ac68_Indx1_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_dd66, 2)}{HyphenArrowRight}{GetUiElementsListOfFrameSimpleUiElementsAsInterfaceWithId(Guid_ac68, 1)}";
        public static readonly string Page_UiElementsList_dd66_FrameComplexUiElementAsClass_dd66_Indx2_UiElementsList_ac68_FrameSimpleUiElementAsInterface_ac68_Indx2_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_dd66, 2)}{HyphenArrowRight}{GetUiElementsListOfFrameSimpleUiElementsAsInterfaceWithId(Guid_ac68, 2)}";

        #endregion Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface

        #region Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface

        private const string Guid_558a = "558a";
        private const string Guid_a401 = "a401";
        public static readonly string Page_UiElementsList_558a_FrameComplexUiElementAsClass_558a_Indx1_FrameComplexUiElementAsClass_a401_FrameSimpleUiElementAsInterface_a401_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_558a, 1)}{HyphenArrowRight}{GetFrameComplexUiElementAsClassWithId(Guid_a401)}{HyphenArrowRight}{GetFrameSimpleUiElementAsInterfaceWithId(Guid_a401)}";
        public static readonly string Page_UiElementsList_558a_FrameComplexUiElementAsClass_558a_Indx2_FrameComplexUiElementAsClass_a401_FrameSimpleUiElementAsInterface_a401_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_558a, 2)}{HyphenArrowRight}{GetFrameComplexUiElementAsClassWithId(Guid_a401)}{HyphenArrowRight}{GetFrameSimpleUiElementAsInterfaceWithId(Guid_a401)}";

        #endregion Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface

        #region Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        private const string Guid_4ffa = "4ffa";
        public static readonly string Page_UiElementsList_4ffa_FrameComplexUiElementAsClass_4ffa_Indx1_ShadowRootHostSimpleUiElementAsInterface_4ffa_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_4ffa, 1)}{HyphenArrowRight}{GetShadowRootHostSimpleUiElementAsInterfaceWithId(Guid_4ffa)}";
        public static readonly string Page_UiElementsList_4ffa_FrameComplexUiElementAsClass_4ffa_Indx2_ShadowRootHostSimpleUiElementAsInterface_4ffa_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_4ffa, 2)}{HyphenArrowRight}{GetShadowRootHostSimpleUiElementAsInterfaceWithId(Guid_4ffa)}";

        #endregion Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        #region Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface

        private const string Guid_93c7 = "93c7";
        private const string Guid_7580 = "7580";
        public static readonly string Page_UiElementsList_93c7_FrameComplexUiElementAsClass_93c7_Indx1_UiElementsList_7580_ShadowRootHostSimpleUiElementAsInterface_7580_Indx1_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_93c7, 1)}{HyphenArrowRight}{GetUiElementsListOfShadowRootHostSimpleUiElementsAsInterfaceWithId(Guid_7580, 1)}";
        public static readonly string Page_UiElementsList_93c7_FrameComplexUiElementAsClass_93c7_Indx1_UiElementsList_7580_ShadowRootHostSimpleUiElementAsInterface_7580_Indx2_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_93c7, 1)}{HyphenArrowRight}{GetUiElementsListOfShadowRootHostSimpleUiElementsAsInterfaceWithId(Guid_7580, 2)}";
        public static readonly string Page_UiElementsList_93c7_FrameComplexUiElementAsClass_93c7_Indx2_UiElementsList_7580_ShadowRootHostSimpleUiElementAsInterface_7580_Indx1_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_93c7, 2)}{HyphenArrowRight}{GetUiElementsListOfShadowRootHostSimpleUiElementsAsInterfaceWithId(Guid_7580, 1)}";
        public static readonly string Page_UiElementsList_93c7_FrameComplexUiElementAsClass_93c7_Indx2_UiElementsList_7580_ShadowRootHostSimpleUiElementAsInterface_7580_Indx2_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_93c7, 2)}{HyphenArrowRight}{GetUiElementsListOfShadowRootHostSimpleUiElementsAsInterfaceWithId(Guid_7580, 2)}";

        #endregion Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface

        #region Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        private const string Guid_76f1 = "76f1";
        public static readonly string Page_UiElementsList_76f1_FrameComplexUiElementAsClass_76f1_Indx1_ShadowRootHostComplexUiElementAsClass_76f1_ShadowRootHostSimpleUiElementAsInterface_76f1_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_76f1, 1)}{HyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId(Guid_76f1)}{HyphenArrowRight}{GetShadowRootHostSimpleUiElementAsInterfaceWithId(Guid_76f1)}";
        public static readonly string Page_UiElementsList_76f1_FrameComplexUiElementAsClass_76f1_Indx2_ShadowRootHostComplexUiElementAsClass_76f1_ShadowRootHostSimpleUiElementAsInterface_76f1_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfFrameComplexUiElementsAsClassWithId(Guid_76f1, 2)}{HyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId(Guid_76f1)}{HyphenArrowRight}{GetShadowRootHostSimpleUiElementAsInterfaceWithId(Guid_76f1)}";

        #endregion Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        #endregion Page -> UiElementsList_Of_Frame_Complex_UiElements_AsClass

        #region Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass

        #region Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> Simple_UiElement_AsInterface

        private const string Guid_bbe0 = "bbe0";
        public static readonly string Page_UiElementsList_bbe0_ShadowRootHostComplexUiElementAsClass_bbe0_Indx1_SimpleUiElementAsInterface_bbe0_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_bbe0, 1)}{HyphenArrowRight}{GetSimpleUiElementAsInterfaceWithId(Guid_bbe0)}";
        public static readonly string Page_UiElementsList_bbe0_ShadowRootHostComplexUiElementAsClass_bbe0_Indx2_SimpleUiElementAsInterface_bbe0_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_bbe0, 2)}{HyphenArrowRight}{GetSimpleUiElementAsInterfaceWithId(Guid_bbe0)}";

        #endregion Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> Simple_UiElement_AsInterface

        #region Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> Complex_UiElement_AsClass

        private const string Guid_46e3 = "46e3";
        private const string Guid_a41c = "a41c";
        public static readonly string Page_UiElementsList_46e3_ShadowRootHostComplexUiElementAsClass_46e3_Indx1_ComplexUiElementAsClass_a41c_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_46e3, 1)}{HyphenArrowRight}{GetComplexUiElementAsClassWithId(Guid_a41c)}";
        public static readonly string Page_UiElementsList_46e3_ShadowRootHostComplexUiElementAsClass_46e3_Indx2_ComplexUiElementAsClass_a41c_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_46e3, 2)}{HyphenArrowRight}{GetComplexUiElementAsClassWithId(Guid_a41c)}";

        #endregion Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> Complex_UiElement_AsClass

        #region Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface

        private const string Guid_a368 = "a368";
        private const string Guid_a1a9 = "a1a9";
        public static readonly string Page_UiElementsList_a368_ShadowRootHostComplexUiElementAsClass_a368_Indx1_UiElementsList_a1a9_SimpleUiElementAsInterface_a1a9_Indx1_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_a368, 1)}{HyphenArrowRight}{GetUiElementsListOfSimpleUiElementsAsInterfaceWithId(Guid_a1a9, 1)}";
        public static readonly string Page_UiElementsList_a368_ShadowRootHostComplexUiElementAsClass_a368_Indx1_UiElementsList_a1a9_SimpleUiElementAsInterface_a1a9_Indx2_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_a368, 1)}{HyphenArrowRight}{GetUiElementsListOfSimpleUiElementsAsInterfaceWithId(Guid_a1a9, 2)}";
        public static readonly string Page_UiElementsList_a368_ShadowRootHostComplexUiElementAsClass_a368_Indx2_UiElementsList_a1a9_SimpleUiElementAsInterface_a1a9_Indx1_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_a368, 2)}{HyphenArrowRight}{GetUiElementsListOfSimpleUiElementsAsInterfaceWithId(Guid_a1a9, 1)}";
        public static readonly string Page_UiElementsList_a368_ShadowRootHostComplexUiElementAsClass_a368_Indx2_UiElementsList_a1a9_SimpleUiElementAsInterface_a1a9_Indx2_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_a368, 2)}{HyphenArrowRight}{GetUiElementsListOfSimpleUiElementsAsInterfaceWithId(Guid_a1a9, 2)}";

        #endregion Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface

        #region Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass

        private const string Guid_c417 = "c417";
        private const string Guid_2b8c = "2b8c";
        public static readonly string Page_UiElementsList_c417_ShadowRootHostComplexUiElementAsClass_c417_Indx1_UiElementsList_2b8c_ComplexUiElementAsClass_2b8c_Indx1_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_c417, 1)}{HyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_2b8c, 1)}";
        public static readonly string Page_UiElementsList_c417_ShadowRootHostComplexUiElementAsClass_c417_Indx1_UiElementsList_2b8c_ComplexUiElementAsClass_2b8c_Indx2_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_c417, 1)}{HyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_2b8c, 2)}";
        public static readonly string Page_UiElementsList_c417_ShadowRootHostComplexUiElementAsClass_c417_Indx2_UiElementsList_2b8c_ComplexUiElementAsClass_2b8c_Indx1_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_c417, 2)}{HyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_2b8c, 1)}";
        public static readonly string Page_UiElementsList_c417_ShadowRootHostComplexUiElementAsClass_c417_Indx2_UiElementsList_2b8c_ComplexUiElementAsClass_2b8c_Indx2_TextContent = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_c417, 2)}{HyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_2b8c, 2)}";

        #endregion Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass

        #region Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> Frame_Simple_UiElement_AsInterface

        private const string Guid_1e08 = "1e08";
        public static readonly string Page_UiElementsList_1e08_ShadowRootHostComplexUiElementAsClass_1e08_Indx1_FrameSimpleUiElementAsInterface_1e08_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_1e08, 1)}{HyphenArrowRight}{GetFrameSimpleUiElementAsInterfaceWithId(Guid_1e08)}";
        public static readonly string Page_UiElementsList_1e08_ShadowRootHostComplexUiElementAsClass_1e08_Indx2_FrameSimpleUiElementAsInterface_1e08_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_1e08, 2)}{HyphenArrowRight}{GetFrameSimpleUiElementAsInterfaceWithId(Guid_1e08)}";

        #endregion Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> Frame_Simple_UiElement_AsInterface

        #region Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface

        private const string Guid_a939 = "a939";
        private const string Guid_4bf9 = "4bf9";
        public static readonly string Page_UiElementsList_a939_ShadowRootHostComplexUiElementAsClass_a939_Indx1_UiElementsList_4bf9_FrameSimpleUiElementAsInterface_4bf9_Indx1_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_a939, 1)}{HyphenArrowRight}{GetUiElementsListOfFrameSimpleUiElementsAsInterfaceWithId(Guid_4bf9, 1)}";
        public static readonly string Page_UiElementsList_a939_ShadowRootHostComplexUiElementAsClass_a939_Indx1_UiElementsList_4bf9_FrameSimpleUiElementAsInterface_4bf9_Indx2_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_a939, 1)}{HyphenArrowRight}{GetUiElementsListOfFrameSimpleUiElementsAsInterfaceWithId(Guid_4bf9, 2)}";
        public static readonly string Page_UiElementsList_a939_ShadowRootHostComplexUiElementAsClass_a939_Indx2_UiElementsList_4bf9_FrameSimpleUiElementAsInterface_4bf9_Indx1_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_a939, 2)}{HyphenArrowRight}{GetUiElementsListOfFrameSimpleUiElementsAsInterfaceWithId(Guid_4bf9, 1)}";
        public static readonly string Page_UiElementsList_a939_ShadowRootHostComplexUiElementAsClass_a939_Indx2_UiElementsList_4bf9_FrameSimpleUiElementAsInterface_4bf9_Indx2_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_a939, 2)}{HyphenArrowRight}{GetUiElementsListOfFrameSimpleUiElementsAsInterfaceWithId(Guid_4bf9, 2)}";

        #endregion Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface

        #region Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface

        private const string Guid_a6e8 = "a6e8";
        private const string Guid_53dd = "53dd";
        public static readonly string Page_UiElementsList_a6e8_ShadowRootHostComplexUiElementAsClass_a6e8_Indx1_FrameComplexUiElementAsClass_53dd_FrameSimpleUiElementAsInterface_53dd_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_a6e8, 1)}{HyphenArrowRight}{GetFrameComplexUiElementAsClassWithId(Guid_53dd)}{HyphenArrowRight}{GetFrameSimpleUiElementAsInterfaceWithId(Guid_53dd)}";
        public static readonly string Page_UiElementsList_a6e8_ShadowRootHostComplexUiElementAsClass_a6e8_Indx2_FrameComplexUiElementAsClass_53dd_FrameSimpleUiElementAsInterface_53dd_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_a6e8, 2)}{HyphenArrowRight}{GetFrameComplexUiElementAsClassWithId(Guid_53dd)}{HyphenArrowRight}{GetFrameSimpleUiElementAsInterfaceWithId(Guid_53dd)}";

        #endregion Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface

        #region Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        private const string Guid_9efe = "9efe";
        public static readonly string Page_UiElementsList_9efe_ShadowRootHostComplexUiElementAsClass_9efe_Indx1_ShadowRootHostSimpleUiElementAsInterface_9efe_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_9efe, 1)}{HyphenArrowRight}{GetShadowRootHostSimpleUiElementAsInterfaceWithId(Guid_9efe)}";
        public static readonly string Page_UiElementsList_9efe_ShadowRootHostComplexUiElementAsClass_9efe_Indx2_ShadowRootHostSimpleUiElementAsInterface_9efe_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_9efe, 2)}{HyphenArrowRight}{GetShadowRootHostSimpleUiElementAsInterfaceWithId(Guid_9efe)}";

        #endregion Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        #region Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface

        private const string Guid_2377 = "2377";
        private const string Guid_cc40 = "cc40";
        public static readonly string Page_UiElementsList_2377_ShadowRootHostComplexUiElementAsClass_2377_Indx1_UiElementsList_cc40_ShadowRootHostSimpleUiElementAsInterface_cc40_Indx1_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_2377, 1)}{HyphenArrowRight}{GetUiElementsListOfShadowRootHostSimpleUiElementsAsInterfaceWithId(Guid_cc40, 1)}";
        public static readonly string Page_UiElementsList_2377_ShadowRootHostComplexUiElementAsClass_2377_Indx1_UiElementsList_cc40_ShadowRootHostSimpleUiElementAsInterface_cc40_Indx2_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_2377, 1)}{HyphenArrowRight}{GetUiElementsListOfShadowRootHostSimpleUiElementsAsInterfaceWithId(Guid_cc40, 2)}";
        public static readonly string Page_UiElementsList_2377_ShadowRootHostComplexUiElementAsClass_2377_Indx2_UiElementsList_cc40_ShadowRootHostSimpleUiElementAsInterface_cc40_Indx1_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_2377, 2)}{HyphenArrowRight}{GetUiElementsListOfShadowRootHostSimpleUiElementsAsInterfaceWithId(Guid_cc40, 1)}";
        public static readonly string Page_UiElementsList_2377_ShadowRootHostComplexUiElementAsClass_2377_Indx2_UiElementsList_cc40_ShadowRootHostSimpleUiElementAsInterface_cc40_Indx2_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_2377, 2)}{HyphenArrowRight}{GetUiElementsListOfShadowRootHostSimpleUiElementsAsInterfaceWithId(Guid_cc40, 2)}";

        #endregion Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface

        #region Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        private const string Guid_378f = "378f";
        private const string Guid_eea5 = "eea5";
        public static readonly string Page_UiElementsList_378f_ShadowRootHostComplexUiElementAsClass_378f_Indx1_ShadowRootHostComplexUiElementAsClass_eea5_ShadowRootHostSimpleUiElementAsInterface_eea5_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_378f, 1)}{HyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId(Guid_eea5)}{HyphenArrowRight}{GetShadowRootHostSimpleUiElementAsInterfaceWithId(Guid_eea5)}";
        public static readonly string Page_UiElementsList_378f_ShadowRootHostComplexUiElementAsClass_378f_Indx2_ShadowRootHostComplexUiElementAsClass_eea5_ShadowRootHostSimpleUiElementAsInterface_eea5_DataTextAttribute = $"{PageHyphenArrowRight}{GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(Guid_378f, 2)}{HyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId(Guid_eea5)}{HyphenArrowRight}{GetShadowRootHostSimpleUiElementAsInterfaceWithId(Guid_eea5)}";

        #endregion Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        #endregion Page -> UiElementsList_Of_ShadowRootHost_Complex_UiElements_AsClass

        #region Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass

        #region Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Simple_UiElement_AsInterface
        private const string Guid_b7bf = "b7bf";
        public static readonly string Page_FrameComplexUiElementAsClass_e720_FrameComplexUiElementAsClass_b7bf_SimpleUiElementAsInterface_b7bf
            = $"{PageHyphenArrowRight}{GetFrameComplexUiElementAsClassWithId("e720")}{HyphenArrowRight}{GetFrameComplexUiElementAsClassWithId(Guid_b7bf)}{HyphenArrowRight}{GetSimpleUiElementAsInterfaceWithId(Guid_b7bf)}";
        #endregion Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Simple_UiElement_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Complex_UiElement_AsClass
        private const string Guid_85b8 = "85b8";
        public static readonly string Page_FrameComplexUiElementAsClass_ae97_FrameComplexUiElementAsClass_85b8_ComplexUiElementAsClass_85b8
            = $"{PageHyphenArrowRight}{GetFrameComplexUiElementAsClassWithId("ae97")}{HyphenArrowRight}{GetFrameComplexUiElementAsClassWithId(Guid_85b8)}{HyphenArrowRight}{GetComplexUiElementAsClassWithId(Guid_85b8)}";
        #endregion Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Complex_UiElement_AsClass

        #region Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface
        public static readonly string Page_FrameComplexUiElementAsClass_d299 = $"{PageHyphenArrowRight}{GetFrameComplexUiElementAsClassWithId("d299")}";
        public static readonly string FrameComplexUiElementAsClass_b6ad = $"{GetFrameComplexUiElementAsClassWithId("b6ad")}";

        private const string Guid_56d9 = "56d9";
        public static readonly string Page_FrameComplexUiElementAsClass_d299_FrameComplexUiElementAsClass_b6ad_UiElementsList_56d9_SimpleUiElementAsInterface_56d9_Indx1_TextContent
            = $"{Page_FrameComplexUiElementAsClass_d299}{HyphenArrowRight}{FrameComplexUiElementAsClass_b6ad}{HyphenArrowRight}{GetUiElementsListOfSimpleUiElementsAsInterfaceWithId(Guid_56d9, 1)}";
        public static readonly string Page_FrameComplexUiElementAsClass_d299_FrameComplexUiElementAsClass_b6ad_UiElementsList_56d9_SimpleUiElementAsInterface_56d9_Indx2_TextContent
            = $"{Page_FrameComplexUiElementAsClass_d299}{HyphenArrowRight}{FrameComplexUiElementAsClass_b6ad}{HyphenArrowRight}{GetUiElementsListOfSimpleUiElementsAsInterfaceWithId(Guid_56d9, 2)}";
        #endregion Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Complex_UiElements_AsClass
        public static readonly string Page_FrameComplexUiElementAsClass_4f4d = $"{PageHyphenArrowRight}{GetFrameComplexUiElementAsClassWithId("4f4d")}";
        public static readonly string FrameComplexUiElementAsClass_9984 = $"{GetFrameComplexUiElementAsClassWithId("9984")}";

        private const string Guid_83f1 = "83f1";
        public static readonly string Page_FrameComplexUiElementAsClass_4f4d_FrameComplexUiElementAsClass_9984_UiElementsList_83f1_ComplexUiElementAsClass_83f1_Indx1_TextContent
            = $"{Page_FrameComplexUiElementAsClass_4f4d}{HyphenArrowRight}{FrameComplexUiElementAsClass_9984}{HyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_83f1, 1)}";
        public static readonly string Page_FrameComplexUiElementAsClass_4f4d_FrameComplexUiElementAsClass_9984_UiElementsList_83f1_ComplexUiElementAsClass_83f1_Indx2_TextContent
            = $"{Page_FrameComplexUiElementAsClass_4f4d}{HyphenArrowRight}{FrameComplexUiElementAsClass_9984}{HyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_83f1, 2)}";
        #endregion Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Complex_UiElements_AsClass

        #region Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface
        //Pairwise combination '2Frames -> Frame' is covered in scope of other test for combination 'Frame -> 2Frames'.
        //Search by 'Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface' inside solution.
        #endregion Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface
        public static readonly string Page_FrameComplexUiElementAsClass_78c2 = $"{PageHyphenArrowRight}{GetFrameComplexUiElementAsClassWithId("78c2")}";
        public static readonly string FrameComplexUiElementAsClass_4160 = $"{GetFrameComplexUiElementAsClassWithId("4160")}";
        private const string Guid_4160 = "4160";

        public static readonly string Page_FrameComplexUiElementAsClass_78c2_FrameComplexUiElementAsClass_4160_UiElementsList_4160_FrameSimpleUiElementAsInterface_4160_Indx1_IdAttribute
            = $"{Page_FrameComplexUiElementAsClass_78c2}{HyphenArrowRight}{FrameComplexUiElementAsClass_4160}{HyphenArrowRight}{GetUiElementsListOfFrameSimpleUiElementsAsInterfaceWithId(Guid_4160, 1)}";
        public static readonly string Page_FrameComplexUiElementAsClass_78c2_FrameComplexUiElementAsClass_4160_UiElementsList_4160_FrameSimpleUiElementAsInterface_4160_Indx2_IdAttribute
            = $"{Page_FrameComplexUiElementAsClass_78c2}{HyphenArrowRight}{FrameComplexUiElementAsClass_4160}{HyphenArrowRight}{GetUiElementsListOfFrameSimpleUiElementsAsInterfaceWithId(Guid_4160, 2)}";
        #endregion Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface
        //Pairwise combination '2Frames -> 2Frames' is covered in scope of other test for combination 'Frame -> 2Frames'.
        //Search by 'Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface' inside solution.
        #endregion Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface
        private const string Guid_2075 = "2075";
        public static readonly string Page_FrameComplexUiElementAsClass_e8a7_FrameComplexUiElementAsClass_2075_ShadowRootHostSimpleUiElementAsInterface_2075_IdAttribute
            = $"{PageHyphenArrowRight}{GetFrameComplexUiElementAsClassWithId("e8a7")}{HyphenArrowRight}{GetFrameComplexUiElementAsClassWithId(Guid_2075)}{HyphenArrowRight}{GetShadowRootHostSimpleUiElementAsInterfaceWithId(Guid_2075)}";
        #endregion Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface
        private const string Guid_4ee2 = "4ee2";
        public static readonly string Page_FrameComplexUiElementAsClass_3279 = $"{PageHyphenArrowRight}{GetFrameComplexUiElementAsClassWithId("3279")}";
        public static readonly string FrameComplexUiElementAsClass_4ee2 = $"{GetFrameComplexUiElementAsClassWithId(Guid_4ee2)}";

        public static readonly string Page_FrameComplexUiElementAsClass_3279_FrameComplexUiElementAsClass_4ee2_UiElementsList_4ee2_ShadowRootHostSimpleUiElementAsInterface_4ee2_Indx1_IdAttribute
            = $"{Page_FrameComplexUiElementAsClass_3279}{HyphenArrowRight}{FrameComplexUiElementAsClass_4ee2}{HyphenArrowRight}{GetUiElementsListOfShadowRootHostSimpleUiElementsAsInterfaceWithId(Guid_4ee2, 1)}";
        public static readonly string Page_FrameComplexUiElementAsClass_3279_FrameComplexUiElementAsClass_4ee2_UiElementsList_4ee2_ShadowRootHostSimpleUiElementAsInterface_4ee2_Indx2_IdAttribute
            = $"{Page_FrameComplexUiElementAsClass_3279}{HyphenArrowRight}{FrameComplexUiElementAsClass_4ee2}{HyphenArrowRight}{GetUiElementsListOfShadowRootHostSimpleUiElementsAsInterfaceWithId(Guid_4ee2, 2)}";
        #endregion Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface

        #region Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface
        private const string Guid_f2ab = "f2ab";
        public static readonly string Page_FrameComplexUiElementAsClass_29f4_FrameComplexUiElementAsClass_f2ab_ShadowRootHostComplexUiElementAsClass_f2ab_ShadowRootHostSimpleUiElementAsInterface_f2ab_IdAttribute
            = $"{PageHyphenArrowRight}{GetFrameComplexUiElementAsClassWithId("29f4")}{HyphenArrowRight}{GetFrameComplexUiElementAsClassWithId(Guid_f2ab)}{HyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId(Guid_f2ab)}{HyphenArrowRight}{GetShadowRootHostSimpleUiElementAsInterfaceWithId(Guid_f2ab)}";
        #endregion Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        #endregion Page -> Frame_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass

        public static string GetSimpleUiElementAsInterfaceWithId(string guid) => $"{SimpleUiElementAsInterface}{GetGuidInBrackets(guid)}";
        public static string GetComplexUiElementAsClassWithId(string guid) => $"{ComplexUiElementAsClass}{GetGuidInBrackets(guid)}";
        public static string GetFrameSimpleUiElementAsInterfaceWithId(string guid) => $"{FrameSimpleUiElementAsInterface}{GetGuidInBrackets(guid)}";
        public static string GetFrameComplexUiElementAsClassWithId(string guid) => $"{FrameComplexUiElementAsClass}{GetGuidInBrackets(guid)}";

        public static string GetShadowRootHostSimpleUiElementAsInterfaceWithId(string guid) => $"{ShadowRootHostSimpleUiElementAsInterface}{GetGuidInBrackets(guid)}";
        public static string GetShadowRootHostComplexUiElementAsClassWithId(string guid) => $"{ShadowRootHostComplexUiElementAsClass}{GetGuidInBrackets(guid)}";

        public static string GetUiElementsListOfSimpleUiElementsAsInterfaceWithId(string guid, int index) => UiElementsList + GetGuidInBrackets(guid) + Underscore + GetSimpleUiElementAsInterfaceWithId(guid) + GetIndexInBrackets(index);
        public static string GetUiElementsListOfComplexUiElementsAsClassWithId(string guid, int index) => UiElementsList + GetGuidInBrackets(guid) + Underscore + GetComplexUiElementAsClassWithId(guid) + GetIndexInBrackets(index);
        public static string GetUiElementsListOfFrameSimpleUiElementsAsInterfaceWithId(string guid, int index) => UiElementsList + GetGuidInBrackets(guid) + Underscore + GetFrameSimpleUiElementAsInterfaceWithId(guid) + GetIndexInBrackets(index);
        public static string GetUiElementsListOfFrameComplexUiElementsAsClassWithId(string guid, int index) => UiElementsList + GetGuidInBrackets(guid) + Underscore + GetFrameComplexUiElementAsClassWithId(guid) + GetIndexInBrackets(index);
        public static string GetUiElementsListOfShadowRootHostSimpleUiElementsAsInterfaceWithId(string guid, int index) => UiElementsList + GetGuidInBrackets(guid) + Underscore + GetShadowRootHostSimpleUiElementAsInterfaceWithId(guid) + GetIndexInBrackets(index);
        public static string GetUiElementsListOfShadowRootHostComplexUiElementsAsClassWithId(string guid, int index) => UiElementsList + GetGuidInBrackets(guid) + Underscore + GetShadowRootHostComplexUiElementAsClassWithId(guid) + GetIndexInBrackets(index);

        public static string GetShadowRootWithId(string guid) => $"{ShadowRoot}{GetGuidInBrackets(guid)}";
        public static string GetShadowRootWithIdHyphenArrowRight(string guid) => $"{GetShadowRootWithId(guid)}{HyphenArrowRight}";
        public static string GetGuidInBrackets(string guid) => $"({guid})";
        public static string GetIndexInBrackets(int index) => $"[{index}]";

        #region Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass

        #region Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> Simple_UiElement_AsInterface
        private const string Guid_14e6 = "14e6";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_d304_ShadowRootHostComplexUiElementAsClass_14e6_SimpleUiElementAsInterface_14e6
            = $"{PageHyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId("d304")}{HyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId(Guid_14e6)}{HyphenArrowRight}{GetSimpleUiElementAsInterfaceWithId(Guid_14e6)}";
        #endregion Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> Simple_UiElement_AsInterface

        #region Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> Complex_UiElement_AsClass
        private const string Guid_4463 = "4463";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_a95f_ShadowRootHostComplexUiElementAsClass_4463_ComplexUiElementAsClass_4463
            = $"{PageHyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId("a95f")}{HyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId(Guid_4463)}{HyphenArrowRight}{GetComplexUiElementAsClassWithId(Guid_4463)}";
        #endregion Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> Complex_UiElement_AsClass

        #region Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_9b04 = $"{PageHyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId("9b04")}";
        public static readonly string ShadowRootHostComplexUiElementAsClass_c24c = $"{GetShadowRootHostComplexUiElementAsClassWithId("c24c")}";

        private const string Guid_5ddd = "5ddd";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_9b04_ShadowRootHostComplexUiElementAsClass_c24c_UiElementsList_5ddd_SimpleUiElementAsInterface_5ddd_Indx1_TextContent
            = $"{Page_ShadowRootHostComplexUiElementAsClass_9b04}{HyphenArrowRight}{ShadowRootHostComplexUiElementAsClass_c24c}{HyphenArrowRight}{GetUiElementsListOfSimpleUiElementsAsInterfaceWithId(Guid_5ddd, 1)}";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_9b04_ShadowRootHostComplexUiElementAsClass_c24c_UiElementsList_5ddd_SimpleUiElementAsInterface_5ddd_Indx2_TextContent
            = $"{Page_ShadowRootHostComplexUiElementAsClass_9b04}{HyphenArrowRight}{ShadowRootHostComplexUiElementAsClass_c24c}{HyphenArrowRight}{GetUiElementsListOfSimpleUiElementsAsInterfaceWithId(Guid_5ddd, 2)}";
        #endregion Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> UiElementsList_Of_Simple_UiElements_AsInterface

        #region Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> UiElementsList_Of_Complex_UiElements_AsClass
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_95c0 = $"{PageHyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId("95c0")}";
        public static readonly string ShadowRootHostComplexUiElementAsClass_074a = $"{GetShadowRootHostComplexUiElementAsClassWithId("074a")}";

        private const string Guid_3503 = "3503";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_95c0_ShadowRootHostComplexUiElementAsClass_074a_UiElementsList_3503_ComplexUiElementAsClass_3503_Indx1_TextContent
            = $"{Page_ShadowRootHostComplexUiElementAsClass_95c0}{HyphenArrowRight}{ShadowRootHostComplexUiElementAsClass_074a}{HyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_3503, 1)}";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_95c0_ShadowRootHostComplexUiElementAsClass_074a_UiElementsList_3503_ComplexUiElementAsClass_3503_Indx2_TextContent
            = $"{Page_ShadowRootHostComplexUiElementAsClass_95c0}{HyphenArrowRight}{ShadowRootHostComplexUiElementAsClass_074a}{HyphenArrowRight}{GetUiElementsListOfComplexUiElementsAsClassWithId(Guid_3503, 2)}";
        #endregion Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> UiElementsList_Of_Complex_UiElements_AsClass

        #region Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface
        private const string Guid_4853 = "4853";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_5621_ShadowRootHostComplexUiElementAsClass_4853_FrameSimpleUiElementAsInterface_4853_IdAttribute
           = $"{PageHyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId("5621")}{HyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId(Guid_4853)}{HyphenArrowRight}{GetFrameSimpleUiElementAsInterfaceWithId(Guid_4853)}";
        #endregion Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface


        #region Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface
        private const string Guid_512f = "512f";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_6c2b = $"{PageHyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId("6c2b")}";
        public static readonly string ShadowRootHostComplexUiElementAsClass_512f = $"{GetShadowRootHostComplexUiElementAsClassWithId(Guid_512f)}";

        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_6c2b_ShadowRootHostComplexUiElementAsClass_512f_UiElementsList_512f_FrameSimpleUiElementAsInterface_512f_Indx1_IdAttribute
            = $"{Page_ShadowRootHostComplexUiElementAsClass_6c2b}{HyphenArrowRight}{ShadowRootHostComplexUiElementAsClass_512f}{HyphenArrowRight}{GetUiElementsListOfFrameSimpleUiElementsAsInterfaceWithId(Guid_512f, 1)}";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_6c2b_ShadowRootHostComplexUiElementAsClass_512f_UiElementsList_512f_FrameSimpleUiElementAsInterface_512f_Indx2_IdAttribute
            = $"{Page_ShadowRootHostComplexUiElementAsClass_6c2b}{HyphenArrowRight}{ShadowRootHostComplexUiElementAsClass_512f}{HyphenArrowRight}{GetUiElementsListOfFrameSimpleUiElementsAsInterfaceWithId(Guid_512f, 2)}";
        #endregion Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> UiElementsList_Of_Frame_Simple_UiElements_AsInterface

        #region Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface
        private const string Guid_5a5b = "5a5b";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_8237_ShadowRootHostComplexUiElementAsClass_5a5b_FrameComplexUiElementAsClass_5a5b_FrameSimpleUiElementAsInterface_5a5b_IdAttribute
           = $"{PageHyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId("8237")}{HyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId(Guid_5a5b)}{HyphenArrowRight}" +
            $"{GetFrameComplexUiElementAsClassWithId(Guid_5a5b)}{HyphenArrowRight}{GetFrameSimpleUiElementAsInterfaceWithId(Guid_5a5b)}";
        #endregion Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> Frame_Complex_UiElement_AsClass -> Frame_Simple_UiElement_AsInterface

        #region Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface
        //Pairwise combination '2ShadowRootHosts -> ShadowRootHost' is covered in scope of other test for combination 'ShadowRootHost -> 2ShadowRootHosts'.
        //Search by 'Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface' inside solution.
        #endregion Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        #region Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface
        private const string Guid_733f = "733f";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_f929 = $"{PageHyphenArrowRight}{GetShadowRootHostComplexUiElementAsClassWithId("f929")}";
        public static readonly string ShadowRootHostComplexUiElementAsClass_733f = $"{GetShadowRootHostComplexUiElementAsClassWithId(Guid_733f)}";

        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_f929_ShadowRootHostComplexUiElementAsClass_733f_UiElementsList_733f_ShadowRootHostSimpleUiElementAsInterface_733f_Indx1_IdAttribute
            = $"{Page_ShadowRootHostComplexUiElementAsClass_f929}{HyphenArrowRight}{ShadowRootHostComplexUiElementAsClass_733f}{HyphenArrowRight}{GetUiElementsListOfShadowRootHostSimpleUiElementsAsInterfaceWithId(Guid_733f, 1)}";
        public static readonly string Page_ShadowRootHostComplexUiElementAsClass_f929_ShadowRootHostComplexUiElementAsClass_733f_UiElementsList_733f_ShadowRootHostSimpleUiElementAsInterface_733f_Indx2_IdAttribute
            = $"{Page_ShadowRootHostComplexUiElementAsClass_f929}{HyphenArrowRight}{ShadowRootHostComplexUiElementAsClass_733f}{HyphenArrowRight}{GetUiElementsListOfShadowRootHostSimpleUiElementsAsInterfaceWithId(Guid_733f, 2)}";
        #endregion Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> UiElementsList_Of_ShadowRootHost_Simple_UiElements_AsInterface

        #region Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface
        //Pairwise combination '2ShadowRootHosts -> 2ShadowRootHosts' is covered in scope of other test for combination 'ShadowRootHost -> 2ShadowRootHosts'.
        //Search by 'Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface' inside solution.
        #endregion Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Simple_UiElement_AsInterface

        #endregion Page -> ShadowRootHost_Complex_UiElement_AsClass -> ShadowRootHost_Complex_UiElement_AsClass
    }
}

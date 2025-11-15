using ApplicationName.QA.TestsBasis.Ui.PageServices;
using FluentAssertions;
using FluentAssertions.Execution;
using Tests.DevQAProdCom.NET.UI.BaseTestClasses;
using Tests.DevQAProdCom.NET.UI.Constants;

namespace Tests.DevQAProdCom.NET.UI.Tests
{
    [Parallelizable(ParallelScope.All)]
    internal class Tests_UiElement_Search_Frames : PerScenarioBaseTest
    {
        [ThreadStatic] private static FramesTestPageService _framesTestPageService;

        [SetUp]
        public void SetUp()
        {
            _framesTestPageService = UiInteractor.Interact<FramesTestPageService>();
        }

        [Test]
        [Description("Page -> Frame (Find Attribute) -> Element")]
        public void Should_Support_Interaction_Page_FindAttributeWithElementInFrame()
        {
            //WHEN
            var actualNonFrameButtonTextBeforeInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();
            var actualFrameButtonText = _framesTestPageService._page.ButtonInsideTopLevel0Frame.GetTextContent();
            var actualNonFrameButtonTextAfterInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualNonFrameButtonTextBeforeInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
                actualFrameButtonText.Should().Be(Const.TextContentOfButtonInsideTopLevel0FrameA);
                actualNonFrameButtonTextAfterInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
            }
        }

        [Test]
        [Description("Page -> Frame (Find Attribute) -> Collection of Elements")]
        public void Should_Support_Interaction_Page_FrameFindAttribute_CollectionOfElements()
        {
            //GIVEN
            var expectedTextContent = new List<string>()
            {
                Const.Table2Rows[0].Cells![0].Text!,
                Const.Table2Rows[0].Cells![1].Text!,
                Const.Table2Rows[1].Cells![0].Text!,
                Const.Table2Rows[1].Cells![1].Text!,
            };

            //WHEN
            var actualNonFrameButtonTextBeforeInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();
            var actualTextContent = _framesTestPageService._page.CellsInsideTopLevel0Frame.Select(x => x.GetTextContent()).ToList();
            var actualNonFrameButtonTextAfterInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualNonFrameButtonTextBeforeInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
                actualTextContent.Should().BeEquivalentTo(expectedTextContent);
                actualNonFrameButtonTextAfterInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
            }
        }

        [Test]
        [Description("Page -> Frame (Find Attribute) -> Element -> Collection of Elements -> Collection of Elements")]
        public void Should_Support_Interaction_Page_FindAttributeWithElementInFrame_CollectionOfElements_CollectionOfElements()
        {
            //GIVEN
            var expectedTextContent = new List<string>()
            {
                Const.Table2Rows[0].Cells![0].Text!,
                Const.Table2Rows[0].Cells![1].Text!,
                Const.Table2Rows[1].Cells![0].Text!,
                Const.Table2Rows[1].Cells![1].Text!,
            };

            //WHEN
            var actualNonFrameButtonTextBeforeInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();
            var actualTextContent = _framesTestPageService._page.Table2InsideTopLevel0Frame.Rows.SelectMany(x => x.Cells.Select(y => y.GetTextContent())).ToList();
            var actualNonFrameButtonTextAfterInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualNonFrameButtonTextBeforeInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
                actualTextContent.Should().BeEquivalentTo(expectedTextContent);
                actualNonFrameButtonTextAfterInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
            }
        }

        [Test]
        [Description("Page -> Frame Attribute -> Frame (Find Attribute) -> Element")]
        public void Should_Support_Interaction_Page_FrameAttribute_FindAttributeWithElementInFrame()
        {
            //WHEN
            var actualNonFrameButtonTextBeforeInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();
            var actualFrameButtonText = _framesTestPageService._page.TopLevelFrame.ButtonFrameInsideFrame.GetTextContent();
            var actualNonFrameButtonTextAfterInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualNonFrameButtonTextBeforeInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
                actualFrameButtonText.Should().Be(Const.ButtonFrameInsideFrame);
                actualNonFrameButtonTextAfterInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
            }
        }

        [Test]
        [Description("Page -> Frame Attribute -> Element")]
        public void Should_Support_Interaction_Page_FrameAttribute_Element()
        {
            //WHEN
            var actualNonFrameButtonTextBeforeInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();
            var actualFrameButtonText = _framesTestPageService._page.TopLevelFrame.ButtonInsideTopLevel0Frame.GetTextContent();
            var actualNonFrameButtonTextAfterInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualNonFrameButtonTextBeforeInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
                actualFrameButtonText.Should().Be(Const.TextContentOfButtonInsideTopLevel0FrameA);
                actualNonFrameButtonTextAfterInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
            }
        }

        [Test]
        [Description("Page -> Frame Attribute -> Collection of Elements")]
        public void Should_Support_Interaction_Page_FrameAttribute_CollectionOfElements()
        {
            //GIVEN
            var expectedTextContent = new List<string>()
            {
                Const.Table2Rows[0].Cells![0].Text!,
                Const.Table2Rows[0].Cells![1].Text!,
                Const.Table2Rows[1].Cells![0].Text!,
                Const.Table2Rows[1].Cells![1].Text!,
            };

            //WHEN
            var actualNonFrameButtonTextBeforeInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();
            var actualTextContent = _framesTestPageService._page.TopLevelFrame.Cells.Select(x => x.GetTextContent()).ToList();
            var actualNonFrameButtonTextAfterInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualNonFrameButtonTextBeforeInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
                actualTextContent.Should().BeEquivalentTo(expectedTextContent);
                actualNonFrameButtonTextAfterInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
            }
        }

        [Test]
        [Description("Page -> Frame Attribute -> Element -> Collection of Elements -> Collection of Elements")]
        public void Should_Support_Interaction_Page_FrameAttribute_Element_CollectionOfElements_CollectionOfElements()
        {
            //GIVEN
            var expectedTextContent = new List<string>()
            {
                Const.Table2Rows[0].Cells![0].Text!,
                Const.Table2Rows[0].Cells![1].Text!,
                Const.Table2Rows[1].Cells![0].Text!,
                Const.Table2Rows[1].Cells![1].Text!,
            };

            //WHEN
            var actualNonFrameButtonTextBeforeInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();
            var actualTextContent = _framesTestPageService._page.TopLevelFrame.Table2.Rows.SelectMany(x => x.Cells.Select(y => y.GetTextContent())).ToList();
            var actualNonFrameButtonTextAfterInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualNonFrameButtonTextBeforeInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
                actualTextContent.Should().BeEquivalentTo(expectedTextContent);
                actualNonFrameButtonTextAfterInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
            }
        }

        [Test]
        [Description("Page -> Frame Attribute -> Frame Attribute -> Element")]
        public void Should_Support_Interaction_Page_FrameAttribute_FrameAttribute_Element()
        {
            //WHEN
            var actualNonFrameButtonTextBeforeInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();
            var actualFrameButtonText = _framesTestPageService._page.TopLevelFrame.FrameInsideFrame.ButtonFrameInsideFrame.GetTextContent();
            var actualNonFrameButtonTextAfterInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualNonFrameButtonTextBeforeInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
                actualFrameButtonText.Should().Be(Const.ButtonFrameInsideFrame);
                actualNonFrameButtonTextAfterInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
            }
        }

        [Test]
        [Description("A) Page -> Frame Attribute -> Frame Attribute -> Collection of Elements -> Collection of Elements; B) Page -> Frame Attribute -> Element -> Collection of Elements")]
        public void Should_Support_2_Consequent_Interactions_A_Page_FrameAttribute_FrameAttribute_CollectionOfElements_CollectionOfElements_B_Page_FrameAttribute_Element_CollectionOfElements()
        {
            //GIVEN
            var expectedTextContent = new List<string>()
            {
                Const.Table2Rows[0].Cells![0].Text!,
                Const.Table2Rows[0].Cells![1].Text!,
                Const.Table2Rows[1].Cells![0].Text!,
                Const.Table2Rows[1].Cells![1].Text!,
            };

            //WHEN
            var actualNonFrameButtonTextBeforeInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();
            var actualTextContentOfFrameInsideFrameTable = _framesTestPageService._page.TopLevelFrame.FrameInsideFrame.Rows.SelectMany(x => x.Cells.Select(y => y.GetTextContent())).ToList();
            var actualTextContentOfTopLevelFrameTable = _framesTestPageService._page.TopLevelFrame.Table2.Rows.SelectMany(x => x.Cells.Select(y => y.GetTextContent())).ToList();
            var actualNonFrameButtonTextAfterInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualNonFrameButtonTextBeforeInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
                actualTextContentOfFrameInsideFrameTable.Should().BeEquivalentTo(expectedTextContent);
                actualTextContentOfTopLevelFrameTable.Should().BeEquivalentTo(expectedTextContent);
                actualNonFrameButtonTextAfterInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
            }
        }

        [Test]
        [Description("A) Page -> Frame Attribute -> Frame Attribute -> Element -> Collection of Elements -> Collection of Elements; B) Page -> Frame Attribute -> Element-> Collection of Elements;")]
        public void Should_Support_2_Consequent_Interactions_A_Page_FrameAttribute_FrameAttribute_Element_CollectionOfElements_CollectionOfElements_B_Page_FrameAttribute_Element_CollectionOfElements()
        {
            //GIVEN
            var expectedTextContent = new List<string>()
            {
                Const.Table2Rows[0].Cells![0].Text!,
                Const.Table2Rows[0].Cells![1].Text!,
                Const.Table2Rows[1].Cells![0].Text!,
                Const.Table2Rows[1].Cells![1].Text!,
            };

            //WHEN
            var actualNonFrameButtonTextBeforeInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();
            var actualTextContentOfFrameInsideFrameTable = _framesTestPageService._page.TopLevelFrame.FrameInsideFrame.Table2.Rows.SelectMany(x => x.Cells.Select(y => y.GetTextContent())).ToList();
            var actualTextContentOfTopLevelFrameTable = _framesTestPageService._page.TopLevelFrame.Table2.Rows.SelectMany(x => x.Cells.Select(y => y.GetTextContent())).ToList();
            var actualNonFrameButtonTextAfterInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualNonFrameButtonTextBeforeInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
                actualTextContentOfFrameInsideFrameTable.Should().BeEquivalentTo(expectedTextContent);
                actualTextContentOfTopLevelFrameTable.Should().BeEquivalentTo(expectedTextContent);
                actualNonFrameButtonTextAfterInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
            }
        }

        [Test]
        [Description("Page -> Element -> Frame Attribute -> Element")]
        public void Should_Support_Interaction_Page_Element_FrameAttribute_Element()
        {
            //WHEN
            var actualNonFrameButtonTextBeforeInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();
            var actualFrameButtonText = _framesTestPageService._page.TableWithCellsWithFrames.TopLevelFrame.ButtonInsideTopLevel0Frame.GetTextContent();
            var actualNonFrameButtonTextAfterInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualNonFrameButtonTextBeforeInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
                actualFrameButtonText.Should().Be(Const.TextContentOfButtonInsideTopLevel0FrameA);
                actualNonFrameButtonTextAfterInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
            }
        }

        [Test]
        [Description("Page -> Element -> Frame Attribute -> Collection of Elements")]
        public void Should_Support_Interaction_Page_Element_FrameAttribute_CollectionOfElements()
        {
            //GIVEN
            var expectedTextContent = new List<string>()
            {
                Const.Table2Rows[0].Cells![0].Text!,
                Const.Table2Rows[0].Cells![1].Text!,
                Const.Table2Rows[1].Cells![0].Text!,
                Const.Table2Rows[1].Cells![1].Text!,
            };

            //WHEN
            var actualNonFrameButtonTextBeforeInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();
            var actualTextContent = _framesTestPageService._page.TableWithCellsWithFrames.TopLevelFrame.Cells.Select(x => x.GetTextContent()).ToList();
            var actualNonFrameButtonTextAfterInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualNonFrameButtonTextBeforeInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
                actualTextContent.Should().BeEquivalentTo(expectedTextContent);
                actualNonFrameButtonTextAfterInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
            }
        }

        [Test]
        [Description("Page -> Element -> Frame Attribute -> Element -> Collection of Elements -> Collection of Elements")]
        public void Should_Support_Interaction_Page_Element_FrameAttribute_Element_CollectionOfElements_CollectionOfElements()
        {
            //GIVEN
            var expectedTextContent = new List<string>()
            {
                Const.Table2Rows[0].Cells![0].Text!,
                Const.Table2Rows[0].Cells![1].Text!,
                Const.Table2Rows[1].Cells![0].Text!,
                Const.Table2Rows[1].Cells![1].Text!,
            };

            //WHEN
            var actualNonFrameButtonTextBeforeInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();
            var actualTextContent = _framesTestPageService._page.TableWithCellsWithFrames.TopLevelFrame.Table2.Rows.SelectMany(x => x.Cells.Select(y => y.GetTextContent())).ToList();
            var actualNonFrameButtonTextAfterInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualNonFrameButtonTextBeforeInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
                actualTextContent.Should().BeEquivalentTo(expectedTextContent);
                actualNonFrameButtonTextAfterInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
            }
        }

        [Test]
        [Description("Page -> Element -> Frame Attribute -> Frame Attribute -> Element")]
        public void Should_Support_Interaction_Page_Element_FrameAttribute_FrameAttribute_Element()
        {
            //WHEN
            var actualNonFrameButtonTextBeforeInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();
            var actualFrameButtonText = _framesTestPageService._page.TableWithCellsWithFrames.TopLevelFrame.FrameInsideFrame.ButtonFrameInsideFrame.GetTextContent();
            var actualNonFrameButtonTextAfterInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualNonFrameButtonTextBeforeInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
                actualFrameButtonText.Should().Be(Const.ButtonFrameInsideFrame);
                actualNonFrameButtonTextAfterInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
            }
        }

        [Test]
        [Description("A) Page -> Element -> Frame Attribute -> Frame Attribute -> Collection of Elements -> Collection of Elements; B) Page -> Frame Attribute -> Element -> Collection of Elements")]
        public void Should_Support_2_Consequent_Interactions_A_Page_Element_FrameAttribute_FrameAttribute_CollectionOfElements_CollectionOfElements_B_Page_FrameAttribute_Element_CollectionOfElements()
        {
            //GIVEN
            var expectedTextContent = new List<string>()
            {
                Const.Table2Rows[0].Cells![0].Text!,
                Const.Table2Rows[0].Cells![1].Text!,
                Const.Table2Rows[1].Cells![0].Text!,
                Const.Table2Rows[1].Cells![1].Text!,
            };

            //WHEN
            var actualNonFrameButtonTextBeforeInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();
            var actualTextContentOfFrameInsideFrameTable = _framesTestPageService._page.TableWithCellsWithFrames.TopLevelFrame.FrameInsideFrame.Rows.SelectMany(x => x.Cells.Select(y => y.GetTextContent())).ToList();
            var actualTextContentOfTopLevelFrameTable = _framesTestPageService._page.TopLevelFrame.Table2.Rows.SelectMany(x => x.Cells.Select(y => y.GetTextContent())).ToList();
            var actualNonFrameButtonTextAfterInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualNonFrameButtonTextBeforeInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
                actualTextContentOfFrameInsideFrameTable.Should().BeEquivalentTo(expectedTextContent);
                actualTextContentOfTopLevelFrameTable.Should().BeEquivalentTo(expectedTextContent);
                actualNonFrameButtonTextAfterInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
            }
        }

        [Test]
        [Description("A) Page -> Element -> Frame Attribute -> Frame Attribute -> Element -> Collection of Elements -> Collection of Elements; B) Page -> Frame Attribute -> Element-> Collection of Elements;")]
        public void Should_Support_2_Consequent_Interactions_A_Page_Element_FrameAttribute_FrameAttribute_Element_CollectionOfElements_CollectionOfElements_B_Page_FrameAttribute_Element_CollectionOfElements()
        {
            //GIVEN
            var expectedTextContent = new List<string>()
            {
                Const.Table2Rows[0].Cells![0].Text!,
                Const.Table2Rows[0].Cells![1].Text!,
                Const.Table2Rows[1].Cells![0].Text!,
                Const.Table2Rows[1].Cells![1].Text!,
            };

            //WHEN
            var actualNonFrameButtonTextBeforeInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();
            var actualTextContentOfFrameInsideFrameTable = _framesTestPageService._page.TableWithCellsWithFrames.TopLevelFrame.FrameInsideFrame.Table2.Rows.SelectMany(x => x.Cells.Select(y => y.GetTextContent())).ToList();
            var actualTextContentOfTopLevelFrameTable = _framesTestPageService._page.TopLevelFrame.Table2.Rows.SelectMany(x => x.Cells.Select(y => y.GetTextContent())).ToList();
            var actualNonFrameButtonTextAfterInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualNonFrameButtonTextBeforeInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
                actualTextContentOfFrameInsideFrameTable.Should().BeEquivalentTo(expectedTextContent);
                actualTextContentOfTopLevelFrameTable.Should().BeEquivalentTo(expectedTextContent);
                actualNonFrameButtonTextAfterInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
            }
        }


        [Test]
        [Description("Page -> Element -> Collection of Elements [Index] -> Collection of Elements [Index] -> Frame Attribute -> Element")]
        public void Should_Support_Interaction_Page_Element_CollectionOfElementsIndex_CollectionOfElementsIndex_FrameAttribute_Element()
        {
            //WHEN
            var actualNonFrameButtonTextBeforeInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();
            var actualFrameButtonText = _framesTestPageService._page.TableWithCellsWithFrames.RowsWithCellsWithFrames[1].CellsWithFrames[1].TopLevelFrame.ButtonInsideTopLevel0Frame.GetTextContent();
            var actualNonFrameButtonTextAfterInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualNonFrameButtonTextBeforeInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
                actualFrameButtonText.Should().Be(Const.TextContentOfButtonInsideTopLevel0FrameA);
                actualNonFrameButtonTextAfterInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
            }
        }

        [Test]
        [Description("Page -> Collection Of Elements With Frames Attributes -> Element")]
        public void Should_Support_Interaction_Page_CollectionOfElementsWithFramesAttributes_Element()
        {
            //GIVEN
            var expectedTextContent = new List<string>()
            {
                Const.TextContentOfButtonInsideTopLevel0FrameA,
                Const.TextContentOfButtonInsideTopLevel0FrameB
            };

            //WHEN
            var actualNonFrameButtonTextBeforeInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();
            var actualTextContent = _framesTestPageService._page.TopLevelFramesList.Select(x => x.ButtonInsideTopLevel0Frame.GetTextContent()).ToList();
            var actualNonFrameButtonTextAfterInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualNonFrameButtonTextBeforeInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
                actualTextContent.Should().BeEquivalentTo(expectedTextContent);
                actualNonFrameButtonTextAfterInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
            }
        }

        [Test]
        [Description("Page -> Collection Of Elements With Frames Attributes -> Collection Of Elements")]
        public void Should_Support_Interaction_Page_CollectionOfElementsWithFramesAttributes_CollectionOfElements()
        {
            //GIVEN
            var expectedTextContent = new List<string>()
            {
                Const.Table2Rows[0].Cells![0].Text!,
                Const.Table2Rows[0].Cells![1].Text!,
                Const.Table2Rows[1].Cells![0].Text!,
                Const.Table2Rows[1].Cells![1].Text!,

                Const.Table2BRows[0].Cells![0].Text!,
                Const.Table2BRows[0].Cells![1].Text!,
                Const.Table2BRows[1].Cells![0].Text!,
                Const.Table2BRows[1].Cells![1].Text!,
            };

            //WHEN
            var actualNonFrameButtonTextBeforeInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();
            var actualTextContent = _framesTestPageService._page.TopLevelFramesList.SelectMany(x => x.Cells).Select(x => x.GetTextContent()).ToList();
            var actualNonFrameButtonTextAfterInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualNonFrameButtonTextBeforeInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
                actualTextContent.Should().BeEquivalentTo(expectedTextContent);
                actualNonFrameButtonTextAfterInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
            }
        }

        [Test]
        [Description("Page -> Collection Of Elements With Frames Attributes -> Element -> Collection of Elements -> Collection of Elements")]
        public void Should_Support_Interaction_Page_CollectionOfElementsWithFramesAttributes_Element_CollectionOfElements_CollectionOfElements()
        {
            //GIVEN
            var expectedTextContent = new List<string>()
            {
                Const.Table2Rows[0].Cells![0].Text!,
                Const.Table2Rows[0].Cells![1].Text!,
                Const.Table2Rows[1].Cells![0].Text!,
                Const.Table2Rows[1].Cells![1].Text!,

                Const.Table2BRows[0].Cells![0].Text!,
                Const.Table2BRows[0].Cells![1].Text!,
                Const.Table2BRows[1].Cells![0].Text!,
                Const.Table2BRows[1].Cells![1].Text!,
            };

            //WHEN
            var actualNonFrameButtonTextBeforeInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();
            var actualTextContent = _framesTestPageService._page.TopLevelFramesList.Select(x => x.Table2).SelectMany(x => x.Rows).SelectMany(x => x.Cells.Select(y => y.GetTextContent())).ToList();
            var actualNonFrameButtonTextAfterInteractionWithFrame = _framesTestPageService._page.ButtonNotInFrame.GetTextContent();

            //THEN
            using (new AssertionScope())
            {
                actualNonFrameButtonTextBeforeInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
                actualTextContent.Should().BeEquivalentTo(expectedTextContent);
                actualNonFrameButtonTextAfterInteractionWithFrame.Should().Be(Const.ButtonNotInFrame);
            }
        }
    }
}

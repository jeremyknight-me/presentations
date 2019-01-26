using ClassLibrary.Presentation.ViewContracts;

namespace UnitTest.Presentation.MockViews
{
    internal sealed class MockCompanyDetailView : ICompanyDetailView
    {
        public MockCompanyDetailView()
        {
            this.CanUserDelete = false;
        }

        public int CompanyIdField { get; set; }
        public string NameField { get; set; }
        public bool CanUserDelete { get; set; }
    }
}

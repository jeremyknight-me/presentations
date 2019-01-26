using System.Collections.Generic;
using System.Linq;
using ClassLibrary.Objects;
using ClassLibrary.Presentation.ViewContracts;

namespace UnitTest.Presentation.MockViews
{
    internal sealed class MockPersonListView : IPersonListView
    {
        private List<Company> companyFilterFieldItems; 

        public MockPersonListView()
        {
            this.companyFilterFieldItems = new List<Company>();
        }

        public int? SelectedCompanyIdField { get; set; }

        public IEnumerable<Company> CompanyFilterFieldItems
        {
            get { return this.companyFilterFieldItems; }
            set { this.companyFilterFieldItems = value.ToList(); }
        }
    }
}

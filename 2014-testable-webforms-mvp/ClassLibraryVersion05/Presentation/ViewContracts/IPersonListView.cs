using System.Collections.Generic;
using ClassLibrary.Objects;

namespace ClassLibrary.Presentation.ViewContracts
{
    public interface IPersonListView
    {
        int? SelectedCompanyIdField { get; set; }

        IEnumerable<Company> CompanyFilterFieldItems { set; }
    }
}

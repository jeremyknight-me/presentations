namespace ClassLibrary.Presentation.ViewContracts
{
    public interface ICompanyDetailView
    {
        int CompanyIdField { get; set; }

        string NameField { get; set; }

        bool CanUserDelete { set; }
    }
}

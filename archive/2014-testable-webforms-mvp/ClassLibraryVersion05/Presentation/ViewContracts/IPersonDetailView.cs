using System;
using System.Collections.Generic;
using ClassLibrary.Objects;

namespace ClassLibrary.Presentation.ViewContracts
{
    public interface IPersonDetailView
    {
        int PersonIdField { get; set; }

        string FirstNameField { get; set; }

        string LastNameField { get; set; }

        string EmailAddressField { get; set; }

        string PhoneNumberField { get; set; }

        string NotesField { get; set; }

        DateTime? BirthdayField { get; set; }

        int? CompanyIdField { get; set; }

        IEnumerable<Company> CompanyFieldItems { set; }

        bool CanUserDelete { set; }
    }
}

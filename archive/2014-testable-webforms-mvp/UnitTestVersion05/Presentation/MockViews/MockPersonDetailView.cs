using System;
using System.Collections.Generic;
using System.Linq;
using ClassLibrary.Objects;
using ClassLibrary.Presentation.ViewContracts;

namespace UnitTest.Presentation.MockViews
{
    internal sealed class MockPersonDetailView : IPersonDetailView
    {
        private List<Company> companyFieldItems; 

        public MockPersonDetailView()
        {
            this.companyFieldItems = new List<Company>();
            this.CanUserDelete = false;
        }

        public int PersonIdField { get; set; }

        public string FirstNameField { get; set; }
        
        public string LastNameField { get; set; }
        
        public string EmailAddressField { get; set; }
        
        public string PhoneNumberField { get; set; }
        
        public string NotesField { get; set; }
        
        public DateTime? BirthdayField { get; set; }
        
        public int? CompanyIdField { get; set; }

        public IEnumerable<Company> CompanyFieldItems
        {
            get { return this.companyFieldItems; }
            set { this.companyFieldItems = value.ToList(); }
        }
        
        public bool CanUserDelete { get; set; }
    }
}

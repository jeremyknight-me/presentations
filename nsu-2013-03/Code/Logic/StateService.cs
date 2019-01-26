using System.Collections.Generic;
using System.Linq;
using WebApplication.Code.Objects;

namespace WebApplication.Code.Logic
{
    public class StateService
    {
        public IEnumerable<ListItemData> GetAll()
        {
            // DONE FOR DEMO PURPOSES - REAL WORLD APPLICATION WOULD PULL FROM DATA STORAGE!!
            var list = new List<ListItemData>
                {
                    new ListItemData { Display = "Alaska", Value = "AK" },
                    new ListItemData { Display = "Alabama", Value = "AL" },
                    new ListItemData { Display = "Arkansas", Value = "AR" },
                    new ListItemData { Display = "Arizona", Value = "AZ" },
                    new ListItemData { Display = "California", Value = "CA" },
                    new ListItemData { Display = "Colorado", Value = "CO" },
                    new ListItemData { Display = "Connecticut", Value = "CT" },
                    new ListItemData { Display = "District of Columbia", Value = "DC" },
                    new ListItemData { Display = "Delaware", Value = "DE" },
                    new ListItemData { Display = "Florida", Value = "FL" },
                    new ListItemData { Display = "Georgia", Value = "GA" },
                    new ListItemData { Display = "Hawaii", Value = "HI" },
                    new ListItemData { Display = "Iowa", Value = "IA" },
                    new ListItemData { Display = "Idaho", Value = "ID" },
                    new ListItemData { Display = "Illinois", Value = "IL" },
                    new ListItemData { Display = "Indiana", Value = "IN" },
                    new ListItemData { Display = "Kansas", Value = "KS" },
                    new ListItemData { Display = "Kentucky", Value = "KY" },
                    new ListItemData { Display = "Louisiana", Value = "LA" },
                    new ListItemData { Display = "Massachusetts", Value = "MA" }, 
                    new ListItemData { Display = "Maryland", Value = "MD" },
                    new ListItemData { Display = "Maine", Value = "ME" },
                    new ListItemData { Display = "Michigan", Value = "MI" },
                    new ListItemData { Display = "Minnesota", Value = "MN" },
                    new ListItemData { Display = "Missouri", Value = "MO" },
                    new ListItemData { Display = "Mississippi", Value = "MS" },
                    new ListItemData { Display = "Montana", Value = "MT" },
                    new ListItemData { Display = "North Carolina", Value = "NC" },
                    new ListItemData { Display = "North Dakota", Value = "ND" },
                    new ListItemData { Display = "Nebraska", Value = "NE" },
                    new ListItemData { Display = "New Hampshire", Value = "NH" },
                    new ListItemData { Display = "New Jersey", Value = "NJ" },
                    new ListItemData { Display = "New Mexico", Value = "NM" },
                    new ListItemData { Display = "Nevada", Value = "NV" },
                    new ListItemData { Display = "New York", Value = "NY" },
                    new ListItemData { Display = "Ohio", Value = "OH" },
                    new ListItemData { Display = "Oklahoma", Value = "OK" }, 
                    new ListItemData { Display = "Oregon", Value = "OR" },
                    new ListItemData { Display = "Pennsylvania", Value = "PA" },
                    new ListItemData { Display = "Rhode Island", Value = "RI" },
                    new ListItemData { Display = "South Carolina", Value = "SC" },
                    new ListItemData { Display = "South Dakota", Value = "SD" },
                    new ListItemData { Display = "Tennessee", Value = "TN" },
                    new ListItemData { Display = "Texas", Value = "TX" },
                    new ListItemData { Display = "Utah", Value = "UT" },
                    new ListItemData { Display = "Virginia", Value = "VA" },
                    new ListItemData { Display = "Vermont", Value = "VT" },
                    new ListItemData { Display = "Washington", Value = "WA" },
                    new ListItemData { Display = "Wisconsin", Value = "WI" },
                    new ListItemData { Display = "West Virginia", Value = "WV" },
                    new ListItemData { Display = "Wyoming", Value = "WY" }
                };

            return list.OrderBy(x => x.Display).ToList();
        }
    }
}
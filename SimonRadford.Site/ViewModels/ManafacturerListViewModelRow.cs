namespace SimonRadford.Site.ViewModels
{
    public class ManafacturerListViewModelRow
    {
        public ManafacturerListViewModelRow(int manafacturerId, string manafacturerName)
        {
            ManafacturerId = manafacturerId;
            ManafacturerName = manafacturerName;
        }

        public ManafacturerListViewModelRow()
        {

        }

        public int ManafacturerId { get; set; }
        public string ManafacturerName { get; set; }

    }
}
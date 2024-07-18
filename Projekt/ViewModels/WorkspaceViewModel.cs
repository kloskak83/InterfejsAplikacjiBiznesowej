using System;
using System.Windows.Input;
using Firma.Helpers;

namespace Firma.ViewModels
{
    // view model do odziedziczenia przez view modele reprezentujące zakładki
    // każda zakładka będzie dziedziczyć po WorkspaceViewModel
    public class WorkspaceViewModel : BaseViewModel
    {
        // każda zakładka ma nazwę i przycisk do zamknięcia

        #region Pola i właściwości

        public string DisplayName { get; set; } // nazwa zakładki
        private BaseCommand _CloseCommand;

        public ICommand CloseCommand
        {
            get
            {
                if (_CloseCommand == null)
                {
                    _CloseCommand = new BaseCommand(OnRequestClose); // komenda do zamykania zakładki
                }

                return _CloseCommand;
            }
        }

        #endregion

        #region RequestClose [event]

        public event EventHandler RequestClose;

        private void OnRequestClose()
        {
            EventHandler handler = RequestClose;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}
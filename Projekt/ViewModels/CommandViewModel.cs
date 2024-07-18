using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Firma.ViewModels
{
    //Każdy link z menu po lewej stronie jest CommandViewModelem 
    public class CommandViewModel : BaseViewModel
    {
        //Kazdy commandviemodel (czyli link w menu po lewej stronie) ma nazwe i komende ktora wywoluje akcje otworzenia zakladki
        #region Properties
        public string DisplayName { get; set; }
        public ICommand Command { get; set; }
        #endregion

        #region Konsturktor
        public CommandViewModel(string displayName,ICommand command)
        {
            DisplayName = displayName;
            Command = command;
        }
        #endregion
    }
}

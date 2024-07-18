using Firma.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.ViewModels
{
    public class ListaKontrahentowViewModel : WorkspaceViewModel //bo wszystkie zakladki dziedzicza po WorkspaceViewModel
    {
        public ListaKontrahentowViewModel()
        {
            base.DisplayName = "Lista kontrahentów";
        }
    }
}

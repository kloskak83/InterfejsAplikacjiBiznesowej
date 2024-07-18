using Firma.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using Projekt.ViewModels;

namespace Firma.ViewModels
{
    public  class MainWindowViewModel : BaseViewModel
    {
        #region CommandsMenu
        //Ta komenda zostaje podpięta pod pasek narzędzi i menugorne
        public ICommand ListaTowarowCommand
        {
            get
            {
                //Komenda wywoluje funkcję create towar
                return new BaseCommand(ListaTowarowCmd);
            }
        }
        public ICommand DodajFaktureCommand
        {
            get
            {
                //Komenda wywoluje funkcję create towar
                return new BaseCommand(DodajFaktureCmd);
            }
        }
        public ICommand ListaFakturCommand
        {
            get
            {
                //Komenda wywoluje funkcję create towar
                return new BaseCommand(ListaFakturCmd);
            }
        }
        public ICommand DodajKontrahentaCommand
        {
            get
            {
                //Komenda wywoluje funkcję create towar
                return new BaseCommand(DodajKontrahentaCmd);
            }
        }
        public ICommand DodajTowarCommand
        {
            get
            {
                //Komenda wywoluje funkcję create towar
                return new BaseCommand(DodajTowarCmd);
            }
        }


        public ICommand ListaKontrahentowCommand
        {
            get
            {
                //Komenda wywoluje funkcję create towar
                return new BaseCommand(ListaKontrahentowCmd);
            }
        }
        #endregion

        //Okno glowne zawiera kolekcje linkow (commandViewModeli) oraz kolekcje zakladek (....
        #region Commands
        private ReadOnlyCollection<CommandViewModel> _Commands;
        public ReadOnlyCollection<CommandViewModel> Commands 
        {
            get 
            {
                if(_Commands == null)
                {
                    //Tworze liste linkow z lewje strony, wypelniam ja 
                    List<CommandViewModel> cmds = CreateCommands(); //lista linkow utworzy sie za pomoco funkcji createcomands
                    _Commands = new ReadOnlyCollection<CommandViewModel>(cmds);
                }
                return _Commands;
            
            }
        }
        private List<CommandViewModel> CreateCommands()
        {
            //Tworze liste commandview modeli
            return new List<CommandViewModel> 
            { 
                //Tu decyduje jakie linki beda po lewej stronie, nazwe tych linkow i jakie funkcje wywoluja
                new CommandViewModel("Dodaj kontrahenta",new BaseCommand(DodajKontrahentaCmd)),
                new CommandViewModel("Lista kontrahentow",new BaseCommand(ListaKontrahentowCmd)),
                new CommandViewModel("Dodaj towar",new BaseCommand(DodajTowarCmd)),
                new CommandViewModel("Lista towarów",new BaseCommand(ListaTowarowCmd)),
                new CommandViewModel("Dodaj fakture",new BaseCommand(DodajFaktureCmd)),
                new CommandViewModel("Lista faktur",new BaseCommand(ListaFakturCmd)),

            };
        }


        #endregion

        #region Workspaces
        //To jest kolekcja zakladek
        private ObservableCollection<WorkspaceViewModel> _Workspaces;
        public ObservableCollection<WorkspaceViewModel> Workspaces 
        { 
            get 
            { 
                if(_Workspaces == null)
                {
                    _Workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _Workspaces.CollectionChanged += this.onWorkspacesChanged;
                }
                return _Workspaces;
            } 
        }
        #endregion

        
        #region Zakładki
        private void onWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.onWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.onWorkspaceRequestClose;
        }
        private void onWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            //workspace.Dispos();
            this.Workspaces.Remove(workspace);
        }
        #endregion

        #region Funkcje wywolujace zakladki
        private void ListaTowarowCmd()
        {
            //Najpierw sprawdzamy czy zakladka wyswietlajaca wszystkie towary juz jest w kolekci zakladek
            ListaTowarowViewModel workspace = Workspaces.FirstOrDefault(vm => vm is ListaTowarowViewModel)
                 as ListaTowarowViewModel;
            //Jezeli takiej zakladki brak
            if (workspace == null)
            {
                //To tworze nowa
                workspace = new ListaTowarowViewModel();
                //i dodaje ja do kolekcji zakaldek
                Workspaces.Add(workspace);
            }
            // Istniejaca zkaladke lub nowa ustawiam na aktywna
            SetActiveWorkspace(workspace);
        }

        private void DodajFaktureCmd()
        {
            //Najpierw tworze nowy WorkSpaces (nowy towar)
            DodajFaktureViewModel workspaces = new DodajFaktureViewModel();
            //Utworzony workspace dodaje do kolekcji workspacow (zakladek)
            Workspaces.Add(workspaces);
            //Wlaczamy aktywnosc dodanej zakladki
            SetActiveWorkspace(workspaces);
        }
        private void ListaFakturCmd()
        {
            //Najpierw sprawdzamy czy zakladka wyswietlajaca wszystkie towary juz jest w kolekci zakladek
            ListaFakturViewModel workspace = Workspaces.FirstOrDefault(vm => vm is ListaFakturViewModel)
                 as ListaFakturViewModel;
            //Jezeli takiej zakladki brak
            if (workspace == null)
            {
                //To tworze nowa
                workspace = new ListaFakturViewModel();
                //i dodaje ja do kolekcji zakaldek
                Workspaces.Add(workspace);
            }
            // Istniejaca zkaladke lub nowa ustawiam na aktywna
            SetActiveWorkspace(workspace);
        }

        private void DodajTowarCmd()
        {
            //Najpierw tworze nowy WorkSpaces (nowy towar)
            DodajTowarViewModel workspaces = new DodajTowarViewModel();
            //Utworzony workspace dodaje do kolekcji workspacow (zakladek)
            Workspaces.Add(workspaces);
            //Wlaczamy aktywnosc dodanej zakladki
            SetActiveWorkspace(workspaces);
        }

        private void DodajKontrahentaCmd()
        {
            //Najpierw tworze nowy WorkSpaces (nowy towar)
            DodajKontrahentaViewModel workspaces = new DodajKontrahentaViewModel();
            //Utworzony workspace dodaje do kolekcji workspacow (zakladek)
            Workspaces.Add(workspaces);
            //Wlaczamy aktywnosc dodanej zakladki
            SetActiveWorkspace(workspaces);

        }

        //Przy tworzeniu towaru za każdym razem tworzę nową zakladke,
        //Przy wyswietlaniu towaru najpierw sprawdzam czy zakladka wyswietlajaca wszystkie towary
        //Juz jest otwarta, jezeli nie to ja tworze, jezeli tak to przywracam jej widocznosc
        private void ListaKontrahentowCmd()
        {
            //Najpierw sprawdzamy czy zakladka wyswietlajaca wszystkie towary juz jest w kolekci zakladek
           ListaKontrahentowViewModel workspace = Workspaces.FirstOrDefault(vm => vm is ListaKontrahentowViewModel) 
                as ListaKontrahentowViewModel;
            //Jezeli takiej zakladki brak
            if(workspace == null)
            {
                //To tworze nowa
                workspace = new ListaKontrahentowViewModel();
                //i dodaje ja do kolekcji zakaldek
                Workspaces.Add(workspace);
            }
            // Istniejaca zkaladke lub nowa ustawiam na aktywna
            SetActiveWorkspace(workspace);

        }
        //WERSJA REFERENCYJNA, DO WYWALENIA
    //    private void ShowAllPracownicy()
    //    {
    //        WszyscyPracownicyViewModel workspace = Workspaces.FirstOrDefault(vm => vm is WszyscyPracownicyViewModel)
    //as WszyscyPracownicyViewModel;
    //        //Jezeli takiej zakladki brak
    //        if (workspace == null)
    //        {
    //            //To tworze nowa
    //            workspace = new WszyscyPracownicyViewModel();
    //            //i dodaje ja do kolekcji zakaldek
    //            Workspaces.Add(workspace);
    //        }
    //        // Istniejaca zkaladke lub nowa ustawiam na aktywna
    //        SetActiveWorkspace(workspace);
    //    }
        private void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
            {
                collectionView.MoveCurrentTo(workspace);
            }
        }
        #endregion





    }
}

using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using FreePcName.Model;

namespace FreePcName.ViewModel
{
    /// <summary>
    /// ViewModel главного окна.
    /// </summary>
    class MainVM : INotifyPropertyChanged
    {
        /// <summary>
        /// Событие изменения свойства.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Вызов события изменения свойства.
        /// </summary>
        /// <param name="prop"></param>
        public void RaisePropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        /// <summary>
        /// Строка поиска.
        /// </summary>
        private string searchString;

        /// <summary>
        /// Указывает, получены ли имена.
        /// </summary>
        private bool isNamesReceived;

        /// <summary>
        /// Первое имя пк.
        /// </summary>
        private string pcName;

        /// <summary>
        /// Первое имя ноутбука.
        /// </summary>
        private string noteName;

        /// <summary>
        /// Первое имя АРМ.
        /// </summary>
        private string armName;

        /// <summary>
        /// Свободные имена хостов.
        /// </summary>
        private Freenames freenames;


        /// <summary>
        /// Строка поиска.
        /// </summary>
        public string SearchString
        {
            get { return searchString; }
            set
            {
                searchString = value;
                RaisePropertyChanged(nameof(SearchString));
                DivisionsList.Filter = new Predicate<object>(DivisionFilter);
            }
        }

        /// <summary>
        /// Указывает, получены ли имена.
        /// </summary>
        public bool IsNamesReceived
        {
            get { return isNamesReceived; }
            set { isNamesReceived = value; RaisePropertyChanged(nameof(IsNamesReceived)); } }

        /// <summary>
        /// Свободные имена хостов в выбранном ОСП.
        /// </summary>
        public Freenames Freenames
        {
            get { return freenames; }
            set { freenames = value; RaisePropertyChanged(nameof(Freenames)); }
        }

        /// <summary>
        /// Первое имя пк.
        /// </summary>
        public string PcName
        {
            get { return pcName; }
            set { pcName = value; RaisePropertyChanged(nameof(PcName)); }
        }

        /// <summary>
        /// Первое имя ноутбука.
        /// </summary>
        public string NoteName
        {
            get { return noteName; }
            set { noteName = value; RaisePropertyChanged(nameof(NoteName)); }
        }

        /// <summary>
        /// Первое имя АРМ.
        /// </summary>
        public string ArmName
        {
            get { return armName; }
            set { armName = value; RaisePropertyChanged(nameof(ArmName)); }
        }

        /// <summary>
        /// Представление коллекции ОСП.
        /// </summary>
        public ListCollectionView DivisionsList { get; set; }

        /// <summary>
        /// Выбранное ОСП.
        /// </summary>
        public Division SelectedDivision { get; set; }

        /// <summary>
        /// Команда получения свободных имен.
        /// </summary>
        public ICommand GetFreenames => new RelayCommand(obj =>
        {
            Freenames = FreenamesSearcher.Searcher.GetFreenames(SelectedDivision.Prefix);
            PcName = Freenames.PcNames.FirstOrDefault();
            NoteName = Freenames.NotebookNames.FirstOrDefault();
            ArmName = Freenames.ArmNames.FirstOrDefault();
            IsNamesReceived = true;
        });

        /// <summary>
        /// Команда копирования имя устройства в буфер обмена.
        /// </summary>
        public ICommand Copy => new RelayCommand(obj =>
        {
            if (obj is string data)
            {
                Clipboard.SetData(DataFormats.Text, data);
            }
        }
        // Доступна когда имя не пустое.
        , (canEx) => !string.IsNullOrEmpty(PcName));

        /// <summary>
        /// Конструктор.
        /// </summary>
        public MainVM()
        {
            try
            {
                DivisionsList = new ListCollectionView(DivisionListBuilder.Builder.GetDivisionsList());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось получить список ОСП по причине:\n{ex.Message}");
            }
        }


        /// <summary>
        /// Фильтрует список ОСП.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool DivisionFilter(object obj)
        {
            if (obj is Division division)
            {
                return division.Name.ToUpper().Contains(SearchString.ToUpper())
                || division.City.ToUpper().Contains(SearchString.ToUpper())
                || division.Address.ToUpper().Contains(SearchString.ToUpper())
                || division.Prefix.ToUpper().Contains(SearchString.ToUpper());
            }
            else
            {
                return false;
            }
        }
    }
}
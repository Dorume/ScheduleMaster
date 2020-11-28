using ScheduleMaster.ViewModels.Base;

namespace ScheduleMaster.ViewModels
{
    internal class MainViewModel : ViewModel
    {
        #region Bindings

        #region Title

        private string _Title;

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #endregion
    }
}
namespace ScheduleMaster.ViewModels.Base
{
    internal class SubjectViewModel : ViewModel
    {
        #region Свойства

        #region Имя предмета
        private string _Name;

        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }
        #endregion

        #endregion
    }
}
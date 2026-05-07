using InteractiveCarousell_P.Resources.Localization;
using InteractiveCarousell_P.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace InteractiveCarousell_P.ViewModels
{
    public class MainVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        //public string Description => AppRes.Description;
        public string CarbonaraDescription => AppRes.CarbonaraDescription;
        public string LasagneDescription => AppRes.LasagneDescription;
        public string RisottoDescription => AppRes.RisottoDescription;
        public string PizzaDescription => AppRes.PizzaDescription;
        public string TiramisuDescription => AppRes.TiramisuDescription;
        public string EstonianButton => AppRes.EstonianButton;
        public string EnglishButton => AppRes.EnglishButton;

        public ICommand SetEnglishCommand { get; }
        public ICommand SetEstonianCommand { get; }

        public MainVM()
        {
            SetEnglishCommand = new Command(() => ChangeLanguage("en"));
            SetEstonianCommand = new Command(() => ChangeLanguage("ee"));

            LanguageService.LanguageChanged += OnLanguageChanged;
        }

        private void ChangeLanguage(string code)
        {
            LanguageService.ChangeLanguage(code);
        }

        private void OnLanguageChanged()
        {
            OnPropertyChanged(nameof(CarbonaraDescription));
            OnPropertyChanged(nameof(LasagneDescription));
            OnPropertyChanged(nameof(RisottoDescription));
            OnPropertyChanged(nameof(PizzaDescription));
            OnPropertyChanged(nameof(TiramisuDescription));
            OnPropertyChanged(nameof(EnglishButton));
            OnPropertyChanged(nameof(EstonianButton));
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

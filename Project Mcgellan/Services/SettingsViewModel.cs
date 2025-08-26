using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Project_Mcgellan.Services
{
    public class SettingsViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _errors = [];
        private Settings _settings;

        private bool _isApplyEnabled;
        public bool IsApplyEnabled
        {
            get => _isApplyEnabled;
            set
            {
                _isApplyEnabled = value;
                OnPropertyChanged(nameof(IsApplyEnabled));
            }
        }

        public SettingsViewModel(Settings settings)
        {
            _settings = new Settings
            {
                WindowWidth = settings.WindowWidth,
                WindowHeight = settings.WindowHeight,
                Theme = settings.Theme,
                FacilityName = settings.FacilityName,
                FacilityID = settings.FacilityID
            };
        }

        public Settings ToSettings() => _settings;

        public string Resolution
        {
            get => $"{_settings.WindowWidth}x{_settings.WindowHeight}";
            set
            {
                ValidateResolution(value);
                if (!_errors.ContainsKey(nameof(Resolution)))
                {
                    var parts = value.Split('x');
                    _settings.WindowWidth = int.Parse(parts[0]);
                    _settings.WindowHeight = int.Parse(parts[1]);
                    IsApplyEnabled = true; // Enable apply when resolution changes
                }
                OnPropertyChanged(nameof(Resolution));
            }
        }

        public string Theme
        {
            get => _settings.Theme;
            set
            {
                _settings.Theme = value;
                OnPropertyChanged(nameof(Theme));
                IsApplyEnabled = true; // Enable apply when theme changes
            }
        }

        public string FacilityName
        {
            get => _settings.FacilityName;
            set
            {
                _settings.FacilityName = value;
                OnPropertyChanged(nameof(FacilityName));
                IsApplyEnabled = true; // Enable apply when facility name changes
            }
        }

        public string FacilityId
        {
            get => _settings.FacilityID;
            set
            {
                _settings.FacilityID = value;
                OnPropertyChanged(nameof(FacilityId));
                IsApplyEnabled = true; // Enable apply when facility ID changes
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Validation / INotifyDataErrorInfo
        private void ValidateResolution(string value)
        {
            if (!Regex.IsMatch(value ?? "", @"^\d+x\d+$"))
                AddError(nameof(Resolution), "Resolution must be in format WIDTHxHEIGHT.");
            else
                ClearErrors(nameof(Resolution));
        }

        private void AddError(string propertyName, string error)
        {
            if (!_errors.ContainsKey(propertyName))
                _errors[propertyName] = [];

            if (!_errors[propertyName].Contains(error))
                _errors[propertyName].Add(error);

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void ClearErrors(string propertyName)
        {
            if (_errors.Remove(propertyName))
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => _errors.Count > 0;
        public IEnumerable GetErrors(string? propertyName) =>
            string.IsNullOrEmpty(propertyName) || _errors.ContainsKey(propertyName) ? Enumerable.Empty<string>() : _errors[propertyName];
        #endregion
    }
}
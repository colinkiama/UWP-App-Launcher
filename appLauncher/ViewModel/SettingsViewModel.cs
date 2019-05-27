﻿using appLauncher.Core;
using appLauncher.Model;
using appLauncher.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace appLauncher.ViewModel
{
    class SettingsViewModel:Notifier
    {
        private string _viewTitle;

        public string ViewTitle
        {
            get { return _viewTitle; }
            set { _viewTitle = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<SettingsItem> _settingsItems;

        public ObservableCollection<SettingsItem> SettingsItems
        {
            get { return _settingsItems; }
            set { _settingsItems = value;
                NotifyPropertyChanged();
            }
        }


        public SettingsViewModel()
        {
            GenerateDefaultItems();
        }

        private void GenerateDefaultItems()
        {
            List<SettingsItem> personalisationSettings = new List<SettingsItem>
            {
                new SettingsItem("Background", "Customise background type", "\uEB9F",null),
                new SettingsItem("Font", "Change font size", "\uE8D2", null)
            };



            List<SettingsItem> itemsToAdd = new List<SettingsItem>
            {
                new SettingsItem("Personalisation", "Customise background, Font Size", "\uE771", personalisationSettings),
                new SettingsItem("About", "Support Info, Open Source Licenses", "\uE897", null)
            };


            SettingsItems = new ObservableCollection<SettingsItem>(itemsToAdd);
        }


        internal void SettingsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = (SettingsItem)e.ClickedItem;
            SettingsItemArgs childrenArgs = clickedItem.NavigateToChild();
            if (childrenArgs.SettingsItems != null)
            {
                ViewTitle = childrenArgs.ViewTitle;
                SettingsItems = new ObservableCollection<SettingsItem>(childrenArgs.SettingsItems);
            }
            else
            {
                NavService.Instance.Navigate(childrenArgs.ViewType);
            }

        }


    }
}

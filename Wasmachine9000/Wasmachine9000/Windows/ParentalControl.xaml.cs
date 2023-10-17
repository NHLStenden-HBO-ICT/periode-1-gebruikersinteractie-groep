﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wasmachine9000.Windows
{
    /// <summary>
    /// Interaction logic for ParentalControl.xaml
    /// </summary>
    public partial class ParentalControl : Window
    {
        public bool PlaytimeToggle;
        public ParentalControl()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                SaveSettings();
                Helpers.OpenWindow(new MainWindow());
            }
        }

      

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
            Helpers.OpenWindow(new MainWindow());
            
        }

        private void SaveSettings()
        {
            CheckToggle();  
            App.GameState.MaxplayTime= (int)TimeSlider.Value;
            App.GameState.PlaytimeControl = PlaytimeToggle;
            
        }

        public void CheckToggle()
        {
            if((bool)ToggleButton.IsChecked) 
            {
                PlaytimeToggle = true;
            }
            else
            {
                PlaytimeToggle = false;
            }
        }
    }
}
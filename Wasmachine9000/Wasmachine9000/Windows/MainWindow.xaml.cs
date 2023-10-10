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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wasmachine9000
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
            // sets WPS height and width to the same height and width as the primary display
            this.Height = SystemParameters.FullPrimaryScreenHeight;
            this.Width = SystemParameters.FullPrimaryScreenWidth;

        }


        #region Event handlers

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Helpers.OpenWindow(new Instellingen());
        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            //GameWindow gameWindow= new GameWindow();
            //this.Close();
            //gameWindow.Show();
            
        }

        private void Winkel_Click(object sender, RoutedEventArgs e)
        {
            //open a new Winkel window and close the current window
            Helpers.OpenWindow(new Winkel());
        }

        private void Oudermenu_Click(object sender, RoutedEventArgs e)
        {
            Helpers.OpenWindow(new ParentPin());
        }

        private void Start_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void Start_MouseEnter(object sender, MouseEventArgs e)
        {

        }
        #endregion
        
        public void OpenWindow(Window window)
        { 
        
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            #region close application + messagebox
            // Check if the "Escape" key is pressed (Key.Escape)
            if (e.Key == Key.Escape)
            {
                // Display a confirmation dialog
                MessageBoxResult result = MessageBox.Show("Weet je zeker dat je de game wilt sluiten?", "Bevestig Afsluiten", MessageBoxButton.YesNo, MessageBoxImage.Question);

                // If the user clicks "Yes," close the application
                if (result == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }
            }
            #endregion
            
        }
    }
}
/*
 * Oliver Byl
 * April 17, 2020
 * Unit 2 Summative (Contact Form Editor)
 */
using System.Collections.Generic;
using System.Windows;

namespace Unit2Summative
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Contact> contacts = new List<Contact>();

        public MainWindow()
        {
            InitializeComponent();
            
            contacts = Contact.ReadFromFile();
            contactForm.ItemsSource = contacts;
        }

        /// <summary>
        /// Code to run when application window closes.
        /// Saves all contacts to file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Contact.SaveToFile(contacts);
        }

        /// <summary>
        /// Code to run when any "Show Age" button is clicked.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnShowAgeClick(object sender, RoutedEventArgs e)
        {
            // This gives the row of the form that is selected.
            int index = contactForm.SelectedIndex;

            /* Make sure that we have a contact to look up info for.
            *
            * This safety is usually triggered when clicking the show age
            * button on the blank row used to create new contacts.
            */
            if (index < 0 || index >= contacts.Count)
            {
                MessageBox.Show("There is no birthday data for this contact.");
                return;
            }

            contacts[contactForm.SelectedIndex].ShowAge();
        }
    }
}

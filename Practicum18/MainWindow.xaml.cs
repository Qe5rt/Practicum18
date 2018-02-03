using System;
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
using System.IO;

namespace Practicum18
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserManager myManager;
        User newUser;
        User currentUser;
        Contact newContact;
        public MainWindow()

        {
            InitializeComponent();
            myManager = new UserManager();
            myManager.LoadUsers();
            btnLogUit.Visibility = Visibility.Hidden;
            btnMaakContact.Visibility = Visibility.Hidden;

        }

        private void btnMaakAan_Click(object sender, RoutedEventArgs e)
        {

            newUser = new User() { Login = txtGebruiker.Text, Paswoord = txtPaswoord.Text };

            try
            {
                UserState current = myManager.AddUser(newUser);

                if (current == UserState.UserCreated) MessageBox.Show("New user added");
                else if (current == UserState.IllegalLogin) MessageBox.Show("Invalid login");
                else MessageBox.Show("User already exists");
            }
            catch (DirectoryNotFoundException dnfex)
            {
               MessageBox.Show(dnfex.Message);
            }
            catch (FileLoadException flex)
            {
                MessageBox.Show(flex.Message);
            }
            catch (FileNotFoundException fnfex)
            {
                MessageBox.Show(fnfex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ClearUserInfo();

        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                currentUser = myManager.Login(txtGebruiker.Text, txtPaswoord.Text);
                myManager.UserContacten(currentUser);
                lblWelkomUser.Content = "Welkom, " + currentUser.Login;
                lstContacten.ItemsSource = currentUser.Contacten;
                lstContacten.Items.Refresh();
                btnLogUit.Visibility = Visibility.Visible;
                btnMaakContact.Visibility = Visibility.Visible;
                btnLogIn.Visibility = Visibility.Hidden;
                btnMaakAan.Visibility = Visibility.Hidden;
            }
            catch (DirectoryNotFoundException dnfex)
            {
                MessageBox.Show(dnfex.Message);
            }
            catch (FileLoadException flex)
            {
                MessageBox.Show(flex.Message);
            }
            catch (FileNotFoundException fnfex)
            {
                MessageBox.Show(fnfex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

          ClearUserInfo();
        }

        private void btnMaakContact_Click(object sender, RoutedEventArgs e)
        {
            newContact = new Contact() { Naam = txtNaam.Text, TelefoonNummer = txtTelefoonNummer.Text, EmailAdress = txtEmail.Text };
            try
            {
                myManager.ContactToevoegen(currentUser, newContact);
                lstContacten.ItemsSource = currentUser.Contacten;
                lstContacten.Items.Refresh();
            }
            catch (DirectoryNotFoundException dnfex)
            {
                MessageBox.Show(dnfex.Message);
            }
            catch (FileLoadException flex)
            {
                MessageBox.Show(flex.Message);
            }
            catch (FileNotFoundException fnfex)
            {
                MessageBox.Show(fnfex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ClearContactInfo();

        }



        private void btnLogUit_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Do you want to log out?", "Log Out", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    lstContacten.ItemsSource = string.Empty;
                    lblWelkomUser.Content = string.Empty;
                    ClearUserInfo();
                    ClearContactInfo();
                    btnLogIn.Visibility = Visibility.Visible;
                    btnMaakAan.Visibility = Visibility.Visible;
                    btnLogUit.Visibility = Visibility.Hidden;
                    btnMaakContact.Visibility = Visibility.Hidden;

                    break;
                case MessageBoxResult.No:
                    lstContacten.Items.Refresh();
                    break;
                default:
                    lstContacten.Items.Refresh();
                    break;
            }
        }

        private void ClearUserInfo()
        {
            txtGebruiker.Clear();
            txtPaswoord.Clear();
        }

        private void ClearContactInfo()
        {
            txtEmail.Clear();
            txtNaam.Clear();
            txtTelefoonNummer.Clear();
        }
    }
}

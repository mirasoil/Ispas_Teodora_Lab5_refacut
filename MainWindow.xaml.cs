using System;
using System.Collections.Generic;
using System.Data;
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
using System.ComponentModel;

namespace Ispas_Teodora_Lab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    

    // pas20 lab5
    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }
    public partial class MainWindow : Window
    {
        //atribute care ne ajuta sa comunicam cu baza de date pas21 lab5
        ActionState action = ActionState.Nothing;
        PhoneNumbersDataSet phoneNumbersDataSet = new PhoneNumbersDataSet();
        PhoneNumbersDataSetTableAdapters.PhoneNumbersTableAdapter tblPhoneNumbersAdapter = new PhoneNumbersDataSetTableAdapters.PhoneNumbersTableAdapter();
        Binding txtPhoneNumberBinding = new Binding();
        Binding txtContractDateBinding = new Binding();
        Binding txtContractValueBinding = new Binding();
        Binding txtSubscriberBinding = new Binding();

        public MainWindow()
        {
            InitializeComponent();
            grdMain.DataContext = phoneNumbersDataSet.PhoneNumbers;
            txtPhoneNumberBinding.Path = new PropertyPath("Phonenum");
            txtSubscriberBinding.Path = new PropertyPath("Subscriber");
            txtContractDateBinding.Path = new PropertyPath("ContractDate");
            txtContractValueBinding.Path = new PropertyPath("ContractValue");

            txtPhoneNumber.SetBinding(TextBox.TextProperty, txtPhoneNumberBinding);
            txtSubscriber.SetBinding(TextBox.TextProperty, txtSubscriberBinding);
            txtContractDate.SetBinding(TextBox.TextProperty, txtContractDateBinding);
            txtContractValue.SetBinding(TextBox.TextProperty, txtContractValueBinding);
        }
        //pas 21  lab5
        private void lstPhonesLoad()
        {
            tblPhoneNumbersAdapter.Fill(phoneNumbersDataSet.PhoneNumbers);
        }

        //pas 22 lab 5 - eveniment handler
        private void grdMain_Loaded(object sender, RoutedEventArgs e)
        {
            lstPhonesLoad();
        }

        //pas 23 lab 5 - event handler buton Exit
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Close Application?", "Question", MessageBoxButton.YesNo,
           MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        //pas 24 lab 5 - event Loaded care incarca lista cu date
        private void frmMain_Loaded(object sender, RoutedEventArgs e)
        {
            PhoneNumbersDataSet phoneNumbersDataSet = ((PhoneNumbersDataSet)(this.FindResource("phoneNumbersDataSet")));
            System.Windows.Data.CollectionViewSource phoneNumbersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("phoneNumbersViewSource")));
            phoneNumbersViewSource.View.MoveCurrentToFirst();
        }

        //pas 25 lab 5 - event care permite adaugarea unei noi inregistrari in baza de date
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;

            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            lstPhones.IsEnabled = false;
            btnPrevious.IsEnabled = false;
            btnNext.IsEnabled = false;
            txtPhoneNumber.IsEnabled = true;
            txtSubscriber.IsEnabled = true;
            txtContractDate.IsEnabled = true;
            txtContractValue.IsEnabled = true;

            BindingOperations.ClearBinding(txtPhoneNumber, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtSubscriber, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtContractValue, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtContractDate, TextBox.TextProperty);

            txtPhoneNumber.Text = "";
            txtSubscriber.Text = "";
            txtContractDate.Text = "";
            txtContractValue.Text = "";
            Keyboard.Focus(txtPhoneNumber);
        }

        //pas 25 lab 5 - event care permite adaugarea unei noi inregistrari in baza de date
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            string tempPhonenum = txtPhoneNumber.Text.ToString();
            string tempSubscriber = txtSubscriber.Text.ToString();
            string tempContract_date = txtContractDate.Text.ToString();
            string tempContract_value = txtContractValue.Text.ToString();
            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            lstPhones.IsEnabled = false;
            btnPrevious.IsEnabled = false;
            btnNext.IsEnabled = false;
            txtPhoneNumber.IsEnabled = true;
            txtSubscriber.IsEnabled = true;
            txtContractDate.IsEnabled = true;
            txtContractValue.IsEnabled = true;
            BindingOperations.ClearBinding(txtPhoneNumber, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtSubscriber, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtContractDate, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtContractValue, TextBox.TextProperty);
            txtPhoneNumber.Text = tempPhonenum;
            txtSubscriber.Text = tempSubscriber;
            txtContractDate.Text = tempContract_date;
            txtContractValue.Text = tempContract_value;
            Keyboard.Focus(txtPhoneNumber);
        }

        //pas 27 lab 5 - handler ce permite stergerea datelor
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
            string tempPhonenum = txtPhoneNumber.Text.ToString();
            string tempSubscriber = txtSubscriber.Text.ToString();
            string tempContract_date = txtContractDate.Text.ToString();
            string tempContract_value = txtContractValue.Text.ToString();

            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            lstPhones.IsEnabled = false;
            btnPrevious.IsEnabled = false;
            btnNext.IsEnabled = false;

            BindingOperations.ClearBinding(txtPhoneNumber, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtSubscriber, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtContractDate, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtContractValue, TextBox.TextProperty);

            txtPhoneNumber.Text = tempPhonenum;
            txtSubscriber.Text = tempSubscriber;
            txtContractValue.Text = tempContract_date;
            txtContractDate.Text = tempContract_value;
        }

        //pas 28 lab 5 - handler care va anula operatiile in curs de desfasurare pe baza de date
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

            action = ActionState.Nothing;
            btnNew.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnSave.IsEnabled = false;
            btnCancel.IsEnabled = false;
            lstPhones.IsEnabled = true;
            btnPrevious.IsEnabled = true;

            btnNext.IsEnabled = true;
            txtPhoneNumber.IsEnabled = false;
            txtSubscriber.IsEnabled = false;
            txtContractDate.IsEnabled = false;
            txtContractValue.IsEnabled = false;

            txtPhoneNumber.SetBinding(TextBox.TextProperty, txtPhoneNumberBinding);
            txtSubscriber.SetBinding(TextBox.TextProperty, txtSubscriberBinding);

        }

        //pas 29 lab 5 - handler care va salva modificarile efectuate pe date in BD
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            if (action == ActionState.New)
            {
                try
                {
                    DataRow newRow = phoneNumbersDataSet.PhoneNumbers.NewRow();
                    newRow.BeginEdit();
                    newRow["Phonenum"] = txtPhoneNumber.Text.Trim();
                    newRow["Subscriber"] = txtSubscriber.Text.Trim();
                    newRow["Contract_value"] = txtContractValue.Text.Trim();
                    newRow["Contract_date"] = txtContractDate.Text.Trim();
                    newRow.EndEdit();
                    phoneNumbersDataSet.PhoneNumbers.Rows.Add(newRow);
                    tblPhoneNumbersAdapter.Update(phoneNumbersDataSet.PhoneNumbers);
                    phoneNumbersDataSet.AcceptChanges();
                }
                catch (DataException ex)
                {
                    phoneNumbersDataSet.RejectChanges();
                    MessageBox.Show(ex.Message);
                }
                btnNew.IsEnabled = true;
                btnEdit.IsEnabled = true;
                btnSave.IsEnabled = false;
                btnCancel.IsEnabled = false;
                lstPhones.IsEnabled = true;
                btnPrevious.IsEnabled = true;
                btnNext.IsEnabled = true;
                txtPhoneNumber.IsEnabled = false;
                txtContractDate.IsEnabled = false;
                txtSubscriber.IsEnabled = false;
                txtContractValue.IsEnabled = false;
            }
            else
            if (action == ActionState.Edit)
            {
                try
                {
                    DataRow editRow = phoneNumbersDataSet.PhoneNumbers.Rows[lstPhones.SelectedIndex];
                    editRow.BeginEdit();
                    editRow["Phonenum"] = txtPhoneNumber.Text.Trim();
                    editRow["Subscriber"] = txtSubscriber.Text.Trim();
                    editRow["ContractValue"] = txtContractValue.Text.Trim();
                    editRow["ContractDate"] = txtContractDate.Text.Trim();
                    editRow.EndEdit();
                    tblPhoneNumbersAdapter.Update(phoneNumbersDataSet.PhoneNumbers);
                    phoneNumbersDataSet.AcceptChanges();
                }
                catch (DataException ex)
                {
                    phoneNumbersDataSet.RejectChanges();
                    MessageBox.Show(ex.Message);
                }
                btnNew.IsEnabled = true;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnSave.IsEnabled = false;
                btnCancel.IsEnabled = false;
                lstPhones.IsEnabled = true;
                btnPrevious.IsEnabled = true;
                btnNext.IsEnabled = true;
                txtPhoneNumber.IsEnabled = false;
                txtContractDate.IsEnabled = false;
                txtContractValue.IsEnabled = false;
                txtSubscriber.IsEnabled = false;
                txtPhoneNumber.SetBinding(TextBox.TextProperty, txtPhoneNumberBinding);
                txtSubscriber.SetBinding(TextBox.TextProperty, txtSubscriberBinding);
                txtContractValue.SetBinding(TextBox.TextProperty, txtContractValueBinding);
                txtContractDate.SetBinding(TextBox.TextProperty, txtContractDateBinding);


            }
            else
                if (action == ActionState.Delete)
                {
                    try
                    {
                        DataRow deleterow = phoneNumbersDataSet.PhoneNumbers.Rows[lstPhones.SelectedIndex];
                        deleterow.Delete();

                        tblPhoneNumbersAdapter.Update(phoneNumbersDataSet.PhoneNumbers);
                        phoneNumbersDataSet.AcceptChanges();
                    }
                    catch (DataException ex)
                    {
                        phoneNumbersDataSet.RejectChanges(); MessageBox.Show(ex.Message);
                        MessageBox.Show(ex.Message);
                    }
                btnNew.IsEnabled = true;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnSave.IsEnabled = false;
                btnCancel.IsEnabled = false;
                lstPhones.IsEnabled = true;
                btnPrevious.IsEnabled = true;
                btnNext.IsEnabled = true;
                txtPhoneNumber.IsEnabled = false;
                txtSubscriber.IsEnabled = false;
                txtContractDate.IsEnabled = false;
                txtContractValue.IsEnabled = false;
                txtPhoneNumber.SetBinding(TextBox.TextProperty, txtPhoneNumberBinding);
                txtSubscriber.SetBinding(TextBox.TextProperty, txtSubscriberBinding);
                txtContractDate.SetBinding(TextBox.TextProperty, txtContractDateBinding);
                txtContractValue.SetBinding(TextBox.TextProperty, txtContractValueBinding);
            }
        }

        //pas 29 lab 5
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {

            ICollectionView navigationView = CollectionViewSource.GetDefaultView(phoneNumbersDataSet.PhoneNumbers);
            navigationView.MoveCurrentToPrevious();
        }

        //pas 30 lab 5
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView navigationView =
           CollectionViewSource.GetDefaultView(phoneNumbersDataSet.PhoneNumbers);
            navigationView.MoveCurrentToNext();
        }

        private void grdMainLoaded(object sender, RoutedEventArgs e)
        {
            lstPhonesLoad();
        }
    }
}

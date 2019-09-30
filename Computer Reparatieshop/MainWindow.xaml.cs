using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Microsoft.Win32;

namespace Computer_Reparatieshop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ObservableCollection<Reparatie> Planning = new ObservableCollection<Reparatie>();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var r = new Reparatie();
            r.Opdrachtgever = txtOpdrachtgever.Text;
            r.Opdracht = txtOpdracht.Text;
            r.Status = (StatusEnum)Enum.Parse(typeof(StatusEnum), lbxStatus.SelectedItem.ToString());
            r.Reparateur = cmbReparateur.Text;
            r.Deadline = dtpDeadline.SelectedDate.Value;

            Planning.Add(r);
            dg.ItemsSource = null;
            dg.ItemsSource = Planning;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lbxStatus.ItemsSource = Enum.GetValues(typeof(StatusEnum));
            cmbReparateur.ItemsSource = Enum.GetValues(typeof(Reparateurenum));

            dtpDeadline.SelectedDate = DateTime.Now;
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialoog = new SaveFileDialog();

            dialoog.ShowDialog();

            string csvData = "";

            foreach (Reparatie r in Planning)
            {
                csvData += string.Join(",", r.Opdrachtgever, r.Opdracht, r.Reparateur, r.Status, r.Deadline, r.Resterendetijd) + Environment.NewLine;
            }

            //File.WriteAllText("c:\\temp\\personen.csv", csvData);
            File.WriteAllText(dialoog.FileName, csvData);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialoog = new OpenFileDialog();
            dialoog.ShowDialog();

            string[] data = File.ReadAllLines(dialoog.FileName);

            foreach (string regel in data)
            {
                string[] velden = regel.Split(',');
                Reparatie r = new Reparatie();
                r.Opdrachtgever = velden[0];
                r.Opdracht = velden[1];
                r.Reparateur = velden[2];
                r.Status = (StatusEnum)Enum.Parse(typeof(StatusEnum), velden[3]);
                r.Deadline = DateTime.Parse(velden[4]);

                Planning.Add(r);
            }
        }

        private void ButtonM_Click(object sender, RoutedEventArgs e)
        {
            var query = from r in Planning
                        where r.Status == StatusEnum.Ontvangen
                        select r;
            List<Reparatie> results = query.ToList();

            dg.ItemsSource = results;
        }

        private void ButtonV_Click(object sender, RoutedEventArgs e)
        {
            var query = from r in Planning
                        where r.Status == StatusEnum.Uitvoering
                        select r;
            List<Reparatie> results = query.ToList();

            dg.ItemsSource = results;
        }

        private void ButtonP_Click(object sender, RoutedEventArgs e)
        {
            var query = from r in Planning
                        orderby r.Deadline
                        select r;
            List<Reparatie> results = query.ToList();

            dg.ItemsSource = results;
        }

        private void ButtonZ_Click(object sender, RoutedEventArgs e)
        {
            var query = from r in Planning
                        where r.Opdrachtgever.ToLower().Contains(txt.Text.ToLower())
                        || r.Opdracht.ToLower().Contains(txt.Text.ToLower())
                        select r;
        }

        private void Txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            var query = from r in Planning
                        where r.Opdrachtgever.ToLower().Contains(txt.Text.ToLower())
                        || r.Opdracht.ToLower().Contains(txt.Text.ToLower())
                        select r;
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            var query = from r in Planning
                        select new
                        {
                            r.Opdracht,
                            r.Opdrachtgever,
                            r.Deadline
                        };
            var results = query.ToList();

            dg.ItemsSource = results;
          
        }
                
    }
}

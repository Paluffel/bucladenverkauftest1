using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Packaging;
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

namespace Buchladen
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Liste für Bücher
        ObservableCollection<Buch> buchListe = new ObservableCollection<Buch>();
        ObservableCollection<Buch> buyList = new ObservableCollection<Buch>();
        ObservableCollection<Buch> searchList = new ObservableCollection<Buch>();
        ObservableCollection<Buch> buylst1 = new ObservableCollection<Buch>();
        private decimal gewinn = 0;
        private string cat = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        //Buch hinzufügen
        public void AddBook(string title, string author, string isbn, string price, string kathegorie)
        {
            buchListe.Add(new Buch
                { Title = title, Author = author, ISBN = isbn, Price = price, kathegorie = kathegorie });
            lst_view.ItemsSource = buchListe;
        }

        //Buch löschen
        public void DeleteBook()
        {
            foreach (Buch b in buchListe)
            {
                if (b == lst_view.SelectedItem)
                {
                    buchListe.Remove(b);
                    lst_view.ItemsSource = buchListe;
                    break;
                }
            }
        }

        //Buch suchen
        public void SearchBook(string name)
        {
            foreach (Buch b in buchListe)
            {
                searchList.Clear();
                if (cbo_search.Text != String.Empty && name != String.Empty)
                {
                    if (b.Title == name && b.kathegorie == cbo_search.Text)
                    {
                        //Gefundene Bücher in neue lst hinzufügen

                        searchList.Add(b);

                        //Listbox updaten
                        lst_sell.ItemsSource = searchList;
                    }
                }
                else if (cbo_search.Text != String.Empty)
                {
                    if (b.kathegorie == cbo_search.Text)
                    {
                        //Gefundene Bücher in neue lst hinzufügen

                        searchList.Add(b);

                        //Listbox updaten
                        lst_sell.ItemsSource = searchList;
                    }
                }
                else if (name != String.Empty)
                {
                    if (b.Title == name)
                    {
                        //Gefundene Bücher in neue lst hinzufügen

                        searchList.Add(b);

                        //Listbox updaten
                        lst_sell.ItemsSource = searchList;
                    }
                }
                else
                {
                    MessageBox.Show("Bitte geben Sie einen Suchbegriff ein!");
                }
            }
        }

        //methode buch Auswählen und zu kaufliste hinzufügen
        public void SelectBook(string isbn)
        {
            foreach (Buch b in buchListe)
            {
                if (b.ISBN == isbn)
                {
                    //Buch in neue Liste hinzufügen
                    buyList.Add(b);
                    lst_Selected.ItemsSource = buyList;
                }
            }
        }


        //methode zum kauf abschließen
        public void BuyBooks()
        {
            //Bücher aus Liste entfernen
            foreach (Buch b in buyList)
            {
                buchListe.Remove(b);
                decimal price;
                if (Decimal.TryParse(b.Price, out price))
                {
                    gewinn += price; // No need to cast to int
                }
                else
                {
                    MessageBox.Show("Fehler beim Preis, Konnte nicht hinzugefügt werden!");
                }
                buylst1.Add(b);
                lst_soled.ItemsSource = buylst1;
                txt_winnigs.Content = gewinn.ToString("F2") + "€"; 
            }

            //Listbox updaten
            lst_view.ItemsSource = buchListe;
            buyList.Clear();
        }

        private void BuchAnlegen_Click(object sender, RoutedEventArgs e)
        {
            AddBook(txt_title.Text, txt_author.Text, txt_isbn.Text, txt_price.Text, cmb_catigorie.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DeleteBook();
        }

        private void ButtonSuche_Click(object sender, RoutedEventArgs e)
        {
            SearchBook(txt_search.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Buch auswählen
            SelectBook(txt_isbn.Text);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cat = cmb_catigorie.Text;
        }

        private void ButtonKaufAbschließen_OnClick(object sender, RoutedEventArgs e)
        {
            BuyBooks();
        }
    }

    class Buch
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Price { get; set; }

        public string kathegorie { get; set; }
    }
}
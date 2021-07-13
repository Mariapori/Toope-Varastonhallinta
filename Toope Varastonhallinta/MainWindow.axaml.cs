using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Toope_Varastonhallinta.Mallit;

namespace Toope_Varastonhallinta
{
    public partial class MainWindow : Window
    {
        public ListBox Tuotelista = new ListBox();
        public ListBox Varastosaldot = new ListBox();

        public TextBox txtVarasto = new TextBox();
        public Button btnLuoVarasto = new Button();

        public TextBox txtTuote = new TextBox();
        public Button btnLuoTuote = new Button();

        public ComboBox cbVarastot = new ComboBox();
        public ComboBox cbTuotteet = new ComboBox();
        public TextBox txtMaara = new TextBox();
        public Button btnLisaaVarastoon = new Button();

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            Tuotelista = this.FindControl<ListBox>("Tuotelista");
            Varastosaldot = this.FindControl<ListBox>("Varastosaldot");

            Tuotelista.Items = App.DB.Tuotteet.ToList();
            Tuotelista.Tapped += Tuotelista_Tapped;

            txtVarasto = this.FindControl<TextBox>("txtVarasto");
            btnLuoVarasto = this.FindControl<Button>("btnLuoVarasto");
            btnLuoVarasto.Click += BtnLuoVarasto_Click;

            txtTuote = this.FindControl<TextBox>("txtTuote");
            btnLuoTuote = this.FindControl<Button>("btnLuoTuote");
            btnLuoTuote.Click += BtnLuoTuote_Click;

            cbVarastot = this.FindControl<ComboBox>("cbVarastot");
            cbTuotteet = this.FindControl<ComboBox>("cbTuotteet");
            txtMaara = this.FindControl<TextBox>("txtMaara");
            btnLisaaVarastoon = this.FindControl<Button>("btnLisaaVarastoon");
            btnLisaaVarastoon.Click += BtnLisaaVarastoon_Click;
            cbTuotteet.Items = App.DB.Tuotteet.ToList();
            cbVarastot.Items = App.DB.Varastot.ToList();

        }

        private void BtnLisaaVarastoon_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {

            if(cbVarastot.SelectedItem != null && cbTuotteet.SelectedItem != null)
            {
                double.TryParse(txtMaara.Text, out double maara);
                Kirjaus uusi = new Kirjaus();
                uusi.maara = maara;
                uusi.Varasto = (Varasto)cbVarastot.SelectedItem;
                uusi.Tuote = (Tuote)cbTuotteet.SelectedItem;

                App.DB.Kirjaukset.Add(uusi);
                App.DB.SaveChanges();

                cbTuotteet.SelectedItem = null;
                cbVarastot.SelectedItem = null;
                txtMaara.Text = "";

                cbVarastot.Items = App.DB.Varastot.ToList();
                Tuotelista.Items = App.DB.Tuotteet.ToList();
                cbTuotteet.Items = App.DB.Tuotteet.ToList();
                if(Tuotelista.SelectedItem != null)
                {
                    Tuotelista_Tapped(sender, e);
                }

            }

        }

        private void BtnLuoTuote_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (txtTuote.Text != "")
            {
                Tuote uusi = new Tuote();
                uusi.TuotteenNimi = txtTuote.Text;

                App.DB.Tuotteet.Add(uusi);
                App.DB.SaveChanges();
                txtTuote.Text = "";
                Tuotelista.Items = App.DB.Tuotteet.ToList();
                cbTuotteet.Items = App.DB.Tuotteet.ToList();
            }
        }

        private void BtnLuoVarasto_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if(txtVarasto.Text != "")
            {
                Varasto uusiVarasto = new Varasto();
                uusiVarasto.VarastonNimi = txtVarasto.Text;

                App.DB.Varastot.Add(uusiVarasto);
                App.DB.SaveChanges();
                txtVarasto.Text = "";
                cbVarastot.Items = App.DB.Varastot.ToList();
            }
        }

        private void Tuotelista_Tapped(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var valittu = (Tuote)Tuotelista.SelectedItem;
            if (valittu != null)
            {
                List<VarastoViewModel> lista = new List<VarastoViewModel>();
                var varastot = App.DB.Kirjaukset.Where(o => o.Tuote == valittu).AsEnumerable().GroupBy(o => o.Varasto);
                foreach (var item in varastot)
                {
                    lista.Add(new VarastoViewModel { VarastonNimi = item.Key.VarastonNimi, Saldo = item.Sum(o => o.maara) });
                }
                Varastosaldot.Items = lista.ToList();
            }
        }
    }
}

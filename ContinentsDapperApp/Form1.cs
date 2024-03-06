using System.Diagnostics;
using System.Reflection;
using ContinentsDapperApp.Classes;
using ContinentsDapperApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ContinentsDapperApp;

public partial class Form1 : Form
{
    private BindingSource _continentBindingSource = new(); 
    private BindingSource _countriesBindingSource = new();

    private DapperOperations operations = new();

    public Form1()
    {
        InitializeComponent();

        Shown += Form1_Shown;
    }

    private void Form1_Shown(object? sender, EventArgs e)
    {

        _continentBindingSource.DataSource = operations.Continents();
        ContinentsComboBox.DataSource = _continentBindingSource;
        _continentBindingSource.PositionChanged += _continentBindingSource_PositionChanged;
        _countriesBindingSource.DataSource = new List<Country>() { new() { Id = -1, CountryName = "Select a continent" } };
        CountriesComboBox.DataSource = _countriesBindingSource;

    }

    private void _countriesBindingSource_PositionChanged(object? sender, EventArgs e)
    {
        CapitalLabel.Text = "";
        FactLabel.Text = "";

        _countriesBindingSource.PositionChanged -= _countriesBindingSource_PositionChanged;
        var current = (Country)_countriesBindingSource.Current;
        if (current.Id > -1)
        {
            var information = operations.CountryInformation(current.Id);
            CapitalLabel.Text = current.CapitalName;
            FactLabel.Text = information!.Fact;
        }
        
        _countriesBindingSource.PositionChanged += _countriesBindingSource_PositionChanged;

    }

    private void _continentBindingSource_PositionChanged(object? sender, EventArgs e)
    {
        
        var continent = (Continent)_continentBindingSource.Current;

        if (continent.Id <= -1)
        {
            _countriesBindingSource.DataSource = new List<Country>() {new() {Id = -1, CountryName = "Select a continent" } };

            return;
        }

        _countriesBindingSource.DataSource = operations.Countries(continent.Id);
        CountriesComboBox.DataSource = _countriesBindingSource;
        CountriesComboBox.SelectedIndex = 0;


        _countriesBindingSource.PositionChanged -= _countriesBindingSource_PositionChanged;
        _countriesBindingSource.PositionChanged += _countriesBindingSource_PositionChanged;

    }
}

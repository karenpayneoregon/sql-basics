using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataGridViewSample.Models;

public class Book : INotifyPropertyChanged
{
    private int _id;
    private string _title;
    private decimal _price;
    private int _categoryId;
    private Categories _category;

    public int Id
    {
        get => _id;
        set
        {
            if (value == _id) return;
            _id = value;
            OnPropertyChanged();
        }
    }

    public string Title
    {
        get => _title;
        set
        {
            if (value == _title) return;
            _title = value;
            OnPropertyChanged();
        }
    }

    public decimal Price
    {
        get => _price;
        set
        {
            if (value == _price) return;
            _price = value;
            OnPropertyChanged();
        }
    }

    public int CategoryId
    {
        get => _categoryId;
        set
        {
            if (value == _categoryId) return;
            _categoryId = value;
            OnPropertyChanged();
        }
    }

    public Categories Category
    {
        get => _category;
        set
        {
            if (Equals(value, _category)) return;
            _category = value;
            OnPropertyChanged();
        }
    }

    public override string ToString() => Title;
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace TestApp.ViewModels
{
  public class ViewModel1 : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    string entryfield;
    public string EntryField
    {
      set { SetProperty(ref entryfield, value); }
      get { return entryfield; }
    }

    string edittedtext;
    public string Edittedtext
    {
      set { SetProperty(ref edittedtext, value); }
      get { return edittedtext; }
    }

    bool isEditing;
    public bool IsEditing
    {
      set { SetProperty(ref isEditing, value); }
      get { return isEditing; }
    }

    public ICommand EnableEntryCommand { private set; get; }
    public ICommand SubmitEntryCommand { private set; get; }

    bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
      if (Object.Equals(storage, value))
        return false;

      storage = value;
      OnPropertyChanged(propertyName);
      return true;
    }

    public ViewModel1()
    {
      EnableEntryCommand = new Command(
        execute: () =>
        {
          IsEditing = true;
          RefreshCanExecutes();
        },
        canExecute: () =>
        {
          return !IsEditing;
        }
        );

      SubmitEntryCommand = new Command(
      execute: () =>
      {
        IsEditing = false;
        RefreshCanExecutes();
        Edittedtext = EntryField;
        EntryField = "";
      },
      canExecute: () =>
      {
        return IsEditing;
      });
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    void RefreshCanExecutes()
    {
      (EnableEntryCommand as Command).ChangeCanExecute();
      (SubmitEntryCommand as Command).ChangeCanExecute();
    }
  }
}

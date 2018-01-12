using RadGridViewTest.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace RadGridViewTest
{
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            this.DataContext = viewModel;
        }
    }

    internal class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private const string all_chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const string some_chars = "ABCD";
        public List<TestDataModel> TestData { get; set; }

        public MainViewModel()
        {
            TestData = new List<TestDataModel>();
            for (int i = 0; i < 10000; i++)
            {
                TestData.Add(new TestDataModel()
                {
                    Id = i,
                    Name = RandomString(5, some_chars),
                    Description = RandomString(50, all_chars),
                });
            }

            OnPropertyChanged("TestData");
        }

        #region INotifyPropertyChanged Implemtation

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static Random random = new Random();
        public static string RandomString(int length, string chars)
        {
            return new string(Enumerable
                .Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
        }

        #endregion INotifyPropertyChanged Implemtation
    }

    internal class TestDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

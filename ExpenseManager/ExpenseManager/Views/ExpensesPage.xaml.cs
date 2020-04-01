using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpenseManager.Models;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpensesPage : TabbedPage    {
        public ExpensesPage()
        {
            InitializeComponent();
            Init();

        }

        public void Init()
        {
            BackgroundColor = Constants.backgroundColor;
            
        }
    }
}

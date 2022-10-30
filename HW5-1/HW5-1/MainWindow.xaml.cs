using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

namespace HW5_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Drink> drinks = new List<Drink>();
        List<OrderItem> order = new List<OrderItem>();
        string takeout;
        public MainWindow()
        {
            InitializeComponent();
            //新增飲料品項至drinks清單內
            AddNewDrink(drinks);

            //顯示所有飲料品項
            DisplayDrinks(drinks);
        }
        private void DisplayDrinks(List<Drink> drinks)
        {
            foreach (Drink d in drinks)
            {
                StackPanel sp = new StackPanel();
                CheckBox cb = new CheckBox();
                //TextBox tb = new TextBox();
                Slider sl = new Slider();
                Label lb = new Label();

                sp.Orientation = Orientation.Horizontal;

                cb.Content = d.Name + d.Size + d.Price;
                cb.Margin = new Thickness(5);
                cb.Width = 150;
                cb.Height = 25;

                sl.Value = 0;
                sl.Width = 100;
                sl.Minimum = 0;
                sl.Maximum = 20;
                sl.TickPlacement = TickPlacement.TopLeft;
                sl.TickFrequency = 1;
                sl.IsSnapToTickEnabled = true;

                lb.Width = 50;

                //tb.Width = 80;
                //tb.Height = 25;
                //tb.TextAlignment = TextAlignment.Right;

                Binding myBinding = new Binding("Value");
                myBinding.Source = sl;
                lb.SetBinding(ContentProperty, myBinding);

                sp.Children.Add(cb);
                //sp.Children.Add(tb);
                sp.Children.Add(sl);
                sp.Children.Add(lb);

                stackpanel_DrinkMenu.Children.Add(sp);
            }
        }

        private void AddNewDrink(List<Drink> mydrink)
        {
            mydrink.Add(new Drink() { Name = "咖啡", Size = "大杯", Price = 60 });
            mydrink.Add(new Drink() { Name = "咖啡", Size = "小杯", Price = 50 });
            mydrink.Add(new Drink() { Name = "紅茶", Size = "大杯", Price = 30 });
            mydrink.Add(new Drink() { Name = "紅茶", Size = "小杯", Price = 20 });
            mydrink.Add(new Drink() { Name = "綠茶", Size = "大杯", Price = 30 });
            mydrink.Add(new Drink() { Name = "綠茶", Size = "小杯", Price = 20 });
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.IsChecked == true)
            {
                takeout = rb.Content.ToString();
            }
        }

        private void orderButton_Click(object sender, RoutedEventArgs e)
        {
            displayTextBlock.Text = "";
            PlaceOrder(order);
            DisplayOrderDetail(order);
        }

        private void DisplayOrderDetail(List<OrderItem> myorder)
        {
            int total = 0;
            displayTextBlock.Text = $"您所訂購的飲品{takeout}，訂購明細如下：\n";
            int i = 1;

            foreach (OrderItem item in myorder)
            {
                total += item.SubTotal;
                Drink drinkItem = drinks[item.Index];
                displayTextBlock.Text += $"訂購品項{i}：{drinkItem.Name}{drinkItem.Size}，單價{drinkItem.Price}元 X {item.Quantity}，小計{item.SubTotal}元。\n";
                i++;
            }
        }

        private void PlaceOrder(List<OrderItem> myorder)
        {
            myorder.Clear();
            for (int i = 0; i < stackpanel_DrinkMenu.Children.Count; i++)
            {
                StackPanel sp = stackpanel_DrinkMenu.Children[i] as StackPanel;
                CheckBox cb = sp.Children[0] as CheckBox;
                Slider sl = sp.Children[1] as Slider;
                int quantity = Convert.ToInt32(sl.Value);

                if (cb.IsChecked == true && quantity != 0)
                {
                    int price = drinks[i].Price;
                    int subtotal = price * quantity;
                    myorder.Add(new OrderItem() { Index = i, Quantity = quantity, SubTotal = subtotal });
                }
            }
        }
    }
}

using practica1.PetShop1DataSetTableAdapters;
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
using System.Windows.Shapes;

namespace practica1
{
    /// <summary>
    /// Логика взаимодействия для WarehouseWindow1.xaml
    /// </summary>
    public partial class WarehouseWindow1 : Window
    {
        WarehouseTableAdapter storehouse = new WarehouseTableAdapter();
        ProductsTableAdapter product = new ProductsTableAdapter();


        public WarehouseWindow1()
        {
            InitializeComponent();
            gridWarehouse.ItemsSource = storehouse.GetData();
            comboBox.ItemsSource = product.GetData();
            comboBox.DisplayMemberPath = "ProductName";
            comboBox.SelectedValuePath = "ProductID";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int productId = (int)comboBox.SelectedValue;
            storehouse.InsertQuery(productId, int.Parse(txtQuantity.Text));
            gridWarehouse.ItemsSource = storehouse.GetData();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (gridWarehouse.SelectedItem != null)
            {
                DataRowView selectedRow = gridWarehouse.SelectedItem as DataRowView;
                object WarehouseID = selectedRow.Row[0];
                int productId = (int)comboBox.SelectedValue; 
                storehouse.UpdateQuery(productId, int.Parse(txtQuantity.Text), Convert.ToInt32(WarehouseID));
                gridWarehouse.ItemsSource = storehouse.GetData();
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            object WarehouseID = (gridWarehouse.SelectedItem as DataRowView).Row[0];
            storehouse.DeleteQuery(Convert.ToInt32(WarehouseID));
            gridWarehouse.ItemsSource = storehouse.GetData();

        }

        private void gridWarehouse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            if (gridWarehouse.SelectedItem != null)
            {
                DataRowView selectedRow = gridWarehouse.SelectedItem as DataRowView;
                comboBox.SelectedValue = selectedRow.Row[1];
                txtQuantity.Text = selectedRow.Row[2].ToString();
            }
        }
    }
}

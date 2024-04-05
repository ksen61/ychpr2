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
    /// Логика взаимодействия для ProductWindow1.xaml
    /// </summary>
    public partial class ProductWindow1 : Window
    {
        ProductsTableAdapter product = new ProductsTableAdapter();
        CategoriesTableAdapter category = new CategoriesTableAdapter();

        public ProductWindow1()
        {
            InitializeComponent();
            gridProducts.ItemsSource = product.GetData();
            comboBox.ItemsSource = category.GetData();
            comboBox.DisplayMemberPath = "CategoryName";
            comboBox.SelectedValuePath = "CategoryID";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int categoryId = (int)comboBox.SelectedValue; 
            product.InsertQuery(txtProductName.Text, categoryId, decimal.Parse(txtPrice.Text));
            gridProducts.ItemsSource = product.GetData();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (gridProducts.SelectedItem != null)
            {
                DataRowView selectedRow = gridProducts.SelectedItem as DataRowView;
                object ProductID = selectedRow.Row[0];
                int categoryId = (int)comboBox.SelectedValue;
                product.UpdateQuery(txtProductName.Text, categoryId, decimal.Parse(txtPrice.Text), Convert.ToInt32(ProductID));
                gridProducts.ItemsSource = product.GetData();
            }
        }

        private void gridProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridProducts.SelectedItem != null)
            {
                DataRowView selectedRow = gridProducts.SelectedItem as DataRowView;
                txtProductName.Text = selectedRow.Row[1].ToString();
                comboBox.SelectedValue = selectedRow.Row[2]; 
                txtPrice.Text = selectedRow.Row[3].ToString();
            }
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (gridProducts.SelectedItem != null)
            {
                DataRowView selectedRow = gridProducts.SelectedItem as DataRowView;
                int productID = Convert.ToInt32(selectedRow.Row[0]);
                WarehouseTableAdapter warehouse = new WarehouseTableAdapter();
                warehouse.DeleteQuery1(productID);
                product.DeleteQuery(productID);
                gridProducts.ItemsSource = product.GetData();
            }
        }




    }
}

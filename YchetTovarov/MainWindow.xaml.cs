using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YchetTovarov
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            update();         
            update_combobox();
        }
        public void update()
        {
            //чтение и обновление


            var list = App.DB.product.ToList();
            product product1 = new product();


           


            //поиск слов
            var poisk = Poiskovik.Text;
            if (poisk != "")
            {
                list = App.DB.product.Where(p => p.name.Contains(poisk)).ToList();
                
            }




            //поиск в комбобокс выборки
            var category = comboBox.SelectedIndex;
            if (category == 0)
            {
                list = App.DB.product.ToList();
            }
            if (category == 1)
            {
                list = App.DB.product.Where(p => p.id_category == 1).ToList();
            }
            if (category == 2)
            {
                list = App.DB.product.Where(p => p.id_category == 2).ToList();
            }
            if (category == 3)
            {
                list = App.DB.product.Where(p => p.id_category == 3).ToList();
            }

           
            

            listView.ItemsSource = list;

        }
        
    
    
        //чтение всех данных комбобокс
        public void update_combobox()
        {

           
            var list_combo = App.DB.category;
            var list = list_combo.ToList();
            List<string> list1 = new List<string>();
            list1.Add("Все");
            foreach (var i in list)
            {
                list1.Add(i.name);

            }
            comboBox.ItemsSource = list1;

        }

        private void Dobav_Click(object sender, RoutedEventArgs e)
        {
            if (NameText.Text == "")
            {
                System.Windows.MessageBox.Show($"Заполните имя");
            }
            if(DataText.Text == "")
            {
                System.Windows.MessageBox.Show($"Заполните дату");
            }
            if (CategoryText.Text == "")
            {
                System.Windows.MessageBox.Show($"Выберите категорию");
            }
            if (CountText.Text == "")
            {
                System.Windows.MessageBox.Show($"Введите количество");
            }
            if (kartinkaupload == null)
            {
                System.Windows.MessageBox.Show($"Добавьте картинку");
            }
            if (NameText.Text != "" && DataText.Text != "" && CategoryText.Text != "" && CountText.Text != "" && kartinkaupload.Text != "")
            {
                product prd1 = new product

                {

                    name = NameText.Text,
                    data = DataText.Text,
                    id_category = Convert.ToInt32(CategoryText.Text),
                    count = CountText.Text,
                    img = kartinkaupload.Text
                };

                App.DB.product.Add(prd1);
                App.DB.SaveChanges();
                update();
                NameText.Clear();
                DataText.Clear();
                CategoryText.Clear();
                CountText.Clear();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button button1 = (System.Windows.Controls.Button)sender;
            product prod1 = (product)button1.DataContext;
            App.DB.product.Remove(prod1);
            App.DB.SaveChanges();
            update();
            System.Windows.MessageBox.Show($"Удален объект:{prod1.name}");
        }

        private void Izmenalist_Click(object sender, RoutedEventArgs e)
        {
            var button1 =(System.Windows.Controls.Button)sender;
            product prod1 = (product)button1.DataContext;
            NameText.Text = prod1.name;
            DataText.Text = prod1.data;
            CategoryText.Text = prod1.id_category.ToString();
            CountText.Text = prod1.count;
            kartinkaupload.Text = prod1.img;
            App.izm_prod = prod1;
            
        }

        private void Izmena_Click(object sender, RoutedEventArgs e)
        {
            product iz_prod = App.DB.product
                .Where(p => p.id == App.izm_prod.id).FirstOrDefault();
            iz_prod.name = NameText.Text;
            iz_prod.data = DataText.Text;
            iz_prod.id_category = Convert.ToInt32(CategoryText.Text);
            iz_prod.count = CountText.Text;
            App.DB.SaveChanges();
            update();
            NameText.Clear();
            DataText.Clear();
            CategoryText.Clear();
            CountText.Clear();
            System.Windows.MessageBox.Show($"Успешно изменен");
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//для комбобокс  отображение из списка
            update();
        }

        private void Poiskovik_TextChanged(object sender, TextChangedEventArgs e)
        {//для поиска отслеживания текстовых изменений
            update();
        }

        public void Kartinka_Click(object sender, RoutedEventArgs e)
        {
            kartinkaupload.IsReadOnly = true;
            // var dialog = new System.Windows.Forms.FolderBrowserDialog();
            //  System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            // kartinkaupload.Text = dialog.SelectedPath;

            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.Title =  @"\img\";
           // openFileDialog1.InitialDirectory = @"C:\Users\Kloswerd\source\repos\YchetTovarov\img";//--"C:\\";
            openFileDialog1.InitialDirectory = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, @"\img\");
            openFileDialog1.Filter = "All files (*.*)|*.*|Text File (*.png)|*.png |Text File (*.jpg)|*.jpg";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.ShowDialog();
            string CombinedPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"\img\");
            openFileDialog1.InitialDirectory = System.IO.Path.GetFullPath(CombinedPath);
            if (openFileDialog1.FileName != "")
            { kartinkaupload.Text = openFileDialog1.Title + System.IO.Path.GetFileName(openFileDialog1.FileName); 
                
            }
            else
            { kartinkaupload.Text = "Вы не вывбрали файл!"; }
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Select background paper image |*.jpg;*.bmp;*.png";
            
        }

        private void Imageupload_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Imageuploader_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

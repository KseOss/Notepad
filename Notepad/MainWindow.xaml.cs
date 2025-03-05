using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Notepad;

namespace Notepad
{
    
    public partial class MainWindow : Window
    {
        Notepad notepad;
        public MainWindow()
        {
            InitializeComponent();
            notepad = new Notepad(fieldEdit);
        }
        //Кнопка создания
        public void Create_Click(object sender, RoutedEventArgs e)
        {
            notepad.Create();//Вызов метода создания нового документа
            this.Title = notepad.NameFile; //Обновляем заголовок окна
        }
        //Кнопка открытия
        public void Open_Click(object sender, RoutedEventArgs e)
        {
            notepad.Load();//Вызов метода загрузки текста из файла
            this.Title = notepad.NameFile; //Обновляем заголовок окна
        }
        //Кнопка сохранения
        public void Save_Click(object sender, RoutedEventArgs e)
        {
            notepad.ASaveNotepad();//Вызов метода сохранения текста
            this.Title = notepad.NameFile; //Обновляем заголовок окна
        }
        //Кнопка сохранения как
        public void ASave_Click(object sender, RoutedEventArgs e)
        {
            notepad.SaveAs();//Вызов метода сохранения длкумента как
            this.Title = notepad.NameFile; //Обновляем заголовок окна
        }
        //Кнопка выхода
        public void Exit_Click(object sender, RoutedEventArgs e)
        {
            if (notepad.Exit())
                this.Close();
        }
        //Кнопка копирования
        public void Copy_Click(object sender, RoutedEventArgs e)
        {
            notepad.Copy();
        }
        //Кнопка вставления
        public void Paste_Click(object sender, RoutedEventArgs e)
        {
            notepad.Paste();
        }
        //Кнопка отменения
        public void Undo_Click(object sender, RoutedEventArgs e)
        {
            notepad.Undo();
        }
        //Кнопка о программе
        public void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Программа: Разработка блокнота\n\nРазработчик: Сухомяткина Ксения Игоревна\nГруппа: ИСП-34", "О Программе", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
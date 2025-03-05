using System;
using Microsoft.Win32; 
using System.IO; 
using System.Windows; 
using System.Windows.Controls; 
using System.Windows.Documents; 

namespace Notepad
{
    class Notepad
    {
        string nameFile; //хранит имя текущего файла
        RichTextBox fieldEdit; //Поле редактирования текста
        public bool Modified { get; set; } //флаг, указывающий был ли текст изменен
        public string NameFile //св-во для получения имени файла
        {
            get { return nameFile; }
        }
        public Notepad(RichTextBox fieldEdit) //Конструктор класса, инициализирует поле редактирования и сбрасывает флаг изменения
        {
            nameFile = ""; //Изначально имя файла пустое
            this.fieldEdit = fieldEdit; //Присваиваем переданное поле редактирования
            Modified = false; //Текст еще не изменен
        }
        public bool ASaveNotepad() //Метод для сохранения текста в файл
        {
            SaveFileDialog sd = new SaveFileDialog(); //Создаем диалог сохранения файда
            sd.DefaultExt = "rtf"; //Устанавливаем по умолчанию
            sd.Filter = "Текстовый файл (*.rtf)|*.rtf|Все файлы(*.*)|*.*"; //Фильтр для выбора типа файла

            if (nameFile == "") //Если имя файоа не задано
            {
                if (sd.ShowDialog() == true) //открываем диалог сохранения и проверяем, что пользователь выбрал файл
                    nameFile = sd.FileName; //Сохраняем выбранное имя файла
                else return false; //Если отмена, возвращем false
            }
            //Сохраняем содержимое RichTextBox в файл
            TextRange doc = new TextRange(fieldEdit.Document.ContentStart, fieldEdit.Document.ContentEnd); //Получаем текст из RichTextBox
            FileStream fs = File.Create(nameFile); //Создаем файл для записи
            doc.Save(fs, DataFormats.Rtf); //Сохраняем текст в формате rtf
            fs.Close(); //Закрываем поток
            Modified = false; //Сбрасываем изменения
            return true; //Возвращаем успешный результат
        }
        //Метод создания нового документа
        public void Create()
        {
            if (Modified == true) //Если текст был изменен
            {
                MessageBoxResult result = MessageBox.Show("Вы хотите сохранить изменения в файле?", "Notepad", MessageBoxButton.YesNoCancel); //Запрашиваем пользоватея о сохранении
                if (result == MessageBoxResult.Yes) //Если да
                {
                    if (!ASaveNotepad()) return; //Пытаемся сохранить файл, если не удалось, то выходим
                }
                else if (result == MessageBoxResult.Cancel) //Если отмена
                {
                    return; //Прерываем создание новго документа
                }
            }
            fieldEdit.Document.Blocks.Clear(); //Очищаем содержимое RichTextBox
            nameFile = ""; //Сбрасываем имя файла
            Modified = false; //Сбрасываем изменения
        }
        //Метод для загрузки текста из файла
        public void Load()
        {
            OpenFileDialog od = new OpenFileDialog(); //Создаем диалог открытия файла
            od.DefaultExt = "rtf"; //Устанавливаем расширение по умолчанию
            od.Filter = "Текстовый файл (*.rtf)|*.rtf|Все файлы(*.*)|*.*"; //Фильтр для выбора типа файла
            if (od.ShowDialog() == true) //Открытие диалога, проверка, что пользователь выбрал файл
            {
                nameFile = od.FileName; //Сохраняем имя выбранного файла
                TextRange doc = new TextRange(fieldEdit.Document.ContentStart, fieldEdit.Document.ContentEnd); //Получаем текст из RichTextBox
                FileStream fs = File.Create(nameFile); //Создаем файл для записи
                doc.Load(fs, DataFormats.Rtf); //Сохраняем текст в формате TXT
                fs.Close(); //Закрываем поток
                Modified = false; //Сбрасываем изменения
            }
        }
        //Метод для сохранения текста с новым именем
        public void SaveAs()
        {
            string oldName = nameFile; //Сохраняем старое имя файла
            nameFile = ""; //Сбрасываем имя файла для того чтобы вызвать диалог сохранения
            if (!ASaveNotepad()) //Попытка сохранения файла с новым именем
            {
                nameFile = oldName; //Если отмена, восстанавливаем старое имя файла
            }
        }
        //Метод для завершения работы блокнота
        public bool Exit()
        {
            if (Modified) //Если текст был изменен
            {
                MessageBoxResult result = MessageBox.Show("Вы хотите сохранить изменения в файле?", "Notepad", MessageBoxButton.YesNoCancel); //Запрашиваем пользоватея о сохранении
                if (result == MessageBoxResult.Yes) //Если да
                {
                    if (!ASaveNotepad()) return false; //Пытаемся сохранить файл, если не удалось, то выходим
                }
                else if (result == MessageBoxResult.Cancel) //Если отмена
                {
                    return false; //Прерываем выход
                }
            }
            return true; //разрешаем выход
        }
        //Метод копирования текст в буфер обмена
        public void Copy()
        {
            fieldEdit.Copy();//Копируем выделенный текст в буфер обмена
        }
        //Метод для вставки текста из буфера обмена
        public void Paste()
        {
            fieldEdit.Paste(); //Вставляем текст из буфера обмена
        }
        //Метод для отмены последнего действия
        public void Undo()
        {
            fieldEdit.Undo();//Отменяем последнее действие 
        }
        //Метод изменения шрифта
        public void ChangeFont(string fontName, double fontSize)
        {
            fieldEdit.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, fontName);
            fieldEdit.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, fontSize);
        }
        //Метод для изменения цвета текста
        public void ChangeTextColor(System.Windows.Media.Color color)
        {
            fieldEdit.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, color);
        }
    }
}

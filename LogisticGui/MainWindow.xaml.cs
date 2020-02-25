using System;
using System.Windows;
using System.Windows.Input;

namespace LogisticGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            String destructionReference = Ref.Text;
            String serialNumber = Serial.Text;

            if (destructionReference.Length == 0)
            {
                Exceptions.Text = "Destruction reference not set.";
                return;
            }

            if (serialNumber.Length == 0)
            {
                Exceptions.Text = "Serial number not set.";
                return;
            }

            try
            {
                int affectedRows = Logistics.Other.AddDestructionTag(serialNumber, destructionReference);
 
                if (affectedRows != 1)
                {
                    Exceptions.Text = "Something went wrong.\nCheck tesseract for Serial number and reference.";
                    return;
                }
            }
            catch (Exception exception)
            {
                Exceptions.Text = exception.Message.Length < 1 ? "Something went wrong.\nCheck tesseract for Serial number and reference." : exception.Message;
                return;
            }
            Serial.Text = "";
            Exceptions.Text = "";





        }

        private void Exceptions_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Exceptions.Text = "";
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Kansū;
using System.Windows.Shell;
using System.Xml;
using DYMO.Label.Framework;
using Label = DYMO.Label.Framework.Label;

namespace Intāfēsu
{
    /// <summary>
    /// Interaction logic for RecentBookins.xaml
    /// </summary>
    public partial class RecentBookins : Page
    {
        internal List<Workshop.BookedIn> CurrentData { get; set; }
        internal List<Workshop.BookedIn> Update { get; set; }
        public RecentBookins()
        {
            InitializeComponent();
        }

        private async void RecentBookins_OnLoaded(object sender, RoutedEventArgs e)
        {
            do
            {
                CurrentData = (List<Workshop.BookedIn>) RecentBookinData.ItemsSource;
                Update = await Task.Run(Workshop.RecentlyBookedIn).ConfigureAwait(true);
                if (CurrentData != null) RecentBookinData.ItemsSource = Update;
                else if (Update != CurrentData) RecentBookinData.ItemsSource = Update;
                BookinByUserData.ItemsSource =  GetBreakdown();
                await Task.Delay(10000).ConfigureAwait(true);

            } while (RecentBookinData.IsVisible);
        }
        private List<Logistics.BookinBreakdown> GetBreakdown()
        {
            var data = Logistics.FetchBookInBreakdown();
            return data;
        }

        private  void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            PrintOuts.Print(callNumber.Text);
        }
    }

    public static class PrintOuts
    {
        public static void Print(string call)
        {

            string str = "<?xml version=\"1.0" +
   "\" encoding=\"utf-8" +
   "\"?>\r\n" +
   "<DieCutLabel Version" +
   "=\"8.0\" Units=\"twi" +
   "ps\">\r\n" +
   "\t<PaperOrientation>" +
   "Landscape</PaperOrie" +
   "ntation>\r\n" +
   "\t<Id>ReturnAddressI" +
   "nt</Id>\r\n" +
   "\t<IsOutlined>false<" +
   "/IsOutlined>\r\n" +
   "\t<PaperName>11352 R" +
   "eturn Address Int</P" +
   "aperName>\r\n" +
   "\t<DrawCommands>\r\n" +
   "\t\t<RoundRectangle " +
   "X=\"0\" Y=\"0\" Widt" +
   "h=\"1440\" Height=\"" +
   "3060\" Rx=\"180\" Ry" +
   "=\"180\" />\r\n" +
   "\t</DrawCommands>\r" +
   "\n" +
   "\t<ObjectInfo>\r\n" +
   "\t\t<TextObject>\r\n" +
   "\t\t\t<Name>product<" +
   "/Name>\r\n" +
   "\t\t\t<ForeColor Alp" +
   "ha=\"255\" Red=\"0\"" +
   " Green=\"0\" Blue=\"" +
   "0\" />\r\n" +
   "\t\t\t<BackColor Alp" +
   "ha=\"0\" Red=\"255\"" +
   " Green=\"255\" Blue=" +
   "\"255\" />\r\n" +
   "\t\t\t<LinkedObjectN" +
   "ame />\r\n" +
   "\t\t\t<Rotation>Rota" +
   "tion0</Rotation>\r\n" +
   "\t\t\t<IsMirrored>Fa" +
   "lse</IsMirrored>\r\n" +
   "\t\t\t<IsVariable>Fa" +
   "lse</IsVariable>\r\n" +
   "\t\t\t<GroupID>-1</G" +
   "roupID>\r\n" +
   "\t\t\t<IsOutlined>Fa" +
   "lse</IsOutlined>\r\n" +
   "\t\t\t<HorizontalAli" +
   "gnment>Center</Horiz" +
   "ontalAlignment>\r\n" +
   "\t\t\t<VerticalAlign" +
   "ment>Top</VerticalAl" +
   "ignment>\r\n" +
   "\t\t\t<TextFitMode>S" +
   "hrinkToFit</TextFitM" +
   "ode>\r\n" +
   "\t\t\t<UseFullFontHe" +
   "ight>True</UseFullFo" +
   "ntHeight>\r\n" +
   "\t\t\t<Verticalized>" +
   "False</Verticalized>" +
   "\r\n" +
   "\t\t\t<StyledText>\r" +
   "\n" +
   "\t\t\t\t<Element>\r" +
   "\n" +
   "\t\t\t\t\t<String xm" +
   "l:space=\"preserve\"" +
   ">Product</String>\r" +
   "\n" +
   "\t\t\t\t\t<Attribute" +
   "s>\r\n" +
   "\t\t\t\t\t\t<Font Fa" +
   "mily=\"Arial\" Size=" +
   "\"12\" Bold=\"False" +
   "\" Italic=\"False\" " +
   "Underline=\"False\" " +
   "Strikeout=\"False\" " +
   "/>\r\n" +
   "\t\t\t\t\t\t<ForeCol" +
   "or Alpha=\"255\" Red" +
   "=\"0\" Green=\"0\" B" +
   "lue=\"0\" HueScale=" +
   "\"100\" />\r\n" +
   "\t\t\t\t\t</Attribut" +
   "es>\r\n" +
   "\t\t\t\t</Element>\r" +
   "\n" +
   "\t\t\t</StyledText>" +
   "\r\n" +
   "\t\t</TextObject>\r" +
   "\n" +
   "\t\t<Bounds X=\"130" +
   "\" Y=\"151.636396928" +
   "267\" Width=\"2846\"" +
   " Height=\"296.590877" +
   "879749\" />\r\n" +
   "\t</ObjectInfo>\r\n" +
   "\t<ObjectInfo>\r\n" +
   "\t\t<TextObject>\r\n" +
   "\t\t\t<Name>Descript" +
   "ion</Name>\r\n" +
   "\t\t\t<ForeColor Alp" +
   "ha=\"255\" Red=\"0\"" +
   " Green=\"0\" Blue=\"" +
   "0\" />\r\n" +
   "\t\t\t<BackColor Alp" +
   "ha=\"0\" Red=\"255\"" +
   " Green=\"255\" Blue=" +
   "\"255\" />\r\n" +
   "\t\t\t<LinkedObjectN" +
   "ame />\r\n" +
   "\t\t\t<Rotation>Rota" +
   "tion0</Rotation>\r\n" +
   "\t\t\t<IsMirrored>Fa" +
   "lse</IsMirrored>\r\n" +
   "\t\t\t<IsVariable>Fa" +
   "lse</IsVariable>\r\n" +
   "\t\t\t<GroupID>-1</G" +
   "roupID>\r\n" +
   "\t\t\t<IsOutlined>Fa" +
   "lse</IsOutlined>\r\n" +
   "\t\t\t<HorizontalAli" +
   "gnment>Center</Horiz" +
   "ontalAlignment>\r\n" +
   "\t\t\t<VerticalAlign" +
   "ment>Middle</Vertica" +
   "lAlignment>\r\n" +
   "\t\t\t<TextFitMode>S" +
   "hrinkToFit</TextFitM" +
   "ode>\r\n" +
   "\t\t\t<UseFullFontHe" +
   "ight>True</UseFullFo" +
   "ntHeight>\r\n" +
   "\t\t\t<Verticalized>" +
   "False</Verticalized>" +
   "\r\n" +
   "\t\t\t<StyledText>\r" +
   "\n" +
   "\t\t\t\t<Element>\r" +
   "\n" +
   "\t\t\t\t\t<String xm" +
   "l:space=\"preserve\"" +
   ">Description</String" +
   ">\r\n" +
   "\t\t\t\t\t<Attribute" +
   "s>\r\n" +
   "\t\t\t\t\t\t<Font Fa" +
   "mily=\"Arial\" Size=" +
   "\"10\" Bold=\"False" +
   "\" Italic=\"False\" " +
   "Underline=\"False\" " +
   "Strikeout=\"False\" " +
   "/>\r\n" +
   "\t\t\t\t\t\t<ForeCol" +
   "or Alpha=\"255\" Red" +
   "=\"0\" Green=\"0\" B" +
   "lue=\"0\" HueScale=" +
   "\"100\" />\r\n" +
   "\t\t\t\t\t</Attribut" +
   "es>\r\n" +
   "\t\t\t\t</Element>\r" +
   "\n" +
   "\t\t\t</StyledText>" +
   "\r\n" +
   "\t\t</TextObject>\r" +
   "\n" +
   "\t\t<Bounds X=\"130" +
   "\" Y=\"524.236424394" +
   "087\" Width=\"2846\"" +
   " Height=\"477.245436" +
   "651056\" />\r\n" +
   "\t</ObjectInfo>\r\n" +
   "</DieCutLabel>";
            var callDetails = new Logistics.CallDetails(call);
            var label = Label.OpenXml(str);
            label.SetObjectText("product", callDetails.Product);
            label.SetObjectText("Description", callDetails.Description);
            label.Print("DYMO LabelWriter 450");
        }
    }
}

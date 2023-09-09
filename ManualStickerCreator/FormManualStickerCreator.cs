using System.Xml;

namespace ManualStickerCreator
{
    public partial class FormManualStickerCreator : Form
    {
        public FormManualStickerCreator()
        {
            InitializeComponent();
            loadDefaultSettings();
        }

        private void loadDefaultSettings()
        {
            XmlDocument xmlDoc = new XmlDocument();
            if (!File.Exists(Sabitler.UygulamaDizini + "\\Settings.xml"))
            {
                MessageBox.Show("I cannot find the Settings file!");
            }
            else
            {
                xmlDoc.Load(Sabitler.UygulamaDizini + "\\Settings.xml");
                txtQuantity.Text = xmlDoc.SelectSingleNode("/Root/Quantity").InnerText;
                txtLine1.Text = xmlDoc.SelectSingleNode("/Root/Line1").InnerText;
                txtLine2.Text = xmlDoc.SelectSingleNode("/Root/Line2").InnerText;
                txtLine3.Text = xmlDoc.SelectSingleNode("/Root/Line3").InnerText;
                txtLeftMargin1.Text = xmlDoc.SelectSingleNode("/Root/LeftMarginLine1").InnerText;
                txtLeftMargin2.Text = xmlDoc.SelectSingleNode("/Root/LeftMarginLine2").InnerText;
                txtLeftMargin3.Text = xmlDoc.SelectSingleNode("/Root/LeftMarginLine3").InnerText;
                txtTopMargin1.Text = xmlDoc.SelectSingleNode("/Root/TopMarginLine1").InnerText;
                txtTopMargin2.Text = xmlDoc.SelectSingleNode("/Root/TopMarginLine2").InnerText;
                txtTopMargin3.Text = xmlDoc.SelectSingleNode("/Root/TopMarginLine3").InnerText;
                txtFontWidth1.Text = xmlDoc.SelectSingleNode("/Root/FontWidthLine1").InnerText;
                txtFontWidth2.Text = xmlDoc.SelectSingleNode("/Root/FontWidthLine2").InnerText;
                txtFontWidth3.Text = xmlDoc.SelectSingleNode("/Root/FontWidthLine3").InnerText;
                txtFontLength1.Text = xmlDoc.SelectSingleNode("/Root/FontLengthLine1").InnerText;
                txtFontLength2.Text = xmlDoc.SelectSingleNode("/Root/FontLengthLine2").InnerText;
                txtFontLength3.Text = xmlDoc.SelectSingleNode("/Root/FontLengthLine3").InnerText;
            }

        }

        private void btnPrintSticker_Click(object sender, EventArgs e)
        {
            BarcodeOperations.PrintSticker(this, new Sticker()
            {
                Quantity = Convert.ToInt32(txtQuantity.Text),
                Line1 = txtLine1.Text,
                Line2 = txtLine2.Text,
                Line3 = txtLine3.Text,
                LeftMarginLine1 = Convert.ToInt32(txtLeftMargin1.Text),
                LeftMarginLine2 = Convert.ToInt32(txtLeftMargin2.Text),
                LeftMarginLine3 = Convert.ToInt32(txtLeftMargin3.Text),
                TopMarginLine1 = Convert.ToInt32(txtTopMargin1.Text),
                TopMarginLine2 = Convert.ToInt32(txtTopMargin2.Text),
                TopMarginLine3 = Convert.ToInt32(txtTopMargin3.Text),
                FontWidthLine1 = Convert.ToInt32(txtFontWidth1.Text),
                FontWidthLine2 = Convert.ToInt32(txtFontWidth2.Text),
                FontWidthLine3 = Convert.ToInt32(txtFontWidth3.Text),
                FontLengthLine1 = Convert.ToInt32(txtFontLength1.Text),
                FontLengthLine2 = Convert.ToInt32(txtFontLength2.Text),
                FontLengthLine3 = Convert.ToInt32(txtFontLength3.Text)
            });
        }

    }
}
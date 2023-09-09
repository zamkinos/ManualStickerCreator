using Microsoft.Win32.SafeHandles;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace ManualStickerCreator
{
    internal static class BarcodeOperations
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern SafeFileHandle CreateFile(string lpFileName, FileAccess dwDesiredAccess,
        uint dwShareMode, IntPtr lpSecurityAttributes, FileMode dwCreationDisposition,
        uint dwFlagsAndAttributes, IntPtr hTemplateFile);


        public static void PrintSticker(Form form, Sticker sticker)
        {
            string dosyaAdi = Sabitler.UygulamaDizini + "\\barcode.txt";
            try
            {
                UpcLabel upcLabel = new UpcLabel();
                PrintDialog pd = new PrintDialog();
                pd.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
                if (DialogResult.OK == pd.ShowDialog(form))
                {
                    StreamWriter sw11 = new StreamWriter(dosyaAdi, false, Encoding.UTF8);
                    if (sticker.Quantity > 3)
                    {
                        writeToTxtAndSend(sticker, sw11, upcLabel, pd, dosyaAdi);
                        sticker.Quantity = sticker.Quantity - 3;
                        if (MessageBox.Show("Please check the printed label!\n\rDo you want to continue? (Y/N)", "Label Control", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            sw11 = new StreamWriter(dosyaAdi, false, Encoding.UTF8);
                            writeToTxtAndSend(sticker, sw11, upcLabel, pd, dosyaAdi);
                        }
                    }
                    else
                    {
                        writeToTxtAndSend(sticker, sw11, upcLabel, pd, dosyaAdi);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static void writeToTxtAndSend(Sticker sticker, StreamWriter sw11, UpcLabel upcLabel, PrintDialog pd, string dosyaAdi)
        {
            try
            {
                for (int i = 0; i < sticker.Quantity; i++)
                {
                    writeToTxtHorzontal(sticker, sw11);
                }
                sw11.Close();
                sw11.Dispose();
                upcLabel.PrintBarcodeFromTheFile(pd.PrinterSettings.PrinterName, dosyaAdi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("[writeToTxtAndSend], " + ex.Message);
            }
        }

        private static void writeToTxtHorzontal(Sticker sticker, StreamWriter sw11)
        {
            try
            {
                sw11.WriteLine("^XA");
                sw11.WriteLine("^FO40,100");
                sw11.WriteLine("^AQ,50,30");
                sw11.WriteLine(string.Format(CultureInfo.GetCultureInfo("en-US"), "^FO{0},{1}^A0,{2},{3}^FD{4}^FS", 
                    sticker.LeftMarginLine1,
                    sticker.TopMarginLine1,
                    sticker.FontWidthLine1,
                    sticker.FontLengthLine1,
                    sticker.Line1));
                sw11.WriteLine(string.Format(CultureInfo.GetCultureInfo("en-US"), "^FO{0},{1}^A0,{2},{3}^FD{4}^FS",
                    sticker.LeftMarginLine2,
                    sticker.TopMarginLine2,
                    sticker.FontWidthLine2,
                    sticker.FontLengthLine2,
                    sticker.Line2));
                sw11.WriteLine(string.Format(CultureInfo.GetCultureInfo("en-US"), "^FO{0},{1}^A0,{2},{3}^FD{4}^FS",
                    sticker.LeftMarginLine3,
                    sticker.TopMarginLine3,
                    sticker.FontWidthLine3,
                    sticker.FontLengthLine3,
                    sticker.Line3));
                sw11.WriteLine("^XZ");
            }
            catch (Exception ex)
            {
                MessageBox.Show("[writeToTxtHorzontal], " + ex.Message);
            }
        }

    }

}

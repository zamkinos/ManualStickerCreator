using System.Globalization;
using System.Text;

namespace ManualStickerCreator
{
    public class UpcLabel
    {

        private string strFirstName;
        private string strLastName;
        private string strNoOfCopies;
        private string PickUpTime;
        private string Cold;
        private string NewMember;

        /// <summary>
        /// 
        /// </summary>
        public UpcLabel()
        {
        }

        public UpcLabel(string strFirstName, string strLastName, string strNoOfCopies, string PickUpTime, string Cold, string NewMember)
        {
            try
            {
                if (strFirstName == null || strLastName == null || strNoOfCopies == "" || strNoOfCopies == "0")
                {
                    throw new ArgumentNullException("strFirstName");
                }

                this.strFirstName = strFirstName;
                this.strLastName = strLastName;
                this.strNoOfCopies = strNoOfCopies;
                this.PickUpTime = PickUpTime;
                this.Cold = Cold;
                this.NewMember = NewMember;
            }
            catch (Exception ex)
            {
                MessageBox.Show("[UpcLabel], " + ex.Message);
            }
        }

        public void PrintBarcodeFromTheFile(string printerName, string fileName)
        {
            try
            {
                if (printerName == null)
                {
                    throw new ArgumentNullException("printerName");
                }
                RawPrinterHelper.SendFileToPrinter(printerName, fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("[PrintBarcodeFromTheFile], " + ex.Message);
            }
        }

        public void PrintBarcode(string printerName, string pProductName, string pBarcode, string strNumOfCopies)
        {
            try
            {
                if (printerName == null)
                {
                    throw new ArgumentNullException("printerName");
                }
                StringBuilder strBldr = new StringBuilder();
                strBldr.AppendLine("^XA");

                strBldr.AppendLine("^FO40,100");
                strBldr.AppendLine("^AQ,50,30");
                // sb1.AppendLine("^FDAnja^FS");
                strBldr.AppendLine(string.Format(CultureInfo.InvariantCulture, "^FD{0}^FS", pProductName));
                //^FO100,100^BY3
                strBldr.AppendLine("^FO80,200^BY3");
                //^BCN,150,Y,N,Y,N
                strBldr.AppendLine("^BCN,125,Y,N,Y,N");
                strBldr.AppendLine(string.Format(CultureInfo.InvariantCulture, "^FD{0}^FS", pBarcode));
                strBldr.AppendLine(string.Format(CultureInfo.InvariantCulture, "^PQ{0}", strNumOfCopies));
                strBldr.AppendLine("^XZ");
                RawPrinterHelper.SendStringToPrinter(printerName, strBldr.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("[PrintBarcode], " + ex.Message);
            }
        }

        public void Print(string printerName)
        {
            try
            {
                if (printerName == null)
                {
                    throw new ArgumentNullException("printerName");
                }
                StringBuilder sb1 = new StringBuilder();
                //^XA=Indicates Starting of Zpl
                sb1.AppendLine("^XA");
                sb1.AppendLine("^LL350");//^FS
                sb1.AppendLine("^PW930");//^FS
                sb1.AppendLine("^FO10,10");
                sb1.AppendLine("^AQ,80,80");

                sb1.AppendLine(string.Format(CultureInfo.InvariantCulture, "^FD{0}^FS", this.strFirstName));
                sb1.AppendLine("^FO10,68");
                sb1.AppendLine("^AQ,80,80");
                sb1.AppendLine(string.Format(CultureInfo.InvariantCulture, "^FD{0}^FS", this.strLastName));

                sb1.AppendLine("^FO10,150");
                sb1.AppendLine("^AQ,50,50");
                sb1.AppendLine(string.Format(CultureInfo.InvariantCulture, "^FD{0}^FS", this.PickUpTime));

                sb1.AppendLine("^FO240,150");
                sb1.AppendLine("^AQ,50,50");
                sb1.AppendLine(string.Format(CultureInfo.InvariantCulture, "^FD{0}^FS", this.Cold));
                //^PQ2= Indicates number of copies to print
                sb1.AppendLine(string.Format(CultureInfo.InvariantCulture, "^PQ{0}", this.strNoOfCopies));
                //sb1.AppendLine("^PQ2");
                //^XZ=Indicates ending of ZPL page
                sb1.AppendLine("^XZ");
                RawPrinterHelper.SendStringToPrinter(printerName, sb1.ToString());
                if (this.NewMember != "")
                {
                    StringBuilder strb = new StringBuilder();
                    strb.AppendLine("^XA");
                    strb.AppendLine("^LL350");//^FS
                    strb.AppendLine("^PW930");//^FS
                    strb.AppendLine("^FO20,30");
                    strb.AppendLine("^AQ,80,80");
                    strb.AppendLine(string.Format(CultureInfo.InvariantCulture, "^FD{0}^FS", this.NewMember));
                    strb.AppendLine("^FO20,150");
                    strb.AppendLine("^AQ,50,50");
                    strb.AppendLine(string.Format(CultureInfo.InvariantCulture, "^FD{0}^FS", this.PickUpTime));

                    strb.AppendLine("^FO240,150");
                    strb.AppendLine("^AQ,50,50");
                    strb.AppendLine(string.Format(CultureInfo.InvariantCulture, "^FD{0}^FS", this.Cold));
                    //^PQ2= Indicates number of copies to print
                    strb.AppendLine(string.Format(CultureInfo.InvariantCulture, "^PQ{0}", "1"));
                    //sb1.AppendLine("^PQ2");
                    //^XZ=Indicates ending of ZPL page
                    strb.AppendLine("^XZ");

                    RawPrinterHelper.SendStringToPrinter(printerName, strb.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Print], " + ex.Message);
            }
        }
    }
}

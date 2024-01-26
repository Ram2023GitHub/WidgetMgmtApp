using log4net;
using WidgetManagementApplication.Helpers;

namespace EncryptDecryptTool
{
    public partial class Form1 : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger("EncryptDecryptToolLogs");
        public Form1()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(rtxtInputString.Text))
                    rtxtOutputString.Text = CryptographyHelper.Encrypt(rtxtInputString.Text);
                else
                    MessageBox.Show("Enter Input string to encrypt...");

            }
            catch (Exception ex)
            {
                Logger.Error("Exception during encrypting inputString :" + ex.Message);

            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(rtxtInputString.Text))

                    rtxtOutputString.Text = CryptographyHelper.Decrypt(rtxtInputString.Text);
                else
                    MessageBox.Show("Enter encrypted Input string to decrypt...");




            }
            catch (Exception ex)
            {
                Logger.Error("Exception during decrypting inputString :" + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            rtxtInputString.Text = string.Empty;
            rtxtOutputString.Text = string.Empty;
        }
    }
}
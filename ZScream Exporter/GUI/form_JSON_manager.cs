using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace ZScream_Exporter.GUI
{
    public partial class ZscreamForm : Form
    {
        private const string NEWLINE = "\r\n";
        public ZscreamForm()
        {
            InitializeComponent();

            ConstantsReader.SetupRegion(RegionId.Region.Japan, "../../");

            string s = ConstantsReader.GetAddress("tile_address").ToString("X2");
        }

        public Exporter exporter;
        public Importer importer;
        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateStatistics();
            TextAndTranslationManager.SetupLanguage(TextAndTranslationManager.XLanguage.English_US, "");
        }

        public void writeLog(string line, Color col, FontStyle fs = FontStyle.Regular)
        {
            Font f = new Font(logTextbox.Font, fs);
            string text = line + NEWLINE;
            logTextbox.AppendText(text);
            logTextbox.Select((logTextbox.Text.Length - text.Length) + 1, text.Length);
            logTextbox.SelectionColor = col;
            logTextbox.SelectionFont = f;
            logTextbox.Refresh();
        }

        private void LoadProjectToROMToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void clearlogsButton_Click(object sender, EventArgs e)
        {
            logTextbox.Clear();
        }

        private void FromROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            if (of.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(of.FileName, FileMode.Open, FileAccess.Read);
                byte[] temp = new byte[fs.Length];
                fs.Read(temp, 0, (int)fs.Length);
                fs.Close();

                ROM.SetRom(temp, out bool isHeadered);

                if (isHeadered)
                    writeLog(TextAndTranslationManager.GetString("form_parent_notice_headered"), Color.Orange);

                temp = null;

                /*
                 * Reset the progress bar if the user exports again.
                 * This prevents crashing.
                 */
                progressBar1.Value = 0;
                //read the ROM if you selected OK
                exporter = new Exporter(ROM.DATA, progressBar1, logTextbox);
                UpdateStatistics();
            }
        }

        private void FromJsonFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            if (of.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(of.FileName, FileMode.Open, FileAccess.Read);
                byte[] temp = new byte[fs.Length];
                fs.Read(temp, 0, (int)fs.Length);
                fs.Close();

                ROM.SetRom(temp, out bool isHeadered);

                if (isHeadered)
                    writeLog(TextAndTranslationManager.GetString("form_parent_notice_headered"), Color.Orange);

                temp = null;

                /*
                 * Reset the progress bar if the user exports again.
                 * This prevents crashing.
                 */
                progressBar1.Value = 0;

                importer = new Importer(ROM.DATA, progressBar1, logTextbox);
                UpdateStatistics();
            }
        }

        public void UpdateStatistics()
        {
            labelInfos.Text = "";

            labelInfos.Text += String.Format("{0} / 296\r\n", LoadedProjectStatistics.usedRooms);
            labelInfos.Text += String.Format("{0}\r\n", LoadedProjectStatistics.objectsRooms);
            labelInfos.Text += String.Format("{0} / {1}\r\n", LoadedProjectStatistics.chestsRooms, LoadedProjectStatistics.chestsRoomsLength);
            labelInfos.Text += String.Format("{0}\r\n", LoadedProjectStatistics.spritesRooms);
            labelInfos.Text += String.Format("{0}\r\n", LoadedProjectStatistics.itemsRooms);
            labelInfos.Text += String.Format("{0} / {1}\r\n", LoadedProjectStatistics.blocksRooms, LoadedProjectStatistics.blocksRoomsLength);
            labelInfos.Text += String.Format("{0} / {1}\r\n", LoadedProjectStatistics.torchesRooms, LoadedProjectStatistics.torchesRoomsLength);
            labelInfos.Text += String.Format("{0} / {1}\r\n", LoadedProjectStatistics.pitsRooms, LoadedProjectStatistics.pitsRoomsLength);
            labelInfos.Text += String.Format("{0} / {1}\r\n", LoadedProjectStatistics.entrancesRooms, LoadedProjectStatistics.entrancesRoomsLength);

            labelInfos.Text += NEWLINE;

            labelInfos.Text += String.Format("{0} / 160\r\n", LoadedProjectStatistics.usedMaps);
            labelInfos.Text += String.Format("{0} / 8864\r\n", LoadedProjectStatistics.tiles32Maps);
            labelInfos.Text += String.Format("{0}\r\n", LoadedProjectStatistics.itemsMaps);
            labelInfos.Text += String.Format("{0}\r\n", LoadedProjectStatistics.spritesMaps);
            labelInfos.Text += String.Format("{0}\r\n", LoadedProjectStatistics.overlaysMaps);
            labelInfos.Text += String.Format("{0} / 129\r\n", LoadedProjectStatistics.entrancesMaps);
            labelInfos.Text += String.Format("{0} / 79\r\n", LoadedProjectStatistics.exitsMaps);
            labelInfos.Text += String.Format("{0} / 19\r\n", LoadedProjectStatistics.holesMaps);
            labelInfos.Text += String.Format("{0} / 16\r\n", LoadedProjectStatistics.whirlpoolMaps);

            labelInfos.Text += NEWLINE;

            labelInfos.Text += String.Format("{0}\r\n", LoadedProjectStatistics.texts);


            labelbytesInfos.Text = "";

            labelbytesInfos.Text += String.Format(" {0} / 9999 Bytes\r\n", LoadedProjectStatistics.usedRoomsBytes);
            labelbytesInfos.Text += String.Format(" {0} / 9999 Bytes\r\n", LoadedProjectStatistics.objectsRoomsBytes);
            labelbytesInfos.Text += String.Format(" {0} / 9999 Bytes\r\n", LoadedProjectStatistics.chestsRoomsBytes);
            labelbytesInfos.Text += String.Format(" {0} / 9999 Bytes\r\n", LoadedProjectStatistics.spritesRoomsBytes);
            labelbytesInfos.Text += String.Format(" {0} / 9999 Bytes\r\n", LoadedProjectStatistics.itemsRoomsBytes);
            labelbytesInfos.Text += String.Format(" {0} / 9999 Bytes\r\n", LoadedProjectStatistics.blocksRoomsBytes);
            labelbytesInfos.Text += String.Format(" {0} / 9999 Bytes\r\n", LoadedProjectStatistics.torchesRoomsBytes);
            labelbytesInfos.Text += String.Format(" {0} / 9999 Bytes\r\n", LoadedProjectStatistics.pitsRoomsBytes);
            labelbytesInfos.Text += String.Format(" {0} / 9999 Bytes\r\n", LoadedProjectStatistics.entrancesRoomsBytes);

            labelbytesInfos.Text += NEWLINE;

            labelbytesInfos.Text += String.Format(" {0} / 9999 Bytes\r\n", LoadedProjectStatistics.usedMapsBytes);
            labelbytesInfos.Text += String.Format(" {0} / 9999 Bytes\r\n", LoadedProjectStatistics.tiles32MapsBytes);
            labelbytesInfos.Text += String.Format(" {0} / 9999 Bytes\r\n", LoadedProjectStatistics.itemsMapsBytes);
            labelbytesInfos.Text += String.Format(" {0} / 9999 Bytes\r\n", LoadedProjectStatistics.spritesMapsBytes);
            labelbytesInfos.Text += String.Format(" {0} / 9999 Bytes\r\n", LoadedProjectStatistics.overlaysMapsBytes);
            labelbytesInfos.Text += String.Format(" {0} / 9999 Bytes\r\n", LoadedProjectStatistics.entrancesMapsBytes);
            labelbytesInfos.Text += String.Format(" {0} / 9999 Bytes\r\n", LoadedProjectStatistics.exitsMapsBytes);
            labelbytesInfos.Text += String.Format(" {0} / 9999 Bytes\r\n", LoadedProjectStatistics.holesMapsBytes);
            labelbytesInfos.Text += String.Format(" {0} / 9999 Bytes\r\n", LoadedProjectStatistics.whirlpoolMapsBytes);

            labelbytesInfos.Text += NEWLINE;

            labelbytesInfos.Text += String.Format(" {0} / 9999 Bytes\r\n", LoadedProjectStatistics.textsBytes);

            projectPanel.Enabled = true;
        }
    }
}
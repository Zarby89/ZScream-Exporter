using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace ZScream_Exporter.GUI
{
    public partial class zscreamForm : Form
    {
        public zscreamForm()
        {
            InitializeComponent();
        }

        public Exporter exporter;
        public Importer importer;
        private void Form1_Load(object sender, EventArgs e)
        {
            updateStatistics();
        }
        byte[] romData;
        private void openROMToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public void writeLog(string line, Color col, FontStyle fs = FontStyle.Regular)
        {
            Font f = new Font(logTextbox.Font, fs);
            string text = line + "\r\n";
            logTextbox.AppendText(text);
            logTextbox.Select((logTextbox.Text.Length - text.Length) + 1, text.Length);
            logTextbox.SelectionColor = col;
            logTextbox.SelectionFont = f;
            logTextbox.Refresh();
        }

        private void loadProjectToROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
  
        }

        private void clearlogsButton_Click(object sender, EventArgs e)
        {
            logTextbox.Clear();
        }

        private void fromROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            if (of.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(of.FileName, FileMode.Open, FileAccess.Read);
                byte[] temp = new byte[fs.Length];
                fs.Read(temp, 0, (int)fs.Length);
                fs.Close();
                romData = new byte[temp.Length];
                if ((temp.Length & 0x200) == 0x200)
                {
                    //Rom is headered, remove header
                    romData = new byte[temp.Length - 0x200];
                    for (int i = 0x200; i < temp.Length; i++)
                    {
                        romData[i - 0x200] = temp[i];
                    }
                    writeLog("Header ROM detected", Color.Orange);
                }
                else
                {
                    romData = (byte[])temp.Clone();
                }
                temp = null;
            }

            exporter = new Exporter(romData, progressBar1, logTextbox);
            updateStatistics();
        }

        private void fromJsonFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            if (of.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(of.FileName, FileMode.Open, FileAccess.Read);
                byte[] temp = new byte[fs.Length];
                fs.Read(temp, 0, (int)fs.Length);
                fs.Close();
                romData = new byte[temp.Length];
                if ((temp.Length & 0x200) == 0x200)
                {
                    //Rom is headered, remove header
                    romData = new byte[temp.Length - 0x200];
                    for (int i = 0x200; i < temp.Length; i++)
                    {
                        romData[i - 0x200] = temp[i];
                    }
                    writeLog("Header ROM detected", Color.Orange);
                }
                else romData = (byte[])temp.Clone();

                temp = null;
            }

            importer = new Importer(romData, progressBar1, logTextbox);
            updateStatistics();


        }

        public void updateStatistics()
        {
            labelInfos.Text = "";
            labelInfos.Text += LoadedProjectStatistics.usedRooms +" / 296\r\n";
            labelInfos.Text += "("+LoadedProjectStatistics.objectsRooms+ " Objects) " + "000000" + " / 000000 bytes\r\n";
            labelInfos.Text += LoadedProjectStatistics.chestsRooms +" / "+ LoadedProjectStatistics.chestsRoomsLength+ "\r\n";
            labelInfos.Text += "("+LoadedProjectStatistics.spritesRooms + " Sprites) " + "000000" + " / 000000 bytes\r\n";
            labelInfos.Text += "(" + LoadedProjectStatistics.itemsRooms + " Items) " + "000000" + " / 000000 bytes\r\n";
            labelInfos.Text += LoadedProjectStatistics.blocksRooms + " / " + LoadedProjectStatistics.blocksRoomsLength + "\r\n";
            labelInfos.Text += LoadedProjectStatistics.torchesRooms + " / " + LoadedProjectStatistics.torchesRoomsLength + "\r\n";
            labelInfos.Text += LoadedProjectStatistics.pitsRooms + " / " + LoadedProjectStatistics.pitsRoomsLength + "\r\n";
            labelInfos.Text += LoadedProjectStatistics.entrancesRooms + " / " + LoadedProjectStatistics.entrancesRoomsLength + "\r\n";

            labelInfos.Text += "\r\n";

            labelInfos.Text += LoadedProjectStatistics.usedMaps + " / " + 160 + "\r\n";
            labelInfos.Text += LoadedProjectStatistics.tiles32Maps + " / " + 8864 + "\r\n";
            labelInfos.Text += "(" + LoadedProjectStatistics.itemsMaps + " Items) " + "000000" + " / 000000 bytes\r\n";
            labelInfos.Text += "(" + LoadedProjectStatistics.spritesMaps + " Sprites) " + "000000" + " / 000000 bytes\r\n";
            labelInfos.Text += LoadedProjectStatistics.overlaysMaps +  " / ?????? bytes\r\n";
            labelInfos.Text += LoadedProjectStatistics.entrancesMaps + " / 129\r\n";
            labelInfos.Text += LoadedProjectStatistics.exitsMaps + " / 79\r\n";
            labelInfos.Text += LoadedProjectStatistics.holesMaps + " / 19\r\n";
            labelInfos.Text += LoadedProjectStatistics.whirlpoolMaps + " / 16\r\n";

            labelInfos.Text += "\r\n";

            labelInfos.Text += LoadedProjectStatistics.texts + " / " + "???" +"\r\n";

            projectPanel.Enabled = true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMP
{
    public partial class FMediaPlayer : Form
    {

        private bool isPaused = false;

        public FMediaPlayer()
        {
            InitializeComponent();
            hScrollBar1.Value = media.settings.volume;
            this.lerPlayList();
        }

        private void lerPlayList()
        {
            String path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            path = path.Remove(path.LastIndexOf("\\") + 1) + "playlist.txt";

            if (System.IO.File.Exists(path))
            {
                String arquivo = System.IO.File.ReadAllText(path);
                String[] playList = arquivo.Trim().Split('\n');
                lstArquivos.Items.AddRange(playList);
            }
        }

        private void play(int index)
        {
            lstArquivos.SelectedIndex = index;
            media.URL = lstArquivos.SelectedItem.ToString();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (isPaused)
            {
                media.Ctlcontrols.play();
                isPaused = false;
            } else
            {
                if (lstArquivos.Items.Count > 0)
                {
                    if(lstArquivos.SelectedIndex != -1)
                    {
                        this.play(lstArquivos.SelectedIndex);
                    }
                }
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (!isPaused)
            {
                media.Ctlcontrols.pause();
                isPaused = true;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            media.Ctlcontrols.stop();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (lstArquivos.Items.Count > 0)
            {
                this.play(0);
            }
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            if (lstArquivos.Items.Count > 0)
            {
                if (0 <= lstArquivos.SelectedIndex - 1)
                {
                    lstArquivos.SelectedIndex--;
                    this.play(lstArquivos.SelectedIndex);
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (lstArquivos.Items.Count > 0)
            {
                if (lstArquivos.Items.Count > (lstArquivos.SelectedIndex + 1))
                {
                    lstArquivos.SelectedIndex++;
                    this.play(lstArquivos.SelectedIndex);
                }
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (lstArquivos.Items.Count > 0)
            {
                this.play(lstArquivos.Items.Count - 1);
            }
        }

        private void btnMute_Click(object sender, EventArgs e)
        {
            media.settings.mute = !media.settings.mute;

            if (media.settings.mute)
            {
                btnMute.ImageIndex = 7;
            } else
            {
                btnMute.ImageIndex = 8;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                lstArquivos.Items.Add(openFileDialog1.FileName);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Deseja remover o arquivo de audio?", "Remover audio", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                lstArquivos.Items.Remove(lstArquivos.SelectedItem);
            }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            media.settings.volume = hScrollBar1.Value;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            path = path.Remove(path.LastIndexOf("\\") + 1) + "playlist.txt";

            String itens = "";
            foreach(String i in lstArquivos.Items)
            {
                itens = itens + i.ToString() + "\n";
            }

            System.IO.File.WriteAllText(path, itens);

            MessageBox.Show("Arquivo salvo com sucesso!", "LMP", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

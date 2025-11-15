using USPG_PII_TareaED.Model;
using USPG_PII_TareaED.Playlist;
using System.Drawing;
using System.Windows.Forms;

namespace USPG_PII_TareaED
{
    internal class PlayerForm : Form
    {
        private Label lblTitle;
        private Label lblArtist;
        private Button btnPrev;
        private Button btnNext;
        private Button btnShuffle;
        private Button btnAddSong;
        private Button btnExport;

        private CircularPlaylist playlist;

        private int songCounter = 1;

        public PlayerForm()
        {
            playlist = new CircularPlaylist();

            InitializeComponent();

            LoadInitialSongs();
            UpdateUI();
        }
        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblArtist = new Label();
            btnPrev = new Button();
            btnNext = new Button();
            btnShuffle = new Button();
            btnAddSong = new Button();
            btnExport = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Arial", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(10, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(380, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "---";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblArtist
            // 
            lblArtist.Font = new Font("Arial", 10F);
            lblArtist.Location = new Point(10, 50);
            lblArtist.Name = "lblArtist";
            lblArtist.Size = new Size(380, 20);
            lblArtist.TabIndex = 1;
            lblArtist.Text = "Presiona 'Agregar Canción'";
            lblArtist.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnPrev
            // 
            btnPrev.Location = new Point(20, 100);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(100, 40);
            btnPrev.TabIndex = 2;
            btnPrev.Text = "<< Anterior";
            btnPrev.Click += BtnPrev_Click;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(150, 100);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(100, 40);
            btnNext.TabIndex = 3;
            btnNext.Text = "Siguiente >>";
            btnNext.Click += BtnNext_Click;
            // 
            // btnShuffle
            // 
            btnShuffle.Location = new Point(280, 100);
            btnShuffle.Name = "btnShuffle";
            btnShuffle.Size = new Size(100, 40);
            btnShuffle.TabIndex = 4;
            btnShuffle.Text = "Shuffle";
            btnShuffle.Click += BtnShuffle_Click;
            // 
            // btnAddSong
            // 
            btnAddSong.Location = new Point(4, 150);
            btnAddSong.Name = "btnAddSong";
            btnAddSong.Size = new Size(200, 30);
            btnAddSong.TabIndex = 5;
            btnAddSong.Text = "Agregar Canción";
            btnAddSong.Click += BtnAddSong_Click;
            // 
            // btnExport
            // 
            btnExport.Location = new Point(210, 150);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(180, 30);
            btnExport.TabIndex = 6;
            btnExport.Text = "Exportar JSON";
            btnExport.Click += BtnExport_Click;
            // 
            // PlayerForm
            // 
            ClientSize = new Size(400, 200);
            Controls.Add(lblTitle);
            Controls.Add(lblArtist);
            Controls.Add(btnPrev);
            Controls.Add(btnNext);
            Controls.Add(btnShuffle);
            Controls.Add(btnAddSong);
            Controls.Add(btnExport);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "PlayerForm";
            Text = "Reproductor de Música Circular";
            ResumeLayout(false);
        }

        private void UpdateUI()
        {
            if (playlist.IsEmpty)
            {
                lblTitle.Text = "---";
                lblArtist.Text = "Playlist Vacía";
                btnPrev.Enabled = false;
                btnNext.Enabled = false;
                btnShuffle.Enabled = false;
            }
            else
            {
                Song? current = playlist.CurrentSong;
                lblTitle.Text = current.Title;
                lblArtist.Text = current.Artist;
                btnPrev.Enabled = true;
                btnNext.Enabled = true;
                btnShuffle.Enabled = (playlist.Count > 1);
            }
        }

        private void LoadInitialSongs()
        {
            playlist.AddLast(new Song("Bohemian Rhapsody", "Queen", 355));
            playlist.AddLast(new Song("Hotel California", "Eagles", 390));
            playlist.AddLast(new Song("Smells Like Teen Spirit", "Nirvana", 301));
        }


        private void BtnPrev_Click(object sender, EventArgs e)
        {
            playlist.Prev();
            UpdateUI();
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            playlist.Next();
            UpdateUI();
        }

        private void BtnShuffle_Click(object sender, EventArgs e)
        {
            playlist.Shuffle(DateTime.Now.Millisecond);
            UpdateUI();
        }

        private void BtnAddSong_Click(object sender, EventArgs e)
        {
            var newSong = new Song($"Canción de Prueba {songCounter}", $"Artista {songCounter}", 180);
            playlist.AddLast(newSong);
            songCounter++;
            UpdateUI();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            string jsonContent = playlist.ExportToJson();
            string filePath = "playlist.json";

            try
            {
                File.WriteAllText(filePath, jsonContent);
                MessageBox.Show($"¡Playlist exportada exitosamente a {filePath}!",
                                "Exportación Completa",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar: {ex.Message}",
                                "Error de Exportación",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}

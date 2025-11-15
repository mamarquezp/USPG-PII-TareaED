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
        private Label lblDuration;
        private Button btnPrev;
        private Button btnNext;
        private Button btnShuffle;
        private Button btnAddSong;
        private Button btnExport;
        private Button btnDelete;

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
            lblDuration = new Label(); 
            btnPrev = new Button();
            btnNext = new Button();
            btnShuffle = new Button();
            btnAddSong = new Button();
            btnExport = new Button();
            btnDelete = new Button();
            SuspendLayout();

            this.Text = "Reproductor de Música Circular";
            this.ClientSize = new Size(400, 200);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            lblTitle.Font = new Font("Arial", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(10, 15); 
            lblTitle.Size = new Size(380, 30);
            lblTitle.Text = "---";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            lblArtist.Font = new Font("Arial", 10F);
            lblArtist.Location = new Point(10, 45);
            lblArtist.Size = new Size(380, 20);
            lblArtist.Text = "Presiona 'Agregar Canción'";
            lblArtist.TextAlign = ContentAlignment.MiddleCenter;

            lblDuration.Font = new Font("Arial", 10F, FontStyle.Italic);
            lblDuration.Location = new Point(10, 65);
            lblDuration.Size = new Size(380, 20);
            lblDuration.Text = "--:--";
            lblDuration.TextAlign = ContentAlignment.MiddleCenter;
            lblDuration.ForeColor = Color.Gray;

            btnPrev.Location = new Point(20, 100);
            btnPrev.Size = new Size(100, 40);
            btnPrev.Text = "<< Anterior";
            btnPrev.Click += BtnPrev_Click;

            btnNext.Location = new Point(150, 100);
            btnNext.Size = new Size(100, 40);
            btnNext.Text = "Siguiente >>";
            btnNext.Click += BtnNext_Click;

            btnShuffle.Location = new Point(280, 100);
            btnShuffle.Size = new Size(100, 40);
            btnShuffle.Text = "Shuffle";
            btnShuffle.Click += BtnShuffle_Click;

            btnAddSong.Location = new Point(10, 150);
            btnAddSong.Size = new Size(120, 30);
            btnAddSong.Text = "Agregar";
            btnAddSong.Click += BtnAddSong_Click;

            btnDelete.Location = new Point(140, 150);
            btnDelete.Size = new Size(120, 30);
            btnDelete.Text = "Eliminar Actual";
            btnDelete.Click += BtnDelete_Click; 
            btnDelete.ForeColor = Color.Red;

            btnExport.Location = new Point(270, 150);
            btnExport.Size = new Size(120, 30);
            btnExport.Text = "Exportar JSON";
            btnExport.Click += BtnExport_Click;

            Controls.Add(lblTitle);
            Controls.Add(lblArtist);
            Controls.Add(lblDuration);
            Controls.Add(btnPrev);
            Controls.Add(btnNext);
            Controls.Add(btnShuffle);
            Controls.Add(btnAddSong);
            Controls.Add(btnExport);
            Controls.Add(btnDelete);
            ResumeLayout(false);
        }

        private void UpdateUI()
        {
            if (playlist.IsEmpty)
            {
                lblTitle.Text = "---";
                lblArtist.Text = "Playlist Vacía";
                lblDuration.Text = "--:--";
                btnPrev.Enabled = false;
                btnNext.Enabled = false;
                btnShuffle.Enabled = false;
                btnDelete.Enabled = false; 
            }
            else
            {
                Song? current = playlist.CurrentSong;
                lblTitle.Text = current.Title;
                lblArtist.Text = current.Artist;
                lblDuration.Text = FormatDuration(current.DurationInSeconds); 

                btnPrev.Enabled = true;
                btnNext.Enabled = true;
                btnDelete.Enabled = true;  
                btnShuffle.Enabled = (playlist.Count > 1); 
            }
        }

        private void LoadInitialSongs()
        {
            playlist.AddLast(new Song("Bohemian Rhapsody", "Queen", 355));
            playlist.AddLast(new Song("Hotel California", "Eagles", 390));
            playlist.AddLast(new Song("Smells Like Teen Spirit", "Nirvana", 301));
            playlist.AddLast(new Song("Don't Cry", "Guns N' Roses", 314));
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

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (playlist.IsEmpty) return;

            Guid idParaEliminar = playlist.CurrentSong.Id;

            DialogResult confirm = MessageBox.Show(
                $"¿Estás seguro de que quieres eliminar '{playlist.CurrentSong.Title}'?",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                playlist.RemoveById(idParaEliminar);

                UpdateUI();
            }
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

        private string FormatDuration(int totalSeconds)
        {
            TimeSpan duration = TimeSpan.FromSeconds(totalSeconds);

            return duration.ToString(@"m\:ss");
        }
    }
}

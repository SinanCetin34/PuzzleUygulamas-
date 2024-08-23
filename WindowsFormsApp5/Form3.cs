using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form3 : Form
    {
        private PictureBox[] pictureBoxes; // Puzzle parçalarını tutan dizi
        private Image image; // Puzzle resmi
        private Label lblMovesLeft; // Kalan hamle sayısını gösterecek etiket
        private int movesLeft; // Kullanıcının kalan hamle sayısını tutan değişken

        public Form3(Image image)
        {
            InitializeComponent(); // Form bileşenlerini başlatır
            this.image = image; // Resmi alır
            StarthardGame(); // Form açıldığında oyunu başlatır
        }

        private void StarthardGame()
        {
            movesLeft = 25; // Başlangıçta kalan hamle sayısını ayarlar
            SetupPuzzle(image, 4, 4); // 4x4'lük puzzle kurar
            UpdateMovesLeftLabel(); // Hamle sayısını ekranda günceller
        }

        private void SetupPuzzle(Image image, int rows, int cols)
        {
            pictureBoxes = new PictureBox[rows * cols]; // Puzzle için PictureBox dizisi oluşturur
            int pieceWidth = image.Width / cols; // Her bir puzzle parçasının genişliğini hesaplar
            int pieceHeight = image.Height / rows; // Her bir puzzle parçasının yüksekliğini hesaplar

            int puzzleWidth = cols * 100;  // Tüm puzzle'ın genişliğini belirler (100 piksel genişlikte her parça)
            int puzzleHeight = rows * 100; // Tüm puzzle'ın yüksekliğini belirler (100 piksel yükseklikte her parça)

            // Puzzle'ı formun ortasında yerleştirmek için offset hesaplar
            int offsetX = (this.ClientSize.Width - puzzleWidth) / 2;
            int offsetY = (this.ClientSize.Height - puzzleHeight) / 2;

            // Parçaların doğru konumları ve karıştırılmış konumları için listeler oluşturur
            var pieceLocations = new List<Point>();
            for (int i = 0; i < rows * cols; i++)
            {
                pieceLocations.Add(new Point(offsetX + (i % cols) * 100, offsetY + (i / cols) * 100));
            }

            // Parçaları karıştırır
            var random = new Random();
            pieceLocations = pieceLocations.OrderBy(p => random.Next()).ToList();

            for (int i = 0; i < rows * cols; i++)
            {
                pictureBoxes[i] = new PictureBox
                {
                    Size = new Size(100, 100), // Her bir puzzle parçasının boyutunu 100x100 piksel olarak ayarlar
                    Location = pieceLocations[i], // Karıştırılmış konumu ayarlar
                    Image = GetImagePart(image, i, rows, cols), // Puzzle parçası için ilgili resim bölümünü alır
                    BorderStyle = BorderStyle.FixedSingle, // Puzzle parçasının kenarlığını belirler
                    SizeMode = PictureBoxSizeMode.StretchImage // Resmi, PictureBox'ın boyutuna göre esnetir
                };
                pictureBoxes[i].Click += PictureBox_Click; // Puzzle parçasına tıklama olayı ekler
                this.Controls.Add(pictureBoxes[i]); // Puzzle parçasını form üzerinde görüntüler
            }
        }

        private Image GetImagePart(Image image, int index, int rows, int cols)
        {
            int width = image.Width / cols; // Her bir puzzle parçasının genişliğini hesaplar
            int height = image.Height / rows; // Her bir puzzle parçasının yüksekliğini hesaplar
            Bitmap bmp = new Bitmap(width, height); // Puzzle parçası için yeni bir bitmap oluşturur
            using (Graphics g = Graphics.FromImage(bmp)) // Resmi çizmek için Graphics nesnesi oluşturur
            {
                g.DrawImage(image, new Rectangle(0, 0, width, height),
                    new Rectangle((index % cols) * width, (index / cols) * height, width, height), GraphicsUnit.Pixel);
                // Resmi, bitmap üzerine ilgili bölgeye çizer
            }
            return bmp; // Oluşturulan resmi döner
        }

        private PictureBox selectedPictureBox = null; // Seçilen ilk puzzle parçasını tutar

        private void PictureBox_Click(object sender, EventArgs e)
        {
            if (movesLeft > 0) // Kalan hamle sayısı varsa
            {
                var clickedPictureBox = sender as PictureBox; // Tıklanan PictureBox'ı alır

                if (selectedPictureBox == null) // Eğer ilk parça seçiliyorsa
                {
                    selectedPictureBox = clickedPictureBox; // Seçilen parçayı ayarlar
                    selectedPictureBox.BorderStyle = BorderStyle.Fixed3D; // Seçilen parçayı belirgin hale getirir
                }
                else // İkinci parça seçiliyorsa ve yer değiştiriliyorsa
                {
                    SwapImages(selectedPictureBox, clickedPictureBox); // İki parçanın resimlerini yer değiştirir
                    selectedPictureBox.BorderStyle = BorderStyle.FixedSingle; // Seçilen parçanın kenarlığını varsayılan hale getirir
                    selectedPictureBox = null; // Seçimi sıfırlar
                    movesLeft--; // Kalan hamle sayısını bir azaltır
                    UpdateMovesLeftLabel(); // Hamle sayısını ekranda günceller
                }
            }
            else // Hamle sayısı biterse
            {
                MessageBox.Show("Hamle sayısı bitti!"); // Kullanıcıya bir uyarı mesajı gösterir
            }
        }

        private void SwapImages(PictureBox pb1, PictureBox pb2)
        {
            var tempImage = pb1.Image; // Geçici bir resim değişkeni oluşturur
            pb1.Image = pb2.Image; // İlk PictureBox'ın resmini ikinci PictureBox'a kopyalar
            pb2.Image = tempImage; // İkinci PictureBox'ın resmini ilk PictureBox'a kopyalar
        }

        private void UpdateMovesLeftLabel()
        {
            lblMovesLeft.Text = $"Kalan Hamle: {movesLeft}"; // Kalan hamle sayısını etiket üzerinde günceller
        }

        private void InitializeComponent()
        {
            this.lblMovesLeft = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblMovesLeft
            // 
            this.lblMovesLeft.AutoSize = true;
            this.lblMovesLeft.Location = new System.Drawing.Point(12, 396);
            this.lblMovesLeft.Name = "lblMovesLeft";
            this.lblMovesLeft.Size = new System.Drawing.Size(73, 13);
            this.lblMovesLeft.TabIndex = 0;
            this.lblMovesLeft.Text = "Kalan Hamle: ";
            // 
            // Form3
            // 
            this.ClientSize = new System.Drawing.Size(646, 446);
            this.Controls.Add(this.lblMovesLeft);
            this.Name = "Form3";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

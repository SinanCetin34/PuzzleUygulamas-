using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form2 : Form
    {
        private PictureBox[] pictureBoxes; // Puzzle parçalarını tutan dizi
        private Image image;        // Puzzle resmi
        private Label lblMovesLeft; // Kalan hamle sayısını gösterecek etiket
        private int movesLeft;     // Kullanıcının kalan hamle sayısını tutan değişken

        public Form2(Image image)
        {
            InitializeComponent(); // Form bileşenlerini başlatır
            this.image = image;   // Resmi formda kullanılan image değişkenine atar
            StartMediumGame();   //  Orta moddaki oyunu başlatır
        }

        private void StartMediumGame()
        {
            movesLeft = 35; // Orta mod için kalan hamle sayısını belirler
            SetupPuzzle(image, 3, 3); // 3x3'lük puzzle kurulumunu yapar
            UpdateMovesLeftLabel(); // Kalan hamle sayısını günceller
        }

        private void SetupPuzzle(Image image, int rows, int cols)
        {
            pictureBoxes = new PictureBox[rows * cols]; // Puzzle parçalarını tutacak PictureBox dizisini oluşturur
            int pieceWidth = image.Width / cols; // Her bir puzzle parçasının genişliğini hesaplar
            int pieceHeight = image.Height / rows; // Her bir puzzle parçasının yüksekliğini hesaplar

            int puzzleWidth = cols * 100;  // Puzzle'ın toplam genişliğini belirler (her bir parça 100 piksel genişliğinde olacak şekilde)
            int puzzleHeight = rows * 100; // Puzzle'ın toplam yüksekliğini belirler (her bir parça 100 piksel yüksekliğinde olacak şekilde)

            // Puzzle'ı formun ortasına yerleştirmek için X ve Y ofsetlerini hesaplar
            int offsetX = (this.ClientSize.Width - puzzleWidth) / 2;
            int offsetY = (this.ClientSize.Height - puzzleHeight) / 2;

            // Parçaların hedef konumlarını tutan bir liste oluşturur
            var pieceLocations = new List<Point>();
            for (int i = 0; i < rows * cols; i++)
            {
                pieceLocations.Add(new Point(offsetX + (i % cols) * 100, offsetY + (i / cols) * 100));
            }

            // Parçaların konumlarını karıştırır
            var random = new Random();
            pieceLocations = pieceLocations.OrderBy(p => random.Next()).ToList();

            // Puzzle parçalarını oluşturur ve form üzerine ekler
            for (int i = 0; i < rows * cols; i++)
            {
                pictureBoxes[i] = new PictureBox
                {
                    Size = new Size(100, 100), // Her bir puzzle parçasının boyutunu belirler
                    Location = pieceLocations[i], // Parçanın konumunu belirler
                    Image = GetImagePart(image, i, rows, cols), // Resmin ilgili parçasını alır
                    BorderStyle = BorderStyle.FixedSingle, // Kenarlık stilini belirler
                    SizeMode = PictureBoxSizeMode.StretchImage // Resmin boyutunu PictureBox'a göre ayarlar
                };
                pictureBoxes[i].Click += PictureBox_Click; // Tıklama olayını tanımlar
                this.Controls.Add(pictureBoxes[i]); // PictureBox'ı forma ekler
            }
        }

        private Image GetImagePart(Image image, int index, int rows, int cols)
        {
            int width = image.Width / cols; // Puzzle parçasının genişliğini hesaplar
            int height = image.Height / rows; // Puzzle parçasının yüksekliğini hesaplar
            Bitmap bmp = new Bitmap(width, height); // Parça boyutunda bir bitmap oluşturur
            using (Graphics g = Graphics.FromImage(bmp)) // Bitmap üzerine çizmeye başlar
            {
                g.DrawImage(image, new Rectangle(0, 0, width, height),
                    new Rectangle((index % cols) * width, (index / cols) * height, width, height), GraphicsUnit.Pixel);
                // Resmin uygun kısmını bitmap'e çizer
            }
            return bmp; // Oluşturulan resim parçasını döner
        }

        private PictureBox selectedPictureBox = null; // Seçilen ilk puzzle parçasını tutar

        private void PictureBox_Click(object sender, EventArgs e)
        {
            if (movesLeft > 0) // Kalan hamle sayısı kontrol edilir
            {
                var clickedPictureBox = sender as PictureBox; // Tıklanan PictureBox alınır

                if (selectedPictureBox == null) // Eğer henüz bir parça seçilmediyse
                {
                    selectedPictureBox = clickedPictureBox; // Tıklanan parça seçilir
                    selectedPictureBox.BorderStyle = BorderStyle.Fixed3D; // Seçilen parçayı belirgin hale getirir
                }
                else // İkinci bir parça seçildiyse ve yer değiştirilecekse
                {
                    SwapImages(selectedPictureBox, clickedPictureBox); // Parçaların resimleri değiştirilir
                    selectedPictureBox.BorderStyle = BorderStyle.FixedSingle; // İlk seçilen parçanın kenarlığını varsayılan hale getirir
                    selectedPictureBox = null; // Seçimi sıfırlar
                    movesLeft--; // Kalan hamle sayısını bir azaltır
                    UpdateMovesLeftLabel(); // Kalan hamle sayısını günceller
                }
            }
            else // Hamle sayısı bittiyse
            {
                MessageBox.Show("Hamle sayısı bitti!"); // Kullanıcıya uyarı mesajı gösterir
            }
        }

        private void SwapImages(PictureBox pb1, PictureBox pb2)
        {
            var tempImage = pb1.Image; // Geçici resim değişkeni oluşturulur
            pb1.Image = pb2.Image; // İlk PictureBox'ın resmini ikinci PictureBox'a kopyalar
            pb2.Image = tempImage; // İkinci PictureBox'ın resmini ilk PictureBox'a kopyalar
        }

        private void UpdateMovesLeftLabel()
        {
            lblMovesLeft.Text = $"Kalan Hamle: {movesLeft}"; // Kalan hamle sayısını ekranda gösterir
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
            // Form2
            // 
            this.ClientSize = new System.Drawing.Size(555, 441);
            this.Controls.Add(this.lblMovesLeft);
            this.Name = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde çalışacak olay burada yer alır
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        private PictureBox[] pictureBoxes; // Puzzle parçalarını tutan dizi
        private Image[] images; // Farklı zorluk seviyeleri için kullanılacak resimleri tutan dizi
        private int movesLeft; // Kullanıcının kalan hamle sayısını tutan değişken

        public Form1()
        {
            InitializeComponent(); // Form bileşenlerini başlatır
            LoadImages(); // Resimleri yükler
        }

        private void LoadImages()
        {
            // Farklı zorluk seviyeleri için kullanılacak resimleri yükler
            images = new Image[]
            {
                Properties.Resources.asdf, // Kolay mod için resim
                Properties.Resources.asdfg, // Orta mod için resim
                Properties.Resources.asdfgh  // Zor mod için resim
            };
        }

        private void StartGame(string difficulty)
        {
            // Mevcut pictureBox'ları temizler
            foreach (var pictureBox in pictureBoxes ?? Array.Empty<PictureBox>())
            {
                this.Controls.Remove(pictureBox); // pictureBox'ı formdan kaldırır
                pictureBox.Dispose(); // Bellekte yer kaplamaması için pictureBox'ı yok eder
            }

            // Zorluk seviyesine göre oyunu başlatır
            switch (difficulty)
            {
                case "easy":
                    movesLeft = 40; // Kolay modda 40 hamle hakkı verir
                    SetupPuzzle(images[0], 2, 2); // 2x2'lik puzzle kurar
                    break;
                case "medium":
                    movesLeft = 35; // Orta modda 35 hamle hakkı verir
                    SetupPuzzle(images[1], 3, 3); // 3x3'lük puzzle kurar
                    break;
                case "hard":
                    movesLeft = 25; // Zor modda 25 hamle hakkı verir
                    SetupPuzzle(images[2], 4, 4); // 4x4'lük puzzle kurar
                    break;
            }

            // Kalan hamle sayısını ekranda günceller
            lblMovesLeft.Text = $"Kalan Tıklama: {movesLeft}";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
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
            Graphics g = Graphics.FromImage(bmp); // Resmi çizmek için Graphics nesnesi oluşturur
            g.DrawImage(image, new Rectangle(0, 0, width, height),
                new Rectangle((index % cols) * width, (index / cols) * height, width, height), GraphicsUnit.Pixel);
            // Resmi, bitmap üzerine ilgili bölgeye çizer
            return bmp; // Oluşturulan resmi döner
        }
        private bool IsPuzzleSolved()
        {
            for (int i = 0; i < pictureBoxes.Length; i++)
            {
                // Her bir PictureBox'ın konumu ve resminin doğru olup olmadığını kontrol eder
                var correctLocation = GetCorrectLocation(i);
                if (pictureBoxes[i].Location != correctLocation || !IsImageInCorrectPosition(pictureBoxes[i], i))
                {
                    return false; // Puzzle çözülmemiş
                }
            }
            return true; // Puzzle çözülmüş
        }

        private Point GetCorrectLocation(int index)
        {
            // Doğru konumun hesaplanması
            int cols = (int)Math.Sqrt(pictureBoxes.Length);
            int x = (index % cols) * 100;
            int y = (index / cols) * 100;
            return new Point(x, y);
        }

        private bool IsImageInCorrectPosition(PictureBox pb, int index)
        {
            // Resmin doğru konumda olup olmadığını kontrol eder
            var correctImage = GetImagePart(images[(int)Math.Sqrt(pictureBoxes.Length)], index, (int)Math.Sqrt(pictureBoxes.Length), (int)Math.Sqrt(pictureBoxes.Length));
            return pb.Image.Equals(correctImage);
        }
        private void Startgame_Click(object sender, EventArgs e)
        {
            // Bu butonun tıklama olayında oyunu başlatmak için kod eklenebilir (şu an boş)
        }

        private void SwapImages(PictureBox pb1, PictureBox pb2)
        {
            // İki PictureBox'ın içindeki resimleri yer değiştirir
            var tempImage = pb1.Image;
            pb1.Image = pb2.Image;
            pb2.Image = tempImage;
        }

        private PictureBox selectedPictureBox = null; // Seçilen ilk puzzle parçasını tutar

        private void PictureBox_Click(object sender, EventArgs e)
        {
            if (movesLeft > 0)
            {
                var clickedPictureBox = sender as PictureBox; // Tıklanan PictureBox'ı alır

                if (selectedPictureBox == null)
                {
                    // Eğer ilk parça seçiliyorsa
                    selectedPictureBox = clickedPictureBox;
                    selectedPictureBox.BorderStyle = BorderStyle.Fixed3D; // Seçilen parçayı belirgin hale getirir
                }
                else
                {
                    // İkinci parça seçiliyorsa ve yer değiştiriliyorsa
                    SwapImages(selectedPictureBox, clickedPictureBox); // İki parçanın resimlerini yer değiştirir
                    selectedPictureBox.BorderStyle = BorderStyle.FixedSingle; // Seçilen parçanın kenarlığını varsayılan hale getirir
                    selectedPictureBox = null; // Seçimi sıfırlar
                    movesLeft--; // Kalan hamle sayısını bir azaltır
                    lblMovesLeft.Text = $"Kalan Tıklama: {movesLeft}"; // Hamle sayısını ekranda günceller

                    // Puzzle'ın doğru çözülüp çözülmediğini kontrol eder
                    if (IsPuzzleSolved())
                    {
                        MessageBox.Show("Tebrikler! Puzzle'ı çözdünüz.");
                        Application.Exit(); // Uygulamayı bitirir
                    }
                }
            }
            else
            {
                // Eğer hamle sayısı biterse, kullanıcıya bir uyarı mesajı gösterir
                MessageBox.Show("Hamle sayısı bitti!");
            }
        }

        private void btnEasy_Click_1(object sender, EventArgs e)
        {
            // kolay mod için yeni bir form (Form4) oluşturur ve resmi iletir
            Form4 form4 = new Form4(images[0]);

            // Form2'yi gösterirken Form1'i gizle
            this.Hide();
            form4.FormClosed += (s, args) => this.Show(); // Form2 kapandığında Form1'i tekrar göster

            form4.Show(); // Yeni formu gösterir
        }

        private void btnMedium_Click(object sender, EventArgs e)
        {
            // Orta mod için yeni bir form (Form2) oluşturur ve resmi iletir
            Form2 form2 = new Form2(images[1]);

            // Form2'yi gösterirken Form1'i gizle
            this.Hide();
            form2.FormClosed += (s, args) => this.Show(); // Form2 kapandığında Form1'i tekrar göster

            form2.Show(); // Yeni formu gösterir
        }

        private void btnHard_Click(object sender, EventArgs e)
        {
            // zor mod için yeni bir form (Form3) oluşturur ve resmi iletir
            Form3 form3 = new Form3(images[2]);

            // Form3'yi gösterirken Form1'i gizle
            this.Hide();
            form3.FormClosed += (s, args) => this.Show(); // Form2 kapandığında Form1'i tekrar göster

            form3.Show(); // Yeni formu gösterir
        }
    }
}

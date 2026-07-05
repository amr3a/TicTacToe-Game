using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Tic_Tac_Toe_Game
{  
 


    public partial class frmMain : Form
    {
        public enum enCellType
        {
            QM,
            X,
            O
        }
        public enum enPlayerName
        {
            Player1,
            Player2,
            Computer,
            UnKnownPlayer
        }
        public enum enPlayerState
        {
            Active,
            Unactive
        }
        private struct stPlayer
        {
           public enPlayerName playerName;
           public enCellType playerType;
           public enPlayerState playerState;

        
            public string PlayerName()
            {
                switch (playerName)
                {
                    case enPlayerName.Player1:
                        return "Player 1";
                    case enPlayerName.Player2:
                        return "Player 2";
                    case enPlayerName.Computer:
                        return "Computer";
                    default:
                        return "Unknown Player";
                }
            }

            private Image PictureBoxImage()
            {
                switch (playerType)
                {
                    case enCellType.X:
                       return Properties.Resources.X;
                    case enCellType.O:
                        return Properties.Resources.O;
                    default:
                        return Properties.Resources.QM;
                }
            }
            private string ConvertPlayerTypeToSTring()
            {
                switch(playerType)
                {
                    case enCellType.X:
                        return "X";
                    case enCellType.O:
                        return "O";

                    default:
                        return "QM";
                }
            }
            public PictureBox UpdatePictureBox(PictureBox pictureBox)
            {
                string PlayerTypeString = ConvertPlayerTypeToSTring();
                pictureBox.Tag = PlayerTypeString;
                pictureBox.Image = PictureBoxImage();
                return pictureBox;
            }

            public bool IsPlayerActive()
            {
                return this.playerState == enPlayerState.Active;
            }
        }
        private struct stGridCells
        {
            public enCellType Cell1;
            public enCellType Cell2;
            public enCellType Cell3;
            public enCellType Cell4;
            public enCellType Cell5;
            public enCellType Cell6;
            public enCellType Cell7;
            public enCellType Cell8;
            public enCellType Cell9;
            public byte SettedCells;

            public bool IsFirstRowHasEqualValues()
            {
                return this.Cell1 == this.Cell2 && this.Cell2 == this.Cell3;
            }
            public bool IsSecondRowHasEqualValues()
            {
                return this.Cell4 == this.Cell5 && this.Cell5 == this.Cell6;
            }
            public bool IsThirdRowHasEqualValues()
            {
                return this.Cell7 == this.Cell8 && this.Cell8 == this.Cell9;
            }
            public bool IsFirstColHasEqualValues()
            {
                return this.Cell1 == this.Cell4 && this.Cell4 == this.Cell7;
            }
            public bool IsSecondColHasEqualValues()
            {
                return this.Cell2 == this.Cell5 && this.Cell5 == this.Cell8;
            }
            public bool IsThirdColHasEqualValues()
            {
                return this.Cell3 == this.Cell6 && this.Cell6 == this.Cell9;
            }
            public bool IsMainDaigonalHasEqualValues()
            {
                return this.Cell1 == this.Cell5 && this.Cell4 == this.Cell9;
            }
            public bool IsSecondaryDaigonalHasEqualValues()
            {
                return this.Cell1 == this.Cell5 && this.Cell4 == this.Cell9;
            }
            public bool IsSettedCellsMoreThan3()
            {
                return this.SettedCells >= 3;
            }
        }

        private stPlayer firstPlayer = new stPlayer();
        private stPlayer secondPlayer = new stPlayer();
        private stGridCells GridCells = new stGridCells();

        private void IntializeFirstPlayer()
        {
            firstPlayer.playerName = enPlayerName.Player1;
            firstPlayer.playerType = enCellType.X;
            firstPlayer.playerState = enPlayerState.Active;
        }
        private void IntializeSecondPlayer()
        {
            secondPlayer.playerName = enPlayerName.Player2;
            secondPlayer.playerType = enCellType.O;
            secondPlayer.playerState = enPlayerState.Unactive;
        }
        private void IntializeGridCells()
        {
            GridCells.Cell1 = enCellType.QM;
            GridCells.Cell2 = enCellType.QM;
            GridCells.Cell3 = enCellType.QM;
            GridCells.Cell4 = enCellType.QM;
            GridCells.Cell5 = enCellType.QM;
            GridCells.Cell6 = enCellType.QM;
            GridCells.Cell7 = enCellType.QM;
            GridCells.Cell8 = enCellType.QM;
            GridCells.Cell9 = enCellType.QM;

            GridCells.SettedCells = 0;
        }
        public frmMain()
        {
            IntializeFirstPlayer();
            IntializeSecondPlayer();
            IntializeGridCells();


            InitializeComponent();
        }
        private void initializeCellContainer()
        {
            lblCellContainer.Width = 300;
            lblCellContainer.Height = 300;
          
        }
        private void SetPicturesInGrid()
        {
            int cellSize = lblCellContainer.Width / 3;
            int cellDoubleSize = cellSize + cellSize;
            int ExtraDistance = 19;
            pbPicture1.Location = new Point(lblCellContainer.Location.X + ExtraDistance, lblCellContainer.Location.Y + ExtraDistance);
            pbPicture2.Location = new Point(lblCellContainer.Location.X + cellSize + ExtraDistance, lblCellContainer.Location.Y + ExtraDistance);
            pbPicture3.Location = new Point(lblCellContainer.Location.X + cellDoubleSize + ExtraDistance, lblCellContainer.Location.Y + ExtraDistance);

            pbPicture4.Location = new Point(lblCellContainer.Location.X + ExtraDistance, lblCellContainer.Location.Y + cellSize + ExtraDistance);
            pbPicture5.Location = new Point(lblCellContainer.Location.X + cellSize + ExtraDistance, lblCellContainer.Location.Y + cellSize + ExtraDistance);
            pbPicture6.Location = new Point(lblCellContainer.Location.X + cellDoubleSize + ExtraDistance, lblCellContainer.Location.Y + cellSize + ExtraDistance);

            pbPicture7.Location = new Point(lblCellContainer.Location.X + ExtraDistance, lblCellContainer.Location.Y + cellDoubleSize + ExtraDistance);
            pbPicture8.Location = new Point(lblCellContainer.Location.X + cellSize + ExtraDistance, lblCellContainer.Location.Y + cellDoubleSize + ExtraDistance);
            pbPicture9.Location = new Point(lblCellContainer.Location.X + cellSize + cellSize + ExtraDistance, lblCellContainer.Location.Y + cellDoubleSize + ExtraDistance);
        }
        private void frmMain_Load(object sender, EventArgs e)
        {

            initializeCellContainer();
            SetPicturesInGrid();

        }

     
        private void DrawGrid(PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black, 2);
            Graphics drawLine = e.Graphics;

            float cellSize = lblCellContainer.Width / 3f;
            PointF verticalPoint1;
            PointF verticalPoint2;

            PointF horizontalPoint1;
            PointF horizontalPoint2;


            for (int i = 0; i < 2; i++)
            {
                verticalPoint1 = new PointF(cellSize, 0.0f);
                verticalPoint2 = new PointF(cellSize, lblCellContainer.Height);

                horizontalPoint1 = new PointF(0.0f, cellSize);
                horizontalPoint2 = new PointF(lblCellContainer.Width, cellSize);

                drawLine.DrawLine(pen, verticalPoint1, verticalPoint2);
                drawLine.DrawLine(pen, horizontalPoint1, horizontalPoint2);
                cellSize += cellSize;
            }
        }
        private void lblCellContainer_Paint(object sender, PaintEventArgs e)
        {
            DrawGrid(e);
           
        }
        
        
        private PictureBox UpdatePictureCell(PictureBox pictureBox)
        {
            if(firstPlayer.IsPlayerActive())
            {
                pictureBox = firstPlayer.UpdatePictureBox(pictureBox);
                firstPlayer.playerState = enPlayerState.Unactive;
                secondPlayer.playerState = enPlayerState.Active;
                
                
            }
            else
            {
                pictureBox = secondPlayer.UpdatePictureBox(pictureBox);
                secondPlayer.playerState = enPlayerState.Unactive;
                firstPlayer.playerState = enPlayerState.Active;

                
            }

            return pictureBox;
        }
       
        
        private void EnablePlayer1Button()
        {
            btnPlayer2.Enabled = false;
            btnPlayer2.BackColor = Color.Gray;

            btnPlayer1.Enabled = true;
            btnPlayer1.BackColor = Color.RoyalBlue;
        }
        private void EnablePlayer2Button()
        {
            btnPlayer1.Enabled = false;
            btnPlayer1.BackColor = Color.Gray;

            btnPlayer2.Enabled = true;
            btnPlayer2.BackColor = Color.DarkRed;
        }
        private void UpdatePlayerButtonState()
        {
            if(firstPlayer.IsPlayerActive())
            {
                EnablePlayer1Button();
        
            }
            else
            {
                EnablePlayer2Button();
            }
        }
        private enCellType GetCellType()
        {
            return firstPlayer.IsPlayerActive() ? firstPlayer.playerType : secondPlayer.playerType;
        }
       
        private void pbPicture1_Click(object sender, EventArgs e)
        {
            pbPicture1 = UpdatePictureCell(pbPicture1);
            UpdatePlayerButtonState();

            GridCells.Cell1 = GetCellType();
            GridCells.SettedCells++;
        }

        private void pbPicture2_Click(object sender, EventArgs e)
        {
            pbPicture2 = UpdatePictureCell(pbPicture2);
            UpdatePlayerButtonState();
        }

        private void pbPicture3_Click(object sender, EventArgs e)
        {
            pbPicture3 = UpdatePictureCell(pbPicture3);
            UpdatePlayerButtonState();
        }

        private void pbPicture4_Click(object sender, EventArgs e)
        {
            pbPicture4 = UpdatePictureCell(pbPicture4);
            UpdatePlayerButtonState();
        }

        private void pbPicture5_Click(object sender, EventArgs e)
        {
            pbPicture5 = UpdatePictureCell(pbPicture5);
            UpdatePlayerButtonState();
        }

        private void pbPicture6_Click(object sender, EventArgs e)
        {
            pbPicture6 = UpdatePictureCell(pbPicture6);
            UpdatePlayerButtonState();
        }

        private void pbPicture7_Click(object sender, EventArgs e)
        {
            pbPicture7 = UpdatePictureCell(pbPicture7);
            UpdatePlayerButtonState();
        }

        private void pbPicture8_Click(object sender, EventArgs e)
        {
            pbPicture8 = UpdatePictureCell(pbPicture8);
            UpdatePlayerButtonState();
        }

        private void pbPicture9_Click(object sender, EventArgs e)
        {
            pbPicture9 = UpdatePictureCell(pbPicture9);
            UpdatePlayerButtonState();
        }

    }
}

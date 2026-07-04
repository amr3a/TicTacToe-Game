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
        public enum enPlayerType
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
        private struct Player
        {
           public enPlayerName playerName;
           public enPlayerType playerType;
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
                    case enPlayerType.X:
                       return Properties.Resources.X;
                    case enPlayerType.O:
                        return Properties.Resources.O;
                    default:
                        return Properties.Resources.QM;
                }
            }
            private string ConvertPlayerTypeToSTring()
            {
                switch(playerType)
                {
                    case enPlayerType.X:
                        return "X";
                    case enPlayerType.O:
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


        }
        private Player firstPlayer = new Player();
        private Player secondPlayer = new Player();
        public frmMain()
        {
            firstPlayer.playerName = enPlayerName.Player1;
            firstPlayer.playerType = enPlayerType.X;
            firstPlayer.playerState = enPlayerState.Active;

            secondPlayer.playerName = enPlayerName.Player2;
            secondPlayer.playerType = enPlayerType.O;
            secondPlayer.playerState = enPlayerState.Unactive;
            InitializeComponent();
        }
        private void initializeCellContainer()
        {
            lblCellContainer.Width = 260;
            lblCellContainer.Height = 260;
          
        }
        private void SetPicturesInGrid()
        {
            int cellSize = lblCellContainer.Width / 3;
            int cellDoubleSize = cellSize + cellSize;
            int ExtraDistance = 13;
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
            if(firstPlayer.playerState == enPlayerState.Active)
            {
                firstPlayer.playerState = enPlayerState.Unactive;
                secondPlayer.playerState = enPlayerState.Active;
                return  firstPlayer.UpdatePictureBox(pictureBox);
                
            }
            else
            {
                secondPlayer.playerState = enPlayerState.Unactive;
                firstPlayer.playerState = enPlayerState.Active;

                return secondPlayer.UpdatePictureBox(pictureBox);
            }
        }
        private void pbPicture1_Click(object sender, EventArgs e)
        {
            pbPicture1 = UpdatePictureCell(pbPicture1);
        }

        private void pbPicture2_Click(object sender, EventArgs e)
        {
            pbPicture2 = UpdatePictureCell(pbPicture2);
        }

        private void pbPicture3_Click(object sender, EventArgs e)
        {
            pbPicture3 = UpdatePictureCell(pbPicture3);
        }

        private void pbPicture4_Click(object sender, EventArgs e)
        {
            pbPicture4 = UpdatePictureCell(pbPicture4);
        }

        private void pbPicture5_Click(object sender, EventArgs e)
        {
            pbPicture5 = UpdatePictureCell(pbPicture5);
        }

        private void pbPicture6_Click(object sender, EventArgs e)
        {
            pbPicture6 = UpdatePictureCell(pbPicture6);
        }

        private void pbPicture7_Click(object sender, EventArgs e)
        {
            pbPicture7 = UpdatePictureCell(pbPicture7);
        }

        private void pbPicture8_Click(object sender, EventArgs e)
        {
            pbPicture8 = UpdatePictureCell(pbPicture8);
        }

        private void pbPicture9_Click(object sender, EventArgs e)
        {
            pbPicture9 = UpdatePictureCell(pbPicture9);
        }

    }
}

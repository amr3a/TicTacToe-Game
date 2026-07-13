using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Tic_Tac_Toe_Game
{  
 


    public partial class frmMain : Form
    {
        private enum enCellType
        {
            QM,
            X,
            O
        }
        private enum enPlayerName
        {
            Player1,
            Player2,
            Computer,
            UnKnownPlayer
        }
        private enum enPlayerState
        {
            Active,
            Unactive
        }
        private enum enGameState
        {
            UnKnown,
            Win,
            Draw,

        }
  

        private struct stGame 
        {
            public struct stPlayer
            {
                public enPlayerName playerName;
                public enCellType playerType;
                public enPlayerState playerState;
                public enGameState gameWinner;

                public string PlayerName()
                {
                    switch (playerName)
                    {
                        case enPlayerName.Player1:
                            return "Player1";
                        case enPlayerName.Player2:
                            return "Player2";
                        case enPlayerName.Computer:
                            return "Computer";
                        default:
                            return "Unknown Player";
                    }
                }
                public string PlayerType()
                {
                    switch (playerType)
                    {
                        case enCellType.X:
                            return "X";
                        case enCellType.O:
                            return "O";
                        default:
                            return "QM";
                    }
                }
                public Image PictureBoxImage()
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
              
                public void UpdatePictureBox(PictureBox pictureBox)
                {
                    string PlayerTypeString = PlayerType();
                    pictureBox.Tag = PlayerTypeString;
                    pictureBox.Image = PictureBoxImage();
                 
                }

                public bool IsPlayerActive()
                {
                    return this.playerState == enPlayerState.Active;
                }
                public bool IsPlayerWinner()
                {
                    return this.gameWinner == enGameState.Win;
                }
            }
        
            public stPlayer firstPlayer;
            public stPlayer secondPlayer;
            public bool gameIsEnded;
            public byte SettedCells;
            private void IntializeFirstPlayer()
            {
                firstPlayer.playerName = enPlayerName.Player1;
                firstPlayer.playerType = enCellType.X;
                firstPlayer.playerState = enPlayerState.Active;
                firstPlayer.gameWinner = enGameState.UnKnown;
            }
            private void IntializeSecondPlayer()
            {
                secondPlayer.playerName = enPlayerName.Player2;
                secondPlayer.playerType = enCellType.O;
                secondPlayer.playerState = enPlayerState.Unactive;
                secondPlayer.gameWinner = enGameState.UnKnown;
            }
          
            public enCellType GetCellType()
            {
                return firstPlayer.IsPlayerActive() ? firstPlayer.playerType : secondPlayer.playerType;
            }
            public bool IsSettedCellsMoreThan3()
            {
                return SettedCells >= 3;
            }
            public void InatializeGameMembers()
            {
                SettedCells = 0;
                gameIsEnded = false;
                IntializeFirstPlayer();
                IntializeSecondPlayer();
                
               
            }

        
            public void UpdatePictureCell(PictureBox pictureBox)
            {
                if (firstPlayer.IsPlayerActive())
                {
                     firstPlayer.UpdatePictureBox(pictureBox);
                }
                else
                {
                    secondPlayer.UpdatePictureBox(pictureBox);
                }
                SettedCells++;
            }
            bool CheckIfAllCellsAreEquallySetted(PictureBox Picture1, PictureBox Picture2, PictureBox Picture3)
            {
                return Picture1.Tag.ToString() != "?" && Picture1.Tag.ToString() == Picture2.Tag.ToString() 
                    && Picture2.Tag.ToString() == Picture3.Tag.ToString() ;
            }
            private Color GetColor()
            {
                return Color.YellowGreen;
            }
            private void SetWinnerCellsColor(PictureBox Picture1, PictureBox Picture2, PictureBox Picture3)
            {
                Color WinnerColor = GetColor();
                Picture1.BackColor = WinnerColor;
                Picture2.BackColor = WinnerColor;
                Picture3.BackColor = WinnerColor;
            }
            public bool WeHaveWinner(PictureBox Picture1, PictureBox Picture2, PictureBox Picture3)
            {
                if(CheckIfAllCellsAreEquallySetted(Picture1, Picture2, Picture3))
                {
     
                    if (firstPlayer.PlayerType() == Picture1.Tag.ToString())
                    {
                        SetWinnerCellsColor(Picture1, Picture2, Picture3);
                        firstPlayer.gameWinner = enGameState.Win;
                        secondPlayer.gameWinner = enGameState.UnKnown;
                        
                    }
                    else if(secondPlayer.PlayerType() == Picture1.Tag.ToString())
                    {
                        SetWinnerCellsColor(Picture1, Picture2, Picture3);
                        secondPlayer.gameWinner = enGameState.Win;
                        firstPlayer.gameWinner = enGameState.UnKnown;

                    }
                
                    return true;
                }
                else if (SettedCells == 9)
                {
                    secondPlayer.gameWinner = enGameState.Draw;
                    firstPlayer.gameWinner = enGameState.Draw;
                    return true;
                }
                else
                {
                    secondPlayer.gameWinner = enGameState.UnKnown;
                    firstPlayer.gameWinner = enGameState.UnKnown;
                    return false;
                }
                
               
            }
            public bool isGameDarw()
            {
                return firstPlayer.gameWinner == enGameState.Draw && secondPlayer.gameWinner == enGameState.Draw;
            }
            public bool IsGameHasWinner()
            {
                return firstPlayer.gameWinner == enGameState.Win || secondPlayer.gameWinner == enGameState.Win;
            }
            public string WhoIsGameWinner()
            {
                if(firstPlayer.IsPlayerWinner())
                {
                    return firstPlayer.PlayerName();
                }
                else if (secondPlayer.IsPlayerWinner())
                {
                    return secondPlayer.PlayerName();
                }
                else
                {
                    return "No Winner";
                }
            }
            
        }

    

        private stGame Game = new stGame();


        public frmMain()
        {
            Game.InatializeGameMembers();
           
            InitializeComponent();
            initializeCellContainer();
            SetPicturesInGrid();
        }
        private void initializeCellContainer()
        {
            lblCellContainer.Width = 320;
            lblCellContainer.Height = lblCellContainer.Width;
          
        }
        private void initializePlayersButtons()
        {
            btnPlayer1.Text = Game.firstPlayer.PlayerName();
            btnPlayer2.Text = Game.secondPlayer.PlayerName();
            
        }
        private void SetPicturesInGrid()
        {
            int cellSize = lblCellContainer.Width / 3;
            int cellDoubleSize = cellSize + cellSize;
            int ExtraDistance = 25;

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
        private void initializePicturesCells()
        {
            pbPicture1.Image = Properties.Resources.QM;
            pbPicture2.Image = Properties.Resources.QM;
            pbPicture3.Image = Properties.Resources.QM;
            pbPicture4.Image = Properties.Resources.QM;
            pbPicture5.Image = Properties.Resources.QM;
            pbPicture7.Image = Properties.Resources.QM;
            pbPicture6.Image = Properties.Resources.QM;
            pbPicture8.Image = Properties.Resources.QM;
            pbPicture9.Image = Properties.Resources.QM;

            pbPicture1.BackColor = Color.FloralWhite;
            pbPicture2.BackColor = Color.FloralWhite;
            pbPicture3.BackColor = Color.FloralWhite;
            pbPicture4.BackColor = Color.FloralWhite;
            pbPicture5.BackColor = Color.FloralWhite;
            pbPicture6.BackColor = Color.FloralWhite;
            pbPicture7.BackColor = Color.FloralWhite;
            pbPicture8.BackColor = Color.FloralWhite;
            pbPicture9.BackColor = Color.FloralWhite;

            pbPicture1.Tag = "?";
            pbPicture2.Tag = "?";
            pbPicture3.Tag = "?";
            pbPicture4.Tag = "?";
            pbPicture5.Tag = "?";
            pbPicture6.Tag = "?";
            pbPicture7.Tag = "?";
            pbPicture8.Tag = "?";
            pbPicture9.Tag = "?";


        }
        private void IntializePlayersTurn()
        {
            ChangeTurnToPlayer1();
        }
        private void ResetControls()
        {
            Game.InatializeGameMembers();
            EnablePlayer1Button();
            DisablePlayer2Button();
            initializePicturesCells();
            initializePlayersButtons();
            IntializePlayersTurn();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            ResetControls();

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
        private void ActivatePlayer1()
        {
            Game.firstPlayer.playerState = enPlayerState.Active;
            Game.secondPlayer.playerState = enPlayerState.Unactive;
        }
        private void ActivatePlayer2()
        {
            Game.secondPlayer.playerState = enPlayerState.Active;
            Game.firstPlayer.playerState = enPlayerState.Unactive;
        }
  
        private void ActivateDisactivatePlayers()
        {
            if (Game.firstPlayer.IsPlayerActive())
            {
                ActivatePlayer2();
            }
            else
            {
                ActivatePlayer1();
            }
        }
   

        private void DisablePlayer1Button()
        {
            
            btnPlayer1.BackColor = Color.FloralWhite;
            btnPlayer1.FlatAppearance.BorderColor = Color.DarkRed;
            btnPlayer1.FlatAppearance.BorderSize = 1;
            btnPlayer1.FlatAppearance.MouseDownBackColor = btnPlayer1.BackColor;
            btnPlayer1.FlatAppearance.MouseOverBackColor = btnPlayer1.BackColor;
            btnPlayer1.ForeColor = Color.Black;
            btnPlayer1.Font = new Font(btnPlayer1.Font, FontStyle.Bold);


        }
        private void EnablePlayer1Button()
        {
            btnPlayer1.BackColor = Color.DarkRed;
            btnPlayer1.FlatAppearance.MouseDownBackColor = btnPlayer1.BackColor;
            btnPlayer1.FlatAppearance.MouseOverBackColor = btnPlayer1.BackColor;
            btnPlayer1.ForeColor = Color.FloralWhite;
            btnPlayer1.Font = new Font(btnPlayer1.Font, FontStyle.Regular);
        }
        private void DisablePlayer2Button()
        {
            btnPlayer2.BackColor = Color.FloralWhite;
            btnPlayer2.FlatAppearance.BorderColor = Color.DarkRed;
            btnPlayer2.FlatAppearance.BorderSize = 1;
            btnPlayer2.FlatAppearance.MouseDownBackColor = btnPlayer2.BackColor;
            btnPlayer2.FlatAppearance.MouseOverBackColor = btnPlayer2.BackColor;
            btnPlayer2.ForeColor = Color.Black;
            btnPlayer2.Font = new Font(btnPlayer1.Font, FontStyle.Bold);
           
        }
        private void EnablePlayer2Button()
        {
            btnPlayer2.BackColor = Color.DarkRed;
            btnPlayer2.FlatAppearance.MouseDownBackColor = btnPlayer1.BackColor;
            btnPlayer2.FlatAppearance.MouseOverBackColor = btnPlayer1.BackColor;
            btnPlayer2.ForeColor = Color.FloralWhite;
            btnPlayer2.Font = new Font(btnPlayer1.Font, FontStyle.Regular);
        }
        private Image GetPlayer1PictureTurn()
        {

            if (Game.firstPlayer.playerType == enCellType.X)
                return Properties.Resources.rx;
            else
                return Properties.Resources.ro;

        }
        private Image GetPlayer2PictureTurn()
        {

            if (Game.secondPlayer.playerType == enCellType.O)
                return Properties.Resources.ro;
            else
                return Properties.Resources.rx;

        }
        private void ChangeTurnToPlayer1()
        {
            pbPlayerPicture.Image = GetPlayer1PictureTurn();
            btnPlayersTurn.Text = Game.firstPlayer.PlayerName();
        }
        private void ChangeTurnToPlayer2()
        {
            pbPlayerPicture.Image = GetPlayer2PictureTurn();
            btnPlayersTurn.Text = Game.secondPlayer.PlayerName();
        }
        private void ChangePlayerTurn()
        {
            if(Game.firstPlayer.IsPlayerActive())
            {
                DisablePlayer2Button();
                EnablePlayer1Button();
                ChangeTurnToPlayer1();
            }
            else
            {
                DisablePlayer1Button();
                EnablePlayer2Button();
                ChangeTurnToPlayer2();
            }
        }
        
       
  
      

        private void UpdateGameScore()
        {
            ushort Player1Score = 0;
            ushort Player2Score = 0;
            ushort DrawScore = 0;
            if(Game.IsGameHasWinner())
            {
                if(Game.firstPlayer.IsPlayerWinner())
                {
                    Player1Score++;
                    btnPlayer1Score.Tag = Convert.ToInt16(btnPlayer1Score.Tag) + Player1Score;
                    btnPlayer1Score.Text = btnPlayer1Score.Tag.ToString();
                }
                else if(Game.secondPlayer.IsPlayerWinner())
                {
                    Player2Score++;
                    btnPlayer2Score.Tag = Convert.ToInt16(btnPlayer2Score.Tag) + Player2Score;
                    btnPlayer2Score.Text = btnPlayer2Score.Tag.ToString();
                }
            }
            else if(Game.isGameDarw())
            {
                DrawScore++;
                btnDrawScore.Tag = Convert.ToInt16(btnDrawScore.Tag) + DrawScore;
                btnDrawScore.Text = btnDrawScore.Tag.ToString();
            }

          
        }

        private void DetermineGameWinner()
        { 
            if(Game.WeHaveWinner(pbPicture1, pbPicture2, pbPicture3) || Game.WeHaveWinner(pbPicture4, pbPicture5, pbPicture6) || Game.WeHaveWinner(pbPicture7, pbPicture8, pbPicture9)
                || Game.WeHaveWinner(pbPicture1, pbPicture4, pbPicture7) || Game.WeHaveWinner(pbPicture2, pbPicture5, pbPicture8) || Game.WeHaveWinner(pbPicture3, pbPicture6, pbPicture9)
                || Game.WeHaveWinner(pbPicture1, pbPicture5, pbPicture9) || Game.WeHaveWinner(pbPicture3, pbPicture5, pbPicture7))
            {
                Game.gameIsEnded = true;
            }
        }
        private void GameWinner()
        {
            if(Game.IsSettedCellsMoreThan3() || Game.gameIsEnded)
            {
                DetermineGameWinner();
                if (Game.IsGameHasWinner())
                {
                    
                    if (Game.firstPlayer.IsPlayerWinner())
                    {
                        ActivatePlayer1();
                        ChangePlayerTurn();
                    }
                    else
                    {
                        ActivatePlayer2();
                        ChangePlayerTurn();
                    }
                }
                else if(Game.isGameDarw())
                {
                    EnablePlayer1Button();
                    EnablePlayer2Button();
                }
                else
                {
                    return;
                }
                UpdateGameScore();
                ShowGameOverMessage();
            }
        }
        private void ShowErrorOfSelectedCellMessage()
        {
            MessageBox.Show("This cell ia aready selected", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private string GetGameStateMessage()
        {
            if (Game.IsGameHasWinner())
            {
                return Game.WhoIsGameWinner() + " is Winner";
            }
            else if (Game.isGameDarw())
            {
                return "Game is Draw";
            }
            else
            {
                return "Unknown Game State";
            }
        }
        private void ShowGameOverMessage()
        {
            string message = GetGameStateMessage();
            DialogResult result = MessageBox.Show(message, "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if(result == DialogResult.OK)
            {
                ResetControls();
            }
        }
        private void ChangeImage(PictureBox Picture )
        {
            if (Picture.Tag.ToString() == "?" && !Game.gameIsEnded)
            {

                Game.UpdatePictureCell(Picture);
                ActivateDisactivatePlayers();
                ChangePlayerTurn();

                GameWinner();
            }
            else if (Picture.Tag.ToString() != "?" && !Game.gameIsEnded)
            {
                ShowErrorOfSelectedCellMessage();
            }
            else
            {
                ShowGameOverMessage();
            }
        }
        private void pbPicture_Click(object sender, EventArgs e)
        {
            ChangeImage((PictureBox)sender);
        }

        private void ResetPlayersScore()
        {
            btnPlayer1Score.Tag = 0;
            btnPlayer1Score.Text = "0";

            btnPlayer2Score.Tag = 0;
            btnPlayer2Score.Text = "0";

            btnDrawScore.Tag = 0;
            btnDrawScore.Text = "0";
        }
        private void ResetGame()
        {
            DialogResult Result = MessageBox.Show("Are you sure for restarting the Game? ", "Warnning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if(Result == DialogResult.OK)
            {
                ResetControls();
                ResetPlayersScore();
            }
           
        }
        private void lblRestartGame_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ResetGame();
        }
    }
}

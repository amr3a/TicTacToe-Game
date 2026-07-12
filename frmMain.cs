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
        private enum enCompletedCells
        {
            unKnown,
            FirstRow,
            SecondRow,
            ThirdRow,
            FirstCol,
            SecondCol,
            ThirdCol,
            MainDiagonal,
            SecondaryDiagonal
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
                private string ConvertPlayerTypeToSTring()
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
                public bool IsPlayerWinner()
                {
                    return this.gameWinner == enGameState.Win;
                }
            }
            public struct stGridCells
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
                    if (this.Cell1 != enCellType.QM)
                        return this.Cell1 == this.Cell2 && this.Cell2 == this.Cell3;
                    else
                        return false;
                }
                public bool IsSecondRowHasEqualValues()
                {
                    if (this.Cell4 != enCellType.QM)
                        return this.Cell4 == this.Cell5 && this.Cell5 == this.Cell6;
                    else
                        return false;
                }
                public bool IsThirdRowHasEqualValues()
                {
                    if (this.Cell7 != enCellType.QM)
                        return this.Cell7 == this.Cell8 && this.Cell8 == this.Cell9;
                    else
                        return false;
                }
                public bool IsFirstColHasEqualValues()
                {
                    if (this.Cell1 != enCellType.QM)
                        return this.Cell1 == this.Cell4 && this.Cell4 == this.Cell7;
                    else
                        return false;
                }
                public bool IsSecondColHasEqualValues()
                {
                    if (this.Cell2 != enCellType.QM)
                        return this.Cell2 == this.Cell5 && this.Cell5 == this.Cell8;
                    else
                        return false;
                }
                public bool IsThirdColHasEqualValues()
                {
                    if (this.Cell3 != enCellType.QM)
                        return this.Cell3 == this.Cell6 && this.Cell6 == this.Cell9;
                    else
                        return false;
                }
                public bool IsMainDaigonalHasEqualValues()
                {
                    if (this.Cell1 != enCellType.QM)
                        return this.Cell1 == this.Cell5 && this.Cell5 == this.Cell9;
                    else
                        return false;
                }
                public bool IsSecondaryDaigonalHasEqualValues()
                {
                    if (this.Cell3 != enCellType.QM)
                        return this.Cell3 == this.Cell5 && this.Cell5 == this.Cell7;
                    else
                        return false;
                }
                public bool IsSettedCellsMoreThan3()
                {
                    return this.SettedCells >= 3;
                }
                public bool IsCellNotSetted(enCellType cell)
                {
                    return cell == enCellType.QM;
                }
                public bool IsCellSetted(enCellType cell)
                {
                    return cell != enCellType.QM;
                }
            }
            public stPlayer firstPlayer;
            public stPlayer secondPlayer;
            public stGridCells GridCells;
            public enCompletedCells CompletedCells;
            public bool gameIsEnded;
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
            public enCellType GetCellType()
            {
                return firstPlayer.IsPlayerActive() ? firstPlayer.playerType : secondPlayer.playerType;
            }

            public void InatializeGameMembers()
            {
                CompletedCells = enCompletedCells.unKnown;
                gameIsEnded = false;
                IntializeFirstPlayer();
                IntializeSecondPlayer();
                IntializeGridCells();
               
            }

            public bool IsGameCompleted()
            {
                return !(GridCells.Cell1 == enCellType.QM || GridCells.Cell2 == enCellType.QM || GridCells.Cell3 == enCellType.QM ||
                       GridCells.Cell4 == enCellType.QM || GridCells.Cell5 == enCellType.QM || GridCells.Cell6 == enCellType.QM ||
                       GridCells.Cell7 == enCellType.QM || GridCells.Cell8 == enCellType.QM || GridCells.Cell9 == enCellType.QM);
               
            }
            public void DetermineTheGameWinners()
            {
                if (GridCells.IsSettedCellsMoreThan3())
                {
                   if(GridCells.IsFirstRowHasEqualValues())
                    {
                        CompletedCells = enCompletedCells.FirstRow;
                        if(GridCells.Cell1 == firstPlayer.playerType)
                        {
                            firstPlayer.gameWinner = enGameState.Win;
                            secondPlayer.gameWinner = enGameState.UnKnown;
                        }
                        else
                        {
                            secondPlayer.gameWinner = enGameState.Win;
                            firstPlayer.gameWinner = enGameState.UnKnown;
                        }
                        
                    }
                    else if (GridCells.IsSecondRowHasEqualValues())
                    {
                        CompletedCells = enCompletedCells.SecondRow;
                        if (GridCells.Cell4 == firstPlayer.playerType)
                        {
                            firstPlayer.gameWinner = enGameState.Win;
                            secondPlayer.gameWinner = enGameState.UnKnown;
                        }
                        else
                        {
                            secondPlayer.gameWinner = enGameState.Win;
                            firstPlayer.gameWinner = enGameState.UnKnown;
                        }
                        
                    }
                    else if (GridCells.IsThirdRowHasEqualValues())
                    {
                        CompletedCells = enCompletedCells.ThirdRow;
                        if (GridCells.Cell7 == firstPlayer.playerType)
                        {
                            firstPlayer.gameWinner = enGameState.Win;
                            secondPlayer.gameWinner = enGameState.UnKnown;
                        }
                        else
                        {
                            secondPlayer.gameWinner = enGameState.Win;
                            firstPlayer.gameWinner = enGameState.UnKnown;
                        }
                      
                    }
                    else if (GridCells.IsFirstColHasEqualValues())
                    {
                        CompletedCells = enCompletedCells.FirstCol;
                        if (GridCells.Cell1 == firstPlayer.playerType)
                        {
                            firstPlayer.gameWinner = enGameState.Win;
                            secondPlayer.gameWinner = enGameState.UnKnown;
                        }
                        else
                        {
                            secondPlayer.gameWinner = enGameState.Win;
                            firstPlayer.gameWinner = enGameState.UnKnown;
                        }
                       
                    }
                    else if (GridCells.IsSecondColHasEqualValues())
                    {
                        CompletedCells = enCompletedCells.SecondCol;
                        if (GridCells.Cell2 == firstPlayer.playerType)
                        {
                            firstPlayer.gameWinner = enGameState.Win;
                            secondPlayer.gameWinner = enGameState.UnKnown;
                        }
                        else
                        {
                            secondPlayer.gameWinner = enGameState.Win;
                            firstPlayer.gameWinner = enGameState.UnKnown;
                        }
                       
                    }
                    else if (GridCells.IsThirdColHasEqualValues())
                    {
                        CompletedCells = enCompletedCells.ThirdCol;
                        if (GridCells.Cell3 == firstPlayer.playerType)
                        {
                            firstPlayer.gameWinner = enGameState.Win;
                            secondPlayer.gameWinner = enGameState.UnKnown;
                        }
                        else
                        {
                            secondPlayer.gameWinner = enGameState.Win;
                            firstPlayer.gameWinner = enGameState.UnKnown;
                        }
                        
                    }
                    else if (GridCells.IsMainDaigonalHasEqualValues())
                    {
                        CompletedCells = enCompletedCells.MainDiagonal;
                        if (GridCells.Cell1 == firstPlayer.playerType)
                        {
                            firstPlayer.gameWinner = enGameState.Win;
                            secondPlayer.gameWinner = enGameState.UnKnown;
                        }
                        else
                        {
                            secondPlayer.gameWinner = enGameState.Win;
                            firstPlayer.gameWinner = enGameState.UnKnown;
                        }
                      
                    }
                    else if (GridCells.IsSecondaryDaigonalHasEqualValues())
                    {
                        CompletedCells = enCompletedCells.SecondaryDiagonal;
                        if (GridCells.Cell3 == firstPlayer.playerType)
                        {
                            firstPlayer.gameWinner = enGameState.Win;
                            secondPlayer.gameWinner = enGameState.UnKnown;
                        }
                        else
                        {
                            secondPlayer.gameWinner = enGameState.Win;
                            firstPlayer.gameWinner = enGameState.UnKnown;
                        }
                       
                    }
                    else if(IsGameCompleted())
                    {

                        firstPlayer.gameWinner = enGameState.Draw;
                        secondPlayer.gameWinner = enGameState.Draw;
                       
                    }
                    else
                    {
                        return;
                    }
                    gameIsEnded = true;
                }
                else
                {

                    firstPlayer.gameWinner = enGameState.UnKnown;
                    secondPlayer.gameWinner = enGameState.UnKnown;
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
        private PictureBox UpdatePictureCell(PictureBox pictureBox)
        {
            if(Game.firstPlayer.IsPlayerActive())
            {
                pictureBox = Game.firstPlayer.UpdatePictureBox(pictureBox);
            }
            else
            {
                pictureBox = Game.secondPlayer.UpdatePictureBox(pictureBox); 
            }

            return pictureBox;
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
        
        private Color GetColor()
        {
            return Color.GreenYellow;
        }
        private void FirstRowCompleted()
        {
            pbPicture1.BackColor = GetColor();
            pbPicture2.BackColor = GetColor();
            pbPicture3.BackColor = GetColor();
        }
        private void SecondRowCompleted()
        {
            pbPicture4.BackColor = GetColor();
            pbPicture5.BackColor = GetColor();
            pbPicture6.BackColor = GetColor();
        }
        private void ThirdRowCompleted()
        {
            pbPicture7.BackColor = GetColor();
            pbPicture8.BackColor = GetColor();
            pbPicture9.BackColor = GetColor();
        }
        private void FirstColCompleted()
        {
            pbPicture1.BackColor = GetColor();
            pbPicture4.BackColor = GetColor();
            pbPicture7.BackColor = GetColor();
        }
        private void SecondColCompleted()
        {
            pbPicture2.BackColor = GetColor();
            pbPicture5.BackColor = GetColor();
            pbPicture8.BackColor = GetColor();
        }
        private void ThirdColCompleted()
        {
            pbPicture3.BackColor = GetColor();
            pbPicture6.BackColor = GetColor();
            pbPicture9.BackColor = GetColor();
        }
        private void MainDiagonalCompleted()
        {
            pbPicture1.BackColor = GetColor();
            pbPicture5.BackColor = GetColor();
            pbPicture9.BackColor = GetColor();
        }
        private void SecondaryDiagonalCompleted()
        {
            pbPicture3.BackColor = GetColor();
            pbPicture5.BackColor = GetColor();
            pbPicture7.BackColor = GetColor();
        }
        private void ChangeCompletedCellsBackground()
        {
            switch (Game.CompletedCells) 
            {
                case enCompletedCells.FirstRow:
                    FirstRowCompleted();
                    break;
                case enCompletedCells.SecondRow:
                    SecondRowCompleted();
                    break;
                case enCompletedCells.ThirdRow:
                    ThirdRowCompleted();
                    break;
                case enCompletedCells.FirstCol:
                    FirstColCompleted();
                    break;
                case enCompletedCells.SecondCol:
                    SecondColCompleted();
                    break;
                case enCompletedCells.ThirdCol:
                    ThirdColCompleted();
                    break;
                case enCompletedCells.MainDiagonal:
                    MainDiagonalCompleted();
                    break;
                case enCompletedCells.SecondaryDiagonal:
                    SecondaryDiagonalCompleted();
                    break;

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
        private void GameWinner()
        {
            if(Game.GridCells.IsSettedCellsMoreThan3() || Game.IsGameCompleted())
            {
                Game.DetermineTheGameWinners();
                if (Game.IsGameHasWinner())
                {
                    ChangeCompletedCellsBackground();
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
        private void pbPicture1_Click(object sender, EventArgs e)
        {
            if(Game.GridCells.IsCellNotSetted(Game.GridCells.Cell1) && !Game.gameIsEnded) {

                pbPicture1 = UpdatePictureCell(pbPicture1);
                Game.GridCells.Cell1 = Game.GetCellType();
                Game.GridCells.SettedCells++;
                ActivateDisactivatePlayers();
                ChangePlayerTurn();

                GameWinner();
            }
            else if (Game.GridCells.IsCellSetted(Game.GridCells.Cell1) && !Game.gameIsEnded)
            {
                ShowErrorOfSelectedCellMessage();
            }
            else
            {
                ShowGameOverMessage();
            }
        }

        private void pbPicture2_Click(object sender, EventArgs e)
        {
            if (Game.GridCells.IsCellNotSetted(Game.GridCells.Cell2) &&  !Game.gameIsEnded)
            {
                pbPicture2 = UpdatePictureCell(pbPicture2);
          

                Game.GridCells.Cell2 = Game.GetCellType();
                Game.GridCells.SettedCells++;
                ActivateDisactivatePlayers();
                ChangePlayerTurn();
                GameWinner();
            }
            else if (Game.GridCells.IsCellSetted(Game.GridCells.Cell2) && !Game.gameIsEnded)
            {
                ShowErrorOfSelectedCellMessage();
            }
            else
            {
                ShowGameOverMessage();
            }
        }

        private void pbPicture3_Click(object sender, EventArgs e)
        {
            if (Game.GridCells.IsCellNotSetted(Game.GridCells.Cell3) && !Game.gameIsEnded)
            {
                pbPicture3 = UpdatePictureCell(pbPicture3);
               

                Game.GridCells.Cell3 = Game.GetCellType();
                Game.GridCells.SettedCells++;
                ActivateDisactivatePlayers();
                ChangePlayerTurn();
                GameWinner();
            }
            else if (Game.GridCells.IsCellSetted(Game.GridCells.Cell3) && !Game.gameIsEnded)
            {
                ShowErrorOfSelectedCellMessage();
            }
            else
            {
                ShowGameOverMessage();
            }
        }

        private void pbPicture4_Click(object sender, EventArgs e)
        {
            if (Game.GridCells.IsCellNotSetted(Game.GridCells.Cell4) && !Game.gameIsEnded)
            {
                pbPicture4 = UpdatePictureCell(pbPicture4);
            

                Game.GridCells.Cell4 = Game.GetCellType();
                Game.GridCells.SettedCells++;
                ActivateDisactivatePlayers();
                ChangePlayerTurn();
                GameWinner();
            }
            else if (Game.GridCells.IsCellSetted(Game.GridCells.Cell4) && !Game.gameIsEnded)
            {
                ShowErrorOfSelectedCellMessage();
            }
            else
            {
                ShowGameOverMessage();
            }
        }

        private void pbPicture5_Click(object sender, EventArgs e)
        {
            if (Game.GridCells.IsCellNotSetted(Game.GridCells.Cell5) && !Game.gameIsEnded)
            {
                pbPicture5 = UpdatePictureCell(pbPicture5);
       

                Game.GridCells.Cell5 = Game.GetCellType();
                Game.GridCells.SettedCells++;
                ActivateDisactivatePlayers();
                ChangePlayerTurn();
                GameWinner();
            }
            else if (Game.GridCells.IsCellSetted(Game.GridCells.Cell5) && !Game.gameIsEnded)
            {
                ShowErrorOfSelectedCellMessage();
            }
            else
            {
                ShowGameOverMessage();
            }

        }

        private void pbPicture6_Click(object sender, EventArgs e)
        {
            if (Game.GridCells.IsCellNotSetted(Game.GridCells.Cell6) && !Game.gameIsEnded)
            {
                pbPicture6 = UpdatePictureCell(pbPicture6);
             

                Game.GridCells.Cell6 = Game.GetCellType();
                Game.GridCells.SettedCells++;
                ActivateDisactivatePlayers();
                ChangePlayerTurn();
                GameWinner();
            }
            else if (Game.GridCells.IsCellSetted(Game.GridCells.Cell6) && !Game.gameIsEnded)
            {
                ShowErrorOfSelectedCellMessage();
            }
            else
            {
                ShowGameOverMessage();
            }
        }

        private void pbPicture7_Click(object sender, EventArgs e)
        {
            if (Game.GridCells.IsCellNotSetted(Game.GridCells.Cell7) && !Game.gameIsEnded)
            {
                pbPicture7 = UpdatePictureCell(pbPicture7);
              

                Game.GridCells.Cell7 = Game.GetCellType();
                Game.GridCells.SettedCells++;
                ActivateDisactivatePlayers();
                ChangePlayerTurn();
                GameWinner();
            }
            else if (Game.GridCells.IsCellSetted(Game.GridCells.Cell7) && !Game.gameIsEnded)
            {
                ShowErrorOfSelectedCellMessage();
            }
            else
            {
                ShowGameOverMessage();
            }
        }

        private void pbPicture8_Click(object sender, EventArgs e)
        {
            if (Game.GridCells.IsCellNotSetted(Game.GridCells.Cell8) && !Game.gameIsEnded)
            {
                pbPicture8 = UpdatePictureCell(pbPicture8);
               

                Game.GridCells.Cell8 = Game.GetCellType();
                Game.GridCells.SettedCells++;
                ActivateDisactivatePlayers();
                ChangePlayerTurn();
                GameWinner();
            }
            else if(Game.GridCells.IsCellSetted(Game.GridCells.Cell8) && !Game.gameIsEnded)
            {
                ShowErrorOfSelectedCellMessage();
            }
            else
            {
                ShowGameOverMessage();
            }
        }

        private void pbPicture9_Click(object sender, EventArgs e)
        {
            if (Game.GridCells.IsCellNotSetted(Game.GridCells.Cell9) && !Game.gameIsEnded)
            {
                pbPicture9 = UpdatePictureCell(pbPicture9);
              

                Game.GridCells.Cell9 = Game.GetCellType();
                Game.GridCells.SettedCells++;
                ActivateDisactivatePlayers();
                ChangePlayerTurn();
                GameWinner();
            }
            else if (Game.GridCells.IsCellSetted(Game.GridCells.Cell9) && !Game.gameIsEnded)
            {
                ShowErrorOfSelectedCellMessage();
            }
            else
            {
                ShowGameOverMessage();
            }
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Strategy
{

    public partial class Map : Form
    {
        Game game;
        int step = 0;
        int size;

        internal Character activePlayer;
        private bool isDragging = false;//dragging is in progress
        private bool isSelectingGoal = false;
        internal List<Squad> squadsNewArmy = new List<Squad>();

        public Map(int number, int cells)
        {
            InitializeComponent();
            MapManager.InitializeMap(this);
            size = cells;
            mapDataGridView.RowCount = size;
            mapDataGridView.ColumnCount = size;
            ResizeCells(size);
            
            game = new Game(size);
            GameOn.InitializeGame(game);
            game.StartGame(number);
            toolTipCamcel.SetToolTip(Cancelbtn, "Cansel sending army");

        }

        private void ResizeCells(int numberOfCells)
        {
            int sizeH;
            int sizeW;
            int min = 46;
            int width = mapDataGridView.Width;
            int height = mapDataGridView.Height;
            sizeH = height / numberOfCells;
            sizeW = width / numberOfCells;
            if (sizeH < min)
            {
                sizeH = min;
            }
            if (sizeW < min)
            {
                sizeW = min;
            }
            for (int i = 0; i < mapDataGridView.ColumnCount; i++)
            {
                mapDataGridView.Columns[i].Width = sizeW;
                mapDataGridView.Rows[i].Height = sizeH;
            }

        }
        
        private void ListBoxMessages_DrawItem(object sender, DrawItemEventArgs e)//customize
        {
            if (e.Index >= 0)
            {
                var lbox = (ListBox)sender;
                string text = lbox.Items[e.Index].ToString();
                bool isStep = Regex.IsMatch(text, "\\bStep\\b");
                var color = e.Index % 2 == 0 ? Color.LightGreen : SystemColors.Window;
                StringFormat format = new StringFormat(StringFormat.GenericDefault);
                format.Alignment = isStep ? StringAlignment.Center : StringAlignment.Near;
                using (var brush = new SolidBrush(color))
                    e.Graphics.FillRectangle(brush, e.Bounds);
                e.Graphics.DrawString(text, e.Font, SystemBrushes.WindowText, e.Bounds, format);
            }
        }
        
        private void ListBoxMessages_MeasureItem(object sender, MeasureItemEventArgs e)//calculate the height
        {
            var lbox = (ListBox)sender;
            var text = lbox.Items[e.Index].ToString();
            var width = lbox.ClientSize.Width-11;//-11 scroll
            var size = e.Graphics.MeasureString(text, lbox.Font, width);
            e.ItemHeight = (int)size.Height;
        }
       
        public bool CheckGoal(int xCoordinate, int yCoordinate) //check not empty cell
        {
            if (mapDataGridView[xCoordinate, yCoordinate].GetType() == typeof(DataGridViewImageCell))
            {
                return true;
            }
            else return false;
            
        }

        public void AddOrMoveOnMap(int XCoordinate, int YCoordinate, string filename, string tooltip, bool turn)//add picture to cell
        {
            Image picture = (Image)Properties.Resources.ResourceManager.GetObject(filename);
            if (picture != null)
            {
                DataGridViewImageCell PictureCell = new DataGridViewImageCell();
                if (turn)//go in left 
                {
                    picture.RotateFlip(RotateFlipType.RotateNoneFlipX);
                }
                PictureCell.Value = picture;
                PictureCell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                PictureCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                PictureCell.ToolTipText= tooltip;
                mapDataGridView[XCoordinate, YCoordinate] = PictureCell;
            }
           
        }

        internal void SetPlayer(Character player)
        {
            activePlayer = player;
        }
        public void ClearCell(int XCoordinate, int YCoordinate)//clear cell
        {
            DataGridViewTextBoxCell TextCell = new DataGridViewTextBoxCell();
            TextCell.Value = null;
            mapDataGridView[XCoordinate, YCoordinate] = TextCell;
            mapDataGridView.CurrentCell = mapDataGridView[0,0];
            
        }

        private void mapDataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            
            if (isDragging)// mouse on the edge
            {
                Dragging(sender, e.RowIndex, e.ColumnIndex);
            }
        }

        async void Dragging(object sender, int rowIndex, int columnIndex)
        {
            //mouse is on the one of border
            if ((rowIndex == mapDataGridView.FirstDisplayedScrollingRowIndex || rowIndex == mapDataGridView.FirstDisplayedScrollingRowIndex + mapDataGridView.DisplayedRowCount(true) - 1) || (columnIndex == mapDataGridView.FirstDisplayedScrollingColumnIndex || columnIndex == mapDataGridView.FirstDisplayedScrollingColumnIndex + mapDataGridView.DisplayedColumnCount(true) - 1))
            {
                await Task.Delay(1000);
                //top
                if (rowIndex == mapDataGridView.FirstDisplayedScrollingRowIndex)
                {
                    if (rowIndex > 0)
                    {
                       
                        mapDataGridView.FirstDisplayedScrollingRowIndex--;
                    }
                }
                else if (rowIndex == mapDataGridView.FirstDisplayedScrollingRowIndex + mapDataGridView.DisplayedRowCount(true) - 1)//bottom
                {
                    if (rowIndex < mapDataGridView.RowCount - 1)
                    {
                        
                        mapDataGridView.FirstDisplayedScrollingRowIndex++;
                    }
                }
                else if (columnIndex == mapDataGridView.FirstDisplayedScrollingColumnIndex)//left
                {
                    if (columnIndex > 0)
                    {
                        
                        mapDataGridView.FirstDisplayedScrollingColumnIndex--;
                    }
                }
                else if (columnIndex == mapDataGridView.FirstDisplayedScrollingColumnIndex + mapDataGridView.DisplayedColumnCount(true) - 1)//right
                {
                    if (columnIndex < mapDataGridView.ColumnCount - 1)
                    {
                       
                        mapDataGridView.FirstDisplayedScrollingColumnIndex++;
                    }
                }
            }
        }

        private void MapDataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                isDragging = true;//dragging is start
                Dragging(sender, e.RowIndex, e.ColumnIndex);
            }
            else
            {
                if (e.ColumnIndex == activePlayer.castle.GetCoordinates()[0] & e.RowIndex == activePlayer.castle.GetCoordinates()[1])//open castle
                {
                    if (!isSelectingGoal)//open castle
                    {
                        Buildes buildes = new Buildes(activePlayer);
                        buildes.Show();
                    }
                    else
                    {
                        MessageBox.Show("You can't attack your castle! ", "Attention", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    if (mapDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ValueType == typeof(Image))
                    {
                        if (isSelectingGoal)
                        {
                            var enemy = GameOn.GetEnemy(e.ColumnIndex, e.RowIndex, activePlayer);//(goal,enemy)
                            if (enemy.Item1 != null && enemy.Item2!=null)//goal
                            {
                                Army newArmy = new Army()
                                {
                                    Squads = squadsNewArmy,
                                    Goal = enemy.Item1,
                                    Enemy = enemy.Item2,
                                    Steps = 0,
                                    BackHome = false,
                                    Name = "ArmyPlayer"
                                };
                                newArmy.CountNecessarySteps();
                                if (!newArmy.CheckCoordinatesGoal())
                                {
                                    newArmy.Enemy.SendToCastle(newArmy);
                                }
                                bool side = false;
                                var coordinates = GetArmyCoordinates(newArmy.XCoordinateGoal, newArmy.YCoordinateGoal, activePlayer.castle.GetCoordinates(), ref side);
                                newArmy.XCoordinate = coordinates.Item1;
                                newArmy.YCoordinate = coordinates.Item2;
                                activePlayer.AddArmy(newArmy);
                                ListBoxMessages.Items.RemoveAt(0);
                                MessageSendArmy(activePlayer.name);
                                AddOrMoveOnMap(newArmy.XCoordinate, newArmy.YCoordinate, newArmy.Name + (activePlayer.id + 1), activePlayer.name + "'s army", side);
                                isSelectingGoal = false;
                                Cancelbtn.Visible = false;
                                Stepbtn.Enabled = true;
                            }
                            //message  in meth0d isGoal
                           

                        }
                    }
                }
            }

        }

        private void MapDataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            isDragging = false;
        }

        private void MapDataGridView_SizeChanged(object sender, EventArgs e)
        {
            ResizeCells(size);
        }
        public void ChooseGoal()
        {
            isSelectingGoal = true;
            Cancelbtn.Visible = true;
            Stepbtn.Enabled=false;
            MessageGoal();
        }
        internal void TakeListSquads(List<Squad> list)
        {
            squadsNewArmy = list;
        }
        
        public (int, int) GetArmyCoordinates(int xGoal, int yGoal, int[] coordinatesCastle, ref bool side)//give army start
        {
            //calculate the distances to the goal along the X and Y castle
            int deltaX = xGoal - coordinatesCastle[0];
            int deltaY = yGoal - coordinatesCastle[1];

            //determine the direction of movement along the X and Y castle
            int directionX = deltaX > 0 ? 1 : deltaX < 0 ? -1 : 0;
            int directionY = deltaY > 0 ? 1 : deltaY < 0 ? -1 : 0;

            //calculate start the army in the direction of the goal
            int x = coordinatesCastle[0] + directionX;
            int y = coordinatesCastle[1] + directionY;
            if (directionX < 0)
            {
                side = true;
            }

            return (x, y);
        }

        private void Cancelbtn_Click(object sender, EventArgs e)
        {
            isSelectingGoal = false;
            Cancelbtn.Visible = false;
            Stepbtn.Enabled = true;
            ListBoxMessages.Items.RemoveAt(0);
            activePlayer.ReturnUnitsInCastle(squadsNewArmy);
        }

        private void Stepbtn_Click(object sender, EventArgs e)
        {
            GameOn.Move();
            step++;
            MessageStep();
        }
        private void Guidebtn_Click(object sender, EventArgs e)
        {
            string tempPath = Path.Combine(Path.GetTempPath(), "Guide.docx");
            File.WriteAllBytes(tempPath, Properties.Resources.Guide);
            Process.Start(new ProcessStartInfo(tempPath) { UseShellExecute = true });
        }

        public void MessageStep()
        {
            ListBoxMessages.Items.Insert(0, "Step " + step);
        }

        public void MessageSendArmy(string name)
        {
            ListBoxMessages.Items.Insert(0, name + " send army.");
        }

        public void MessageDefeatArmy(string name, string enemy)
        {
            ListBoxMessages.Items.Insert(0, name + " defeated "+enemy+"'s army.");
        }

        public void MessageDestroyСastle(string name, string enemy)
        {
            ListBoxMessages.Items.Insert(0, name + "'s army destroyed "+enemy+"'s castle.");
            ListBoxMessages.Items.Insert(0, enemy+" lost.");
        }
        public void MessageBackToCastle(string name)
        {
            ListBoxMessages.Items.Insert(0, name + "'s army returns to castle.");
        }
        public void MessageDefendCastle(string name, string enemy)
        {
            ListBoxMessages.Items.Insert(0, name + " defended the castle from " + enemy +"'s army."); 
        }

        private void MessageGoal()
        {
            ListBoxMessages.Items.Insert(0, "Select a goal on the map!");
        }
        public void MessageNoGoal(string name)
        {
            ListBoxMessages.Items.Insert(0, name+ "'s goal is missing. Army returns to castle.");
        }

       
    }

    internal static class MapManager
    {
        private static Map mapInstance;
        public static void InitializeMap(Map map)
        {
            mapInstance = map;
        }
        private static void MessageError()
        {
            MessageBox.Show("Error: Game not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }

        public static bool CheckGoal(int X, int Y)
        {
            if (mapInstance != null)
            {
                return mapInstance.CheckGoal(X, Y);
            }
            else
            {
                MessageError();
                return false;
            }

        }
        public static void AddOrMoveOnMap(int X, int Y, string filename, string tooltip, bool side)
        {
            if (mapInstance != null)
            {
                mapInstance.AddOrMoveOnMap(X, Y, filename,tooltip, side);
            }
            else
            {
                MessageError();
            }
        }

        public static void ClearCell(int X, int Y)
        {
            if (mapInstance != null)
            {
                mapInstance.ClearCell(X, Y);
            }
            else
            {
                MessageError();
            }
        }

        public static void TakePlayer(Character player)
        {
            if (mapInstance != null)
            {
                mapInstance.SetPlayer(player);
            }
            else
            {
                MessageError();
            }
        }
        public static void ChooseArmyGoal(List<Squad> squadsNewArmy)
        {
            if (mapInstance != null)
            {
                mapInstance.ChooseGoal();
                mapInstance.TakeListSquads(squadsNewArmy);
            }
            else
            {
                MessageError();
            }
        }
        public static (int, int) GiveArmyCoordinates(int xGoal, int yGoal, int[] coordinatesCastle, ref bool side)
        {
            if (mapInstance != null)
            {
               return mapInstance.GetArmyCoordinates(xGoal, yGoal, coordinatesCastle, ref side);
            }
            else
            {
                MessageError();
                return (0,0);
            }
        }
        public static void MessageDefeatArmy(string name,string enemy)
        {
            if (mapInstance != null)
            {
                mapInstance.MessageDefeatArmy(name,enemy);
            }
            else
            {
                MessageError();
            }
        }
        public static void MessageDestroyCastle(string name, string enemy)
        {
            if (mapInstance != null)
            {
                mapInstance.MessageDestroyСastle(name, enemy);
            }
            else
            {
               MessageError();
            }
        }
        public static void MessageSendArmy(string name)
        {
            if (mapInstance != null)
            {
                mapInstance.MessageSendArmy(name);
            }
            else
            {
                MessageError();
            }
        }
        public static void MessageBackToCastle(string name)
        {
            if (mapInstance != null)
            {
                mapInstance.MessageBackToCastle(name);
            }
            else
            {
                MessageError();
            }
        }
        public static void MessageDefendCastle(string name, string enemy)
        {
            if (mapInstance != null)
            {
                mapInstance.MessageDefendCastle(name, enemy);
            }
            else
            {
                MessageError();
            }
        }
        public static void MessageNoGoal(string name)
        {
            if (mapInstance != null)
            {
                mapInstance.MessageNoGoal(name);
            }
            else
            {
                MessageError();
            }
        }

    }
}



using GameWPF.Logic;
using GameWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameWPF
{
    /// <summary>
    /// Interaction logic for AttackWindow.xaml
    /// </summary>
    public partial class AttackWindow : Window
    {
        public Enemy Enemy { get; set; }
        public MainWindow MainWindow { get; set; }
        public string URL { get; set; }
        public object UIHelper { get; set; }
        public int Distance { get; set; }
        public double SpeedOfMovement { get; set; }

        int numberOfSteps;

        int attackUnits;
        int defenceUnits;
        int speedUnits;

        public AttackWindow()
        {
            InitializeComponent();
        }

        public AttackWindow(Enemy enemy, MainWindow window)
        {
            InitializeComponent();
            Enemy = enemy;
            MainWindow = window;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                attackUnits = Convert.ToInt32(AttackUnitsTxt.Text);
                defenceUnits = Convert.ToInt32(DefenceUnitsTxt.Text);
                speedUnits = Convert.ToInt32(SpeedUnitsTxt.Text);

                if (attackUnits > 0 || defenceUnits > 0 || speedUnits > 0 && attackUnits <= MainWindow.Base.Army.AttackUnits
                    && defenceUnits <= MainWindow.Base.Army.DefenceUnits && speedUnits <= MainWindow.Base.Army.SpeedUnits)
                {
                    Army army = new Army(speedUnits, attackUnits, defenceUnits);
                    BattleLogic logic = new BattleLogic(MainWindow.Base,Enemy, army, MainWindow.enemies, MainWindow.GetGameStepDuration(), true, MainWindow, this);
                    logic.WarProcess();
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Количество юнитов не может быть меньше 0. \n" +
                        "Вы не можете отправить больше юнитове, чем есть в вашей армии.",
                                           "Confirmation",
                                           MessageBoxButton.OK,
                                           MessageBoxImage.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show("Количество юнитов не может быть меньше 0. \n",
                                           "Confirmation",
                                           MessageBoxButton.OK,
                                           MessageBoxImage.Exclamation);
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Speedlbl.Content = "Скорости: " + Convert.ToInt32(MainWindow.Base.Army.SpeedUnits);
            Attacklbl.Content = "Атаки: " + Convert.ToInt32(MainWindow.Base.Army.AttackUnits);
            Defencelbl.Content = "Защиты: " + Convert.ToInt32(MainWindow.Base.Army.DefenceUnits);
        }
        //private void SetImage(int row, int col, string image)
        //{
        //    Button btn = new Button();
        //    Image img = new Image();

        //    BitmapImage bitImg = new BitmapImage();
        //    bitImg.BeginInit();
        //    bitImg.UriSource = new Uri(image, UriKind.Relative);
        //    bitImg.EndInit();
        //    img.Stretch = Stretch.Fill;
        //    img.Source = bitImg;

        //    btn.Content = img;

        //    Grid.SetRow(btn, row);
        //    Grid.SetColumn(btn, col);

        //    MainWindow.grid.Children.Remove(btn);
        //    MainWindow.grid.Children.Add(btn);
        //}
        //private void CalculateDistance()
        //{
        //    Distance = Math.Abs(Enemy.Position[0] - MainWindow.Base.Position[0]) + Math.Abs(Enemy.Position[1] - MainWindow.Base.Position[1]);
        //}
        //private void GetSpeed()
        //{
        //    if (Convert.ToInt32(DefenceUnitsTxt.Text) > 0)
        //    {
        //        SpeedOfMovement = MainWindow.Base.Army.Defence.Speed;
        //    }
        //    else if (Convert.ToInt32(AttackUnitsTxt.Text) > 0)
        //    {
        //        SpeedOfMovement = MainWindow.Base.Army.Attack.Speed;
        //    }
        //    else if (Convert.ToInt32(SpeedUnitsTxt.Text) > 0)
        //    {
        //        SpeedOfMovement = MainWindow.Base.Army.Speed.Speed;
        //    }
        //}
        //private async void ArmyMovement(int[] firstPoint, int[] secondPoint)
        //{
        //    bool movementRight = false;
        //    bool movementDown = false;

        //    CalculateDistance();
        //    GetSpeed();

        //    int speed = (int)SpeedOfMovement;

        //    TimeSpan stepDuration = MainWindow.GetGameStepDuration();
        //    stepDuration = TimeSpan.FromTicks(stepDuration.Ticks / speed);

        //    if (firstPoint[0] - secondPoint[0] > 0)
        //    {
        //        movementRight = false;
        //    }
        //    else
        //    {
        //        movementRight = true;
        //    }

        //    if (firstPoint[1] - secondPoint[1] > 0)
        //    {
        //        movementDown = false;
        //    }
        //    else
        //    {
        //        movementDown = true;
        //    }

        //    int i = 0;
        //    int j = 0;
        //    int jMax = Math.Abs(firstPoint[1] - secondPoint[1]);
        //    int iMax = Math.Abs(firstPoint[0] - secondPoint[0]);
        //    int max = Math.Max(iMax, jMax);


        //    for (int move = 0; move < max; move++)
        //    {
        //        SetImage(MainWindow.Base.Position[0], MainWindow.Base.Position[1], "Image/ownCastle.png");
        //        if (movementDown == false)
        //        {
        //            if (movementRight == false)
        //            {
        //                if (i < iMax)
        //                {
        //                    SetImage(firstPoint[0] - i - 1, firstPoint[1] - j, "Image/ownKnight.png");
        //                    SetImage(firstPoint[0] - i, firstPoint[1] - j, "Image/grass.png");
        //                    SetImage(MainWindow.Base.Position[0], MainWindow.Base.Position[1], "Image/ownCastle.png");
        //                    await Task.Delay(stepDuration);
        //                    i++;
        //                }
        //                if (j < jMax)
        //                {
        //                    SetImage(firstPoint[0] - i, firstPoint[1] - j - 1, "Image/ownKnight.png");
        //                    SetImage(firstPoint[0] - i, firstPoint[1] - j, "Image/grass.png");
        //                    SetImage(MainWindow.Base.Position[0], MainWindow.Base.Position[1], "Image/ownCastle.png");
        //                    await Task.Delay(stepDuration);

        //                    j++;
        //                }
        //            }
        //            else if (movementRight == true)
        //            {
        //                if (i < iMax)
        //                {
        //                    SetImage(firstPoint[0] + i + 1, firstPoint[1] - j, "Image/ownKnight.png");
        //                    SetImage(firstPoint[0] + i, firstPoint[1] - j, "Image/grass.png");
        //                    SetImage(MainWindow.Base.Position[0], MainWindow.Base.Position[1], "Image/ownCastle.png");
        //                    await Task.Delay(stepDuration);
        //                    i++;
        //                }
        //                if (j < jMax)
        //                {
        //                    SetImage(firstPoint[0] + i, firstPoint[1] - j - 1, "Image/ownKnight.png");
        //                    SetImage(firstPoint[0] + i, firstPoint[1] - j, "Image/grass.png");
        //                    SetImage(MainWindow.Base.Position[0], MainWindow.Base.Position[1], "Image/ownCastle.png");
        //                    await Task.Delay(stepDuration);

        //                    j++;
        //                }
        //            }
        //        }
        //        else if (movementDown == true)
        //        {
        //            if (movementRight == false)
        //            {
        //                if (i < iMax)
        //                {
        //                    SetImage(firstPoint[0] - i - 1, firstPoint[1] + j, "Image/ownKnight.png");
        //                    SetImage(firstPoint[0] - i, firstPoint[1] + j, "Image/grass.png");
        //                    SetImage(MainWindow.Base.Position[0], MainWindow.Base.Position[1], "Image/ownCastle.png");
        //                    await Task.Delay(stepDuration);
        //                    i++;
        //                }
        //                if (j < jMax)
        //                {
        //                    SetImage(firstPoint[0] - i, firstPoint[1] + j + 1, "Image/ownKnight.png");
        //                    SetImage(firstPoint[0] - i, firstPoint[1] + j, "Image/grass.png");
        //                    SetImage(MainWindow.Base.Position[0], MainWindow.Base.Position[1], "Image/ownCastle.png");
        //                    await Task.Delay(stepDuration);

        //                    j++;
        //                }
        //            }
        //            else if (movementRight == true)
        //            {
        //                if (i < iMax)
        //                {
        //                    SetImage(firstPoint[0] + i + 1, firstPoint[1] + j, "Image/ownKnight.png");
        //                    SetImage(firstPoint[0] + i, firstPoint[1] + j, "Image/grass.png");
        //                    SetImage(MainWindow.Base.Position[0], MainWindow.Base.Position[1], "Image/ownCastle.png");
        //                    await Task.Delay(stepDuration);
        //                    i++;
        //                }
        //                if (j < jMax)
        //                {

        //                    SetImage(firstPoint[0] + i, firstPoint[1] + j + 1, "Image/ownKnight.png");
        //                    SetImage(firstPoint[0] + i, firstPoint[1] + j, "Image/grass.png");
        //                    SetImage(MainWindow.Base.Position[0], MainWindow.Base.Position[1], "Image/ownCastle.png");
        //                    await Task.Delay(stepDuration);

        //                    j++;
        //                }
        //            }
        //        }
        //    }
        //}
        //public double GetArmyPower()
        //{
        //    double power = attackUnits * MainWindow.Base.Army.Attack.Attack + speedUnits * MainWindow.Base.Army.Speed.Attack + defenceUnits * MainWindow.Base.Army.Defence.Attack;
        //    return power;
        //}
        //private double GetSurvived()
        //{
        //    double attack = GetArmyPower();
        //    Enemy.DefencePower();
        //    double defence = Enemy.Defence;

        //    double difference = attack - defence;
        //    double survived = difference / attack;
        //    return survived;
        //}
        //public double GetDefenceLeft(Army army, Base enemy)
        //{
        //    double attack = GetArmyPower();
        //    enemy.DefencePower();
        //    double defence = enemy.Defence;

        //    double difference = defence - attack - 100;
        //    double survived = difference / defence;
        //    return survived;
        //}
    }
}


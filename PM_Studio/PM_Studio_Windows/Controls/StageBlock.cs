using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace PM_Studio
{
    public class StageBlock : Border
    {

        #region Designing Variables

        StackPanel Container = new StackPanel();
        TextBlock lbVersion = new TextBlock();
        TextBlock lbDate = new TextBlock();
        TextBlock lbStageType = new TextBlock();

        #endregion

        #region Variables

        private Stage stage;

        #endregion

        #region Constructor

        public StageBlock(Stage _stage)
        {
            Stage = _stage;
            SetControlsProperties();
            FillBlockData();
            AddControlsToContainer();
        }

        #endregion

        #region Designing Methods

        void SetControlsProperties()
        {
            lbVersion.FontSize = 30;
            lbDate.FontSize = 20;
            lbStageType.FontSize = 15;

            lbVersion.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CFCFCF"));
            lbDate.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CFCFCF"));

            lbVersion.TextWrapping = System.Windows.TextWrapping.Wrap;
            lbDate.TextWrapping = System.Windows.TextWrapping.Wrap;
            lbDate.Margin = new System.Windows.Thickness(0, 10, 0, 10);

            this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6b6b6b"));
            this.CornerRadius = new System.Windows.CornerRadius(3);
            this.Padding = new System.Windows.Thickness(4);
        }

        void AddControlsToContainer()
        {
            Container.Children.Add(lbVersion);
            Container.Children.Add(lbDate);
            Container.Children.Add(lbStageType);

            this.Child = Container;
        }

        #endregion

        #region Methods

        void FillBlockData()
        {
            lbVersion.Text = "Version: " + Stage.Version;
            lbStageType.Text = Stage.StageType;
            lbDate.Text = Stage.StartDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture) + " till " + Stage.EndDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
            if(lbStageType.Text == "Alpha")
            {
                lbStageType.Foreground = Brushes.Red;
            }
            else if (lbStageType.Text == "Beta")
            {
                lbStageType.Foreground = Brushes.Blue;
            }
            if (lbStageType.Text == "Release")
            {
                lbStageType.Foreground = Brushes.Lime;
            }
            
        }

        #endregion

        #region Properties

        public Stage Stage
        {
            get
            {
                return stage;
            }
            set
            {
                stage = value;
            }
        }

        #endregion

    }
}

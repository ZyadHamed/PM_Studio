using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace PM_Studio
{
    public class FeatureBlock : Border
    {

        #region Designing Variables
        DockPanel Container = new DockPanel();
        TextBlock lbFeatureHeader = new TextBlock();
        TextBlock lbFeatureDescription = new TextBlock();
        Button btnMore = new Button();
        ContextMenu menu = new ContextMenu();
        MenuItem EditFeatureMenuItem = new MenuItem();
        MenuItem DeleteFeatureMenuItem = new MenuItem();

        #endregion

        #region Variables

        private Feature feature;

        #endregion

        #region Constructor

        public FeatureBlock(Feature _feature)
        {
            Feature = _feature;
            btnMore.Click += btnMore_Click;
            btnMore.Visibility = System.Windows.Visibility.Hidden;
            this.MouseEnter += FeatureBlock_MouseEnter;
            this.MouseLeave += FeatureBlock_MouseLeave;
            SetControlsProperties();
            SetControlsData();
        }

        #endregion

        #region Designing Methods

        void SetControlsProperties()
        {

            lbFeatureHeader.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EBEBEB"));
            lbFeatureHeader.FontSize = 20;
            //lbFeatureHeader.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            lbFeatureHeader.VerticalAlignment = System.Windows.VerticalAlignment.Top;

            lbFeatureDescription.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EBEBEB"));
            lbFeatureDescription.FontSize = 15;
            //lbFeatureDescription.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            lbFeatureDescription.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;

            btnMore.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#332F2E"));
            btnMore.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCCCCC"));
            btnMore.Content = "...";
            btnMore.FontSize = 15;

            EditFeatureMenuItem.Header = "Edit Feature";
            DeleteFeatureMenuItem.Header = "Delete Feature";

            menu.Items.Add(EditFeatureMenuItem);
            menu.Items.Add(DeleteFeatureMenuItem);

            Container.LastChildFill = false;
            Container.Children.Add(lbFeatureHeader);
            Container.Children.Add(lbFeatureDescription);
            Container.Children.Add(btnMore);

            btnMore.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            btnMore.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            btnMore.Margin = new System.Windows.Thickness(20,0,0,30);

            this.Child = Container;
            this.CornerRadius = new System.Windows.CornerRadius(4);
            this.BorderThickness = new System.Windows.Thickness(2);
            this.BorderBrush = Brushes.LightGray;
            this.Padding = new System.Windows.Thickness(5);
            this.Margin = new System.Windows.Thickness(5,10,5,10);

        }

        #endregion

        #region Methods

        void SetControlsData()
        {
            lbFeatureHeader.Text = Feature.Header;
            lbFeatureDescription.Text = Feature.Description;
        }

        #endregion

        #region Events

        private void btnMore_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            menu.IsOpen = true;
        }

        private void FeatureBlock_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnMore.Visibility = System.Windows.Visibility.Visible;
            this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3D3837"));
        }

        private void FeatureBlock_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnMore.Visibility = System.Windows.Visibility.Hidden;
            this.Background = Brushes.Transparent;
        }

        #endregion

        #region Properties

        public Feature Feature
        {
            get
            {
                return feature;
            }

            set
            {
                feature = value;
                SetControlsData();
            }
        }

        #endregion

    }
}

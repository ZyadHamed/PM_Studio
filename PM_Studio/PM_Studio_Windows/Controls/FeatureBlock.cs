using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace PM_Studio
{
    public class FeatureBlock : DockPanel
    {

        #region Designing Variables

        TextBlock lbFeatureText = new TextBlock();
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
        }

        #endregion

        #region Designing Methods

        void SetControlsProperties()
        {

            lbFeatureText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EBEBEB"));
            lbFeatureText.FontSize = 20;
            lbFeatureText.Text = "Something";

            btnMore.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#332F2E"));
            btnMore.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCCCCC"));
            btnMore.Content = "...";
            btnMore.FontSize = 15;

            EditFeatureMenuItem.Header = "Edit Feature";
            DeleteFeatureMenuItem.Header = "Delete Feature";

            menu.Items.Add(EditFeatureMenuItem);
            menu.Items.Add(DeleteFeatureMenuItem);

            this.Children.Add(lbFeatureText);
            this.Children.Add(btnMore);

            btnMore.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            btnMore.VerticalAlignment = System.Windows.VerticalAlignment.Center;

        }

        #endregion

        #region Methods

        void SetControlsData()
        {

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
            this.Background = Brushes.Green;
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
            }
        }

        #endregion

    }
}

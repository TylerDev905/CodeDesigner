using System.Windows.Forms;
using System.Drawing;


namespace CodeDesigner
{

    class TabControlEx : TabControl
    {
        public string pathCloseImg = @"\images\close.png";
        public string onCloseMsg = "Would you like to close this tab?";

        public TabControlEx()
        {
            Setup();
        }

        public void Setup()
        {
            Padding = new Point(25, 3);
            DrawMode = TabDrawMode.OwnerDrawFixed;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {

            RectangleF tabFill = (RectangleF)GetTabRect(e.Index);

            if (e.Index == SelectedIndex)
            {
                Brush textBrush = new SolidBrush(Color.White);
                Brush fillBrush = new SolidBrush(Color.FromArgb(255, 64, 64, 64));
                e.Graphics.FillRectangle(fillBrush, tabFill);
                e.Graphics.DrawString(TabPages[e.Index].Text, Font, textBrush, new Point(e.Bounds.X + 5, e.Bounds.Y + 5));
                int offset = (e.Bounds.Height - 16) / 2;
                e.Graphics.DrawImage(Image.FromFile(pathCloseImg), e.Bounds.X + e.Bounds.Width - 20, e.Bounds.Y + offset);
                textBrush.Dispose();
                fillBrush.Dispose();
            }
            else
            {
                Brush textBrush = new SolidBrush(Color.Black);
                Brush fillBrush = new SolidBrush(Color.LightGray);
                e.Graphics.FillRectangle(fillBrush, tabFill);
                fillBrush.Dispose();
                e.Graphics.DrawString(TabPages[e.Index].Text, Font, textBrush, new Point(e.Bounds.X + 5, e.Bounds.Y + 3));
                int offset = (e.Bounds.Height - 16) / 2;
                e.Graphics.DrawImage(Image.FromFile(pathCloseImg), e.Bounds.X + e.Bounds.Width - 20, e.Bounds.Y + offset + 2);
                textBrush.Dispose();
                fillBrush.Dispose();
            }

        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            for (int i = 0; i < TabPages.Count; i++)
            {
                Rectangle bounds = GetTabRect(i);

                int offset = (bounds.Height - 16) / 2;
                Rectangle closeButton = new Rectangle(bounds.X + bounds.Width - 20, bounds.Y + offset, 14, 16);

                if (closeButton.Contains(e.Location))
                {
                    if (MessageBox.Show(onCloseMsg, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        TabPages[i].Dispose();
                        break;
                    }
                }
            }
        }

    }
}

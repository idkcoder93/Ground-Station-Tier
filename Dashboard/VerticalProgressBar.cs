using System;
using System.Windows.Forms;
using System.Drawing;

public class VerticalProgressBar : ProgressBar
{
    public VerticalProgressBar()
    {
        // This allows us to draw the progress bar manually.
        this.SetStyle(ControlStyles.UserPaint, true);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Rectangle rect = this.ClientRectangle;
        Graphics g = e.Graphics;

        // Draw background
        ProgressBarRenderer.DrawHorizontalBar(g, rect);
        rect.Inflate(-3, -3); // Adjust the rectangle for the inner bar

        if (this.Value > 0)
        {
            // Calculate the size of the filled-in part of the progress bar (vertical orientation)
            Rectangle clip = new Rectangle(rect.X, rect.Bottom - (int)((rect.Height * ((double)this.Value / this.Maximum))),
                                            rect.Width, (int)(rect.Height * ((double)this.Value / this.Maximum)));

            // Draw the filled-in part in vertical orientation
            g.FillRectangle(Brushes.Green, clip);
        }
    }
}

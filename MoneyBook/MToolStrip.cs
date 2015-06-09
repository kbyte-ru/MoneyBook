using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MoneyBook.WinApp
{

  class MToolStrip : ToolStrip
  {

    private Pen _ComboBorderPen = new Pen(SystemColors.ControlDark);

    public MToolStrip()
    {
      this.CanOverflow = false;
      this.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.ShowItemToolTips = false;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);

      /*foreach (var item in this.Items)
      {
        if (item.GetType() != typeof(ToolStripComboBox))
        {
          continue;
        }

        var combo = (ToolStripComboBox)item;
        var r = new Rectangle
        (
          combo.ComboBox.Location.X - 1,
          combo.ComboBox.Location.Y - 1,
          combo.ComboBox.Size.Width + 1,
          combo.ComboBox.Size.Height + 1
        );

        e.Graphics.DrawRectangle(_ComboBorderPen, r);
      }*/
    }

  }

}
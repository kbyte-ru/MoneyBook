using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MoneyBook.WinApp
{

  class MToolStrip : ToolStrip
  {

    public MToolStrip()
    {
      this.CanOverflow = false;
      this.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
    }

  }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LVLGuide.ViewModelProcessor;
using LVLGuide.ViewModelProcessor.Nodes;

namespace LVLGuide.ViewModel
{
    internal class GuideStepCopyViewModel:IMenu
    {
        [HideName]
        public ButtonNode Copy { get; set; }
        [SameLine]
        public LabelNode Text { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExileCore;
using LVLGuide.model;

namespace LVLGuide.Model.SubSteps
{
    internal class CopyableSubStep:IGuideSubStep
    {
        public bool IsComplete
        {
            get => true;
            set {}
        }

        public string Text { get; }

        public void Update(GameController gameController)
        {
        }

        public CopyableSubStep(string text)
        {
            Text = text;
        }
    }
}

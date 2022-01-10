using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ImGuiNET;
using LVLGuide.model;
using LVLGuide.Model.SubSteps;
using LVLGuide.ViewModelProcessor;
using LVLGuide.ViewModelProcessor.Nodes;

namespace LVLGuide.ViewModel
{
    public class GuideViewModel:IMenu
    {
        public GuideViewModel(Guide guide)
        {
            GuideSteps = guide.GetCurrentStep().SubSteps.Select(x =>
            {
                if (x is CopyableSubStep)
                {
                    var node2 = new GuideStepCopyViewModel()
                    {
                        Copy = new ButtonNode(),
                        Text = new LabelNode(x.Text)
                    };
                    node2.Copy.OnPressed += () =>
                    {
                        Thread thread = new Thread(() => Clipboard.SetText(node2.Text.Text));
                        thread.SetApartmentState(ApartmentState.STA); //Set the thread to STA
                        thread.Start();
                        thread.Join();
                    };
                    return node2 as IMenu;
                }
                var node = new GuideStepViewModel
                {
                   // Copy = new ButtonNode(),
                    Done = new ToggleNode(x.IsComplete),
                    Text = new LabelNode(x.Text)
                };
                node.Done.OnValueChanged += (_, newValue) =>
                {
                    x.IsComplete = newValue;
                    guide.AutoGoNext = true;
                };
               
                return node as IMenu;
            }).ToList();
            ButtonLeft.OnPressed += () =>
            {
                guide.AutoGoNext = false;
                guide.Previous();
            };
            ButtonRight.OnPressed += guide.Next;
            Label = new LabelNode($"Step {guide.Step()} of {guide.Steps()}");
        }

        public ArrowButtonNode ButtonLeft { get; } = new ArrowButtonNode { Direction = ImGuiDir.Left };
        [HideName]
        [SameLine]
        public LabelNode Label { get; }
        [SameLine]
        public ArrowButtonNode ButtonRight { get; } = new ArrowButtonNode { Direction = ImGuiDir.Right };
        public List<IMenu> GuideSteps { get; }
    }
}
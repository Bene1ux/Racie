using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ExileCore;
using LVLGuide.model.SubSteps;
using LVLGuide.Model.SubSteps;

namespace LVLGuide.model
{
    public static class GuideParser
    {
        public static Guide ParseGuideFile(string path)
        {
            DebugWindow.LogDebug(path);
            var lines = File.ReadAllLines(path);
            var currentSteps = new List<IGuideSubStep>();
            var guideSteps = new List<GuideStep>();
            for (var index = 0; index < lines.Length; index++)
            {
                var line = lines[index];
                if (string.IsNullOrEmpty(line))
                {
                    guideSteps.Add(new GuideStep(currentSteps.ToList()));
                    currentSteps.Clear();
                    continue;
                }

                currentSteps.Add(GuideStepFactory(line));
                if (index + 1 != lines.Length)
                {
                    continue;
                }

                guideSteps.Add(new GuideStep(currentSteps.ToList()));
                currentSteps.Clear();
            }

            return new Guide(guideSteps);
        }

        private static IGuideSubStep GuideStepFactory(string line)
        {

            if (line.StartsWith("@")&&line.Length>=2)
            {
                return new CopyableSubStep(line.Substring(1));
            }
            var matches = line.Split('[', ']');
            if (matches.Length == 1 || string.IsNullOrEmpty(matches[1]))
            {
                return new DefaultSubStep(line);
            }

            var match = matches[1];
            // match = "QS a1q1 3"
            var commandWithArgs = match.Split(new[] {' '}, 2); // ["QS","a1q1 3"]
            var operation = commandWithArgs[0]; // QS
            var text = line;
            if (line.Contains('['))
            {
                text = line.Split('[')[0];
            }

            var commandArg = commandWithArgs[1];

            switch (operation)
            {
                case "QS":
                    var p = commandArg.Split(' ');
                    var stageId = Convert.ToInt32(p[1]);
                    var questId = p[0];
                    return new QsSubStep(text, stageId, questId);
                case "QT":
                    return new QsSubStep(text, 0, commandArg);
                case "G":
                    return new GSubStep(text, commandArg);
                case "P":
                    return new PSubStep(text, commandArg);
                case "WP":
                    return new WpSubStep(text, commandArg);
                case "XP":
                    DebugWindow.LogMsg(line);
                    return new XpSubStep(text, Convert.ToInt32(commandArg));
                default:
                    DebugWindow.LogMsg($"Operation: {operation}: {line}", 10.0f);
                    return new DefaultSubStep(line);
            }
        }
    }
}